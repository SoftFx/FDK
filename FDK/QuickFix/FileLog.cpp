/****************************************************************************
** Copyright (c) quickfixengine.org  All rights reserved.
**
** This file is part of the QuickFIX FIX Engine
**
** This file may be distributed under the terms of the quickfixengine.org
** license as defined by quickfixengine.org and appearing in the file
** LICENSE included in the packaging of this file.
**
** This file is provided AS IS with NO WARRANTY OF ANY KIND, INCLUDING THE
** WARRANTY OF DESIGN, MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
**
** See http://www.quickfixengine.org/LICENSE for licensing information.
**
** Contact ask@quickfixengine.org if any conditions of this licensing are
** not clear to you.
**
****************************************************************************/
#include "stdafx.h"

#include "FixMessage.h"

#include "FileLog.h"


namespace FIX
{

	std::string generatePrefix( const SessionID& s )
	{
		const std::string& begin =
			s.getBeginString().getString();
		const std::string& sender =
			s.getSenderCompID().getString();
		const std::string& target =
			s.getTargetCompID().getString();
		const std::string& qualifier =
			s.getSessionQualifier();

		std::string prefix = begin + "-" + sender + "-" + target;
		if( qualifier.size() )
			prefix += "-" + qualifier;

		return prefix;
	}
	std::string CombinePath(const std::string& directory, const std::string& path)
	{
		char buffer[MAX_PATH] = "";
		const char* result = PathCombineA(buffer, directory.c_str(), path.c_str());
		if (nullptr == result)
		{
			std::string message = std::string("Couldn't combine path: directory = ") + directory + "; path = " + path;
			throw std::runtime_error(message);
		}
		return result;
	}


}

namespace FIX
{
	Log* FileLogFactory::create()
	{
		Log* result = new NullLog();
		return result;
	}

	Log* FileLogFactory::create( const SessionID& s )
	{
		std::string eventsPath;
		std::string messagesPath;

		std::string eventsFileName = m_eventsFileName;
		std::string messagesFileName = m_messagesFileName;

		if (!m_path.empty())
		{
			if (eventsFileName.empty())
			{
				eventsFileName = generatePrefix(s) + ".events.log";
			}
			if (messagesFileName.empty())
			{
				messagesFileName = generatePrefix(s) + ".messages.log";
			}
		}
		if (!eventsFileName.empty())
		{
			eventsPath = CombinePath(m_path, eventsFileName);
		}
		if (!messagesFileName.empty())
		{
			messagesPath = CombinePath(m_path, messagesFileName);
		}
		return new FileLog( eventsPath, messagesPath ,m_excludeMessagesFromLogs, m_decodeFixMessages);
	}

