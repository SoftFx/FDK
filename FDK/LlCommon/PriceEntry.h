#pragma once

#include "TradeSide.h"

namespace FDK
{
	class CPriceEntry
	{
	public:
		double Bid;
		double Ask;
	public:
		inline CPriceEntry()
            : Bid()
            , Ask()
		{
		}

		inline CPriceEntry(const double bid, const double ask)
            : Bid(bid)
            , Ask(ask)
		{
		}

		/// <summary>
		/// Returns ask for buy and bid for sell
		/// </summary>
		/// <param name="side">trade entry side</param>
		/// <returns></returns>
		inline double PriceFromSide(TradeSide side)
		{
			if (TradeSide_Buy == side)
			{
				return Ask;
			}
			else if (TradeSide_Sell == side)
			{
				return Bid;
			}
			throw std::runtime_error("CPriceEntry::PriceFromSide() unknown side");
		}

		/// <summary>
		/// Returns bid for buy and ask for sell
		/// </summary>
		/// <param name="side">trade entry side</param>
		/// <returns></returns>
		inline double PriceFromOppositeSide(TradeSide side)
		{
			if (TradeSide_Buy == side)
			{
				return Bid;
			}
			else if (TradeSide_Sell == side)
			{
				return Ask;
			}
			throw std::runtime_error("CPriceEntry::PriceFromSide() unknown side");
		}

		/// <summary>
		/// Returns price rate, which should be used as multiplier for converting profit
		/// from profit currency to account currency.
		/// Example: xxx/yyy => zzz
		/// in this case we need profit conversion from yyy to zzz
		/// profit(zzz) = profit(yyy) * PriceMultiplierFromProfit(profit)
		/// </summary>
		/// <param name="profit">a converting profit</param>
		/// <returns></returns>
		inline double PriceMultiplierFromProfit(double profit)
		{
			// Price1 - ask if Py < 0, bid if Py >= 0;
			if (profit >= 0)
			{
				return Bid;
			}
			return Ask;
		}

		/// <summary>
		/// Returns price rate, which should be used as divisor for converting profit
		/// from profit currency to account currency.
		/// Example: xxx/yyy => zzz
		/// in this case we need profit conversion from yyy to zzz
		/// profit(zzz) = profit(yyy) / PriceDivisorFromProfit(profit)
		/// </summary>
		/// <param name="profit">a converting profit</param>
		/// <returns></returns>
		inline double PriceDivisorFromProfit(double profit)
		{
			// Price2 - bid if Py < 0, ask if Py >= 0;
			if (profit >= 0)
			{
				return Ask;
			}
			return Bid;
		}

	private:
#ifdef LLCOMMON_EXPORTS
		friend std::ostream& operator << (std::ostream& stream, const CPriceEntry& entry);
		friend std::istream& operator >> (std::istream& stream, CPriceEntry& entry);
#endif
	};
}