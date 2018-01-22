#include "stdafx.h"
#include "FxTradeTransactionReport.h"

CFxTradeTransactionReport::CFxTradeTransactionReport()
{
    this->TradeTransactionReportType = FxTradeTransactionReportType_None;
    this->TradeTransactionReason = FxTradeTransactionReason_None;
    this->InitialTradeRecordType = FxOrderType_None;
    this->TradeRecordType = FxOrderType_None;
    this->TradeRecordSide = FxTradeRecordSide_None;

    this->AccountBalance = 0;
    this->TransactionAmount = 0;

    this->Magic = 0;
    this->Quantity = 0;
    this->LeavesQuantity = 0;
    this->Price = 0;
    this->StopPrice = 0;

    this->IsReducedOpenCommission = false;
    this->IsReducedCloseCommission = false;
    this->ImmediateOrCancel = false;
    this->MarketWithSlippage = false;

    this->PosOpenReqPrice = 0;
    this->PosOpenPrice = 0;
    this->PositionQuantity = 0;
    this->PositionLastQuantity = 0;
    this->PositionLeavesQuantity = 0;
    this->PositionCloseRequestedPrice = 0;
    this->PositionClosePrice = 0;

    this->Commission = 0;
    this->AgentCommission = 0;
    this->Swap = 0;

    this->StopLoss = 0;
    this->TakeProfit = 0;

    this->ActionId = 0;
    this->PosRemainingSide = FxTradeRecordSide_None;
}
