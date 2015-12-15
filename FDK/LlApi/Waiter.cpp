#include "stdafx.h"
#include "Waiter.h"

CWaiter::CWaiter(const uint32 timeoutInMilliseconds)
    : m_waitingInterval(timeoutInMilliseconds)
    , m_timeout(timeoutInMilliseconds)
{
}

CWaiter::~CWaiter()
{
}

const string& CWaiter::Id() const
{
	return m_id;
}

void CWaiter::ResetTimeout(const uint32 timeoutInMilliseconds)
{
	m_timeout = CTimeout(timeoutInMilliseconds);
}
