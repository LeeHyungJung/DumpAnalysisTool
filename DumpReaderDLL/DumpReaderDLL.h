

#include "ToolInfo.h"
#include "ToolCallStack.h"

extern "C" __declspec(dllexport) bool READ_DUMP_INFO(ToolInfo::DumpInfo* _info, char* _dumpPath, char** _result);
extern "C" __declspec(dllexport) bool READ_DUMP_CALL_STACK(ToolCallStack::CallStackInfoWrapper* _info, int bufferSize, char* _dumpPath, char* modulePath, char* pdbPath, char** result);
extern "C" __declspec(dllexport) void SYMBOL_INITIALIZE();
extern "C" __declspec(dllexport) void SYMBOL_TERMINATE();
extern "C" __declspec(dllexport) void CLEAR_UNMANAGED_MEMORY();
