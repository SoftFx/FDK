#ifndef FIX44_ACCOUNTINFO_H
#define FIX44_ACCOUNTINFO_H

#include "Message.h"

namespace FIX44
{

  class AccountInfo : public Message
  {
  public:
    AccountInfo() : Message(MsgType()) {}
    AccountInfo(const FIX::Message& m) : Message(m) {}
    AccountInfo(const Message& m) : Message(m) {}
    AccountInfo(const AccountInfo& m) : Message(m) {}
    static FIX::MsgType MsgType() { return FIX::MsgType("U1006"); }

    AccountInfo(
      const FIX::Leverage& aLeverage,
      const FIX::Balance& aBalance,
      const FIX::Margin& aMargin,
      const FIX::Equity& aEquity,
      const FIX::Currency& aCurrency,
      const FIX::Account& aAccount )
    : Message(MsgType())
    {
      set(aLeverage);
      set(aBalance);
      set(aMargin);
      set(aEquity);
      set(aCurrency);
      set(aAccount);
    }

    FIELD_SET(*this, FIX::AcInfReqID);
    FIELD_SET_EX(std::string, AcInfReqID);
    FIELD_SET(*this, FIX::Leverage);
    FIELD_SET_EX(int, Leverage);
    FIELD_SET(*this, FIX::Balance);
    FIELD_SET_EX(double, Balance);
    FIELD_SET(*this, FIX::Margin);
    FIELD_SET_EX(double, Margin);
    FIELD_SET(*this, FIX::Equity);
    FIELD_SET_EX(double, Equity);
    FIELD_SET(*this, FIX::Currency);
    FIELD_SET_EX(std::string, Currency);
    FIELD_SET(*this, FIX::Account);
    FIELD_SET_EX(std::string, Account);
    FIELD_SET(*this, FIX::AccountingType);
    FIELD_SET_EX(char, AccountingType);
    FIELD_SET(*this, FIX::AccountingSystemType);
    FIELD_SET_EX(char, AccountingSystemType);
    FIELD_SET(*this, FIX::AccMarginCallLevel);
    FIELD_SET_EX(double, AccMarginCallLevel);
    FIELD_SET(*this, FIX::AccStopOutLevel);
    FIELD_SET_EX(double, AccStopOutLevel);
    FIELD_SET(*this, FIX::AccountValidFlag);
    FIELD_SET_EX(bool, AccountValidFlag);
    FIELD_SET(*this, FIX::AccountBlockedFlag);
    FIELD_SET_EX(bool, AccountBlockedFlag);
    FIELD_SET(*this, FIX::InvestorLoginFlag);
    FIELD_SET_EX(bool, InvestorLoginFlag);
    FIELD_SET(*this, FIX::AccountName);
    FIELD_SET_EX(std::string, AccountName);
    FIELD_SET(*this, FIX::NoAssets);
    FIELD_SET_EX(int, NoAssets);
    class NoAssets: public FIX::Group
    {
    public:
    NoAssets() : FIX::Group(10117,10118,FIX::message_order(10118,10119,10120,0)) {}
      FIELD_SET(*this, FIX::AssetBalance);
      FIELD_SET_EX(double, AssetBalance);
      FIELD_SET(*this, FIX::AssetTradeAmt);
      FIELD_SET_EX(double, AssetTradeAmt);
      FIELD_SET(*this, FIX::AssetCurrency);
      FIELD_SET_EX(std::string, AssetCurrency);
    };
  };

}

#endif
