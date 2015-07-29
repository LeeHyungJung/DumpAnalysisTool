#include "stdafx.h"
#include "ToolInfo.h"
#include "DumpFile.h"
#include "Utility.h"
#include "ToolCallStack.h"

//--------------------------------------------------------------------------- ToolInfo

namespace ToolInfo
{

	bool run(DumpInfo* info, const wchar_t* dumpPath, char** result)
	{
		DumpFilePtr dmp = DumpFile::open(dumpPath);
		if (dmp == nullptr)
		{
			*result = "Dump not Exist";
			return false;
		}
		*result = "DumpInfo Read Success";

		{
			const MINIDUMP_EXCEPTION_STREAM& e = dmp->getException();
			const MINIDUMP_EXCEPTION& r = e.ExceptionRecord;

			info->threadId = e.ThreadId;
			info->exceptionCode = r.ExceptionCode;
			info->exceptionFlag = r.ExceptionFlags;
			info->exceptionRecord = uint32(r.ExceptionRecord);
			info->exceptionAddress = uint32(r.ExceptionAddress);
		}


		{
			const MINIDUMP_SYSTEM_INFO& s = dmp->getSysInfo();

			info->processorArchitecture = s.ProcessorArchitecture;
			info->processorLevel = s.ProcessorLevel;
			info->processorRevision = s.ProcessorRevision;

			info->majorVersion = s.MajorVersion;
			info->minorVersion = s.MinorVersion;
			info->buildNumber = s.BuildNumber;
			info->platformId = s.PlatformId;

			wstring csd;
			dmp->getRvaString(s.CSDVersionRva, &csd);

			info->csdVersion = (char*)CoTaskMemAlloc(sizeof(char)*csd.length() + 1);
			char* csdPtr = CharUtility::getInstance().WCharToChar(csd.c_str());
			sprintf_s(info->csdVersion, strlen(csdPtr) + 1, csdPtr);

			auto c = s.Cpu.X86CpuInfo;
			char vendorIdStr[13] = { 0, };
			memcpy(vendorIdStr, c.VendorId, 12);
			info->vendorIdStr = (char*)CoTaskMemAlloc(sizeof(char)*strlen(vendorIdStr) + 1);
			sprintf_s(info->vendorIdStr, strlen(vendorIdStr) + 1, vendorIdStr);

			info->versionInformation = c.VersionInformation;
			info->featureInformation = c.FeatureInformation;
			info->AMDExtendedCpuFeatures = c.AMDExtendedCpuFeatures;
		}

		ULONG32 ThreadId;
		{

			const vector<MINIDUMP_THREAD>& thread = dmp->getThreads();
			info->threadCount = thread.size();

			info->threadInfos = (ThreadInfo*)CoTaskMemAlloc(sizeof(ToolInfo::ThreadInfo) * info->threadCount);

			int thread_index = 0;
			for (auto t = thread.begin(), t_end = thread.end(); t != t_end; ++t)
			{
				ThreadId = t->ThreadId;

				CONTEXT ctx;
				ThreadInfo* tempInfo = (ThreadInfo*)CoTaskMemAlloc(sizeof(ToolInfo::ThreadInfo));
				if (dmp->getRvaContext(t->ThreadContext.Rva, &ctx))
				{
					tempInfo->ctxEip = ctx.Eip;
					tempInfo->ctxEbp = ctx.Ebp;
				}
				else{
					tempInfo->ctxEip = 0;
					tempInfo->ctxEbp = 0;
				}

				tempInfo->threadId = ThreadId;
				tempInfo->suspendCount = t->SuspendCount;
				tempInfo->priorityClass = t->PriorityClass;
				tempInfo->priority = t->Priority;
				tempInfo->startOfMemoryRange = t->Stack.StartOfMemoryRange;


				info->threadInfos[thread_index] = *tempInfo;

				thread_index++;
			}
		}

		{
			const vector<MINIDUMP_MODULE>& modules = dmp->getModules();

			info->moduleCount = modules.size();

			info->moduleInfos = (ModuleInfo*)CoTaskMemAlloc(sizeof(ToolInfo::ModuleInfo) * info->moduleCount);

			int module_index = 0;
			for (auto m = modules.begin(), m_end = modules.end(); m != m_end; ++m)
			{
				wstring name;
				dmp->getRvaString(m->ModuleNameRva, &name);

				ModuleInfo* moduleInfo = (ModuleInfo*)CoTaskMemAlloc(sizeof(ModuleInfo));
				moduleInfo->cv_path = NULL;
				moduleInfo->moduleId = m - modules.begin();


				char* moduleName = CharUtility::getInstance().WCharToChar(name.c_str());
				moduleInfo->name = (char*)malloc(sizeof(char)*strlen(moduleName) + 1);
				sprintf_s(moduleInfo->name, strlen(moduleName) + 1, moduleName);

				moduleInfo->baseOfImage = uint32(m->BaseOfImage);
				moduleInfo->sizeOfImage = m->SizeOfImage;
				moduleInfo->checkSum = m->CheckSum;
				moduleInfo->timeDateStamp = m->TimeDateStamp;


				const byte* cvData = dmp->getRvaMemory(m->CvRecord.Rva, m->CvRecord.DataSize);
				if (cvData != nullptr && memcmp(cvData, "RSDS", 4) == 0)
				{
					const int* cdi = (const int*)cvData;
					moduleInfo->cv_age = cdi[5];

					char* cv_path = CharUtility::getInstance().WCharToChar(utf82wstr((char*)cvData + 24).c_str());
					moduleInfo->cv_path = (char*)malloc(sizeof(char)*strlen(cv_path) + 1);
					sprintf_s(moduleInfo->cv_path, strlen(cv_path) + 1, cv_path);
				}

				info->moduleInfos[module_index] = *moduleInfo;
				module_index++;
			}

		}

		{

			const vector<MINIDUMP_MEMORY_DESCRIPTOR>& memories = dmp->getMemories();

			int memeory_index = 0;
			info->memoryCount = memories.size();
			info->memoryInfos = (MemoryInfo*)CoTaskMemAlloc(sizeof(ToolInfo::MemoryInfo) * info->moduleCount);

			for (auto m = memories.begin(), m_end = memories.end(); m != m_end; ++m)
			{
				MemoryInfo* tempInfo = (MemoryInfo*)CoTaskMemAlloc(sizeof(ToolInfo::MemoryInfo));

				tempInfo->address = uint32(m->StartOfMemoryRange);
				tempInfo->dataSize = m->Memory.DataSize;
				info->memoryInfos[memeory_index] = *tempInfo;
				memeory_index++;
			}
		}

		return true;


	} // namespace ToolInfo

}
