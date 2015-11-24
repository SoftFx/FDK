#ifndef FIX44_MARKETDATAHISTORYMETADATAREQUEST_H
#define FIX44_MARKETDATAHISTORYMETADATAREQUEST_H

#include "Message.h"

namespace FIX44
{

  class MarketDataHistoryMetadataRequest : public Message
  {
  public:
    MarketDataHistoryMetadataRequest() : Message(MsgType()) {}
    MarketDataHistoryMetadataRequest(const FIX::Message& m) : Message(m) {}
    MarketDataHistoryMetadataRequest(const Message& m) : Message(m) {}
    MarketDataHistoryMetadataRequest(const MarketDataHistoryMetadataRequest& m) : Message(m) {}
    static FIX::MsgType MsgType() { return FIX::MsgType("U1013"); }

    MarketDataHistoryMetadataRequest(
      const FIX::MDHstMetaReqID& aMDHstMetaReqID,
      const FIX::HstGraphType& aHstGraphType,
      const FIX::Symbol& aSymbol )
    : Message(MsgType())
    {
      set(aMDHstMetaReqID);
      set(aHstGraphType);
      set(aSymbol);
    }

    FIELD_SET(*this, FIX::MDHstMetaReqID);
    FIELD_SET_EX(std::string, MDHstMetaReqID);
    FIELD_SET(*this, FIX::HstGraphType);
    FIELD_SET_EX(char, HstGraphType);
    FIELD_SET(*this, FIX::Symbol);
    FIELD_SET_EX(std::string, Symbol);
    FIELD_SET(*this, FIX::ForexPriceType);
    FIELD_SET_EX(char, ForexPriceType);
    FIELD_SET(*this, FIX::HstGraphPeriodID);
    FIELD_SET_EX(std::string, HstGraphPeriodID);
  };

}

#endif
