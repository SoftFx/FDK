#include "StdAfx.h"
#include "LogStream.h"


CLogStream::CLogStream()
{
}
CLogStream& CLogStream::operator << (const char* arg)
{
	try
	{
		if (NULL != arg)
		{
			m_stream<<arg;
		}
	}
	catch (...)
	{
		assert(!"an exception has been thrown");
	}
	return *this;
}
CLogStream& CLogStream::operator<<(ostream& (*arg)(ostream&))
{
	try
	{
		m_stream<<arg;
	}
	catch (...)
	{
		assert(!"an exception has been thrown");
	}
	return *this;
}
std::string CLogStream::ToString() const
{
	return m_stream.str();
}
