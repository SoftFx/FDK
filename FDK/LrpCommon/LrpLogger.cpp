#include "stdafx.h"
#include "LrpLogger.h"

namespace
{
	const string cEventsLogFileName = "EventsLogFileName";
	const string cMessagesLogFileName = "MessagesLogFileName";
}

namespace
{
	void CreateDirectoryRecursively(const string& path)
	{
		auto begin = path.begin();
		auto end = path.end();
		auto it = find(begin, end, '\\');

		for (; it != end;)
		{
			string st(begin, it);
			CreateDirectoryA(st.c_str(), NULL);
			++it;
			it = find(it, end, '\\');
		}
	}
	string CombinePath(const string& directory, const string& fileName)
	{
		string result = directory;
		if (!result.empty() && ('\\' != result.back()))
		{
			result += '\\';
		}
		result += fileName;
		return result;	
	}
}
CLrpLogger::CLrpLogger(const CFxParams& params) : m_continue(true), m_event(0, 1)
{
	string eventsLogPath = params.GetString(cEventsLogFileName);
	string messagessLogPath = params.GetString(cMessagesLogFileName);
	Construct(eventsLogPath, messagessLogPath);
}
CLrpLogger::CLrpLogger(const string& eventsLogPath, const string& messagesLogPath) : m_continue(true), m_event(0, 1)
{
	Construct(eventsLogPath, messagesLogPath);
}
CLrpLogger::CLrpLogger(const string& logsLocation, const string& eventLogFileName, const string& messagesLogFileName) : m_continue(true), m_event(0, 1)
{
	CreateDirectoryRecursively(logsLocation);
	const string eventsLogPath = CombinePath(logsLocation, eventLogFileName);
	const string messagesLogPath = CombinePath(logsLocation, messagesLogFileName);
	Construct(eventsLogPath, messagesLogPath);
}
void CLrpLogger::Construct(const string& eventsLogPath, const string& messagesLogPath)
{
	m_eventsLog.open(eventsLogPath, ios::app);
	m_messagesLog.open(messagesLogPath, ios::app);

	Delegate<void ()> func(this, &CLrpLogger::Loop);
	func.DoAsynch(m_thread);
}
CLrpLogger::~CLrpLogger()
{
	m_continue = false;
	m_event.Release();
	m_thread.Finalize();
}
void CLrpLogger::WriteIncommingMessage(const uint16 componentId, const uint16 methodId, const MemoryBuffer& buffer)
{
	WriteMessage(0, LrpLogEntryType_Incomming, componentId, methodId, buffer);
}
void CLrpLogger::WriteIncommingMessage(const uint64 id, const uint16 componentId, const uint16 methodId, const MemoryBuffer& buffer)
{
	WriteMessage(id, LrpLogEntryType_Incomming, componentId, methodId, buffer);
}
void CLrpLogger::WriteOutgoingMessage(const uint16 componentId, const uint16 methodId, const MemoryBuffer& buffer)
{
	WriteMessage(0, LrpLogEntryType_Outgoing, componentId, methodId, buffer);
}
void CLrpLogger::WriteOutgoingMessage(const uint64 id, const uint16 componentId, const uint16 methodId, const MemoryBuffer& buffer)
{
	WriteMessage(id, LrpLogEntryType_Outgoing, componentId, methodId, buffer);
}
void CLrpLogger::WriteEvent(const string& message)
{
	WriteEvent(0, message);
}
void CLrpLogger::WriteEvent(const uint64 id, const string& message)
{
	bool isEmpty = false;
	{
		CLock lock(m_synchronizer);
		isEmpty = m_first.empty();
		m_first.push_back(CLrpLogEntry(id, message));
	}
	if (isEmpty)
	{
		m_event.Release();
	}
}
void CLrpLogger::WriteMessage(const uint64 id, const LrpLogEntryType type, const uint16 componentId, const uint16 methodId, const MemoryBuffer& buffer)
{
	bool isEmpty = false;
	try
	{
		CLock lock(m_synchronizer);
		isEmpty = m_first.empty();
		m_first.push_back(CLrpLogEntry(id, type, componentId, methodId, buffer));
	}
	catch (const std::exception&)
	{
	}
	if (isEmpty)
	{
		m_event.Release();
	}
}
void CLrpLogger::Loop()
{
	for (m_event.WaitFor(); m_continue; m_event.WaitFor())
	{
		Step();
	}
}
void CLrpLogger::Step()
{
	try
	{
		DoStep();
	}
	catch (const std::exception&)
	{
	}
}
void CLrpLogger::DoStep()
{
	m_second.clear();
	{
		CLock lock(m_synchronizer);
		std::swap(m_first, m_second);
	}
	auto it = m_second.begin();
	auto end = m_second.end();
	for (; it != end; ++it)
	{
		DoStep(*it);
	}

	m_messagesLog<<flush;
	m_eventsLog<<flush;

	m_second.clear();
}
void CLrpLogger::DoStep(const CLrpLogEntry& entry)
{
	const LrpLogEntryType type = entry.GetType();
	if ((LrpLogEntryType_Incomming == type) || (LrpLogEntryType_Outgoing == type))
	{
		m_messagesLog<<entry<<endl;
	}
	else
	{
		m_eventsLog<<entry<<endl;
	}
}
void operator>>(const CLogStream& stream, CLrpLogger& logger)
{
	logger.WriteEvent(stream.ToString());
}