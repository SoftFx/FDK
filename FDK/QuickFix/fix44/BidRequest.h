#ifndef FIX44_BIDREQUEST_H
#define FIX44_BIDREQUEST_H

#include "Message.h"

namespace FIX44
{

  class BidRequest : public Message
  {
  public:
    BidRequest() : Message(MsgType()) {}
    BidRequest(const FIX::Message& m) : Message(m) {}
    BidRequest(const Message& m) : Message(m) {}
    BidRequest(const BidRequest& m) : Message(m) {}
    static FIX::MsgType MsgType() { return FIX::MsgType("k"); }

    BidRequest(
      const FIX::ClientBidID& aClientBidID,
      const FIX::BidRequestTransType& aBidRequestTransType,
      const FIX::TotNoRelatedSym& aTotNoRelatedSym,
      const FIX::BidType& aBidType,
      const FIX::BidTradeType& aBidTradeType,
      const FIX::BasisPxType& aBasisPxType )
    : Message(MsgType())
    {
      set(aClientBidID);
      set(aBidRequestTransType);
      set(aTotNoRelatedSym);
      set(aBidType);
      set(aBidTradeType);
      set(aBasisPxType);
    }

    FIELD_SET(*this, FIX::BidID);
    FIELD_SET_EX(std::string, BidID);
    FIELD_SET(*this, FIX::ClientBidID);
    FIELD_SET_EX(std::string, ClientBidID);
    FIELD_SET(*this, FIX::BidRequestTransType);
    FIELD_SET_EX(char, BidRequestTransType);
    FIELD_SET(*this, FIX::ListName);
    FIELD_SET_EX(std::string, ListName);
    FIELD_SET(*this, FIX::TotNoRelatedSym);
    FIELD_SET_EX(int, TotNoRelatedSym);
    FIELD_SET(*this, FIX::BidType);
    FIELD_SET_EX(int, BidType);
    FIELD_SET(*this, FIX::NumTickets);
    FIELD_SET_EX(int, NumTickets);
    FIELD_SET(*this, FIX::Currency);
    FIELD_SET_EX(std::string, Currency);
    FIELD_SET(*this, FIX::SideValue1);
    FIELD_SET_EX(double, SideValue1);
    FIELD_SET(*this, FIX::SideValue2);
    FIELD_SET_EX(double, SideValue2);
    FIELD_SET(*this, FIX::LiquidityIndType);
    FIELD_SET_EX(int, LiquidityIndType);
    FIELD_SET(*this, FIX::WtAverageLiquidity);
    FIELD_SET_EX(double, WtAverageLiquidity);
    FIELD_SET(*this, FIX::ExchangeForPhysical);
    FIELD_SET_EX(bool, ExchangeForPhysical);
    FIELD_SET(*this, FIX::OutMainCntryUIndex);
    FIELD_SET_EX(double, OutMainCntryUIndex);
    FIELD_SET(*this, FIX::CrossPercent);
    FIELD_SET_EX(double, CrossPercent);
    FIELD_SET(*this, FIX::ProgRptReqs);
    FIELD_SET_EX(int, ProgRptReqs);
    FIELD_SET(*this, FIX::ProgPeriodInterval);
    FIELD_SET_EX(int, ProgPeriodInterval);
    FIELD_SET(*this, FIX::IncTaxInd);
    FIELD_SET_EX(int, IncTaxInd);
    FIELD_SET(*this, FIX::ForexReq);
    FIELD_SET_EX(bool, ForexReq);
    FIELD_SET(*this, FIX::NumBidders);
    FIELD_SET_EX(int, NumBidders);
    FIELD_SET(*this, FIX::TradeDate);
    FIELD_SET_EX(std::string, TradeDate);
    FIELD_SET(*this, FIX::BidTradeType);
    FIELD_SET_EX(char, BidTradeType);
    FIELD_SET(*this, FIX::BasisPxType);
    FIELD_SET_EX(char, BasisPxType);
    FIELD_SET(*this, FIX::StrikeTime);
    FIELD_SET_EX(FIX::UtcTimeStamp, StrikeTime);
    FIELD_SET(*this, FIX::Text);
    FIELD_SET_EX(std::string, Text);
    FIELD_SET(*this, FIX::EncodedTextLen);
    FIELD_SET_EX(int, EncodedTextLen);
    FIELD_SET(*this, FIX::EncodedText);
    FIELD_SET_EX(std::string, EncodedText);
    FIELD_SET(*this, FIX::NoBidDescriptors);
    FIELD_SET_EX(int, NoBidDescriptors);
    class NoBidDescriptors: public FIX::Group
    {
    public:
    NoBidDescriptors() : FIX::Group(398,399,FIX::message_order(399,400,401,404,441,402,403,405,406,407,408,0)) {}
      FIELD_SET(*this, FIX::BidDescriptorType);
      FIELD_SET_EX(int, BidDescriptorType);
      FIELD_SET(*this, FIX::BidDescriptor);
      FIELD_SET_EX(std::string, BidDescriptor);
      FIELD_SET(*this, FIX::SideValueInd);
      FIELD_SET_EX(int, SideValueInd);
      FIELD_SET(*this, FIX::LiquidityValue);
      FIELD_SET_EX(double, LiquidityValue);
      FIELD_SET(*this, FIX::LiquidityNumSecurities);
      FIELD_SET_EX(int, LiquidityNumSecurities);
      FIELD_SET(*this, FIX::LiquidityPctLow);
      FIELD_SET_EX(double, LiquidityPctLow);
      FIELD_SET(*this, FIX::LiquidityPctHigh);
      FIELD_SET_EX(double, LiquidityPctHigh);
      FIELD_SET(*this, FIX::EFPTrackingError);
      FIELD_SET_EX(double, EFPTrackingError);
      FIELD_SET(*this, FIX::FairValue);
      FIELD_SET_EX(double, FairValue);
      FIELD_SET(*this, FIX::OutsideIndexPct);
      FIELD_SET_EX(double, OutsideIndexPct);
      FIELD_SET(*this, FIX::ValueOfFutures);
      FIELD_SET_EX(double, ValueOfFutures);
    };
    FIELD_SET(*this, FIX::NoBidComponents);
    FIELD_SET_EX(int, NoBidComponents);
    class NoBidComponents: public FIX::Group
    {
    public:
    NoBidComponents() : FIX::Group(420,66,FIX::message_order(66,54,336,625,430,63,64,1,660,0)) {}
      FIELD_SET(*this, FIX::ListID);
      FIELD_SET_EX(std::string, ListID);
      FIELD_SET(*this, FIX::Side);
      FIELD_SET_EX(char, Side);
      FIELD_SET(*this, FIX::TradingSessionID);
      FIELD_SET_EX(std::string, TradingSessionID);
      FIELD_SET(*this, FIX::TradingSessionSubID);
      FIELD_SET_EX(std::string, TradingSessionSubID);
      FIELD_SET(*this, FIX::NetGrossInd);
      FIELD_SET_EX(int, NetGrossInd);
      FIELD_SET(*this, FIX::SettlType);
      FIELD_SET_EX(std::string, SettlType);
      FIELD_SET(*this, FIX::SettlDate);
      FIELD_SET_EX(std::string, SettlDate);
      FIELD_SET(*this, FIX::Account);
      FIELD_SET_EX(std::string, Account);
      FIELD_SET(*this, FIX::AcctIDSource);
      FIELD_SET_EX(int, AcctIDSource);
    };
  };

}

#endif
