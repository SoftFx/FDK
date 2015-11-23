#pragma once

class CTimeout 
{
public:
	CTimeout(const size_t intervalInMilliseconds);
public:
	size_t ToMilliseconds() const;
	timeval ToTimeValue() const;
	operator size_t () const;
private:
	unsigned __int64 m_finishTimeInMilliseconds;
};

inline CTimeout::operator size_t() const
{
	return ToMilliseconds();
}
