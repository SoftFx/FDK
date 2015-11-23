#include "stdafx.h"
#include "PriceData.h"

namespace FDK
{
	CPriceData::CPriceData()
        : Bid()
        , Ask()
	{
	}

	CPriceData::CPriceData(const std::string& symbol, double bid, double ask)
        : Symbol(symbol)
        , Bid(bid)
        , Ask(ask)
	{
	}
}