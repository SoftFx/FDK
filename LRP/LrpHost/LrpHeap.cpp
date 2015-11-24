#include "stdafx.h"
#include "LrpHeap.h"

CLrpHeap::CLrpHeap() : m_heap(HeapCreate(0, 0, 0))
{
	if (nullptr == m_heap)
	{
		throw runtime_error("Couldn't create a new memory heap");
	}
}
CLrpHeap::~CLrpHeap()
{
	HeapDestroy(m_heap);
	m_heap = nullptr;
}
void* CLrpHeap::Heap()
{
	return m_heap;
}
