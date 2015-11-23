#include "stdafx.h"
#include "HandlesPool.h"
#include "ArgumentNullException.h"
#include "InvalidHandleException.h"

#pragma warning (disable : 4996)

namespace
{
	CHandlesPool gPools[1024];
}

CHandlesPool::~CHandlesPool()
{
	//assert(m_handles.empty());
}

CHandlesPool& CHandlesPool::PoolByHandle(CFxHandle* handle)
{
	size_t address = reinterpret_cast<size_t>(handle);
	size_t index = address / 4;
	index %= _countof(gPools);
	return gPools[index];
}

void CHandlesPool::New(CFxHandle* handle)
{
	CHandlesPool& pool = PoolByHandle(handle);
	pool.DoNew(handle);
}

void CHandlesPool::Acquire(CFxHandle* handle)
{
	CHandlesPool& pool = PoolByHandle(handle);
	pool.DoAcquire(handle);
}

void CHandlesPool::Release(CFxHandle* handle)
{
	CHandlesPool& pool = PoolByHandle(handle);
	pool.DoRelease(handle);
}

void CHandlesPool::Delete(CFxHandle* handle)
{
	CHandlesPool& pool = PoolByHandle(handle);
	pool.DoDelete(handle);
}

void CHandlesPool::DoNew(CFxHandle* handle)
{
	CLock lock(m_synchronizer);
	m_handles.insert(handle);
}

void CHandlesPool::DoAcquire(CFxHandle* handle)
{
	CLock lock(m_synchronizer);
	handle->DoAcquire();
}

void CHandlesPool::DoRelease(CFxHandle* handle)
{
	bool status = false;
	{
		CLock lock(m_synchronizer);
		//assert(m_handles.count(handle) > 0);
		status = handle->DoRelease();
		if (status)
		{
			m_handles.erase(handle);
		}
	}
	if (status)
	{
		delete handle;
	}
}

void CHandlesPool::DoDelete(CFxHandle* handle)
{
	CLock lock(m_synchronizer);
	//assert(m_handles.count(handle) > 0);
	m_handles.erase(handle);
}

bool CHandlesPool::AcquireIfValid(CFxHandle* handle)
{
	if (nullptr == handle)
	{
		throw CArgumentNullException("handle is null");
	}
	{
		CLock lock(m_synchronizer);
		if (m_handles.count(handle) > 0)
		{
			handle->DoAcquire();
			return true;
		}
	}
	throw CInvalidHandleException(handle);
}

