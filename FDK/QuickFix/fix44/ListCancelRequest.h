#ifndef FIX44_LISTCANCELREQUEST_H
#define FIX44_LISTCANCELREQUEST_H

#include "Message.h"

namespace FIX44
{

  class ListCancelRequest : public Message
  {
  public:
    ListCancelRequest() : Message(MsgType()) {}
    ListCancelRequest(const FIX::Message& m) : Message(m) {}
    ListCancelRequest(const Message& m) : Message(m) {}
    ListCancelRequest(const ListCancelRequest& m) : Message(m) {}
    static FIX::MsgType MsgType() { return FIX::MsgType("K"); }

    ListCancelRequest(
      const FIX::ListID& aListID,
      const FIX::TransactTime& aTransactTime )
    : Message(MsgType())
    {
      set(aListID);
      set(aTransactTime);
    }

    FIELD_SET(*this, FIX::ListID);
    FIELD_SET_EX(std::string, ListID);
    FIELD_SET(*this, FIX::TransactTime);
    FIELD_SET_EX(FIX::UtcTimeStamp, TransactTime);
    FIELD_SET(*this, FIX::TradeOriginationDate);
    FIELD_SET_EX(std::string, TradeOriginationDate);
    FIELD_SET(*this, FIX::TradeDate);
    FIELD_SET_EX(std::string, TradeDate);
    FIELD_SET(*this, FIX::Text);
    FIELD_SET_EX(std::string, Text);
    FIELD_SET(*this, FIX::EncodedTextLen);
    FIELD_SET_EX(int, EncodedTextLen);
    FIELD_SET(*this, FIX::EncodedText);
    FIELD_SET_EX(std::string, EncodedText);
  };

}

#endif
