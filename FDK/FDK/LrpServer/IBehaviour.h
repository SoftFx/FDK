#pragma once
#include "ChannelState.h"
#include "Transport.h"




class IBehaviour
{
public:
	virtual HRESULT VProcess(const uint64 now) = 0;
	virtual ~IBehaviour() {};
};