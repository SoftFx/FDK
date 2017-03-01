#ifndef __FixProvider_Fix_Dialect__
#define __FixProvider_Fix_Dialect__


#define FIX_ORDER_TYPE_MARKET 0
#define FIX_ORDER_TYPE_POSITION 1
#define FIX_ORDER_TYPE_LIMIT 2
#define FIX_ORDER_TYPE_STOP 3


#define FIX_ORDER_STATUS_NEW 0x0
#define FIX_ORDER_STATUS_PARTIALLY_FILLED 0x1
#define FIX_ORDER_STATUS_FILLED 0x2
#define FIX_ORDER_STATUS_DONE 0x3
#define FIX_ORDER_STATUS_CANCELLED 0x4
#define FIX_ORDER_STATUS_PENDING_CANCEL 0x6
#define FIX_ORDER_STATUS_REJECTED 0x8
#define FIX_ORDER_STATUS_CALCULATED 0xB
#define FIX_ORDER_STATUS_EXPIRED 0xC
#define FIX_ORDER_STATUS_PENDING_REPLACE 0xE

#define FIX_ORDER_SIDE_BUY 1
#define FIX_ORDER_SIDE_SELL 2


#define FIX_EXEC_TYPE_NEW 0
#define FIX_EXEC_TYPE_CANCELED 1
#define FIX_EXEC_TYPE_PENDING_CANCEL 2
#define FIX_EXEC_TYPE_REJECTED 3
#define FIX_EXEC_TYPE_CALCULATED 4
#define FIX_EXEC_TYPE_EXPIRED 5
#define FIX_EXEC_TYPE_PENDING_REPLACE 6
#define FIX_EXEC_TYPE_TRADE 7
#define FIX_EXEC_TYPE_ORDER_STATUS 8





#define FIX_ORDER_REJECT_REASON_UNKNOWN_SYMBOL 1
#define FIX_ORDER_REJECT_REASON_ORDER_EXCEEDS_LIMITS 3
#define FIX_ORDER_REJECT_REASON_UNKNOWN_ORDER 5
#define FIX_ORDER_REJECT_REASON_DUPLICATE_ORDER 6
#define FIX_ORDER_REJECT_REASON_UNSUPPORTED_ORDER_CHARACTERISTICS 11
#define FIX_ORDER_REJECT_REASON_INCORRECT_QUANTITY 13
#define FIX_ORDER_REJECT_REASON_OTHER 99




class CFxFixExecutionReport : public CFxHandle
{
public:
    CFxFixExecutionReport(const FIX44::ExecutionReport& message);
public:
    double ExecutedVolume;
    double InitialVolume;
    double LeavesVolume;
    double TradeAmount;
    double AveragePrice;
    double Price;
    double StopPrice;
    double TakeProfit;
    double StopLoss;
    CDateTime Expiration;

    std::string OrderId;
    std::string ClientOrderId;
    std::string Symbol;
    std::string Text;

    int32 ExecutionType;
    int32 OrderStatus;
    int32 OrderType;
    int32 OrderSide;
    int32 OrderRejectReason;
};






#endif