#include "stdafx.h"
#include "FxEventInfo.h"

CFxEventInfo::CFxEventInfo()
    : Status()
    , SendingTime()
    , ReceivingTime()
{
	ReceivingTime = FxUtcNow();
}

CFxEventInfo::CFxEventInfo(const string& id)
    : Status()
    , ID(id)
    , SendingTime()
    , ReceivingTime()
{
	ReceivingTime = FxUtcNow();
}

CFxEventInfo::CFxEventInfo(const string& id, const string& message)
    : Status()
    , ID(id)
    , SendingTime()
    , ReceivingTime()
    , Message(message)
{
	ReceivingTime = FxUtcNow();
}

bool CFxEventInfo::IsExternalSynchCall() const
{
	if (ID.size() < 2)
	{
		return false;
	}
	const bool result = ('E' == ID[0]) && ('S' == ID[1]);
	return result;
}

bool CFxEventInfo::IsNotification() const
{
	const bool result = ID.empty();
	return result;
}

bool CFxEventInfo::IsCall() const
{
	const bool result = !ID.empty();
	return result;
}

bool CFxEventInfo::IsInternalAsynchCall() const
{
	if (ID.size() < 2)
	{
		return false;
	}
	const bool result = ('I' == ID[0]) && ('A' == ID[1]);
	return result;
}