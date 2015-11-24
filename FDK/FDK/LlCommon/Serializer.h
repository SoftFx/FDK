#pragma once

#include "CalculatorData.h"

namespace FDK
{
	class CSerializer
	{
	public:
		static string Serialize(const CCalculatorData& calc);
		static CCalculatorData Deserialize(const string& text);
	};
}
