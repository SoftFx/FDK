#include "stdafx.h"
#include "Heap.h"

namespace
{
	CHeap gHeap;
}
CHeap::CHeap() : m_heap()
{
	m_heap = HeapCreate(0, 0, 0);
	if (nullptr == m_heap)
	{
		throw runtime_error("Can not create a new memory heap");
	}
}
CHeap::~CHeap()
{
	HeapDestroy(m_heap);
}
void* CHeap::Instance()
{
	return gHeap.m_heap;
}
