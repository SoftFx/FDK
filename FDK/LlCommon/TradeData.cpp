#include "stdafx.h"
#include "TradeData.h"

namespace FDK
{
	CTradeData::CTradeData()
        : Type(TradeType_None)
        , Side(TradeSide_None)
        , Price()
        , Volume()
        , Commission()
        , AgentCommission()
        , Swap()
        , Rate()
        , ProfitStatus(TradeEntryStatus_NotCalculated)
        , MarginStatus(TradeEntryStatus_NotCalculated)
	{
	}

	CTradeData::CTradeData(const CTradeEntry& entry)
	{
		stringstream stream;
		stream << entry.Tag;
		Tag = stream.str();

		Type = entry.GetType();
		Side = entry.GetSide();
		Symbol = entry.GetSymbol();
		Price = entry.GetPrice();
		Volume = entry.GetVolume();
        Commission = entry.GetCommission();
        AgentCommission = entry.GetAgentCommission();
        Swap = entry.GetSwap();
		Rate = entry.GetStaitcMarinRate();
		ProfitStatus = entry.GetProfitStatus();
		MarginStatus = entry.GetMarginStatus();
		Profit = entry.GetProfit();
		Margin = entry.GetMargin();
	}

	CTradeEntry CTradeData::ToTradeEntry() const
	{
		CTradeEntry result(Type, Side, Symbol, Price, Volume, Rate);
		result.Tag = Tag;
        result.m_commission = Commission;
        result.m_agentCommission = AgentCommission;
        result.m_swap = Swap;
		result.m_profitStatus = ProfitStatus;
		result.m_marginStatus = MarginStatus;
		result.m_profit = Profit;
		result.m_margin = Margin;
		return result;
	}
}