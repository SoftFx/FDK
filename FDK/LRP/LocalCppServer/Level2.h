#pragma once
#include "QuoteEntry.h"



class CLevel2
{
public:
	string Symbol;
	CDateTime CreatingTime;
	vector<CQuoteEntry> Bids;
	vector<CQuoteEntry> Asks;
public:
	inline CLevel2()
	{
	}
	inline CLevel2(const string& symbol) : Symbol(Symbol)
	{
	}
};