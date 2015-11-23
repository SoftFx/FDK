#ifndef __Sal_DateTime__
#define __Sal_DateTime__

class CDateTime
{
public:
	CDateTime();
	CDateTime(uint64 ticks);
public:
	operator uint64() const;
private:
	uint64 m_ticks;
private:
	friend bool operator < (const CDateTime& first, const CDateTime& second);
	friend bool operator > (const CDateTime& first, const CDateTime& second);
	friend bool operator <= (const CDateTime& first, const CDateTime& second);
	friend bool operator >= (const CDateTime& first, const CDateTime& second);
	friend bool operator == (const CDateTime& first, const CDateTime& second);
	friend bool operator != (const CDateTime& first, const CDateTime& second);
private:
	friend bool operator < (const int32 first, const CDateTime& second);
	friend bool operator > (const int32 first, const CDateTime& second);
	friend bool operator <= (const int32 first, const CDateTime& second);
	friend bool operator >= (const int32 first, const CDateTime& second);
	friend bool operator == (const int32 first, const CDateTime& second);
	friend bool operator != (const int32 first, const CDateTime& second);
private:
	friend bool operator < (const CDateTime& first, const int32 second);
	friend bool operator > (const CDateTime& first, const int32 second);
	friend bool operator <= (const CDateTime& first, const int32 second);
	friend bool operator >= (const CDateTime& first, const int32 second);
	friend bool operator == (const CDateTime& first, const int32 second);
	friend bool operator != (const CDateTime& first, const int32 second);
private:
	friend SAL_API std::ostream& operator << (std::ostream& stream, const CDateTime& arg);
};

SAL_API CDateTime FxUtcNow();

inline CDateTime::operator uint64 () const
{
	return m_ticks;
}
inline CDateTime::CDateTime() : m_ticks()
{
}
inline CDateTime::CDateTime(uint64 ticks) : m_ticks(ticks)
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
// int32 <-> CDateTime
inline bool operator < (const int32 first, const CDateTime& second)
{
	return (first < second.m_ticks);
}
inline bool operator > (const int32 first, const CDateTime& second)
{
	return (first > second.m_ticks);
}
inline bool operator <= (const int32 first, const CDateTime& second)
{
	return (first <= second.m_ticks);
}
inline bool operator >= (const int32 first, const CDateTime& second)
{
	return (first >= second.m_ticks);
}
inline bool operator == (const int32 first, const CDateTime& second)
{
	return (first == second.m_ticks);
}
inline bool operator != (const int32 first, const CDateTime& second)
{
	return (first != second.m_ticks);
}
// CDateTime <-> int32
inline bool operator < (const CDateTime& first, const int32 second)
{
	return (first.m_ticks < second);
}
inline bool operator > (const CDateTime& first, const int32 second)
{
	return (first.m_ticks > second);
}
inline bool operator <= (const CDateTime& first, const int32 second)
{
	return (first.m_ticks <= second);
}
inline bool operator >= (const CDateTime& first, const int32 second)
{
	return (first.m_ticks >= second);
}
inline bool operator == (const CDateTime& first, const int32 second)
{
	return (first.m_ticks == second);
}
inline bool operator != (const CDateTime& first, const int32 second)
{
	return (first.m_ticks != second);
}
#endif