	void FileLogFactory::destroy( Log* pLog )
	{
		delete pLog;
	}
	LogEntry::LogEntry(const LogEntryType type, const std::string& value) : Type(type), Value(value)
	{
	}
	FileLog::FileLog( const std::string& eventsPath, const std::string& messagesPath, const std::string& excludeMessagesFromLogs, bool decodeFixMessages )
		: m_eventsEnabled()
        , m_messagesEnabled()
        , m_decodeFixMessages(decodeFixMessages)
        , m_pattern(nullptr)
        , m_thread()
        , m_semaphore()
        , m_continue()
        , m_passwordFieldRegex("(\\cA)(554|925)=[^\\cA]+(\\cA)")
        , m_passwordReplacement("$1$2=*****$3")
	{
		if (!eventsPath.empty())
		{
			m_event.open( eventsPath.c_str(), std::ios::out | std::ios::app );
			if (!m_event.is_open())
			{
				throw ConfigError( "Could not open event file: " + eventsPath);
			}
		}
		if (!messagesPath.empty())
		{
			m_messages.open( messagesPath.c_str(), std::ios::out | std::ios::app );
			if (!m_messages.is_open())
			{
				throw ConfigError( "Could not open messages file: " + messagesPath );
			}
		}

		if (!excludeMessagesFromLogs.empty())
		{
			m_pattern = new std::regex(excludeMessagesFromLogs);
		}

		m_eventsEnabled = !eventsPath.empty();
		m_messagesEnabled = !messagesPath.empty();

		Construct();
	}
	void FileLog::Construct()
	{
		m_continue = true;
		m_semaphore = CreateEvent(nullptr, FALSE, FALSE, nullptr);
		if (nullptr == m_semaphore)
		{
			throw std::runtime_error("Couldn't create new event");
		}
		if (!thread_spawn(&FileLog::ThreadMethod, this, m_thread))
		{
			CloseHandle(m_semaphore);
			throw std::runtime_error("Couldn't spawn new thread");
		}

	}
	THREAD_PROC FileLog::ThreadMethod( void* p )
	{
		try
		{
			FileLog* pThis = reinterpret_cast<FileLog*>(p);
			pThis->Loop();
			return 0;
		}
		catch (...)
		{
			return 1;
		}
	}
	void FileLog::Loop()
	{
		for (WaitForSingleObject(m_semaphore, INFINITE); m_continue; WaitForSingleObject(m_semaphore, INFINITE))
		{
			Step();
		}
		Step();
	}
	void FileLog::Step()
	{
		m_second.clear();

		{
			FIX::Locker lock(m_synchronizer);
			std::swap(m_first, m_second);
		}

		for each(const auto& element in m_second)
		{
			if (LogEntryType_Incomming == element.Type)
			{
				m_messages << UtcTimeStampConvertor::convert( element.TimeStamp, true ) << " : incoming> ";
				if(m_decodeFixMessages)
				{
					m_messages<< CFixMessage(element.Value);
				}
				else
				{
					m_messages<<element.Value;
				}
				m_messages<< std::endl<<std::endl;
			}
			else if (LogEntryType_Outgoing == element.Type)
			{
				m_messages << UtcTimeStampConvertor::convert( element.TimeStamp, true ) << " : outgoing> ";
				if (m_decodeFixMessages)
				{
					m_messages<<CFixMessage(element.Value);
				}
				else
				{
					m_messages<<element.Value;
				}
				m_messages << std::endl<<std::endl;
			}
			else
			{
				assert(LogEntryType_Event == element.Type);
				m_event << UtcTimeStampConvertor::convert( element.TimeStamp, true )<< " : event> " << element.Value << std::endl;
			}
		}
		m_second.clear();
	}
	FileLog::~FileLog()
	{
		m_continue = false;
		SetEvent(m_semaphore);

		thread_join(m_thread);

		m_messages.close();
		m_event.close();
		if (nullptr != m_pattern)
		{
			delete m_pattern;
			m_pattern = nullptr;
		}
	}
	void FileLog::clear()
	{
	}
	void FileLog::backup()
	{
	}
	void FileLog::onIncoming( const std::string& value )
	{
		if (m_messagesEnabled)
		{
			if(ExcludeFromLogs(value))
			{
				return;
			}
			Add(LogEntryType_Incomming, value);
		}
	}
	void FileLog::onOutgoing( const std::string& value )
	{
		if (m_messagesEnabled)
		{
			if(ExcludeFromLogs(value))
			{
				return;
			}
            // Neat solution would be adding configurable regex for removing / replacing specific fields like it's done for messages
			Add(LogEntryType_Outgoing, RemovePasswordValue(value));
		}
	}
	void FileLog::onEvent( const std::string& value )
	{
		if (m_eventsEnabled)
		{
			Add(LogEntryType_Event, value);
		}
	}

    std::string FileLog::RemovePasswordValue(const std::string& value)const
    {
        return std::regex_replace(value, m_passwordFieldRegex, m_passwordReplacement);
    }

	void FileLog::Add(const LogEntryType type, const std::string& value)
	{
		LogEntry entry(type, value);
		bool isEmpty = false;
		{
			FIX::Locker lock(m_synchronizer);
			isEmpty = m_first.empty();
			m_first.push_back(entry);
		}
		if (isEmpty)
		{
			SetEvent(m_semaphore);
		}
	}
	bool FileLog::ExcludeFromLogs(const std::string& value)const
	{
		if (nullptr == m_pattern)
		{
			return false;
		}
		size_t start = value.find("35=");
		if (std::string::npos == start)
		{
			return false;
		}
		start += 3;
		size_t finish = value.find(1, start);
		if (std::string::npos == finish)
		{
			return false;
		}
		std::string st = value.substr(start, finish - start);
		const bool result = std::regex_match(st, *m_pattern);
		return result;
	}

} //namespace FIX
