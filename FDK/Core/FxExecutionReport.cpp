#include "stdafx.h"
#include "FxExecutionReport.h"

CFxExecutionReport::CFxExecutionReport()
{
	ExecutedVolume = numeric_limits<double>::quiet_NaN();
	InitialVolume = numeric_limits<double>::quiet_NaN();
	LeavesVolume = numeric_limits<double>::quiet_NaN();
	TradeAmount = numeric_limits<double>::quiet_NaN();
	AveragePrice = numeric_limits<double>::quiet_NaN();
	Price = numeric_limits<double>::quiet_NaN();
	TradePrice = numeric_limits<double>::quiet_NaN();
	StopPrice = numeric_limits<double>::quiet_NaN();
	TakeProfit = numeric_limits<double>::quiet_NaN();
	StopLoss = numeric_limits<double>::quiet_NaN();
	Balance =  numeric_limits<double>::quiet_NaN();
	Commission = 0;
	AgentCommission = 0;
	Swap = 0;//numeric_limits<double>::quiet_NaN();

	ExecutionType = FxExecutionType_None;
	OrderStatus = FxOrderStatus_None;
	OrderType = FxOrderType_None;

	ReportsNumber = 0;
	RejectReason = FxRejectReason_None;
	OrderSide = FxTradeRecordSide_None;
}

bool CFxExecutionReport::TryGetTradeRecord(CFxOrder& order) const
{
	if (TryGetPosition(order))
	{
		return true;
	}
	if (TryGetLimitOrder(order))
	{
		return true;
	}
	if (TryGetStopOrder(order))
	{
		return true;
	}
	return false;
}

bool CFxExecutionReport::TryGetPosition(CFxOrder& order) const
{
	if (FxOrderType_Position == OrderType)
	{
		return TryGetPositionFromPosition(order);
	}
	else if (FxOrderType_Market == OrderType)
	{
		return TryGetPositionFromMarket(order);
	}
	return false;
}

bool CFxExecutionReport::TryGetPositionFromMarket(CFxOrder& order) const
{
	if (FxOrderStatus_Filled != OrderStatus)
	{
		return false;
	}
	if (FxExecutionType_Trade != ExecutionType)
	{
		return false;
	}
	order.Type = FxTradeRecordType_Position;

	CopyCommonFieldsToRecord(order);
	order.Volume = *this->TradeAmount;
	order.Price = this->TradePrice;

	return CopyCreatedAndModifiedDateTime(order);
}

bool CFxExecutionReport::TryGetPositionFromPosition(CFxOrder& order) const
{
	if (FxOrderStatus_Calculated != OrderStatus)
	{
		return false;
	}
	if ((FxExecutionType_Calculated != ExecutionType) && (FxExecutionType_OrderStatus != ExecutionType) && (FxExecutionType_Replace != ExecutionType))
	{
		return false;
	}
	order.Type = FxTradeRecordType_Position;

	CopyCommonFieldsToRecord(order);
	order.Price = *(this->Price);

	return CopyCreatedAndModifiedDateTime(order);
}

