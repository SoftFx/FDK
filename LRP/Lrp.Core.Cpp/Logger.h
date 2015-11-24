#pragma once
#include "Formatting.h"


typedef void (*LrpLogHandler)(void* pUserParam, const char* message);

class CLogger
{
public:
	CLogger(LrpLogHandler pLogHandler, void* pUserParam);
public:
	void Output(const char* message)
	{
		if (nullptr != m_logHandler)
		{
			m_logHandler(m_userParam, message);
		}
	}
	void Output(const string& message)
	{
		if (nullptr != m_logHandler)
		{
			m_logHandler(m_userParam, message.c_str());
		}
	}
	template<typename P0> void Output(const char* format, const P0& a0)
	{
		if (nullptr != m_logHandler)
		{
			string message = Format(format, a0);
			m_logHandler(m_userParam, message.c_str());
		}
	}
	template<typename P0, typename P1> void Output(const char* format, const P0& a0, const P1& a1)
	{
		if (nullptr != m_logHandler)
		{
			string message = Format(format, a0, a1);
			m_logHandler(m_userParam, message.c_str());
		}
	}
private:
	LrpLogHandler m_logHandler;
	void* m_userParam;
};