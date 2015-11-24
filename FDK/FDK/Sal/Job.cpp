#include "stdafx.h"
#include "Job.h"
#include "CallsWaiter.h"


void CJob::SafeInvoke()
{
	__try
	{
		Invoke();
	}
	__except(EXCEPTION_EXECUTE_HANDLER)
	{
	}
	if (nullptr != m_waiter)
	{
		m_waiter->Release();
		m_waiter = nullptr;
	}
}
