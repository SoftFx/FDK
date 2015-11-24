#include "stdafx.h"
#include "ThreadPool.h"

CThreadPool::CThreadPool(const size_t threadsNumber) : m_threadsNumber(threadsNumber), m_counter(), m_threads(), m_first(), m_last(), m_continue(), m_disableNewJobs()
{
	if (!Construct(threadsNumber))
	{
		Finalize();
		throw runtime_error("could not construct thread pool");
	}
}
bool CThreadPool::Construct(const size_t threadsNumber)
{
	if (0 == threadsNumber)
	{
		return false;
	}
	m_threads = new HANDLE[threadsNumber];
	m_continue = true;
	bool result = true;
	for (size_t index = 0; index < threadsNumber; index++)
	{
		HANDLE thread = reinterpret_cast<HANDLE>(_beginthreadex(nullptr, 0, &CThreadPool::ThreadFunction, this, 0, nullptr));
		if (nullptr == thread)
		{
			result = false;
		}
		m_threads[index] = thread;
	}
	return result;
}
CThreadPool::~CThreadPool()
{
	Finalize();
	delete[] m_threads;
	m_threads = nullptr;
}
void CThreadPool::Finalize()
{
	m_continue = false;
	Release(m_threadsNumber);
	Join();
	for (CJob* pCurrent = m_first; nullptr != pCurrent;)
	{
		CJob* pTemp = pCurrent;
		pCurrent = pCurrent->Next();
		delete pTemp;
	}
	m_first = nullptr;
	m_last = nullptr;
}
void CThreadPool::JoinAndFinalize()
{
	Release(m_threadsNumber);
	Join();
	assert(nullptr == m_first);
	assert(nullptr == m_last);
}
void CThreadPool::AddJob(CJob* pJob)
{
	assert(!m_disableNewJobs);
	assert(nullptr != pJob);
	if (m_disableNewJobs)
	{
		delete pJob;
		return;
	}
	m_synchronizer.Acquire();
	if (nullptr == m_first)
	{
		assert(nullptr == m_last);
		m_first = pJob;
		m_last = pJob;
	}
	else
	{
		m_last->Add(pJob);
		m_last = pJob;
	}
	m_synchronizer.Release();
	Release();
}
void CThreadPool::Join()
{
	for (size_t index = 0; index < m_threadsNumber; index++)
	{
		HANDLE thread = m_threads[index];
		WaitForSingleObject(thread, INFINITE);
		CloseHandle(thread);
		m_threads[index] = nullptr;
	}
}
unsigned int __stdcall CThreadPool::ThreadFunction(void* pointer)
{
	CThreadPool* thisInstance = reinterpret_cast<CThreadPool*>(pointer);
	__try
	{
		thisInstance->Loop();
	}
	__except(EXCEPTION_EXECUTE_HANDLER)
	{
		return 1;
	}
	return 0;
}
void CThreadPool::Loop()
{
	for (WaitFor(); m_continue; WaitFor())
	{
		m_synchronizer.Acquire();
		CJob* pJob = m_first;
		if (nullptr == pJob)
		{
			m_synchronizer.Release();
			break;
		}
		m_first = pJob->Next();
		if (nullptr == m_first)
		{
			m_last = nullptr;
		}
		m_synchronizer.Release();
		// can't throw exception
		pJob->SafeInvoke();
		delete pJob;
	}
}
void CThreadPool::WaitFor()
{
	LONG newCounter = InterlockedDecrement(&m_counter);
	if (newCounter < 0)
	{
		m_semaphore.WaitFor();
	}
}
void CThreadPool::Release()
{
	LONG newCounter = InterlockedIncrement(&m_counter);
	if (newCounter < 1)
	{
		m_semaphore.Release();
	}
}
void CThreadPool::Release(size_t count)
{
	for (; count > 0; --count)
	{
		Release();
	}
}
