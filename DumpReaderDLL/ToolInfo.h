#pragma once

//------------------------------------------------------------------------------- ToolInfo

namespace ToolInfo
{

	typedef struct
	{
		unsigned int  moduleId;
		char*		  name;

		unsigned int  baseOfImage;
		unsigned long sizeOfImage;
		unsigned long checkSum;
		unsigned long timeDateStamp;

		int cv_age;
		char* cv_path;

	}ModuleInfo;

	typedef struct
	{
		unsigned long threadId;
		unsigned long suspendCount;
		unsigned long priorityClass;
		unsigned long priority;
		unsigned long long startOfMemoryRange;
		unsigned long ctxEip;
		unsigned long ctxEbp;
	}ThreadInfo;

	typedef struct
	{
		unsigned int  address;
		unsigned int  dataSize;
	}MemoryInfo;

	typedef struct
	{

		//Exception
		unsigned int threadId;
		unsigned int exceptionCode;
		unsigned int exceptionFlag;
		unsigned int exceptionRecord;
		unsigned int exceptionAddress;

		//System
		unsigned short processorArchitecture;
		unsigned short processorLevel;
		unsigned short processorRevision;

		//OS
		unsigned long  majorVersion;
		unsigned long  minorVersion;
		unsigned long  buildNumber;
		unsigned long  platformId;

		//csd
		char* csdVersion;

		//cpu
		char* vendorIdStr;

		unsigned int versionInformation;
		unsigned int featureInformation;
		unsigned int AMDExtendedCpuFeatures;

		int threadCount;
		ThreadInfo* threadInfos;

		int moduleCount;
		ModuleInfo* moduleInfos;

		int memoryCount;
		MemoryInfo* memoryInfos;
	}DumpInfo;

	bool run(DumpInfo* info, const wchar_t* dumpDirectory, char** result);
}
