#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)
#pragma warning (disable : 4251)

#include "FxEventInfo.h"

class CORE_API CFxMessage
{
public:
	CFxMessage();
	CFxMessage(const CFxEventInfo& info);
	CFxMessage(int32 type, const CFxEventInfo& info);
public:
	int32 Type;
	Nullable<CDateTime> SendingTime;
	Nullable<CDateTime> ReceivingTime;
	void* Data;
};

#pragma warning (pop)

