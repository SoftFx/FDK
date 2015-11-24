#pragma once
#include "MemoryStream.h"



class SAL_API CLogStream
{
public:
	CLogStream();
	CLogStream& operator << (const char* arg);
	CLogStream& operator << (std::ostream& (*arg)(std::ostream&));
	inline operator std::ostream&()
	{
		return m_stream;
	}
public:
	template<typename T> CLogStream& operator << (const T& arg)
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
public:
	std::string ToString() const;
private:
	CLogStream(const CLogStream&);
	CLogStream& operator = (const CLogStream&);
protected:
	CMemoryStream m_stream;
};