#include "stdafx.h"
#include "FxHandle.h"
#include "HandlesPool.h"

CFxHandle::CFxHandle()
    : m_counter(1)
{
	CHandlesPool::New(this);
}

CFxHandle::CFxHandle(const CFxHandle& )
    : m_counter(1)
{
	CHandlesPool::New(this);
}

CFxHandle& CFxHandle::operator= (const CFxHandle& )
{
	return *this;
}

CFxHandle::~CFxHandle()
{
	// normally m_counter should be zero,
	if (0 != m_counter)
	{
		// it is correct situation, if child class constructor throws an exception.
		assert(1 == m_counter);
		CHandlesPool::Delete(this);
	}
}

void CFxHandle::Acquire()
{
	CHandlesPool::Acquire(this);
}

void CFxHandle::DoAcquire()
{
	++m_counter;
}

void CFxHandle::Release()
{
	CHandlesPool::Release(this);
}

bool CFxHandle::DoRelease()
{
	--m_counter;
	if (0 == m_counter)
	{
		return true;
	}
	return false;
}
