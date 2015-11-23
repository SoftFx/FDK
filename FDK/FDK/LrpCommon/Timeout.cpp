#include "stdafx.h"
#include "Timeout.h"




namespace
{

	volatile unsigned __int64 gCurrentTimeInMilliseconds = 0;
	volatile DWORD gLastUpdatedTime = 0;
	const DWORD cGetTickCountMaximum = numeric_limits<DWORD>::max();
	CCriticalSection gSection;
}

namespace
{
	unsigned __int64 GetTickCountEx()
	{
		gSection.Acquire();
		const DWORD currentTime = GetTickCount();
		const DWORD lastUpdateTime = gLastUpdatedTime;
		if (currentTime >= lastUpdateTime)
		{
			gCurrentTimeInMilliseconds += (currentTime - lastUpdateTime);
		}
		else
		{
			gCurrentTimeInMilliseconds += currentTime + (cGetTickCountMaximum - lastUpdateTime) + 1;
		}
		gLastUpdatedTime = currentTime;
		const __int64 result = gCurrentTimeInMilliseconds;
		gSection.Release();
		return result;
	}
}

CTimeout::CTimeout(const size_t intervalInMilliseconds)
{
	m_finishTimeInMilliseconds = GetTickCountEx() + intervalInMilliseconds;
}

size_t CTimeout::ToMilliseconds() const
{
	unsigned __int64 currentTimeInMilliseconds = GetTickCountEx();
	if (currentTimeInMilliseconds >= m_finishTimeInMilliseconds)
	{
		return 0;
	}
	unsigned __int64 delta = m_finishTimeInMilliseconds - currentTimeInMilliseconds;
	assert(delta <= numeric_limits<DWORD>::max());
	const size_t result = static_cast<size_t>(delta);
	return result;
}

timeval CTimeout::ToTimeValue() const
{
	const size_t milliseconds = ToMilliseconds();
	timeval result;
	result.tv_sec = static_cast<long>(milliseconds / 1000);
    result.tv_usec = static_cast<long>(milliseconds % 1000);
	return result;
}
