#pragma once

namespace FDK
{
	/// <summary>
	/// Possible states of trade entry properties.
	/// </summary>
	__declspec(align(4)) enum TradeEntryStatus
	{
		/// <summary>
		/// Property of trade entry are not calculated.
		/// </summary>
		TradeEntryStatus_NotCalculated = 0,
		/// <summary>
		/// Property of trade entry are calculated successfully.
		/// </summary>
		TradeEntryStatus_Calculated = 1,
		/// <summary>
		/// Can not calculate property of trade entry due to unknown symbol.
		/// </summary>
		TradeEntryStatus_UnknownSymbol = 2,
		/// <summary>
		/// Can not calculate property of trade entry due to off quotes.
		/// </summary>
		TradeEntryStatus_OffQuotes = 3
	};


	inline std::ostream& operator << (std::ostream& stream, TradeEntryStatus argument)
	{
		if (TradeEntryStatus_NotCalculated == argument)
		{
			stream << "NotCalculated";
		}
		else if (TradeEntryStatus_Calculated == argument)
		{
			stream << "Calculated";
		}
		else if (TradeEntryStatus_UnknownSymbol == argument)
		{
			stream << "UnknownSymbol";
		}
		else if (TradeEntryStatus_OffQuotes == argument)
		{
			stream << "OffQuotes";
		}
		else
		{
			stream << static_cast<int>(argument);
		}
		return stream;
	}
}