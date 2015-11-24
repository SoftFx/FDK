#include "stdafx.h"
#include "LocalChannelsPool.h"
#include "ChannelsPool.h"

void* CLocalChannelsPool::Constructor(const CParameters& params)
{
	CChannelsPool* result = new CChannelsPool(params);
	return result;
}
void CLocalChannelsPool::Destructor(void* handle)
{
	CChannelsPool* pChannels = reinterpret_cast<CChannelsPool*>(handle);
	delete pChannels;
}
