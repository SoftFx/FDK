#include "stdafx.h"
#include "LrpLogEntryData.h"


CLrpLogEntryData::CLrpLogEntryData(const uint64 id, const LrpLogEntryType type, const uint16 componentId, const uint16 methodId, const MemoryBuffer& buffer) :
	ID(id), ThreadId(GetCurrentThreadId()), Type(type), ComponentId(componentId), MethodId(methodId)
{
	GetSystemTimeAsFileTime(&TimeStamp);

	void* heap = CHeap::Instance();
	const size_t size = buffer.GetSize() - buffer.GetPosition();
	void* data = HeapAlloc(heap, 0, size);
	if (nullptr == data)
	{
		throw std::bad_alloc();
	}

	memcpy(data, reinterpret_cast<const uint8*>(buffer.GetData()) + buffer.GetPosition(), size);

	MemoryBuffer temp(heap, data, size, size);
	std::swap(temp, Buffer);
}
CLrpLogEntryData::CLrpLogEntryData(const uint64 id, const string& message) : ID(id), ThreadId(GetCurrentThreadId()), Type(LrpLogEntryType_Event), ComponentId(), MethodId()
{
	GetSystemTimeAsFileTime(&TimeStamp);

	void* heap = CHeap::Instance();
	const size_t size = message.size() + sizeof(uint32);
	void* data = HeapAlloc(heap, 0, size);
	if (nullptr == data)
	{
		throw std::bad_alloc();
	}
	MemoryBuffer temp(heap, data, size, size);
	std::swap(temp, Buffer);
	WriteAString(message, Buffer);
	Buffer.SetPosition(0);
}
