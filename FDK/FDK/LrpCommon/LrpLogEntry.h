#pragma once
#include "LrpLogEntryData.h"


class CLrpLogEntry
{
public:
	CLrpLogEntry(const uint64 id, const LrpLogEntryType type, const uint16 componentId, const uint16 methodId, const MemoryBuffer& buffer);
	CLrpLogEntry(const uint64 id, const string& message);
	CLrpLogEntry(const CLrpLogEntry& entry);
	CLrpLogEntry& operator = (const CLrpLogEntry& entry);
	~CLrpLogEntry();
public:
	LrpLogEntryType GetType() const;
private:
	void Format(ostream& stream) const;
	void DoFormat(const LrpLogEntryType type, const uint64 id, ostream& stream) const;
private:
	CLrpLogEntryData* m_data;
private:
	friend ostream& operator << (ostream& stream, const CLrpLogEntry& arg);
};