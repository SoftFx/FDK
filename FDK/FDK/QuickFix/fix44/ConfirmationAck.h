#ifndef FIX44_CONFIRMATIONACK_H
#define FIX44_CONFIRMATIONACK_H

#include "Message.h"

namespace FIX44
{

  class ConfirmationAck : public Message
  {
  public:
    ConfirmationAck() : Message(MsgType()) {}
    ConfirmationAck(const FIX::Message& m) : Message(m) {}
    ConfirmationAck(const Message& m) : Message(m) {}
    ConfirmationAck(const ConfirmationAck& m) : Message(m) {}
    static FIX::MsgType MsgType() { return FIX::MsgType("AU"); }

    ConfirmationAck(
      const FIX::ConfirmID& aConfirmID,
      const FIX::TradeDate& aTradeDate,
      const FIX::TransactTime& aTransactTime,
      const FIX::AffirmStatus& aAffirmStatus )
    : Message(MsgType())
    {
      set(aConfirmID);
      set(aTradeDate);
      set(aTransactTime);
      set(aAffirmStatus);
    }

    FIELD_SET(*this, FIX::ConfirmID);
    FIELD_SET_EX(std::string, ConfirmID);
    FIELD_SET(*this, FIX::TradeDate);
    FIELD_SET_EX(std::string, TradeDate);
    FIELD_SET(*this, FIX::TransactTime);
    FIELD_SET_EX(FIX::UtcTimeStamp, TransactTime);
    FIELD_SET(*this, FIX::AffirmStatus);
    FIELD_SET_EX(int, AffirmStatus);
    FIELD_SET(*this, FIX::ConfirmRejReason);
    FIELD_SET_EX(int, ConfirmRejReason);
    FIELD_SET(*this, FIX::MatchStatus);
    FIELD_SET_EX(char, MatchStatus);
    FIELD_SET(*this, FIX::Text);
    FIELD_SET_EX(std::string, Text);
    FIELD_SET(*this, FIX::EncodedTextLen);
    FIELD_SET_EX(int, EncodedTextLen);
    FIELD_SET(*this, FIX::EncodedText);
    FIELD_SET_EX(std::string, EncodedText);
  };

}

#endif
