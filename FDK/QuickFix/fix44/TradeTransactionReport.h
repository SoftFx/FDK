#ifndef FIX44_TRADETRANSACTIONREPORT_H
#define FIX44_TRADETRANSACTIONREPORT_H

#include "Message.h"

namespace FIX44
{

  class TradeTransactionReport : public Message
  {
  public:
    TradeTransactionReport() : Message(MsgType()) {}
    TradeTransactionReport(const FIX::Message& m) : Message(m) {}
    TradeTransactionReport(const Message& m) : Message(m) {}
    TradeTransactionReport(const TradeTransactionReport& m) : Message(m) {}
    static FIX::MsgType MsgType() { return FIX::MsgType("U1017"); }

    TradeTransactionReport(
      const FIX::TradeReportID& aTradeReportID )
    : Message(MsgType())
    {
      set(aTradeReportID);
    }

    FIELD_SET(*this, FIX::TradeReportID);
    FIELD_SET_EX(std::string, TradeReportID);
    FIELD_SET(*this, FIX::TradeRequestID);
    FIELD_SET_EX(std::string, TradeRequestID);
    FIELD_SET(*this, FIX::TradeTransReportType);
    FIELD_SET_EX(int, TradeTransReportType);
    FIELD_SET(*this, FIX::TradeTransReason);
    FIELD_SET_EX(int, TradeTransReason);
    FIELD_SET(*this, FIX::TransactTime);
    FIELD_SET_EX(FIX::UtcTimeStamp, TransactTime);
    FIELD_SET(*this, FIX::AccBalance);
    FIELD_SET_EX(double, AccBalance);
    FIELD_SET(*this, FIX::AccTrAmount);
    FIELD_SET_EX(double, AccTrAmount);
    FIELD_SET(*this, FIX::AccTrCurry);
    FIELD_SET_EX(std::string, AccTrCurry);
    FIELD_SET(*this, FIX::OrderID);
    FIELD_SET_EX(std::string, OrderID);
    FIELD_SET(*this, FIX::ActionID);
    FIELD_SET_EX(int, ActionID);
    FIELD_SET(*this, FIX::ClOrdID);
    FIELD_SET_EX(std::string, ClOrdID);
    FIELD_SET(*this, FIX::OrderQty);
    FIELD_SET_EX(double, OrderQty);
    FIELD_SET(*this, FIX::OrderLeavesQty);
    FIELD_SET_EX(double, OrderLeavesQty);
    FIELD_SET(*this, FIX::OrderPrice);
    FIELD_SET_EX(double, OrderPrice);
    FIELD_SET(*this, FIX::OrderStopPx);
    FIELD_SET_EX(double, OrderStopPx);
    FIELD_SET(*this, FIX::OrdType);
    FIELD_SET_EX(char, OrdType);
    FIELD_SET(*this, FIX::Side);
    FIELD_SET_EX(char, Side);
    FIELD_SET(*this, FIX::Symbol);
    FIELD_SET_EX(std::string, Symbol);
    FIELD_SET(*this, FIX::OrdCreated);
    FIELD_SET_EX(FIX::UtcTimeStamp, OrdCreated);
    FIELD_SET(*this, FIX::OrdModified);
    FIELD_SET_EX(FIX::UtcTimeStamp, OrdModified);
    FIELD_SET(*this, FIX::PosID);
    FIELD_SET_EX(std::string, PosID);
    FIELD_SET(*this, FIX::PosByID);
    FIELD_SET_EX(std::string, PosByID);
    FIELD_SET(*this, FIX::PosOpened);
    FIELD_SET_EX(FIX::UtcTimeStamp, PosOpened);
    FIELD_SET(*this, FIX::PosOpenReqPrice);
    FIELD_SET_EX(double, PosOpenReqPrice);
    FIELD_SET(*this, FIX::PosOpenPrice);
    FIELD_SET_EX(double, PosOpenPrice);
    FIELD_SET(*this, FIX::PosQty);
    FIELD_SET_EX(double, PosQty);
    FIELD_SET(*this, FIX::PosLastQty);
    FIELD_SET_EX(double, PosLastQty);
    FIELD_SET(*this, FIX::PosLeavesQty);
    FIELD_SET_EX(double, PosLeavesQty);
    FIELD_SET(*this, FIX::PosCloseReqPrice);
    FIELD_SET_EX(double, PosCloseReqPrice);
    FIELD_SET(*this, FIX::PosClosePrice);
    FIELD_SET_EX(double, PosClosePrice);
    FIELD_SET(*this, FIX::PosClosed);
    FIELD_SET_EX(FIX::UtcTimeStamp, PosClosed);
    FIELD_SET(*this, FIX::PosModified);
    FIELD_SET_EX(FIX::UtcTimeStamp, PosModified);
    FIELD_SET(*this, FIX::PosRemainingSide);
    FIELD_SET_EX(char, PosRemainingSide);
    FIELD_SET(*this, FIX::PosRemainingPrice);
    FIELD_SET_EX(double, PosRemainingPrice);
    FIELD_SET(*this, FIX::Commission);
    FIELD_SET_EX(double, Commission);
    FIELD_SET(*this, FIX::CommType);
    FIELD_SET_EX(char, CommType);
    FIELD_SET(*this, FIX::CommCurrency);
    FIELD_SET_EX(std::string, CommCurrency);
    FIELD_SET(*this, FIX::FundRenewWaiv);
    FIELD_SET_EX(char, FundRenewWaiv);
    FIELD_SET(*this, FIX::AgentCommission);
    FIELD_SET_EX(double, AgentCommission);
    FIELD_SET(*this, FIX::AgentCommType);
    FIELD_SET_EX(char, AgentCommType);
    FIELD_SET(*this, FIX::AgentCommCurrency);
    FIELD_SET_EX(std::string, AgentCommCurrency);
    FIELD_SET(*this, FIX::Swap);
    FIELD_SET_EX(double, Swap);
    FIELD_SET(*this, FIX::StopLoss);
    FIELD_SET_EX(double, StopLoss);
    FIELD_SET(*this, FIX::TakeProfit);
    FIELD_SET_EX(double, TakeProfit);
    FIELD_SET(*this, FIX::EncodedCommentLen);
    FIELD_SET_EX(int, EncodedCommentLen);
    FIELD_SET(*this, FIX::EncodedComment);
    FIELD_SET_EX(std::string, EncodedComment);
    FIELD_SET(*this, FIX::EncodedTagLen);
    FIELD_SET_EX(int, EncodedTagLen);
    FIELD_SET(*this, FIX::EncodedTag);
    FIELD_SET_EX(std::string, EncodedTag);
    FIELD_SET(*this, FIX::Magic);
    FIELD_SET_EX(int, Magic);
    FIELD_SET(*this, FIX::MarginRateInitial);
    FIELD_SET_EX(double, MarginRateInitial);
    FIELD_SET(*this, FIX::LastQty);
    FIELD_SET_EX(double, LastQty);
    FIELD_SET(*this, FIX::LastPx);
    FIELD_SET_EX(double, LastPx);
    FIELD_SET(*this, FIX::OpenConversionRate);
    FIELD_SET_EX(double, OpenConversionRate);
    FIELD_SET(*this, FIX::CloseConversionRate);
    FIELD_SET_EX(double, CloseConversionRate);
    FIELD_SET(*this, FIX::ParentOrderID);
    FIELD_SET_EX(std::string, ParentOrderID);
    FIELD_SET(*this, FIX::ParentOrderType);
    FIELD_SET_EX(char, ParentOrderType);
    FIELD_SET(*this, FIX::ExpireDate);
    FIELD_SET_EX(std::string, ExpireDate);
    FIELD_SET(*this, FIX::ExpireTime);
    FIELD_SET_EX(FIX::UtcTimeStamp, ExpireTime);
    FIELD_SET(*this, FIX::ReqOpenPrice);
    FIELD_SET_EX(double, ReqOpenPrice);
    FIELD_SET(*this, FIX::ReqOpenQty);
    FIELD_SET_EX(double, ReqOpenQty);
    FIELD_SET(*this, FIX::ReqClosePrice);
    FIELD_SET_EX(double, ReqClosePrice);
    FIELD_SET(*this, FIX::ReqCloseQty);
    FIELD_SET_EX(double, ReqCloseQty);
    FIELD_SET(*this, FIX::ImmediateOrCancelFlag);
    FIELD_SET_EX(bool, ImmediateOrCancelFlag);
    FIELD_SET(*this, FIX::MarketWithSlippageFlag);
    FIELD_SET_EX(bool, MarketWithSlippageFlag);
    FIELD_SET(*this, FIX::SrcAssetCurrency);
    FIELD_SET_EX(std::string, SrcAssetCurrency);
    FIELD_SET(*this, FIX::SrcAssetAmount);
    FIELD_SET_EX(double, SrcAssetAmount);
    FIELD_SET(*this, FIX::SrcAssetMovement);
    FIELD_SET_EX(double, SrcAssetMovement);
    FIELD_SET(*this, FIX::DstAssetCurrency);
    FIELD_SET_EX(std::string, DstAssetCurrency);
    FIELD_SET(*this, FIX::DstAssetAmount);
    FIELD_SET_EX(double, DstAssetAmount);
    FIELD_SET(*this, FIX::DstAssetMovement);
    FIELD_SET_EX(double, DstAssetMovement);
  };

}

#endif
