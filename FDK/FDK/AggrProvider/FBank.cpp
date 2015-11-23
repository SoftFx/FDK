#include "stdafx.h"
#include "FBank.h"

CFBank::CFBank() : Code()
{
}
void CFBank::Clear()
{
	Code = 0;
	Bids.clear();
	Asks.clear();
}
void CFBank::UpdateQuote(CFxQuote& quote) const
{
	quote.Bids.clear();
	quote.Asks.clear();

	quote.CreatingTime = CDateTime();

	for each(const auto& element in Bids)
	{
		quote.AddBid(element.Price, element.Volume);
		quote.CreatingTime = std::max(quote.CreatingTime, element.Setting.toFileTime());
	}
	for each(const auto& element in Asks)
	{
		quote.AddAsk(element.Price, element.Volume);
		quote.CreatingTime = std::max(quote.CreatingTime, element.Setting.toFileTime());
	}
}
CBinaryReader& operator>>(CBinaryReader& stream, CFBank& arg)
{
	arg.Clear();
	stream>>arg.Code>>arg.Bids>>arg.Asks;
	return stream;
}
