#pragma once
#include "TradeEntry.h"


namespace FDK
{
    class CTradeData
    {
    public:
        CTradeData();
        CTradeData(const CTradeEntry& entry);
    public:
        string Tag;
        TradeType Type;
        TradeSide Side;
        string Symbol;
        double Volume;
        Nullable<double> Price;
        Nullable<double> StopPrice;
        double Commission;
        double AgentCommission;
        double Swap;
        Nullable<double> Rate;
        TradeEntryStatus ProfitStatus;
        TradeEntryStatus MarginStatus;
        Nullable<double> Profit;
        Nullable<double> Margin;
    public:
        CTradeEntry ToTradeEntry() const;
    };
}