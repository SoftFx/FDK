#include "stdafx.h"
#include "CriticalSection.h"
#ifdef _MSC_VER
CCriticalSection::CCriticalSection()
{
	InitializeCriticalSection(&m_section);
}
CCriticalSection::~CCriticalSection()
{
	DeleteCriticalSection(&m_section);
}
void CCriticalSection::Acquire()
{
	EnterCriticalSection(&m_section);
}
void CCriticalSection::Release()
{
	LeaveCriticalSection(&m_section);
}
#else
CCriticalSection::CCriticalSection()
{
	m_count = 0;
	m_threadID = 0;
	pthread_mutex_init(&m_mutex, 0);
}
CCriticalSection::~CCriticalSection()
{
	pthread_mutex_destroy(&m_mutex);
}
void CCriticalSection::Acquire()
{
	pthread_t id = 0;
	if (m_count > 0)
	{
		id = pthread_self();
		if (m_threadID == id)
		{
			++m_count;
			return;
		}
	}
	pthread_mutex_lock(&m_mutex);
	++m_count;
	m_threadID = id;
}
void CCriticalSection::Release()
{
	--m_count;
	if (0 == m_count)
	{
		m_threadID = 0;
		pthread_mutex_unlock(&m_mutex);
	}
}

#endif
