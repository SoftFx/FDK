#ifndef FIX44_BIDRESPONSE_H
#define FIX44_BIDRESPONSE_H

#include "Message.h"

namespace FIX44
{

  class BidResponse : public Message
  {
  public:
    BidResponse() : Message(MsgType()) {}
    BidResponse(const FIX::Message& m) : Message(m) {}
    BidResponse(const Message& m) : Message(m) {}
    BidResponse(const BidResponse& m) : Message(m) {}
    static FIX::MsgType MsgType() { return FIX::MsgType("l"); }

    FIELD_SET(*this, FIX::BidID);
    FIELD_SET_EX(std::string, BidID);
    FIELD_SET(*this, FIX::ClientBidID);
    FIELD_SET_EX(std::string, ClientBidID);
    FIELD_SET(*this, FIX::NoBidComponents);
    FIELD_SET_EX(int, NoBidComponents);
    class NoBidComponents: public FIX::Group
    {
    public:
    NoBidComponents() : FIX::Group(420,12,FIX::message_order(12,13,479,497,66,421,54,44,423,406,430,63,64,336,625,58,354,355,0)) {}
      FIELD_SET(*this, FIX::Commission);
      FIELD_SET_EX(double, Commission);
      FIELD_SET(*this, FIX::CommType);
      FIELD_SET_EX(char, CommType);
      FIELD_SET(*this, FIX::CommCurrency);
      FIELD_SET_EX(std::string, CommCurrency);
      FIELD_SET(*this, FIX::FundRenewWaiv);
      FIELD_SET_EX(char, FundRenewWaiv);
      FIELD_SET(*this, FIX::ListID);
      FIELD_SET_EX(std::string, ListID);
      FIELD_SET(*this, FIX::Country);
      FIELD_SET(*this, FIX::Side);
      FIELD_SET_EX(char, Side);
      FIELD_SET(*this, FIX::Price);
      FIELD_SET_EX(double, Price);
      FIELD_SET(*this, FIX::PriceType);
      FIELD_SET_EX(int, PriceType);
      FIELD_SET(*this, FIX::FairValue);
      FIELD_SET_EX(double, FairValue);
      FIELD_SET(*this, FIX::NetGrossInd);
      FIELD_SET_EX(int, NetGrossInd);
      FIELD_SET(*this, FIX::SettlType);
      FIELD_SET_EX(std::string, SettlType);
      FIELD_SET(*this, FIX::SettlDate);
      FIELD_SET_EX(std::string, SettlDate);
      FIELD_SET(*this, FIX::TradingSessionID);
      FIELD_SET_EX(std::string, TradingSessionID);
      FIELD_SET(*this, FIX::TradingSessionSubID);
      FIELD_SET_EX(std::string, TradingSessionSubID);
      FIELD_SET(*this, FIX::Text);
      FIELD_SET_EX(std::string, Text);
      FIELD_SET(*this, FIX::EncodedTextLen);
      FIELD_SET_EX(int, EncodedTextLen);
      FIELD_SET(*this, FIX::EncodedText);
      FIELD_SET_EX(std::string, EncodedText);
    };
  };

}

#endif
