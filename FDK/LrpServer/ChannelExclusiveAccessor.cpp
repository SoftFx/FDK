#include "stdafx.h"
#include "ChannelExclusiveAccessor.h"
#include "ChannelsPool.h"

CChannelAccessor::CChannelAccessor(CChannelsPool& pool) : m_pool(pool), m_entry(nullptr)
{
	m_entry = pool.Acquire();
}
CChannelAccessor::~CChannelAccessor()
{
	m_pool.Release(m_entry);
}
CChannel* CChannelAccessor::GetChannel()
{
	if (nullptr == m_entry)
	{
		return nullptr;
	}
	return m_entry->Channel;
}
void CChannelAccessor::Reset()
{
	m_entry = nullptr;
}
