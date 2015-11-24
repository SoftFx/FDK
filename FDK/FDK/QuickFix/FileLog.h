/* -*- C++ -*- */

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

#ifndef FIX_FILELOG_H
#define FIX_FILELOG_H

#ifdef _MSC_VER
#pragma warning( disable : 4503 4355 4786 4290 )
#endif

#include "Log.h"
#include "SessionSettings.h"
#include <fstream>

namespace FIX
{
	/**
	* Creates a file based implementation of Log
	*
	* This stores all log events into flat files
	*/
	class FileLogFactory : public LogFactory
	{
	public:
		FileLogFactory( const std::string& path, const std::string& eventsFileName, const std::string& messagesFileName, const std::string& excludeMessagesFromLogs ,const bool decodeFixMessages )
			: m_path( path ), m_eventsFileName(eventsFileName), m_messagesFileName(messagesFileName),
			m_excludeMessagesFromLogs(excludeMessagesFromLogs), m_decodeFixMessages(decodeFixMessages) {};
	public:
		Log* create();
		Log* create( const SessionID& );
		void destroy( Log* log );
	private:
		const std::string m_path;
		const std::string m_eventsFileName;
		const std::string m_messagesFileName;
		const std::string m_excludeMessagesFromLogs;
		const bool m_decodeFixMessages;
	};


	enum LogEntryType
	{
		LogEntryType_Incomming,
		LogEntryType_Outgoing,
		LogEntryType_Event
	};


	class LogEntry
	{
	public:
		LogEntryType Type;
		UtcTimeStamp TimeStamp;
		std::string Value;
	public:
		LogEntry(const LogEntryType type, const std::string& value);
	};


	/**
	* File based implementation of Log
	*
	* Two files are created by this implementation.  One for messages, 
	* and one for events.
	*
	*/
	class FileLog : public Log
	{
	public:
		FileLog( const std::string& eventsPath, const std::string& messagesPath, const std::string& excludeMessagesFromLogs, bool decodeFixMessages );
		virtual ~FileLog();
	private:
		void Construct();
	public:
		void clear();
		void backup();
		void onIncoming( const std::string& value );
		void onOutgoing( const std::string& value );
		void onEvent( const std::string& value );
		bool getMillisecondsInTimeStamp() const;
		void setMillisecondsInTimeStamp ( bool value );
	private:
		static THREAD_PROC ThreadMethod( void* p );
		void Loop();
		void Step();
		void Add(const LogEntryType type, const std::string& value);
	private:
		bool ExcludeFromLogs(const std::string& value)const;
        std::string RemovePasswordValue(const std::string& value)const;
	private:
		bool m_eventsEnabled;
		bool m_messagesEnabled;
		std::ofstream m_messages;
		std::ofstream m_event;
		bool m_decodeFixMessages;
		std::regex* m_pattern;
	private:
		thread_id m_thread;
		HANDLE m_semaphore;
		volatile bool m_continue;
		Mutex m_synchronizer;
		std::vector<LogEntry> m_first;
		std::vector<LogEntry> m_second;

        const std::regex m_passwordFieldRegex;
        const std::string m_passwordReplacement;
	};
}

#endif //FIX_LOG_H
