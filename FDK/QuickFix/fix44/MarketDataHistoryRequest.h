#ifndef FIX44_MARKETDATAHISTORYREQUEST_H
#define FIX44_MARKETDATAHISTORYREQUEST_H

#include "Message.h"

namespace FIX44
{

  class MarketDataHistoryRequest : public Message
  {
  public:
    MarketDataHistoryRequest() : Message(MsgType()) {}
    MarketDataHistoryRequest(const FIX::Message& m) : Message(m) {}
    MarketDataHistoryRequest(const Message& m) : Message(m) {}
    MarketDataHistoryRequest(const MarketDataHistoryRequest& m) : Message(m) {}
    static FIX::MsgType MsgType() { return FIX::MsgType("U1000"); }

    MarketDataHistoryRequest(
      const FIX::MarketHistReqID& aMarketHistReqID,
      const FIX::Symbol& aSymbol,
      const FIX::HstTo& aHstTo,
      const FIX::HstGraphType& aHstGraphType )
    : Message(MsgType())
    {
      set(aMarketHistReqID);
      set(aSymbol);
      set(aHstTo);
      set(aHstGraphType);
    }

    FIELD_SET(*this, FIX::MarketHistReqID);
    FIELD_SET_EX(std::string, MarketHistReqID);
    FIELD_SET(*this, FIX::Symbol);
    FIELD_SET_EX(std::string, Symbol);
    FIELD_SET(*this, FIX::HstReqBars);
    FIELD_SET_EX(int, HstReqBars);
    FIELD_SET(*this, FIX::HstTo);
    FIELD_SET_EX(FIX::UtcTimeStamp, HstTo);
    FIELD_SET(*this, FIX::ForexPriceType);
    FIELD_SET_EX(char, ForexPriceType);
    FIELD_SET(*this, FIX::HstGraphPeriodID);
    FIELD_SET_EX(std::string, HstGraphPeriodID);
    FIELD_SET(*this, FIX::HstReportType);
    FIELD_SET_EX(char, HstReportType);
    FIELD_SET(*this, FIX::HstGraphType);
    FIELD_SET_EX(char, HstGraphType);
  };

}

#endif
