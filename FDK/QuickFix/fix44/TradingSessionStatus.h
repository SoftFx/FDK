#ifndef FIX44_TRADINGSESSIONSTATUS_H
#define FIX44_TRADINGSESSIONSTATUS_H

#include "Message.h"

namespace FIX44
{

  class TradingSessionStatus : public Message
  {
  public:
    TradingSessionStatus() : Message(MsgType()) {}
    TradingSessionStatus(const FIX::Message& m) : Message(m) {}
    TradingSessionStatus(const Message& m) : Message(m) {}
    TradingSessionStatus(const TradingSessionStatus& m) : Message(m) {}
    static FIX::MsgType MsgType() { return FIX::MsgType("h"); }

    TradingSessionStatus(
      const FIX::TradingSessionID& aTradingSessionID,
      const FIX::TradSesStatus& aTradSesStatus )
    : Message(MsgType())
    {
      set(aTradingSessionID);
      set(aTradSesStatus);
    }

    FIELD_SET(*this, FIX::TradSesReqID);
    FIELD_SET_EX(std::string, TradSesReqID);
    FIELD_SET(*this, FIX::TradingSessionID);
    FIELD_SET_EX(std::string, TradingSessionID);
    FIELD_SET(*this, FIX::TradingSessionSubID);
    FIELD_SET_EX(std::string, TradingSessionSubID);
    FIELD_SET(*this, FIX::TradSesMethod);
    FIELD_SET_EX(int, TradSesMethod);
    FIELD_SET(*this, FIX::TradSesMode);
    FIELD_SET_EX(int, TradSesMode);
    FIELD_SET(*this, FIX::UnsolicitedIndicator);
    FIELD_SET_EX(bool, UnsolicitedIndicator);
    FIELD_SET(*this, FIX::TradSesStatus);
    FIELD_SET_EX(int, TradSesStatus);
    FIELD_SET(*this, FIX::TradSesStatusRejReason);
    FIELD_SET_EX(int, TradSesStatusRejReason);
    FIELD_SET(*this, FIX::TradSesStartTime);
    FIELD_SET_EX(FIX::UtcTimeStamp, TradSesStartTime);
    FIELD_SET(*this, FIX::TradSesOpenTime);
    FIELD_SET_EX(FIX::UtcTimeStamp, TradSesOpenTime);
    FIELD_SET(*this, FIX::TradSesPreCloseTime);
    FIELD_SET_EX(FIX::UtcTimeStamp, TradSesPreCloseTime);
    FIELD_SET(*this, FIX::TradSesCloseTime);
    FIELD_SET_EX(FIX::UtcTimeStamp, TradSesCloseTime);
    FIELD_SET(*this, FIX::TradSesEndTime);
    FIELD_SET_EX(FIX::UtcTimeStamp, TradSesEndTime);
    FIELD_SET(*this, FIX::TotalVolumeTraded);
    FIELD_SET_EX(double, TotalVolumeTraded);
    FIELD_SET(*this, FIX::Text);
    FIELD_SET_EX(std::string, Text);
    FIELD_SET(*this, FIX::EncodedTextLen);
    FIELD_SET_EX(int, EncodedTextLen);
    FIELD_SET(*this, FIX::EncodedText);
    FIELD_SET_EX(std::string, EncodedText);
    FIELD_SET(*this, FIX::PlatformTimezoneOffset);
    FIELD_SET_EX(int, PlatformTimezoneOffset);
    FIELD_SET(*this, FIX::PlatformName);
    FIELD_SET_EX(std::string, PlatformName);
    FIELD_SET(*this, FIX::PlatformCompany);
    FIELD_SET_EX(std::string, PlatformCompany);
    FIELD_SET(*this, FIX::NoStatusGroups);
    FIELD_SET_EX(int, NoStatusGroups);
    class NoStatusGroups: public FIX::Group
    {
    public:
    NoStatusGroups() : FIX::Group(10171,10170,FIX::message_order(10170,340,341,345,0)) {}
      FIELD_SET(*this, FIX::StatusGroupID);
      FIELD_SET_EX(std::string, StatusGroupID);
      FIELD_SET(*this, FIX::TradSesStatus);
      FIELD_SET_EX(int, TradSesStatus);
      FIELD_SET(*this, FIX::TradSesStartTime);
      FIELD_SET_EX(FIX::UtcTimeStamp, TradSesStartTime);
      FIELD_SET(*this, FIX::TradSesEndTime);
      FIELD_SET_EX(FIX::UtcTimeStamp, TradSesEndTime);
    };
  };

}

#endif
