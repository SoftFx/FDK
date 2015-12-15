#ifndef FIX44_MARKETDATAHISTORY_H
#define FIX44_MARKETDATAHISTORY_H

#include "Message.h"

namespace FIX44
{

  class MarketDataHistory : public Message
  {
  public:
    MarketDataHistory() : Message(MsgType()) {}
    MarketDataHistory(const FIX::Message& m) : Message(m) {}
    MarketDataHistory(const Message& m) : Message(m) {}
    MarketDataHistory(const MarketDataHistory& m) : Message(m) {}
    static FIX::MsgType MsgType() { return FIX::MsgType("U1002"); }

    MarketDataHistory(
      const FIX::MarketHistReqID& aMarketHistReqID,
      const FIX::Symbol& aSymbol,
      const FIX::HstFrom& aHstFrom,
      const FIX::HstTo& aHstTo,
      const FIX::AllHstFrom& aAllHstFrom,
      const FIX::AllHstTo& aAllHstTo )
    : Message(MsgType())
    {
      set(aMarketHistReqID);
      set(aSymbol);
      set(aHstFrom);
      set(aHstTo);
      set(aAllHstFrom);
      set(aAllHstTo);
    }

    FIELD_SET(*this, FIX::MarketHistReqID);
    FIELD_SET_EX(std::string, MarketHistReqID);
    FIELD_SET(*this, FIX::Symbol);
    FIELD_SET_EX(std::string, Symbol);
    FIELD_SET(*this, FIX::HstFrom);
    FIELD_SET_EX(FIX::UtcTimeStamp, HstFrom);
    FIELD_SET(*this, FIX::HstTo);
    FIELD_SET_EX(FIX::UtcTimeStamp, HstTo);
    FIELD_SET(*this, FIX::LastTickId);
    FIELD_SET_EX(std::string, LastTickId);
    FIELD_SET(*this, FIX::ForexPriceType);
    FIELD_SET_EX(char, ForexPriceType);
    FIELD_SET(*this, FIX::HstGraphPeriodID);
    FIELD_SET_EX(std::string, HstGraphPeriodID);
    FIELD_SET(*this, FIX::AllHstFrom);
    FIELD_SET_EX(FIX::UtcTimeStamp, AllHstFrom);
    FIELD_SET(*this, FIX::AllHstTo);
    FIELD_SET_EX(FIX::UtcTimeStamp, AllHstTo);
    FIELD_SET(*this, FIX::HstBinData);
    FIELD_SET_EX(std::string, HstBinData);
    FIELD_SET(*this, FIX::NoBars);
    FIELD_SET_EX(int, NoBars);
    class NoBars: public FIX::Group
    {
    public:
    NoBars() : FIX::Group(10004,10005,FIX::message_order(10005,10006,10007,10008,10009,10040,10041,0)) {}
      FIELD_SET(*this, FIX::BarHi);
      FIELD_SET_EX(double, BarHi);
      FIELD_SET(*this, FIX::BarLow);
      FIELD_SET_EX(double, BarLow);
      FIELD_SET(*this, FIX::BarOpen);
      FIELD_SET_EX(double, BarOpen);
      FIELD_SET(*this, FIX::BarClose);
      FIELD_SET_EX(double, BarClose);
      FIELD_SET(*this, FIX::BarTime);
      FIELD_SET_EX(FIX::UtcTimeStamp, BarTime);
      FIELD_SET(*this, FIX::BarVolume);
      FIELD_SET_EX(int, BarVolume);
      FIELD_SET(*this, FIX::BarVolumeEx);
      FIELD_SET_EX(double, BarVolumeEx);
    };
    FIELD_SET(*this, FIX::NoFiles);
    FIELD_SET_EX(int, NoFiles);
    class NoFiles: public FIX::Group
    {
    public:
    NoFiles() : FIX::Group(10068,10017,FIX::message_order(10017,0)) {}
      FIELD_SET(*this, FIX::AttachedFileId);
      FIELD_SET_EX(std::string, AttachedFileId);
    };
  };

}

#endif