bool CFxExecutionReport::TryGetLimitOrder(CFxOrder& order) const
{
	if (FxOrderType_Limit != OrderType)
	{
		return false;
	}
	if(FxOrderStatus_Calculated != OrderStatus)
	{
		return false;
	}
	if ((FxExecutionType_Calculated != ExecutionType) && (FxExecutionType_OrderStatus != ExecutionType) && (FxExecutionType_Replace != ExecutionType))
	{
		return false;
	}

	order.Type = FxTradeRecordType_Limit;
	order.Price = *(this->Price);

	CopyCommonFieldsToRecord(order);

	return CopyCreatedAndModifiedDateTime(order);
}
bool CFxExecutionReport::TryGetStopOrder(CFxOrder& order) const
{
	if (FxOrderType_Stop != OrderType)
	{
		return false;
	}
	if(FxOrderStatus_Calculated != OrderStatus)
	{
		return false;
	}
	if ((FxExecutionType_Calculated != ExecutionType) && (FxExecutionType_OrderStatus != ExecutionType) && (FxExecutionType_Replace != ExecutionType))
	{
		return false;
	}

	order.Type = FxTradeRecordType_Stop;
	order.Price = *(this->StopPrice);

	CopyCommonFieldsToRecord(order);


	return CopyCreatedAndModifiedDateTime(order);
}
void CFxExecutionReport::CopyCommonFieldsToRecord(CFxOrder& order) const
{
	order.Side = this->OrderSide;
	order.Volume = this->LeavesVolume;
	order.StopLoss = this->StopLoss;
	order.TakeProfit = this->TakeProfit;
	order.OrderId = this->OrderId;
	order.ClientOrderId = this->ClientOrderId;
	order.Symbol = this->Symbol;
	order.Commission = this->Commission;
	order.AgentCommission = this->AgentCommission;
	order.Swap = this->Swap;
	order.Expiration = this->Expiration;
	order.Comment = this->Comment;
	if (this->InitialVolume.HasValue())
	{
		order.InitialVolume = this->InitialVolume.Value();
	}
}

bool CFxExecutionReport::IsReject() const
{
	const bool result = ((FxOrderStatus_Rejected == OrderStatus) || (FxExecutionType_Rejected == ExecutionType));
	return result;
}

bool CFxExecutionReport::IsExpired()const
{
	const bool result = ((FxOrderStatus_Expired == OrderStatus) || (FxExecutionType_Expired == ExecutionType));
	return result;
}

bool CFxExecutionReport::IsCanceled()const
{
	const bool result = ((FxOrderStatus_Canceled == OrderStatus) || (FxExecutionType_Canceled == ExecutionType));
	return result;
}

void CFxExecutionReport::Reject()
{
	this->OrderStatus = FxOrderStatus_Rejected;
	this->ExecutionType = FxExecutionType_Rejected;
}

const string& CFxExecutionReport::GetOrderId()const
{
	return OrderId;
}

bool CFxExecutionReport::CopyCreatedAndModifiedDateTime(CFxOrder& order) const
{
	order.Created = this->Created;
	order.Modified = this->Modified;
	return true;
}

bool CFxExecutionReport::TryGetClosedPosition(string& orderId, double& leavesVolume, double& commission, double& agentCommission, double& swap) const
{
	if ( !( (FxOrderType_Limit != OrderType || FxOrderType_Stop != OrderType) && (FxExecutionType_Trade == ExecutionType)) )
	{
		return false;
	}

    commission = this->Commission;
    agentCommission = this->AgentCommission;
    swap = this->Swap;

	if ((FxOrderStatus_Calculated == OrderStatus) || (FxOrderStatus_Filled == OrderStatus))
	{
		orderId = this->OrderId;
		leavesVolume = this->LeavesVolume;
		return true;
	}
	return false;
}

bool CFxExecutionReport::TryGetDeletedOrder(string& orderId) const
{
	if ((FxOrderStatus_Expired == OrderStatus) && (FxExecutionType_Expired == ExecutionType))
	{
		if ((FxOrderType_Limit == OrderType) || (FxOrderType_Stop == OrderType))
		{
			orderId = GetOrderId();
			return true;
		}
	}
	if ((FxOrderStatus_Canceled == OrderStatus) && (FxExecutionType_Canceled == ExecutionType))
	{
		if ((FxOrderType_Limit == OrderType) || (FxOrderType_Stop == OrderType) || (FxOrderType_Position == OrderType))
		{
			orderId = GetOrderId();
			return true;
		}
	}
	return false;
}

bool CFxExecutionReport::TryGetActivatedOrder(string& orderId) const
{
    if ((FxOrderStatus_Filled == OrderStatus) && (FxExecutionType_Trade == ExecutionType))
	{
		if ((FxOrderType_Limit == OrderType) || (FxOrderType_Stop == OrderType))
		{
			orderId = GetOrderId();
			return true;
		}
	}
	return false;
}

