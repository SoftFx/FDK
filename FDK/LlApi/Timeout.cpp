#include "stdafx.h"
#include "Timeout.h"


CTimeout::CTimeout(const uint32 intervalInMilliseconds)
{
	m_finishTimeInMilliseconds = FxGetTickCount()+ intervalInMilliseconds;
}

uint32 CTimeout::ToMilliseconds() const
{
	uint64 currentTimeInMilliseconds = FxGetTickCount();
	if (currentTimeInMilliseconds >= m_finishTimeInMilliseconds)
	{
		return 0;
	}
	uint64 delta = m_finishTimeInMilliseconds - currentTimeInMilliseconds;
	assert(delta <= numeric_limits<uint32>::max());
	const uint32 result = static_cast<uint32>(delta);
	return result;
}
