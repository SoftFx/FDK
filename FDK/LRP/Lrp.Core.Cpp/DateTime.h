#ifndef __FXOpenApi_Sal_DateTime__
#define __FXOpenApi_Sal_DateTime__

class CDateTime
{
public:
	CDateTime();
	CDateTime(unsigned __int64 ticks);
public:
	operator unsigned __int64() const;
public:
	static CDateTime UtcNow();
private:
	unsigned __int64 m_ticks;
private:
	friend bool operator < (const CDateTime& first, const CDateTime& second);
	friend bool operator > (const CDateTime& first, const CDateTime& second);
	friend bool operator <= (const CDateTime& first, const CDateTime& second);
	friend bool operator >= (const CDateTime& first, const CDateTime& second);
	friend bool operator == (const CDateTime& first, const CDateTime& second);
	friend bool operator != (const CDateTime& first, const CDateTime& second);
private:
	friend bool operator < (const __int32 first, const CDateTime& second);
	friend bool operator > (const __int32 first, const CDateTime& second);
	friend bool operator <= (const __int32 first, const CDateTime& second);
	friend bool operator >= (const __int32 first, const CDateTime& second);
	friend bool operator == (const __int32 first, const CDateTime& second);
	friend bool operator != (const __int32 first, const CDateTime& second);
private:
	friend bool operator < (const CDateTime& first, const __int32 second);
	friend bool operator > (const CDateTime& first, const __int32 second);
	friend bool operator <= (const CDateTime& first, const __int32 second);
	friend bool operator >= (const CDateTime& first, const __int32 second);
	friend bool operator == (const CDateTime& first, const __int32 second);
	friend bool operator != (const CDateTime& first, const __int32 second);
};


inline CDateTime::operator unsigned __int64 () const
{
	return m_ticks;
}
inline CDateTime::CDateTime() : m_ticks()
{
}
inline CDateTime::CDateTime(unsigned __int64 ticks) : m_ticks(ticks)
{
}
// CDateTime <-> CDateTime
inline bool operator < (const CDateTime& first, const CDateTime& second)
{
	return (first.m_ticks < second.m_ticks);
}
inline bool operator > (const CDateTime& first, const CDateTime& second)
{
	return (first.m_ticks > second.m_ticks);
}
inline bool operator <= (const CDateTime& first, const CDateTime& second)
{
	return (first.m_ticks <= second.m_ticks);
}
inline bool operator >= (const CDateTime& first, const CDateTime& second)
{
	return (first.m_ticks >= second.m_ticks);
}
inline bool operator == (const CDateTime& first, const CDateTime& second)
{
	return (first.m_ticks == second.m_ticks);
}
inline bool operator != (const CDateTime& first, const CDateTime& second)
{
	return (first.m_ticks != second.m_ticks);
}
// __int32 <-> CDateTime
inline bool operator < (const __int32 first, const CDateTime& second)
{
	return (first < second.m_ticks);
}
inline bool operator > (const __int32 first, const CDateTime& second)
{
	return (first > second.m_ticks);
}
inline bool operator <= (const __int32 first, const CDateTime& second)
{
	return (first <= second.m_ticks);
}
inline bool operator >= (const __int32 first, const CDateTime& second)
{
	return (first >= second.m_ticks);
}
inline bool operator == (const __int32 first, const CDateTime& second)
{
	return (first == second.m_ticks);
}
inline bool operator != (const __int32 first, const CDateTime& second)
{
	return (first != second.m_ticks);
}
// CDateTime <-> __int32
inline bool operator < (const CDateTime& first, const __int32 second)
{
	return (first.m_ticks < second);
}
inline bool operator > (const CDateTime& first, const __int32 second)
{
	return (first.m_ticks > second);
}
inline bool operator <= (const CDateTime& first, const __int32 second)
{
	return (first.m_ticks <= second);
}
inline bool operator >= (const CDateTime& first, const __int32 second)
{
	return (first.m_ticks >= second);
}
inline bool operator == (const CDateTime& first, const __int32 second)
{
	return (first.m_ticks == second);
}
inline bool operator != (const CDateTime& first, const __int32 second)
{
	return (first.m_ticks != second);
}
#include <sys/timeb.h>
inline CDateTime CDateTime::UtcNow()
{
	timeb time;
	ftime(&time);
	unsigned __int64 result = time.time * 1000 + time.millitm;
	return result;
}
#endif