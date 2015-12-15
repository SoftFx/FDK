#include "stdafx.h"
#include "ChannelState.h"

CChannelState::CChannelState(CChannel& owner) : Buffer(CHeap::Instance()), Messages(owner)
{
	LastIncommingEvent = GetTickCount64();
	LastOutgoingEvent = LastIncommingEvent;
	Buffer.Construct(DefaultBufferSizeInBytes);
}
