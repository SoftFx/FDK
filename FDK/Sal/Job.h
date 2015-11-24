#pragma once
#include "CallsWaiter.h"

class CJob
{
public:
	CJob(CCallsWaiter* waiter = nullptr) throw();
	CJob* Next()const throw();
	void Add(CJob* call)throw();
public:
	void SafeInvoke();
	virtual ~CJob();
protected:
	virtual void Invoke() = 0;
private:
	CJob(const CJob&);
	CJob& operator = (const CJob&);
private:
	CCallsWaiter* m_waiter;
	CJob* m_next;
};
inline CJob* CJob::Next()const throw()
{
	return m_next;
}
inline void CJob::Add(CJob *call) throw()
{
	assert(nullptr != call);
	assert(nullptr == m_next);
	m_next = call;
}
inline CJob::CJob(CCallsWaiter* waiter /* = nullptr */) throw() : m_waiter(waiter), m_next()
{
	if (nullptr!= m_waiter)
	{
		m_waiter->Acquire();
	}
}
inline CJob::~CJob()
{
	if (nullptr != m_waiter)
	{
		m_waiter->Release();
	}
}
