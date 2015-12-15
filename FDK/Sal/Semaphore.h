#ifndef __Sal_Semaphore__
#define __Sal_Semaphore__


class SAL_API CSemaphore
{
public:
	CSemaphore();
	CSemaphore(const size_t initialCount);
	CSemaphore(const size_t initialCount, const size_t maximumCount);
	~CSemaphore();
public:
	bool WaitFor();
	bool WaitFor(const size_t timeoutInMilliseconds);
	void Release();
	void Release(size_t count);
private:
	#ifdef _MSC_VER
	HANDLE m_handle;
	#else
	int32 m_counter;
	pthread_mutex_t m_mutex;
	pthread_cond_t m_condition;
	#endif
};
#endif