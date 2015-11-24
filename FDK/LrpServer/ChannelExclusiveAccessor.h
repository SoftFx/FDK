#pragma once

#include "ChannelEntry.h"

class CChannelsPool;

class CChannelAccessor
{
public:
	CChannelAccessor(CChannelsPool& pool);
	~CChannelAccessor();
public:
	CChannel* GetChannel();
	void Reset();
private:
	CChannelAccessor(const CChannelAccessor&);
	CChannelAccessor& operator = (const CChannelAccessor&);
private:
	CChannelsPool& m_pool;
	CChannelEntry* m_entry;
};