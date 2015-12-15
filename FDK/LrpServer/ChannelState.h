#pragma once
#include "Parameters.h"
#include "Messages.h"

class CChannel;

class CChannelState
{
public:
	uint64 LastIncommingEvent;
	uint64 LastOutgoingEvent;
	MemoryBuffer Buffer;
	CMessages Messages;
public:
	const static size_t DefaultBufferSizeInBytes = sizeof(uint16) + 0xffff;
public:
	CChannelState(CChannel& owner);
private:
	CChannelState(const CChannelState&);
	CChannelState& operator = (const CChannelState&);
};