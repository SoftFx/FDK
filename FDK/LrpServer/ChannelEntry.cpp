#include "stdafx.h"
#include "ChannelEntry.h"

CChannelEntry::CChannelEntry() : IsProcessed(false), Channel(nullptr)
{
}
CChannelEntry::CChannelEntry(CChannel* pChannel) : IsProcessed(false), Channel(pChannel)
{
}
