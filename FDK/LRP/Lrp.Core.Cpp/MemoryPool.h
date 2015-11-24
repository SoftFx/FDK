#pragma once


class MemoryBuffer;
class CMemoryPool
{
public:
	CMemoryPool();
	~CMemoryPool();
public:
	void* Heap();
private:
	CMemoryPool(const CMemoryPool&);
	CMemoryPool& operator = (const CMemoryPool&);
private:
	void* m_heap;
};

