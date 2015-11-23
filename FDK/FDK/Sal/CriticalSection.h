#ifndef __Sal_Critical_Section__
#define __Sal_Critical_Section__


class SAL_API CCriticalSection
{
public:
	CCriticalSection();
	~CCriticalSection();
public:
	void Acquire();
	void Release();
private:
	CCriticalSection(const CCriticalSection&);
	CCriticalSection& operator = (const CCriticalSection&);
private:
	#ifdef _MSC_VER
	CRITICAL_SECTION m_section;
	#else
	int32 m_count;
	pthread_t m_threadID;
	pthread_mutex_t m_mutex;
	#endif
};

#endif