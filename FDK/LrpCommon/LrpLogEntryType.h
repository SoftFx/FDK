#pragma once


enum LrpLogEntryType
{
	LrpLogEntryType_Event,
	LrpLogEntryType_Incomming,
	LrpLogEntryType_Outgoing,
};


ostream& operator << (ostream& stream, const LrpLogEntryType type);