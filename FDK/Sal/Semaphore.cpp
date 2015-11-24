#include "stdafx.h"
#include "Semaphore.h"

#ifdef _MSC_VER
CSemaphore::CSemaphore()
{
	m_handle = CreateSemaphore(nullptr, 0, numeric_limits<int32>::max(), nullptr);
}
CSemaphore::CSemaphore(const size_t initialCount)
{
	m_handle = CreateSemaphore(nullptr, static_cast<LONG>(initialCount), numeric_limits<int32>::max(), nullptr);
}
CSemaphore::CSemaphore(const size_t initialCount, const size_t maximumCount)
{
	m_handle = CreateSemaphore(nullptr, static_cast<LONG>(initialCount), static_cast<LONG>(maximumCount), nullptr);
}
CSemaphore::~CSemaphore()
{
	CloseHandle(m_handle);
}
bool CSemaphore::WaitFor()
{
	return WaitFor(INFINITE);
}
bool CSemaphore::WaitFor(const size_t timeoutInMilliseconds)
{
	const DWORD status = WaitForSingleObject(m_handle, static_cast<DWORD>(timeoutInMilliseconds));
	const bool result = (WAIT_OBJECT_0 == status);
	return result;
}
void CSemaphore::Release()
{
	Release(1);
}
void CSemaphore::Release(size_t count)
{
	ReleaseSemaphore(m_handle, static_cast<LONG>(count), nullptr);
}
#else
CSemaphore::CSemaphore() : m_counter()
{
	pthread_mutex_init(&m_mutex, NULL);
	pthread_cond_init(&m_condition, NULL);
}
CSemaphore::CSemaphore(const size_t initialCount) : m_counter(initialCount)
{
	pthread_mutex_init(&m_mutex, NULL);
	pthread_cond_init(&m_condition, NULL);
}

CSemaphore::CSemaphore(const size_t initialCount, const size_t maximumCount)
{

}
CSemaphore::~CSemaphore()
{
	pthread_mutex_destroy(&m_mutex);
	pthread_cond_destroy(&m_condition);
}
bool CSemaphore::WaitFor()
{
	pthread_mutex_lock(&m_mutex);
	pthread_cond_wait(&m_condition, &m_mutex);
	--m_counter;
	pthread_mutex_unlock(&m_mutex);
}
bool CSemaphore::WaitFor(const size_t timeoutInMilliseconds)
{
	timeb time;
	ftime(&time);

	time.time += timeoutInMilliseconds / 1000;
	time.millitm += timeoutInMilliseconds % 1000;
	if (time.millitm > 999)
	{
		time.millitm -= 1000;
		time.time++;
	}

	timespec when;
	when.tv_sec = time.time;
	when.tv_nsec = time.millitm * 1000000;

	pthread_mutex_lock(&m_mutex);

	int status = 0;

	while (m_counter <= 0)
	{
		status = pthread_cond_timedwait(&m_condition, &m_mutex, &when);
		if (status)
		{
			break;
		}
	}
	if (status)
	{
		return false;
	}
	m_counter--;
	pthread_mutex_unlock(&m_mutex);
}
void CSemaphore::Release()
{
	Release(1);
}
void CSemaphore::Release(size_t count)
{
	if (count > 0)
	{
		pthread_mutex_lock(&m_mutex);
		m_counter += count;
		pthread_mutex_unlock(&m_mutex);
		pthread_cond_signal(&m_condition);
	}
}
#endif
