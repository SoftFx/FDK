#pragma once

class CChannel;

class CChannelEntry
{
public:
	bool IsProcessed;
	CChannel* Channel;
public:
	CChannelEntry();
	CChannelEntry(CChannel* pChannel);
};