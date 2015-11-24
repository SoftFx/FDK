#include "stdafx.h"
#include "FixDialect.h"


const double cNAN = numeric_limits<double>::quiet_NaN();

namespace
{
	int32 GetExecutionType(const FIX44::ExecutionReport& message)
	{
		const char type = message.GetExecType();
		if (FIX::ExecType_NEW == type)
		{
			return FIX_EXEC_TYPE_NEW;
		}
		if (FIX::ExecType_CANCELED == type)
		{
			return FIX_EXEC_TYPE_CANCELED;
		}
		if (FIX::ExecType_PENDING_CANCEL == type)
		{
			return FIX_EXEC_TYPE_PENDING_CANCEL;
		}
		if (FIX::ExecType_REJECTED == type)
		{
			return FIX_EXEC_TYPE_REJECTED;
		}
		if (FIX::ExecType_CALCULATED == type)
		{
			return FIX_EXEC_TYPE_CALCULATED ;
		}
		if (FIX::ExecType_EXPIRED == type)
		{
			return FIX_EXEC_TYPE_EXPIRED;
		}
		if (FIX::ExecType_PENDING_REPLACE == type)
		{
			return FIX_EXEC_TYPE_PENDING_REPLACE;
		}
		if (FIX::ExecType_TRADE == type)
		{
			return FIX_EXEC_TYPE_TRADE;
		}
		if (FIX::ExecType_ORDER_STATUS == type)
		{
			return FIX_EXEC_TYPE_ORDER_STATUS;
		}
		return -1;
	}
	int32 GetOrderStatus(const FIX44::ExecutionReport& message)
	{
		const char status = message.GetOrdStatus();

		if (FIX::OrdStatus_NEW == status)
		{
			return FIX_ORDER_STATUS_NEW;
		}
		if (FIX::OrdStatus_PARTIALLY_FILLED == status)
		{
			return FIX_ORDER_STATUS_PARTIALLY_FILLED;
		}
		if (FIX::OrdStatus_FILLED == status)
		{
			return FIX_ORDER_STATUS_FILLED;
		}
		if (FIX::OrdStatus_CANCELED == status)
		{
			return FIX_ORDER_STATUS_CANCELLED;
		}
		if (FIX::OrdStatus_PENDING_CANCEL == status)
		{
			return FIX_ORDER_STATUS_PENDING_CANCEL;
		}
		if (FIX::OrdStatus_REJECTED == status)
		{
			return FIX_ORDER_STATUS_REJECTED;
		}
		if (FIX::OrdStatus_CALCULATED == status)
		{
			return FIX_ORDER_STATUS_CALCULATED;
		}
		if (FIX::OrdStatus_EXPIRED == status)
		{
			return FIX_ORDER_STATUS_EXPIRED;
		}
		if (FIX::OrdStatus_PENDING_REPLACE == status)
		{
			return FIX_ORDER_STATUS_PENDING_REPLACE;
		}
		return -1;
	}
	int32 GetOrderType(const FIX44::ExecutionReport& message)
	{
		char type = 0;
		if (!message.TryGetOrdType(type))
		{
			return -1;
		}
		if (FIX::OrdType_MARKET == type)
		{
			return FIX_ORDER_TYPE_MARKET;
		}
		if ('N' == type)
		{
			return FIX_ORDER_TYPE_POSITION;
		}
		if (FIX::OrdType_LIMIT == type)
		{
			return FIX_ORDER_TYPE_LIMIT;
		}
		if (FIX::OrdType_STOP == type)
		{
			return FIX_ORDER_TYPE_STOP;
		}
		return -1;
	}
	int32 GetOrderSide(const FIX44::ExecutionReport& message)
	{
		char side = 0;
		if (!message.TryGetSide(side))
		{
			return -1;
		}
		if (FIX::Side_BUY == side)
		{
			return FIX_ORDER_SIDE_BUY;
		}
		if (FIX::Side_SELL == side)
		{
			return FIX_ORDER_SIDE_SELL;
		}
		return -1;
	}
	int32 GetOrderRejectReason(const FIX44::ExecutionReport& message)
	{
		int reason = 0;
		if (!message.TryGetOrdRejReason(reason))
		{
			return -1;
		}
		if (FIX::OrdRejReason_UNKNOWN_SYMBOL == reason)
		{
			return FIX_ORDER_REJECT_REASON_UNKNOWN_SYMBOL;
		}
		if (FIX::OrdRejReason_ORDER_EXCEEDS_LIMIT == reason)
		{
			return FIX_ORDER_REJECT_REASON_ORDER_EXCEEDS_LIMITS;
		}
		if (FIX::OrdRejReason_UNKNOWN_ORDER == reason)
		{
			return FIX_ORDER_REJECT_REASON_UNKNOWN_ORDER;
		}
		if (FIX::OrdRejReason_DUPLICATE_ORDER == reason)
		{
			return FIX_ORDER_REJECT_REASON_DUPLICATE_ORDER;
		}
		if (FIX::OrdRejReason_UNSUPPORTED_ORDER_CHARACTERISTIC == reason)
		{
			return FIX_ORDER_REJECT_REASON_UNSUPPORTED_ORDER_CHARACTERISTICS;
		}
		if (FIX::OrdRejReason_INCORRECT_QUANTITY == reason)
		{
			return FIX_ORDER_REJECT_REASON_INCORRECT_QUANTITY;
		}
		if (FIX::OrdRejReason_OTHER == reason)
		{
			return FIX_ORDER_REJECT_REASON_OTHER;
		}
		return -1;
	}
}





CFxFixExecutionReport::CFxFixExecutionReport(const FIX44::ExecutionReport& message) :
	ExecutedVolume(cNAN), InitialVolume(cNAN), LeavesVolume(cNAN), TradeAmount(cNAN), AveragePrice(cNAN),
	Price(cNAN), StopPrice(cNAN), TakeProfit(cNAN), StopLoss(cNAN), Expiration(),
	OrderId(), ClientOrderId(), Symbol(), Text(),
	ExecutionType(), OrderStatus(), OrderType(), OrderSide(), OrderRejectReason()
{
	ExecutedVolume = message.GetCumQty();
	message.TryGetOrderQty(InitialVolume);
	LeavesVolume = message.GetLeavesQty();
	message.TryGetLastQty(TradeAmount);
	AveragePrice = message.GetAvgPx();
	message.TryGetPrice(Price);
	message.TryGetStopPx(StopPrice);
	message.TryGetTakeProfit(TakeProfit);
	message.TryGetStopLoss(StopLoss);

	FIX::UtcTimeStamp time((time_t)(0));
	if (message.TryGetExpireTime(time))
	{
		Expiration = time.toFileTime();
	}
	this->OrderId = message.GetOrderID();


	message.TryGetClOrdID(this->ClientOrderId);
	message.TryGetSymbol(this->Symbol);
	message.TryGetText(this->Text);
	
	ExecutionType = GetExecutionType(message);
	OrderStatus = GetOrderStatus(message);
	OrderType = GetOrderType(message);
	OrderSide = GetOrderSide(message);
	OrderRejectReason = GetOrderRejectReason(message);
}
