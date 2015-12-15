#pragma once


class CLrpPipe
{
public:
	CLrpPipe(const TCHAR* pipeId);
	~CLrpPipe();
public:
	void Accept();
	void WriteBuffer(const MemoryBuffer& buffer);
	void WriteBuffer(const MemoryBuffer& buffer, int result);
	void ReadBuffer(MemoryBuffer& buffer);
	void ReadBuffer(unsigned short& componentId, unsigned short& methodId, MemoryBuffer& buffer);
private:
	void WriteData(size_t count, const void* buffer);
	void ReadData(size_t count, void* buffer);
private:
	HANDLE m_pipe;
};