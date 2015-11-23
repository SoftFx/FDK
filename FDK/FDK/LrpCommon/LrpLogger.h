#pragma once
#include "LrpLogEntry.h"


class CLrpLogger
{
public:
	CLrpLogger(const CFxParams& params);
	CLrpLogger(const string& eventsLogPath, const string& messagesLogPath);
	CLrpLogger(const string& logsLocation, const string& eventLogFileName, const string& messagesLogFileName);
	~CLrpLogger();
private:
	void Construct(const string& eventsLogPath, const string& messagesLogPath);
public:
	void WriteIncommingMessage(const uint16 componentId, const uint16 methodId, const MemoryBuffer& buffer);
	void WriteOutgoingMessage(const uint16 componentId, const uint16 methodId, const MemoryBuffer& buffer);
	void WriteEvent(const string& message);
public:
	void WriteIncommingMessage(const uint64 id, const uint16 componentId, const uint16 methodId, const MemoryBuffer& buffer);
	void WriteOutgoingMessage(const uint64 id, const uint16 componentId, const uint16 methodId, const MemoryBuffer& buffer);
	void WriteEvent(const uint64 id, const string& message);
private:
	void WriteMessage(const uint64 id, const LrpLogEntryType type, const uint16 componentId, const uint16 methodId, const MemoryBuffer& buffer);
private:
	void Loop();
	void Step();
	void DoStep();
	void DoStep(const CLrpLogEntry& entry);
private:
	volatile bool m_continue;
	CSemaphore m_event;
	CCriticalSection m_synchronizer;
	CThreadPool m_thread;
private:
	vector<CLrpLogEntry> m_first;
	vector<CLrpLogEntry> m_second;
private:
	ofstream m_eventsLog;
	ofstream m_messagesLog;
};


void operator >> (const CLogStream& stream, CLrpLogger& logger);