#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)
#pragma warning (disable : 4251)

class CORE_API CFxTradeTransactionReport
{
public:
    CFxTradeTransactionReport();
public:
    FxTradeTransactionReportType TradeTransactionReportType;
    FxTradeTransactionReason TradeTransactionReason;
    double AccountBalance;
    double TransactionAmount;
    std::string TransactionCurrency;
    std::string Id;
    std::string ClientId;
    double Quantity;
    double LeavesQuantity;
    double Price;
    double StopPrice;
    FxOrderType TradeRecordType;
    FxTradeRecordSide TradeRecordSide;
    std::string Symbol;
    std::wstring Comment;
    std::wstring Tag;
    Nullable<int> Magic;
    CDateTime OrderCreated;
    CDateTime OrderModified;

    bool ImmediateOrCancel;
    bool MarketWithSlippage;

    Nullable<double> ReqOpenPrice;
    Nullable<double> ReqOpenQuantity;
    Nullable<double> ReqClosePrice;
    Nullable<double> ReqCloseQuantity;

    std::string PositionId;
    std::string PositionById;
    CDateTime PositionOpened;
    double PosOpenReqPrice;
    double PosOpenPrice;
    double PositionQuantity;
    double PositionLastQuantity;
    double PositionLeavesQuantity;
    double PositionCloseRequestedPrice;
    double PositionClosePrice;
    CDateTime PositionClosed;
    CDateTime PositionModified;
    FxTradeRecordSide PosRemainingSide;
    Nullable<double>  PosRemainingPrice;

    double Commission;
    double AgentCommission;
    double Swap;
    std::string CommCurrency;
    double StopLoss;
    double TakeProfit;

    std::string NextStreamPositionId;

    CDateTime TransactionTime;
    Nullable<double> OrderFillPrice;
    Nullable<double> OrderLastFillAmount;
    Nullable<double> OpenConversionRate;
    Nullable<double> CloseConversionRate;

    Nullable<CDateTime> Expiration;

    int ActionId;

    std::string SrcAssetCurrency;
    Nullable<double> SrcAssetAmount;
    Nullable<double> SrcAssetMovement;
    std::string DstAssetCurrency;
    Nullable<double> DstAssetAmount;
    Nullable<double> DstAssetMovement;
};

#pragma warning (pop)
