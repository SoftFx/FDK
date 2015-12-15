#pragma once


enum TradeSide
{
	TradeSide_None = 0,
	TradeSide_Buy = 1,
	TradeSide_Sell = 2,
	TradeSide_Last = INT_MAX
};
inline ostream& operator << (ostream& stream, TradeSide& side)
{
	if (TradeSide_None == side)
	{
		stream<<"none";
	}
	else if (TradeSide_Buy == side)
	{
		stream<<"buy";
	}
	else if (TradeSide_Sell == side)
	{
		stream<<"sell";
	}
	stream<<'('<<(int)side<<')';
	return stream;
}
