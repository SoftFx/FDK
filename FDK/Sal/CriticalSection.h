#ifndef __Sal_Critical_Section__
#define __Sal_Critical_Section__

class SAL_API CCriticalSection
{
public:

	CCriticalSection();
	~CCriticalSection();

	void Acquire();
	void Release();

private:

	CCriticalSection(const CCriticalSection&);
	CCriticalSection& operator = (const CCriticalSection&);

#ifdef _MSC_VER
	CRITICAL_SECTION m_section;
#else
	int32 m_count;
	pthread_t m_threadID;
	pthread_mutex_t m_mutex;
#endif
};

class CLock
{
public:

    CLock(CCriticalSection& section);
    ~CLock();

private:

    CLock(const CLock&);
    CLock& operator = (const CLock&);

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