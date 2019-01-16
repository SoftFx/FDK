#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)
#pragma warning (disable : 4251)

class CORE_API CFxOrder
{
public:
    CFxOrder();
public:
    string OrderId;
    string ClientOrderId;
    string Symbol;
    FxTradeRecordType InitialType;
    FxTradeRecordType Type;
    FxTradeRecordSide Side;
    double InitialVolume;
    Nullable<double> Volume;
    Nullable<double> MaxVisibleVolume;
    Nullable<double> Price;
    Nullable<double> StopPrice;
    Nullable<double> StopLoss;
    Nullable<double> TakeProfit;
    double Commission;
    double AgentCommission;
    double Swap;
    Nullable<double> Profit;
    Nullable<CDateTime> Expiration;
    Nullable<CDateTime> Created;
    Nullable<CDateTime> Modified;
    wstring Comment;
    wstring Tag;
    Nullable<int> Magic;
    bool IsReducedOpenCommission;
    bool IsReducedCloseCommission;
    bool ImmediateOrCancel;
    bool MarketWithSlippage;
    Nullable<bool> IOCOverride;
    Nullable<bool> IFMOverride;
    Nullable<double> PrevVolume;
    Nullable<double> Slippage;
};


#pragma warning (pop)
