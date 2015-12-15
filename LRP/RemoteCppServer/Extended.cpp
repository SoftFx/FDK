#include "stdafx.h"
#include "Extended.h"


CReturnType CExtended::Do(CInType inArg, CInOutType& inOutArg, COutType& outArg)
{
	inOutArg.Used = inArg.Used;
	outArg.Used = inArg.Used;
	inOutArg.Value2 = inOutArg.Value2 * inOutArg.Value2;
	outArg.Value3 = inArg.Value - 1;

	CReturnType result;
	result.Used = inArg.Used;
	result.Value4 = inArg.Value * 3;
	return result;
}

int CExtended::MarketBuy(const string& symbol, double price, double& volume, double& amount)
{
	amount = volume * price;
	volume = 0;
	return 0;
}
void CExtended::Update(map<string, double>& reports, unordered_map<string, double>& reports2)
{
	reports[string("XAUUSD")] = 1;
	reports2[string("XAGUSD")] = 1;
}
