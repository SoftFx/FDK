#include "stdafx.h"
#include "LrpLogEntry.h"

namespace
{
	ostream& operator << (ostream& stream, SYSTEMTIME& time)
	{
		stream<<setw(4)<<setfill('0')<<time.wYear<<'.';
		stream<<setw(2)<<setfill('0')<<time.wMonth<<'.';
		stream<<setw(2)<<setfill('0')<<time.wDay;

		stream<<" ";

		stream<<setw(2)<<setfill('0')<<time.wHour<<':';
		stream<<setw(2)<<setfill('0')<<time.wMinute<<':';
		stream<<setw(2)<<setfill('0')<<time.wSecond<<".";
		stream<<setw(3)<<setfill('0')<<time.wMilliseconds;
		return stream;
	}
}
CLrpLogEntry::CLrpLogEntry(const uint64 id, const LrpLogEntryType type, const uint16 componentId, const uint16 methodId, const MemoryBuffer& buffer)
{
	m_data = new CLrpLogEntryData(id, type, componentId, methodId, buffer);
}
CLrpLogEntry::CLrpLogEntry(const CLrpLogEntry& entry) : m_data(entry.m_data)
{
	m_data->Acquire();
}
CLrpLogEntry::CLrpLogEntry(const uint64 id, const string& message)
{
	m_data = new CLrpLogEntryData(id, message);
}
CLrpLogEntry& CLrpLogEntry::operator = (const CLrpLogEntry& entry)
{
	if (m_data != entry.m_data)
	{
		m_data->Release();
		m_data = entry.m_data;
		m_data->Acquire();
	}
	return *this;
}
CLrpLogEntry::~CLrpLogEntry()
{
	m_data->Release();
}
LrpLogEntryType CLrpLogEntry::GetType() const
{
	return m_data->Type;
}
void CLrpLogEntry::Format(ostream& stream) const
{
	SYSTEMTIME systemTime;
	ZeroMemory(&systemTime, sizeof(systemTime));
	FileTimeToSystemTime(&(m_data->TimeStamp), &systemTime);
	stream<<systemTime;

	const uint64 id = m_data->ID;

	if (id > 0)
	{
		stream<<", ID = "<<id;
	}
	stream<<", TID = "<<m_data->ThreadId<<", "<<m_data->Type<<">: ";

	const LrpLogEntryType type = m_data->Type;

	try
	{
		DoFormat(type, id, stream);
	}
	catch (const std::exception& ex)
	{
		stream<<ex.what();
	}
}

void CLrpLogEntry::DoFormat(const LrpLogEntryType type, const uint64 id, ostream& stream) const
{
	if (LrpLogEntryType_Outgoing == type)
	{
		if (id > 0)
		{
			CLrpClientLogger logger(stream);
			logger.Format(m_data->ComponentId, m_data->MethodId, m_data->Buffer);
		}
		else
		{
			CLrpServerLogger logger(stream);
			logger.Format(m_data->ComponentId, m_data->MethodId, m_data->Buffer);
		}
	}
	else if (LrpLogEntryType_Incomming == type)
	{
		if (id > 0)
		{
			CLrpServerLogger logger(stream);
			logger.Format(m_data->ComponentId, m_data->MethodId, m_data->Buffer);
		}
		else
		{
			CLrpClientLogger logger(stream);
			logger.Format(m_data->ComponentId, m_data->MethodId, m_data->Buffer);
		}
	}
	else
	{
		string st = ReadAString(m_data->Buffer);
		stream<<st;
	}
}
ostream& operator << (ostream& stream, const CLrpLogEntry& arg)
{
	arg.Format(stream);
	return stream;
}
