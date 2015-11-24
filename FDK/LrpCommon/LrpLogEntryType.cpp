#include "stdafx.h"
#include "LrpLogEntryType.h"


ostream& operator << (ostream& stream, const LrpLogEntryType type)
{
	if (LrpLogEntryType_Event == type)
	{
		stream<<"event";
	}
	else if (LrpLogEntryType_Incomming == type)
	{
		stream<<"in";
	}
	else if (LrpLogEntryType_Outgoing == type)
	{
		stream<<"out";
	}
	else
	{
		stream<<static_cast<size_t>(type);
	}
	return stream;
}