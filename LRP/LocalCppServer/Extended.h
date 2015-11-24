#pragma once

#include "InType.h"
#include "InOutType.h"
#include "OutType.h"
#include "ReturnType.h"

class CExtended
{
public:
	CReturnType Do(CInType inArg, CInOutType& inOutArg, COutType& outArg);
	int MarketBuy(const string& symbol, double price, double& volume, double& amount);
	void SpeedTest();
};


CExtended& GetExtended();