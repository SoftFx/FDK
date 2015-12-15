#pragma once



class CChannel;
class CChannelsPool;

class CChannelSharedAccessor
{
public:
	CChannelSharedAccessor(uint64 id, CChannelsPool& pool);
	~CChannelSharedAccessor();
public:
	CChannel* GetChannel() const;
private:
	CChannelSharedAccessor(const CChannelSharedAccessor&);
	CChannelSharedAccessor& operator = (const CChannelSharedAccessor&);
private:
	CChannel* m_channel;
};