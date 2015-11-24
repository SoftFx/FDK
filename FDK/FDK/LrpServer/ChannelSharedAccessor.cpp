#include "stdafx.h"
#include "ChannelSharedAccessor.h"
#include "ChannelsPool.h"
#include "Channel.h"

CChannelSharedAccessor::CChannelSharedAccessor(uint64 id, CChannelsPool& pool) : m_channel()
{
	m_channel = pool.Acquire(id);
}
CChannelSharedAccessor::~CChannelSharedAccessor()
{
	if (nullptr != m_channel)
	{
		m_channel->Release();
	}
}
CChannel* CChannelSharedAccessor::GetChannel() const
{
	return m_channel;
}
