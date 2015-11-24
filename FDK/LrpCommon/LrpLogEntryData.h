#pragma once
#include "LrpLogEntryType.h"



class CLrpLogEntryData : public CReferenceable
{
public:
	CLrpLogEntryData(const uint64 id, const LrpLogEntryType type, const uint16 componentId, const uint16 methodId, const MemoryBuffer& buffer);
	CLrpLogEntryData(const uint64 id, const string& message);
private:
	CLrpLogEntryData(const CLrpLogEntryData&);
	CLrpLogEntryData& operator = (const CLrpLogEntryData&);
public:
	FILETIME TimeStamp;
	uint64 ID;
	DWORD ThreadId;
	LrpLogEntryType Type;
	uint16 ComponentId;
	uint16 MethodId;
	MemoryBuffer Buffer;
};


