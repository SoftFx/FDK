#include "stdafx.h"
#include "CallsWaiter.h"

CCallsWaiter::CCallsWaiter() : m_isWaiting(false), m_counter(), m_event(0, 1)
{
}
CCallsWaiter::~CCallsWaiter()
{
	assert(0 == m_counter);
}
void CCallsWaiter::WaitForFinish()
{
	size_t counter = 0;
	{
		CLock lock(m_synchronizer);
		m_isWaiting = true;
		counter = m_counter;
	}
	if (counter > 0)
	{
		m_event.WaitFor();
	}
}
void CCallsWaiter::Acquire()
{
	CLock lock(m_synchronizer);
	m_counter++;
}
void CCallsWaiter::Release()
{
	CLock lock(m_synchronizer);
	assert(m_counter > 0);
	m_counter--;
	if (m_isWaiting && (0 == m_counter))
	{
		m_event.Release();
	}
}
