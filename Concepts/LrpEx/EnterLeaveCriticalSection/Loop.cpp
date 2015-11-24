#include "stdafx.h"
#include "Loop.h"


CLoop::CLoop(const size_t count, HANDLE event, CRITICAL_SECTION* pSynchronizer) :
	m_count(count), m_event(event), m_synchronizer(pSynchronizer), m_thread(), m_duration()
{
}
CLoop::~CLoop()
{
	if (nullptr != m_thread)
	{
		CloseHandle(m_thread);
		m_thread = nullptr;
	}
}
void CLoop::Construct()
{
	if (nullptr != m_thread)
	{
		throw runtime_error("This instance is constructed already");
	}
	m_thread = CreateThread(nullptr, 0, &CLoop::ThreadFunction, this, 0, nullptr);
	if (nullptr == m_thread)
	{
		throw runtime_error("Couldn't create a new thread");
	}
}
void CLoop::WaitFor() const
{
	WaitForSingleObject(m_thread, INFINITE);
}
size_t CLoop::GetDuration() const
{
	return m_duration;
}
DWORD __stdcall CLoop::ThreadFunction(void* arg)
{
	CLoop* pThis = reinterpret_cast<CLoop*>(arg);
	pThis->Loop();
	return 0;
}
void CLoop::Loop()
{
	const size_t count = m_count;
	CRITICAL_SECTION* pSynchronizer = m_synchronizer;

	const ULONGLONG start = GetTickCount64();

	for (size_t index = 0; index < count; ++index)
	{
		EnterCriticalSection(pSynchronizer);
		LeaveCriticalSection(pSynchronizer);
	}

	const ULONGLONG finish = GetTickCount64();

	m_duration = static_cast<size_t>(finish - start);
}

ostream& operator << (ostream& stream, const CLoop& loop)
{
	const size_t duration = loop.GetDuration();
	double speed = loop.m_count * 1000.0 / duration;
	stream<<'\t'<<duration<<'\t'<<speed;
	return stream;
}