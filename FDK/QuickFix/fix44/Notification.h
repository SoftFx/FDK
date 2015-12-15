#ifndef FIX44_NOTIFICATION_H
#define FIX44_NOTIFICATION_H

#include "Message.h"

namespace FIX44
{

  class Notification : public Message
  {
  public:
    Notification() : Message(MsgType()) {}
    Notification(const FIX::Message& m) : Message(m) {}
    Notification(const Message& m) : Message(m) {}
    Notification(const Notification& m) : Message(m) {}
    static FIX::MsgType MsgType() { return FIX::MsgType("U1010"); }

    Notification(
      const FIX::ExecID& aExecID,
      const FIX::NotifCode& aNotifCode,
      const FIX::NotifSeverity& aNotifSeverity )
    : Message(MsgType())
    {
      set(aExecID);
      set(aNotifCode);
      set(aNotifSeverity);
    }

    FIELD_SET(*this, FIX::ExecID);
    FIELD_SET_EX(std::string, ExecID);
    FIELD_SET(*this, FIX::NotifCode);
    FIELD_SET_EX(int, NotifCode);
    FIELD_SET(*this, FIX::NotifSeverity);
    FIELD_SET_EX(int, NotifSeverity);
    FIELD_SET(*this, FIX::Text);
    FIELD_SET_EX(std::string, Text);
    FIELD_SET(*this, FIX::EncodedTextLen);
    FIELD_SET_EX(int, EncodedTextLen);
    FIELD_SET(*this, FIX::EncodedText);
    FIELD_SET_EX(std::string, EncodedText);
    FIELD_SET(*this, FIX::AccBalance);
    FIELD_SET_EX(double, AccBalance);
    FIELD_SET(*this, FIX::AccTrAmount);
    FIELD_SET_EX(double, AccTrAmount);
    FIELD_SET(*this, FIX::AccTrCurry);
    FIELD_SET_EX(std::string, AccTrCurry);
  };

}

#endif
