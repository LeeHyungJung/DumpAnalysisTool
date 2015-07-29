#pragma once

//-------------------------------------------------------------------------- ToolCallStack

namespace ToolCallStack
{
	typedef struct
	{
		unsigned int address;
		char  tag;
		char* fuctionName;
		int	  line;
		char* fileName;

	}CallStackInfo;

	typedef struct
	{
		int callStackCount;
		CallStackInfo* info;
		CallStackInfo* callStackInfos;

	}CallStackInfoWrapper;

	bool run(CallStackInfoWrapper* info, int bufferSize, const wchar_t* dumpDirectory, char* modulePath, char* pdbPath, char** result);
}
