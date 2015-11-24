#include "stdafx.h"
#include "SharedExclusiveLock.h"

#ifdef _MSC_VER

/*
http://processhacker.svn.sourceforge.net/viewvc/processhacker/1.x/trunk/ProcessHacker.Common/Threading/FastResourceLock.cs?revision=HEAD&view=markup
*/


namespace
{
	// Lock owned: 1 bit.
	const LONG LockOwned = 0x1;

	// Exclusive waking: 1 bit.
	const LONG LockExclusiveWaking = 0x2;

	// Shared owners count: 10 bits.
	const LONG LockSharedOwnersShift = 2;
	const LONG LockSharedOwnersMask = 0x3ff;
	const LONG LockSharedOwnersIncrement = 0x4; // 2 ^ 2

	// Shared waiters count: 10 bits.
	const LONG LockSharedWaitersShift = 12;
	const LONG LockSharedWaitersMask = 0x3ff;
	const LONG LockSharedWaitersIncrement = 0x1000; // 2 ^ 12

	// Exclusive waiters count: 10 bits.
	const LONG LockExclusiveWaitersShift = 22;
	const LONG LockExclusiveWaitersMask = 0x3ff;
	const LONG LockExclusiveWaitersIncrement = 0x400000; // 2 ^ 22

	const LONG SpinCount = 4000;

	const LONG ExclusiveMask = LockExclusiveWaking | (LockExclusiveWaitersMask << LockExclusiveWaitersShift);
}


CSharedExclusiveLock::CSharedExclusiveLock()
 : m_value()
{
	m_sharedWakeEvent = CreateSemaphore(NULL, 0, LONG_MAX, NULL);
	m_exclusiveWakeEvent = CreateSemaphore(NULL, 0, LONG_MAX, NULL);
}

CSharedExclusiveLock::~CSharedExclusiveLock()
{
	CloseHandle(m_sharedWakeEvent);
	CloseHandle(m_exclusiveWakeEvent);
}

// Acquires the lock in shared mode, blocking if necessary.
// Exclusive acquires are given precedence over shared acquires.
void CSharedExclusiveLock::AcquireShared()
{
	for (int i = 0; ;)
	{
		int value = m_value;

		// Case 1: lock not owned AND no exclusive waiter is waking up AND
		// there are no shared owners AND there are no exclusive waiters
		if ((value & (LockOwned | (LockSharedOwnersMask << LockSharedOwnersShift) | ExclusiveMask)) == 0)
		{
			if (InterlockedCompareExchange(&m_value, value + LockOwned + LockSharedOwnersIncrement, value) == value)
				break;
		}
		// Case 2: lock is owned AND no exclusive waiter is waking up AND
		// there are shared owners AND there are no exclusive waiters
		else if ((value & LockOwned) != 0 && ((value >> LockSharedOwnersShift) & LockSharedOwnersMask) != 0 &&(value & ExclusiveMask) == 0)
		{
			if (InterlockedCompareExchange(& m_value, value + LockSharedOwnersIncrement, value ) == value)
				break;
		}
		// Other cases.
		else if (i >= SpinCount)
		{
			if (InterlockedCompareExchange(&m_value, value + LockSharedWaitersIncrement, value) == value)
			{
				// Go to sleep.
				WaitForSingleObject(m_sharedWakeEvent, INFINITE);
				// Go back and try again.
				continue;
			}
		}
		i++;
	}
}

// Releases the lock in shared mode.
void CSharedExclusiveLock::ReleaseShared()
{
	for (;;)
	{
		int value = m_value;
		int sharedOwners = (value >> LockSharedOwnersShift) & LockSharedOwnersMask;

		// Case 1: there are multiple shared owners.
		if (sharedOwners > 1)
		{
			if (InterlockedCompareExchange(&m_value, value - LockSharedOwnersIncrement, value) == value)
				break;
		}
		// Case 2: we are the last shared owner AND there are exclusive waiters.
		else if (((value >> LockExclusiveWaitersShift) & LockExclusiveWaitersMask) != 0)
		{
			if (InterlockedCompareExchange(&m_value, value - LockOwned + LockExclusiveWaking - LockSharedOwnersIncrement - LockExclusiveWaitersIncrement, value) == value)
			{
				ReleaseSemaphore(m_exclusiveWakeEvent, 1, NULL);
				break;
			}
		}
		// Case 3: we are the last shared owner AND there are no exclusive waiters.
		else
		{
			if (InterlockedCompareExchange(&m_value, value - LockOwned - LockSharedOwnersIncrement, value) == value)
				break;
		}
	}
}
// Acquires the lock in exclusive mode, blocking if necessary.
// Exclusive acquires are given precedence over shared acquires.
void CSharedExclusiveLock::AcquireExclusive()
{
	for (int i = 0; ; i++)
	{
		int value = m_value;

		// Case 1: lock not owned AND an exclusive waiter is not waking up.
		// Here we don't have to check if there are exclusive waiters, because
		// if there are the lock would be owned, and we are checking that anyway.
		if ((value & (LockOwned | LockExclusiveWaking)) == 0)
		{
			if (InterlockedCompareExchange(&m_value, value + LockOwned, value) == value)
				break;
		}
		// Case 2: lock owned OR lock not owned and an exclusive waiter is waking up.
		// The second case means an exclusive waiter has just been woken up and is
		// going to acquire the lock. We have to go to sleep to make sure we don't
		// steal the lock.
		else if (i >= SpinCount)
		{
			if (InterlockedCompareExchange(& m_value, value + LockExclusiveWaitersIncrement, value) == value)
			{
				// Go to sleep.
				WaitForSingleObject(m_exclusiveWakeEvent, INFINITE);

				// Acquire the lock.
				// At this point *no one* should be able to steal the lock from us.
				do
				{
					value = m_value;

				} while (InterlockedCompareExchange(&m_value, value + LockOwned - LockExclusiveWaking, value) != value);

				break;
			}
		}
	}
}
// Releases the lock in exclusive mode.
void CSharedExclusiveLock::ReleaseExclusive()
{
	for (;;)
	{
		int value = m_value;

		// Case 1: if we have exclusive waiters, release one.
		if (((value >> LockExclusiveWaitersShift) & LockExclusiveWaitersMask) != 0)
		{
			if (InterlockedCompareExchange(&m_value, value - LockOwned + LockExclusiveWaking - LockExclusiveWaitersIncrement, value) == value)
			{
				ReleaseSemaphore(m_exclusiveWakeEvent, 1, NULL);
				break;
			}
		}
		// Case 2: if we have shared waiters, release all of them.
		else
		{
			int sharedWaiters = (value >> LockSharedWaitersShift) & LockSharedWaitersMask;

			if (InterlockedCompareExchange(& m_value, value & ~(LockOwned | (LockSharedWaitersMask << LockSharedWaitersShift)), value) == value)
			{
				if (sharedWaiters != 0)
					ReleaseSemaphore(m_sharedWakeEvent, sharedWaiters, NULL);
				break;
			}
		}
	}
}
#else
CSharedExclusiveLock::CSharedExclusiveLock()
{
}
CSharedExclusiveLock::~CSharedExclusiveLock()
{
}
void CSharedExclusiveLock::AcquireExclusive()
{
	m_synchronizer.Acquire();
}
void CSharedExclusiveLock::ReleaseShared()
{
	m_synchronizer.Release();
}
void CSharedExclusiveLock::AcquireShared()
{
	m_synchronizer.Acquire();
}
void CSharedExclusiveLock::ReleaseExclusive()
{
	m_synchronizer.Release();
}
#endif