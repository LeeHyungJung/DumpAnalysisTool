#pragma once

//------------------------------------------------------------------------------- DumpFile

class DumpFile;
typedef shared_ptr<DumpFile> DumpFilePtr;

class DumpFile
{
public:
	static DumpFilePtr open(const wstring& path);

private:
	DumpFile(const wstring& path, uint32 fileSize, const byte* mappedMem);
	void loadInfoFromDump();

public:
	virtual ~DumpFile();

public:
	uint32 getFileSize() const;

	const byte* getMappedMem() const;
	const byte* getStream(MINIDUMP_STREAM_TYPE type, uint32* size = nullptr) const;

	const MINIDUMP_EXCEPTION_STREAM& getException() const;
	const MINIDUMP_SYSTEM_INFO& getSysInfo() const;

	const vector<MINIDUMP_THREAD>& getThreads() const;
	const MINIDUMP_THREAD* getThread(uint32 threadId) const;

	const vector<MINIDUMP_MODULE>& getModules() const;
	const MINIDUMP_MODULE* getModuleHasAddr(uint32 addr) const;

	const vector<MINIDUMP_MEMORY_DESCRIPTOR>& getMemories() const;

	const byte* getRvaMemory(RVA rva, int size) const;
	bool getRvaString(RVA rva, wstring* str) const;
	bool getRvaContext(RVA rva, CONTEXT* context) const;

	//! ������ �ּҿ��� ������ ũ�⸸ŭ �����͸� ���� �� ������ ���� �� �ִ�
	//! �޸� �ּҸ� ��ȯ�մϴ�. �����ϸ� nullptr �� ��ȯ�մϴ�.
	const byte* getMemoryPointer(uint32 addr, int32 size) const;

	//! ������ �ּҿ��� ������ ũ�⸸ŭ �����͸� �о� buffer �� �����մϴ�.
	//! ���� �����Ͱ� �����ϴٸ� ���� �� �ִ� ��ŭ�� �����ϸ� ������ ũ�⸦ ��ȯ�մϴ�.
	int32 readMemory(uint32 addr, int32 size, byte* buffer) const;

	//! ������ �ּҿ��� T Ÿ���� �����͸� �о� ��ȯ�մϴ�. 
	//! T Ÿ�� ũ�⸸ŭ ������ ���� ���ϸ� false �� ��ȯ�մϴ�.
	template <typename T> bool readMemoryValue(uint32 addr, T* value) const
	{
		return readMemory(addr, sizeof(*value), (byte*)value) == sizeof(*value);
	}

private:
	wstring path_;
	uint32 fileSize_;
	const byte* mappedMem_;
	MINIDUMP_EXCEPTION_STREAM exception_;
	MINIDUMP_SYSTEM_INFO sysInfo_;
	vector<MINIDUMP_THREAD> threads_;
	vector<MINIDUMP_MODULE> modules_;
	vector<MINIDUMP_MEMORY_DESCRIPTOR> memories_;
};
