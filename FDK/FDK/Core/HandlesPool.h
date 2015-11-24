#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)
#pragma warning (disable : 4251)

#include "FxHandle.h"


class CORE_API CHandlesPool
{
public:
	static void New(CFxHandle* handle);
	static void Acquire(CFxHandle* handle);
	static void Release(CFxHandle* handle);
	static void Delete(CFxHandle* handle);
public:
	template<typename T> static T* TypeFromHandle(CFxHandle* handle)
	{
		CHandlesPool& pool = PoolByHandle(handle);
		return pool.DoTypeFromHandle<T>(handle);
	}
public:
	~CHandlesPool();
private:
	void DoNew(CFxHandle* handle);
	void DoAcquire(CFxHandle* handle);
	void DoRelease(CFxHandle* handle);
	void DoDelete(CFxHandle* handle);
private:
	template<typename T> T* DoTypeFromHandle(CFxHandle* handle)
	{
		if (!AcquireIfValid(handle))
		{
			return nullptr;
		}
		T* result = dynamic_cast<T*>(handle);
		if (nullptr != result)
		{
			return result;
		}
		DoRelease(handle);
		return nullptr;
	}
	bool AcquireIfValid(CFxHandle* handle);
private:
	static CHandlesPool& PoolByHandle(CFxHandle* handle);
private:
	CCriticalSection m_synchronizer;
	set<CFxHandle*> m_handles;
};

#pragma warning (pop)