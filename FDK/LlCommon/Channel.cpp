#include "stdafx.h"
#include "Channel.h"

namespace FDK
{
	CSerializer& CChannel::GetSerializer()
	{
		return *reinterpret_cast<CSerializer*>(nullptr);
	}

	CFinCalcImpl& CChannel::GetFinCalc()
	{
		return *reinterpret_cast<CFinCalcImpl*>(nullptr);
	}
}
