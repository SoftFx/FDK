#ifndef __Sal_Shared_Exclusive_Lock__
#define __Sal_Shared_Exclusive_Lock__
#ifndef _MSC_VER
#include "CriticalSection.h"
#endif



///////////////////////////////////////////////////////////////////////////////
// Supports at least 1024 threads
class SAL_API CSharedExclusiveLock
{
public:
	CSharedExclusiveLock();
	~CSharedExclusiveLock();
private:
	CSharedExclusiveLock(const CSharedExclusiveLock&);
	CSharedExclusiveLock& operator = (const CSharedExclusiveLock&);
public:
	void AcquireShared();
	void ReleaseShared();
	void AcquireExclusive();
	void ReleaseExclusive();
private:
	#ifdef _MSC_VER
	volatile LONG m_value;
	HANDLE m_sharedWakeEvent;
	HANDLE m_exclusiveWakeEvent;
	#else
	CCriticalSection m_synchronizer;
	#endif
};

// Many threads can shared acquire in the same time.
class CSharedLocker
{
public:
	inline CSharedLocker(CSharedExclusiveLock& instance):m_instance(instance)
	{
		instance.AcquireShared();
	}
	inline ~CSharedLocker()
	{
		m_instance.ReleaseShared();
	}
private:
	CSharedLocker(const CSharedLocker&);
	CSharedLocker& operator = (const CSharedLocker&);

	CSharedExclusiveLock& m_instance;
};

// Only one thread can exclusively acquire and no threads can shared acquire in the same time.
class CExclusiveLocker
{
public:
	inline CExclusiveLocker(CSharedExclusiveLock& instance):m_instance(instance)
	{
		instance.AcquireExclusive();
	}
	inline ~CExclusiveLocker()
	{
		m_instance.ReleaseExclusive();
	}
private:
	CExclusiveLocker(const CExclusiveLocker&);
	CExclusiveLocker& operator = (const CExclusiveLocker&);

	CSharedExclusiveLock& m_instance;
};


#endif
