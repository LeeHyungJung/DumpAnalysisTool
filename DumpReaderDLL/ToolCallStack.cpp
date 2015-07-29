#include "stdafx.h"
#include "ToolCallStack.h"
#include "Symbols.h"
#include "Utility.h"

//-------------------------------------------------------------------------- ToolCallStack

namespace ToolCallStack
{

	bool isCommonEntryFunc(const string& funcName)
	{
		return
			funcName == "__tmainCRTStartup" ||
			funcName == "wmainCRTStartup";
	}

	bool run(CallStackInfoWrapper* info, int bufferSize, const wchar_t* dumpPath, char* modulePath, char* pdbPath, char** result)
	{
		DumpFilePtr dmp = DumpFile::open(dumpPath);
		if (dmp == nullptr)
		{
			*result = "DumpFile Not Exist";
			return false;
		}

		Symbols::loadModuleAllInDumpFile(dmp.get(), modulePath, pdbPath);

		// ���� ������ ������ ���� �б� (������ ������ Ȥ�� ���ܰ� �߻��� ������)

		uint32 threadId = 0;
		threadId = dmp->getException().ThreadId;

		const MINIDUMP_THREAD* thread = dmp->getThread(threadId);
		if (thread == nullptr)
		{
			*result = "Thread Not Exist";
			return false;
		}

		// �����͸� ���� �� �ּ� 4 bytes ������ �ǵ��� ����

		uint32 stackAddr = dmp->getThread(threadId)->Stack.StartOfMemoryRange;//    uint32(thread->Stack.StartOfMemoryRange);
		uint32 stackSize = dmp->getThread(threadId)->Stack.Memory.DataSize;// thread->Stack.Memory.DataSize;
		if ((stackAddr % 4) != 0)
		{
			int32 a = 4 - (stackAddr % 4);
			stackAddr += a;
			stackSize -= a;
		}

		// ������ �о�鼭 Ȯ��

		uint32 lastFrameSavedAddr = 0;
		uint32 lastFrameSavedValue = 0;
		bool lastFrameLinked = false;

		string test = "";
		uint32 eip = 0;
		CONTEXT threadContext;
		if (dmp->getRvaContext(dmp->getException().ThreadContext.Rva, &threadContext))
		{
			Symbols::FuncInfo funcInfo;
			if (Symbols::getFuncInfo(threadContext.Eip, &funcInfo))
			{
				eip = threadContext.Eip;

				info->info = (CallStackInfo*)CoTaskMemAlloc(sizeof(CallStackInfo));

				info->info->fileName = (char*)malloc(sizeof(char)*funcInfo.fileName.length() + 1);
				info->info->fuctionName = (char*)malloc(sizeof(char)*funcInfo.name.length() + 1);

				info->info->address = threadContext.Eip;
				info->info->line = funcInfo.line;

				sprintf_s(info->info->fuctionName, funcInfo.name.length() + 1, funcInfo.name.c_str());
				sprintf_s(info->info->fileName, funcInfo.fileName.length() + 1, funcInfo.fileName.c_str());

				printf("EIP             I(0x%08x) %s", threadContext.Eip, funcInfo.name.c_str());
				printf(" %s\n", funcInfo.getSourceString(threadContext.Eip).c_str());
			}

			lastFrameSavedValue = threadContext.Ebp;

			stackAddr = threadContext.Esp;
		}

		vector<CallStackInfo*> wrapperContainer;

		uint32 stackAddrEnd = stackAddr + stackSize;

		int moduleHitCount = 0;

		for (uint32 addr = stackAddr; addr < stackAddrEnd; addr += 4)
		{
			uint32 value;
			if (dmp->readMemoryValue(addr, &value) == false)
			{
				*result = "Memory Read End";
				break;
			}


			if (value >= stackAddr && value < stackAddrEnd)
			{
				lastFrameLinked = (lastFrameSavedValue == addr);
				lastFrameSavedAddr = addr;
				lastFrameSavedValue = value;
			}

			const MINIDUMP_MODULE* moduleHit = dmp->getModuleHasAddr(value);
			if (moduleHit)
			{
				Symbols::FuncInfo funcInfo;
				Symbols::getFuncInfo(value, &funcInfo);

				char tag = ' ';
				if (lastFrameSavedAddr == addr - 4)
					tag = lastFrameLinked ? '+' : '-';

				moduleHitCount++;
				if (!funcInfo.fileName.empty())
				{
					*result = "CallStack Read Success";
					if (moduleHitCount < bufferSize)
					{
						CallStackInfo* callStackInfo = (CallStackInfo*)CoTaskMemAlloc(sizeof(CallStackInfo));
						callStackInfo->fuctionName = (char*)malloc(sizeof(char)*funcInfo.name.length() + 1);
						callStackInfo->fileName = (char*)malloc(sizeof(char)*funcInfo.fileName.length());

						callStackInfo->address = addr;
						callStackInfo->tag = tag;
						callStackInfo->line = funcInfo.line;

						sprintf_s(callStackInfo->fuctionName, funcInfo.name.length() + 1, funcInfo.name.c_str());
						sprintf_s(callStackInfo->fileName, funcInfo.fileName.length() + 1, funcInfo.fileName.c_str());

						wrapperContainer.push_back(callStackInfo);
					}
					else{
						break;
					}
				}
			}
		}

		info->callStackInfos = (CallStackInfo*)CoTaskMemAlloc(sizeof(CallStackInfo)* wrapperContainer.size());
		for (int i = 0; i < wrapperContainer.size(); i++)
		{
			info->callStackInfos[i] = *wrapperContainer[i];
		}
		info->callStackCount = wrapperContainer.size();
		return true;
	}

} // namespace ToolCallStack
