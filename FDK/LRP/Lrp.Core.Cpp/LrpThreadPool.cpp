#include "stdafx.h"
//#include "LrpThreadPool.h"
//#include "MtChannelImpl.h"
//
//CLrpThreadPool::CLrpThreadPool(size_t threadsNumber) : m_continue(true)
//{
//	const LONG maximum = (numeric_limits<LONG>::max)();
//	m_semaphore = CreateSemaphore(NULL, 0, maximum, NULL);
//	if (NULL == m_semaphore)
//	{
//		throw runtime_error("Can not create a new semaphore");
//	}
//
//	m_threads.reserve(threadsNumber);
//
//	for (size_t index = 0; index < threadsNumber; index++)
//	{
//		HANDLE thread = reinterpret_cast<HANDLE>(_beginthreadex(nullptr, 0, &CLrpThreadPool::ThreadFunction, this, 0, nullptr));
//		m_threads.push_back(thread);
//	}
//}
//CLrpThreadPool::~CLrpThreadPool()
//{
//	Finalize();
//	CloseHandle(m_semaphore);
//}
//void CLrpThreadPool::Add(const Channel& channel)
//{
//	{
//		CLock lock(m_synchronizer);
//		if (m_continue)
//		{
//			m_jobs.push_back(channel);
//		}
//		else
//		{
//			return;
//		}
//		
//	}
//	ReleaseSemaphore(m_semaphore, 1, nullptr);
//}
//void CLrpThreadPool::Finalize()
//{
//	m_continue = false;
//	if (nullptr != m_semaphore)
//	{
//		ReleaseSemaphore(m_semaphore, static_cast<LONG>(m_threads.size()), nullptr);
//	}
//	for each(const auto element in m_threads)
//	{
//		WaitForSingleObject(element, INFINITE);
//		CloseHandle(element);
//	}
//	m_threads.clear();
//	if (nullptr != m_semaphore)
//	{
//		CloseHandle(m_semaphore);
//		m_semaphore = nullptr;
//	}
//}
//
//unsigned __stdcall CLrpThreadPool::ThreadFunction(void* arg)
//{
//	CLrpThreadPool* pThreadPool = reinterpret_cast<CLrpThreadPool*>(arg);
//	__try
//	{
//		pThreadPool->Loop();
//	}
//	__except(LrpExceptionHandler(GetExceptionInformation()))
//	{
//		return 1;
//	}
//	return 0;
//}
//
//void CLrpThreadPool::Loop()
//{
//	for(WaitForSingleObject(m_semaphore, INFINITE); m_continue; WaitForSingleObject(m_semaphore, INFINITE))
//	{
//		Invoke();
//	}
//}
//
//void CLrpThreadPool::Invoke()
//{
//	Channel channel;
//	{
//		CLock lock(m_synchronizer);
//		if (!m_jobs.empty())
//		{
//			channel = m_jobs.front();
//			m_jobs.pop_front();
//		}
//		else
//		{
//			return;
//		}
//	}
//	channel->Process();
//}
