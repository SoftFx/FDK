#ifndef __Native_Timeout__
#define __Native_Timeout__

class CTimeout 
{
public:
	CTimeout(const uint32 intervalInMilliseconds);
public:
	uint32 ToMilliseconds() const;
	operator uint32 () const;
private:
	uint64 m_finishTimeInMilliseconds;
};

inline CTimeout::operator uint32() const
{
	return ToMilliseconds();
}

#endif
