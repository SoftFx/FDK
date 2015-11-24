#pragma once


#include "Serializer.h"
#include "FinCalcImpl.h"

namespace FDK
{

	class CChannel
	{
	public:
		static CSerializer& GetSerializer();
		static CFinCalcImpl& GetFinCalc();

	};
}