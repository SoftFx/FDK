#include "stdafx.h"
#include "MemoryBuffer.h"
#include "MemoryPool.h"

namespace
{
	CMemoryPool gPool;
	volatile int64 gCounter;
}
MemoryBuffer::MemoryBuffer(size_t size) : m_heap(gPool.Heap()), m_data(), m_size(), m_capacity(size), m_position()
{
	m_data = reinterpret_cast<char*>(HeapAlloc(m_heap, 0, size));
	if (nullptr == m_data)
	{
		throw std::bad_alloc();
	}
}
MemoryBuffer::MemoryBuffer(unsigned __int16 componentId, unsigned __int16 methodId) : m_heap(gPool.Heap()), m_data(), m_size(16), m_capacity(16), m_position()
{
	m_data = reinterpret_cast<char*>(HeapAlloc(m_heap, 0, 16));
	if (nullptr == m_data)
	{
		throw std::bad_alloc();
	}
	int64 id = InterlockedIncrement64(&gCounter);
	WriteUInt32(0, *this);
	WriteInt64(id, *this);
	WriteUInt16(componentId, *this);
	WriteUInt16(methodId, *this);
}