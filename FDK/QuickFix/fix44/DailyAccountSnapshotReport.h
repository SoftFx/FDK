#ifndef FIX44_DAILYACCOUNTSNAPSHOTREPORT_H
#define FIX44_DAILYACCOUNTSNAPSHOTREPORT_H

#include "Message.h"

namespace FIX44
{

  class DailyAccountSnapshotReport : public Message
  {
  public:
    DailyAccountSnapshotReport() : Message(MsgType()) {}
    DailyAccountSnapshotReport(const FIX::Message& m) : Message(m) {}
    DailyAccountSnapshotReport(const Message& m) : Message(m) {}
    DailyAccountSnapshotReport(const DailyAccountSnapshotReport& m) : Message(m) {}
    static FIX::MsgType MsgType() { return FIX::MsgType("U1027"); }

    DailyAccountSnapshotReport(
      const FIX::SnapshotRequestID& aSnapshotRequestID )
    : Message(MsgType())
    {
      set(aSnapshotRequestID);
    }

    FIELD_SET(*this, FIX::SnapshotRequestID);
    FIELD_SET_EX(std::string, SnapshotRequestID);
    FIELD_SET(*this, FIX::Leverage);
    FIELD_SET_EX(int, Leverage);
    FIELD_SET(*this, FIX::Balance);
    FIELD_SET_EX(double, Balance);
    FIELD_SET(*this, FIX::Margin);
    FIELD_SET_EX(double, Margin);
    FIELD_SET(*this, FIX::MarginLevel);
    FIELD_SET_EX(double, MarginLevel);
    FIELD_SET(*this, FIX::Equity);
    FIELD_SET_EX(double, Equity);
    FIELD_SET(*this, FIX::Swap);
    FIELD_SET_EX(double, Swap);
    FIELD_SET(*this, FIX::Profit);
    FIELD_SET_EX(double, Profit);
    FIELD_SET(*this, FIX::Commission);
    FIELD_SET_EX(double, Commission);
    FIELD_SET(*this, FIX::AgentCommission);
    FIELD_SET_EX(double, AgentCommission);
    FIELD_SET(*this, FIX::Currency);
    FIELD_SET_EX(std::string, Currency);
    FIELD_SET(*this, FIX::Timestamp);
    FIELD_SET_EX(FIX::UtcTimeStamp, Timestamp);
    FIELD_SET(*this, FIX::Account);
    FIELD_SET_EX(std::string, Account);
    FIELD_SET(*this, FIX::AccountingType);
    FIELD_SET_EX(char, AccountingType);
    FIELD_SET(*this, FIX::AccountValidFlag);
    FIELD_SET_EX(bool, AccountValidFlag);
    FIELD_SET(*this, FIX::AccountBlockedFlag);
    FIELD_SET_EX(bool, AccountBlockedFlag);
    FIELD_SET(*this, FIX::AccountReadonlyFlag);
    FIELD_SET_EX(bool, AccountReadonlyFlag);
    FIELD_SET(*this, FIX::BalanceCurrencyToUsdConversionRate);
    FIELD_SET_EX(double, BalanceCurrencyToUsdConversionRate);
    FIELD_SET(*this, FIX::UsdToBalanceCurrencyConversionRate);
    FIELD_SET_EX(double, UsdToBalanceCurrencyConversionRate);
    FIELD_SET(*this, FIX::ProfitCurrencyToUsdConversionRate);
    FIELD_SET_EX(double, ProfitCurrencyToUsdConversionRate);
    FIELD_SET(*this, FIX::UsdToProfitCurrencyConversionRate);
    FIELD_SET_EX(double, UsdToProfitCurrencyConversionRate);
    FIELD_SET(*this, FIX::BalanceCurrencyToReportConversionRate);
    FIELD_SET_EX(double, BalanceCurrencyToReportConversionRate);
    FIELD_SET(*this, FIX::ReportToBalanceCurrencyConversionRate);
    FIELD_SET_EX(double, ReportToBalanceCurrencyConversionRate);
    FIELD_SET(*this, FIX::ProfitCurrencyToReportConversionRate);
    FIELD_SET_EX(double, ProfitCurrencyToReportConversionRate);
    FIELD_SET(*this, FIX::ReportToProfitCurrencyConversionRate);
    FIELD_SET_EX(double, ReportToProfitCurrencyConversionRate);
    FIELD_SET(*this, FIX::NoAssets);
    FIELD_SET_EX(int, NoAssets);
    class NoAssets: public FIX::Group
    {
    public:
    NoAssets() : FIX::Group(10117,10118,FIX::message_order(10118,10154,10119,10120,10201,10202,10236,10237,0)) {}
      FIELD_SET(*this, FIX::AssetBalance);
      FIELD_SET_EX(double, AssetBalance);
      FIELD_SET(*this, FIX::AssetLockedAmt);
      FIELD_SET_EX(double, AssetLockedAmt);
      FIELD_SET(*this, FIX::AssetTradeAmt);
      FIELD_SET_EX(double, AssetTradeAmt);
      FIELD_SET(*this, FIX::AssetCurrency);
      FIELD_SET_EX(std::string, AssetCurrency);
      FIELD_SET(*this, FIX::SrcAssetToUsdConversionRate);
      FIELD_SET_EX(double, SrcAssetToUsdConversionRate);
      FIELD_SET(*this, FIX::UsdToSrcAssetConversionRate);
      FIELD_SET_EX(double, UsdToSrcAssetConversionRate);
      FIELD_SET(*this, FIX::SrcAssetToReportConversionRate);
      FIELD_SET_EX(double, SrcAssetToReportConversionRate);
      FIELD_SET(*this, FIX::ReportToSrcAssetConversionRate);
      FIELD_SET_EX(double, ReportToSrcAssetConversionRate);
    };
    FIELD_SET(*this, FIX::NoPositions);
    FIELD_SET_EX(int, NoPositions);
    class NoPositions: public FIX::Group
    {
    public:
    NoPositions() : FIX::Group(702,55,FIX::message_order(55,54,730,151,12,10096,10085,10036,10030,10223,10224,10225,0)) {}
      FIELD_SET(*this, FIX::Symbol);
      FIELD_SET_EX(std::string, Symbol);
      FIELD_SET(*this, FIX::Side);
      FIELD_SET_EX(char, Side);
      FIELD_SET(*this, FIX::SettlPrice);
      FIELD_SET_EX(double, SettlPrice);
      FIELD_SET(*this, FIX::LeavesQty);
      FIELD_SET_EX(double, LeavesQty);
      FIELD_SET(*this, FIX::Commission);
      FIELD_SET_EX(double, Commission);
      FIELD_SET(*this, FIX::Swap);
      FIELD_SET_EX(double, Swap);
      FIELD_SET(*this, FIX::PosModified);
      FIELD_SET_EX(FIX::UtcTimeStamp, PosModified);
      FIELD_SET(*this, FIX::PosID);
      FIELD_SET_EX(std::string, PosID);
      FIELD_SET(*this, FIX::Margin);
      FIELD_SET_EX(double, Margin);
      FIELD_SET(*this, FIX::Profit);
      FIELD_SET_EX(double, Profit);
      FIELD_SET(*this, FIX::CurrentBestAsk);
      FIELD_SET_EX(double, CurrentBestAsk);
      FIELD_SET(*this, FIX::CurrentBestBid);
      FIELD_SET_EX(double, CurrentBestBid);
    };
    FIELD_SET(*this, FIX::ReportCurrency);
    FIELD_SET_EX(std::string, ReportCurrency);
    FIELD_SET(*this, FIX::TokenCommissionCurrency);
    FIELD_SET_EX(std::string, TokenCommissionCurrency);
    FIELD_SET(*this, FIX::TokenCommissionCurrencyDiscount);
    FIELD_SET_EX(double, TokenCommissionCurrencyDiscount);
    FIELD_SET(*this, FIX::TokenCommissionEnabled);
    FIELD_SET_EX(bool, TokenCommissionEnabled);
  };

}

#endif
