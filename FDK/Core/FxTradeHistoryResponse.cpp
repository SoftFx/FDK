#include "stdafx.h"
#include "FxTradeHistoryResponse.h"

CFxTradeHistoryResponse::CFxTradeHistoryResponse()
{
	ZeroMemory(this, sizeof(*this));
}

CFxTradeHistoryResponse::CFxTradeHistoryResponse(const CFxTradeHistoryResponse& arg)
{
	memcpy(this, &arg, sizeof(*this));
}

CFxTradeHistoryResponse& CFxTradeHistoryResponse::operator = (const CFxTradeHistoryResponse& arg)
{
	if (this != &arg)
	{
		CFxTradeHistoryResponse temp(arg);
		swap(*this, temp);
	}
	return *this;
}

CFxTradeHistoryResponse::~CFxTradeHistoryResponse()
{
}