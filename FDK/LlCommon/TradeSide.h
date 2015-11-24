#pragma once

namespace FDK
{
	__declspec(align(4)) enum TradeSide
	{
		TradeSide_None = -1,
		TradeSide_Buy = 1,
		TradeSide_Sell = 2,
	};

	inline std::ostream& operator << (std::ostream& stream, TradeSide argument)
	{
		if (TradeSide_None == argument)
		{
			stream << "None";
		}
		else if (TradeSide_Buy == argument)
		{
			stream << "Buy";
		}
		else if (TradeSide_Sell == argument)
		{
			stream << "Sell";
		}
		else
		{
			stream << static_cast<int>(argument);
		}
		return stream;
	}
}