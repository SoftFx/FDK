#include "stdafx.h"
#include "MessageData.h"
#include "Heap.h"

CMessageData::CMessageData(const ptrdiff_t key) : Key(key), ComponentId(numeric_limits<uint16>::max()), MethodId(numeric_limits<uint16>::max()), Buffer(CHeap::Instance())
{
}
CMessageData::CMessageData(const ptrdiff_t key, const uint16 componentId, const uint16 methodId, MemoryBuffer& buffer) : Key(key), ComponentId(componentId), MethodId(methodId)
{
	std::swap(Buffer, buffer);
}
