#include "stdafx.h"
#include "FxDataHistoryRequest.h"

CFxDataHistoryRequest::CFxDataHistoryRequest()
{
}

CFxDataHistoryRequest::CFxDataHistoryRequest(const string& symbol)
    : Symbol(symbol)
{
}

CFxDataHistoryRequest::CFxDataHistoryRequest(const string& symbol, const string& period)
    : Symbol(symbol)
    , GraphPeriod(period)
{
}

const string& CFxDataHistoryRequest::GetSymbol()const
{
	return this->Symbol;
}

const string& CFxDataHistoryRequest::GetPeriod()const
{
	return this->GraphPeriod;
}