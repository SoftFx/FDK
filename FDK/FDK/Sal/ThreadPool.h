#ifndef __Sal_ThreadPool__
#define __Sal_ThreadPool__

#include "CriticalSection.h"
#include "Semaphore.h"
#include "Job.h"

class SAL_API CThreadPool
{
public:
	CThreadPool(const size_t threadsNumber = 1);
	~CThreadPool();
public:
	void AddJob(CJob* pJob);
	void Finalize();
	void JoinAndFinalize();
private:
	bool Construct(const size_t threadsNumber);
	static unsigned int __stdcall ThreadFunction(void* pointer);
	void Loop();
	void Join();
	void Dispose();
private:
	void WaitFor();
	void Release();
	void Release(size_t count);
private:
	const size_t m_threadsNumber;
	HANDLE* m_threads;
private:
	CSemaphore m_semaphore;
	LONG m_counter;
private:
	CCriticalSection m_synchronizer;
	CJob* m_first;
	CJob* m_last;
	volatile bool m_continue;
	volatile bool m_disableNewJobs;
};
#endif