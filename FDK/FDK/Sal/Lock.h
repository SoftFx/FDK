#ifndef __Sal_Lock__
#define __Sal_Lock__


class CLock
{
public:
	CLock(CCriticalSection& section);
	~CLock();
private:
	CLock(const CLock&);
	CLock& operator = (const CLock&);
private:
	CCriticalSection& m_section;
};

inline CLock::CLock(CCriticalSection& section) : m_section(section)
{
	section.Acquire();
}
inline CLock::~CLock()
{
	m_section.Release();
}

#endif