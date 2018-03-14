#ifndef FIX44_DAILYACCOUNTSNAPSHOTREQUESTACK_H
#define FIX44_DAILYACCOUNTSNAPSHOTREQUESTACK_H

#include "Message.h"

namespace FIX44
{

  class DailyAccountSnapshotRequestAck : public Message
  {
  public:
    DailyAccountSnapshotRequestAck() : Message(MsgType()) {}
    DailyAccountSnapshotRequestAck(const FIX::Message& m) : Message(m) {}
    DailyAccountSnapshotRequestAck(const Message& m) : Message(m) {}
    DailyAccountSnapshotRequestAck(const DailyAccountSnapshotRequestAck& m) : Message(m) {}
    static FIX::MsgType MsgType() { return FIX::MsgType("U1026"); }

    DailyAccountSnapshotRequestAck(
      const FIX::SnapshotRequestID& aSnapshotRequestID,
      const FIX::SnapshotRequestResult& aSnapshotRequestResult )
    : Message(MsgType())
    {
      set(aSnapshotRequestID);
      set(aSnapshotRequestResult);
    }

    FIELD_SET(*this, FIX::SnapshotRequestID);
    FIELD_SET_EX(std::string, SnapshotRequestID);
    FIELD_SET(*this, FIX::TotNumTradeReports);
    FIELD_SET_EX(int, TotNumTradeReports);
    FIELD_SET(*this, FIX::CurNumTradeReports);
    FIELD_SET_EX(int, CurNumTradeReports);
    FIELD_SET(*this, FIX::EndOfStrm);
    FIELD_SET_EX(bool, EndOfStrm);
    FIELD_SET(*this, FIX::SnapshotRequestResult);
    FIELD_SET_EX(int, SnapshotRequestResult);
    FIELD_SET(*this, FIX::Text);
    FIELD_SET_EX(std::string, Text);
    FIELD_SET(*this, FIX::EncodedTextLen);
    FIELD_SET_EX(int, EncodedTextLen);
    FIELD_SET(*this, FIX::EncodedText);
    FIELD_SET_EX(std::string, EncodedText);
  };

}

#endif
