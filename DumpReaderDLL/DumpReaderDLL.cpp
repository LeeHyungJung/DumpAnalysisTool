// DumpReaderDLL.cpp : DLL 응용 프로그램을 위해 내보낸 함수를 정의합니다.
//

#include "stdafx.h"
#include "DumpReaderDLL.h"

#include "Symbols.h"
#include "Utility.h"


extern "C" __declspec(dllexport) bool READ_DUMP_INFO(ToolInfo::DumpInfo* _info, char* _dumpPath, char** _result)
{
	_CrtSetDbgFlag(_CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF);

	wchar_t* dumpPath = CharUtility::getInstance().CharToWChar(_dumpPath);

	if (ToolInfo::run(_info, dumpPath, _result) == false)
	{
		*_result = "fail to read dump file ;<";
		return false;
	}

	return true;
}
extern "C" __declspec(dllexport) bool READ_DUMP_CALL_STACK(ToolCallStack::CallStackInfoWrapper* _info, int bufferSize, char* _dumpDirectory, char* modulePath, char* pdbPath, char** result)
{
	_CrtSetDbgFlag(_CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF);

	wchar_t* dumpPath = CharUtility::getInstance().CharToWChar(_dumpDirectory);

	if (ToolCallStack::run(_info, bufferSize, dumpPath, modulePath, pdbPath, result) == false)
	{
		return false;
	}

	return true;
}


extern "C" __declspec(dllexport) void SYMBOL_INITIALIZE()
{
	Symbols::initialize();

}
extern "C" __declspec(dllexport) void SYMBOL_TERMINATE()
{
	Symbols::terminate();
}
extern "C" __declspec(dllexport) void CLEAR_UNMANAGED_MEMORY()
{
	CharUtility::getInstance().ClearMemory();
}