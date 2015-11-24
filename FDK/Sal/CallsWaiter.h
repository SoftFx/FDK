#ifndef __Sal_CallsWaiter__
#define __Sal_CallsWaiter__
#include "CriticalSection.h"
#include "Semaphore.h"

class CJob;
class SAL_API CCallsWaiter
{
public:
	CCallsWaiter();
	~CCallsWaiter();
public:
	void WaitForFinish();
private:
	void Acquire();
	void Release();
private:
	bool m_isWaiting;
	size_t m_counter;
	CSemaphore m_event;
	CCriticalSection m_synchronizer;
private:
	friend class CJob;
};

#endif