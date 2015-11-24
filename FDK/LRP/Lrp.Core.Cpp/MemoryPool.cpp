#include "stdafx.h"
#include "MemoryPool.h"
#include "MemoryBuffer.h"
CMemoryPool::CMemoryPool()
{
	m_heap = HeapCreate(0, 0, 0);
	if (nullptr == m_heap)
	{
		throw runtime_error("Can not create a new memory heap");
	}
}
CMemoryPool::~CMemoryPool()
{
	HeapDestroy(m_heap);
	m_heap = nullptr;
}
void* CMemoryPool::Heap()
{
	return m_heap;
}
