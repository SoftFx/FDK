#ifndef FIX44_MULTILEGORDERCANCELREPLACE_H
#define FIX44_MULTILEGORDERCANCELREPLACE_H

#include "Message.h"

namespace FIX44
{

  class MultilegOrderCancelReplace : public Message
  {
  public:
    MultilegOrderCancelReplace() : Message(MsgType()) {}
    MultilegOrderCancelReplace(const FIX::Message& m) : Message(m) {}
    MultilegOrderCancelReplace(const Message& m) : Message(m) {}
    MultilegOrderCancelReplace(const MultilegOrderCancelReplace& m) : Message(m) {}
    static FIX::MsgType MsgType() { return FIX::MsgType("AC"); }

    MultilegOrderCancelReplace(
      const FIX::OrigClOrdID& aOrigClOrdID,
      const FIX::ClOrdID& aClOrdID,
      const FIX::Side& aSide,
      const FIX::TransactTime& aTransactTime,
      const FIX::OrdType& aOrdType )
    : Message(MsgType())
    {
      set(aOrigClOrdID);
      set(aClOrdID);
      set(aSide);
      set(aTransactTime);
      set(aOrdType);
    }

    FIELD_SET(*this, FIX::OrderID);
    FIELD_SET_EX(std::string, OrderID);
    FIELD_SET(*this, FIX::OrigClOrdID);
    FIELD_SET_EX(std::string, OrigClOrdID);
    FIELD_SET(*this, FIX::ClOrdID);
    FIELD_SET_EX(std::string, ClOrdID);
    FIELD_SET(*this, FIX::SecondaryClOrdID);
    FIELD_SET_EX(std::string, SecondaryClOrdID);
    FIELD_SET(*this, FIX::ClOrdLinkID);
    FIELD_SET_EX(std::string, ClOrdLinkID);
    FIELD_SET(*this, FIX::OrigOrdModTime);
    FIELD_SET_EX(FIX::UtcTimeStamp, OrigOrdModTime);
    FIELD_SET(*this, FIX::NoPartyIDs);
    FIELD_SET_EX(int, NoPartyIDs);
    class NoPartyIDs: public FIX::Group
    {
    public:
    NoPartyIDs() : FIX::Group(453,448,FIX::message_order(448,447,452,802,0)) {}
      FIELD_SET(*this, FIX::PartyID);
      FIELD_SET_EX(std::string, PartyID);
      FIELD_SET(*this, FIX::PartyIDSource);
      FIELD_SET_EX(char, PartyIDSource);
      FIELD_SET(*this, FIX::PartyRole);
      FIELD_SET_EX(int, PartyRole);
      FIELD_SET(*this, FIX::NoPartySubIDs);
      FIELD_SET_EX(int, NoPartySubIDs);
      class NoPartySubIDs: public FIX::Group
      {
      public:
      NoPartySubIDs() : FIX::Group(802,523,FIX::message_order(523,803,0)) {}
        FIELD_SET(*this, FIX::PartySubID);
        FIELD_SET_EX(std::string, PartySubID);
        FIELD_SET(*this, FIX::PartySubIDType);
        FIELD_SET_EX(int, PartySubIDType);
      };
    };
    FIELD_SET(*this, FIX::TradeOriginationDate);
    FIELD_SET_EX(std::string, TradeOriginationDate);
    FIELD_SET(*this, FIX::TradeDate);
    FIELD_SET_EX(std::string, TradeDate);
    FIELD_SET(*this, FIX::Account);
    FIELD_SET_EX(std::string, Account);
    FIELD_SET(*this, FIX::AcctIDSource);
    FIELD_SET_EX(int, AcctIDSource);
    FIELD_SET(*this, FIX::AccountType);
    FIELD_SET_EX(int, AccountType);
    FIELD_SET(*this, FIX::DayBookingInst);
    FIELD_SET_EX(char, DayBookingInst);
    FIELD_SET(*this, FIX::BookingUnit);
    FIELD_SET_EX(char, BookingUnit);
    FIELD_SET(*this, FIX::PreallocMethod);
    FIELD_SET_EX(char, PreallocMethod);
    FIELD_SET(*this, FIX::AllocID);
    FIELD_SET_EX(std::string, AllocID);
    FIELD_SET(*this, FIX::SettlType);
    FIELD_SET_EX(std::string, SettlType);
    FIELD_SET(*this, FIX::SettlDate);
    FIELD_SET_EX(std::string, SettlDate);
    FIELD_SET(*this, FIX::CashMargin);
    FIELD_SET_EX(char, CashMargin);
    FIELD_SET(*this, FIX::ClearingFeeIndicator);
    FIELD_SET_EX(std::string, ClearingFeeIndicator);
    FIELD_SET(*this, FIX::HandlInst);
    FIELD_SET_EX(char, HandlInst);
    FIELD_SET(*this, FIX::ExecInst);
    FIELD_SET(*this, FIX::MinQty);
    FIELD_SET_EX(double, MinQty);
    FIELD_SET(*this, FIX::MaxFloor);
    FIELD_SET_EX(double, MaxFloor);
    FIELD_SET(*this, FIX::ExDestination);
    FIELD_SET(*this, FIX::ProcessCode);
    FIELD_SET_EX(char, ProcessCode);
    FIELD_SET(*this, FIX::Side);
    FIELD_SET_EX(char, Side);
    FIELD_SET(*this, FIX::Symbol);
    FIELD_SET_EX(std::string, Symbol);
    FIELD_SET(*this, FIX::SymbolSfx);
    FIELD_SET_EX(std::string, SymbolSfx);
    FIELD_SET(*this, FIX::SecurityID);
    FIELD_SET_EX(std::string, SecurityID);
    FIELD_SET(*this, FIX::SecurityIDSource);
    FIELD_SET_EX(std::string, SecurityIDSource);
    FIELD_SET(*this, FIX::Product);
    FIELD_SET_EX(int, Product);
    FIELD_SET(*this, FIX::CFICode);
    FIELD_SET_EX(std::string, CFICode);
    FIELD_SET(*this, FIX::SecurityType);
    FIELD_SET_EX(std::string, SecurityType);
    FIELD_SET(*this, FIX::SecuritySubType);
    FIELD_SET_EX(std::string, SecuritySubType);
    FIELD_SET(*this, FIX::MaturityMonthYear);
    FIELD_SET(*this, FIX::MaturityDate);
    FIELD_SET_EX(std::string, MaturityDate);
    FIELD_SET(*this, FIX::CouponPaymentDate);
    FIELD_SET_EX(std::string, CouponPaymentDate);
    FIELD_SET(*this, FIX::IssueDate);
    FIELD_SET_EX(std::string, IssueDate);
    FIELD_SET(*this, FIX::RepoCollateralSecurityType);
    FIELD_SET_EX(int, RepoCollateralSecurityType);
    FIELD_SET(*this, FIX::RepurchaseTerm);
    FIELD_SET_EX(int, RepurchaseTerm);
    FIELD_SET(*this, FIX::RepurchaseRate);
    FIELD_SET_EX(double, RepurchaseRate);
    FIELD_SET(*this, FIX::Factor);
    FIELD_SET_EX(double, Factor);
    FIELD_SET(*this, FIX::CreditRating);
    FIELD_SET_EX(std::string, CreditRating);
    FIELD_SET(*this, FIX::InstrRegistry);
    FIELD_SET_EX(std::string, InstrRegistry);
    FIELD_SET(*this, FIX::CountryOfIssue);
    FIELD_SET(*this, FIX::StateOrProvinceOfIssue);
    FIELD_SET_EX(std::string, StateOrProvinceOfIssue);
    FIELD_SET(*this, FIX::LocaleOfIssue);
    FIELD_SET_EX(std::string, LocaleOfIssue);
    FIELD_SET(*this, FIX::RedemptionDate);
    FIELD_SET_EX(std::string, RedemptionDate);
    FIELD_SET(*this, FIX::StrikePrice);
    FIELD_SET_EX(double, StrikePrice);
    FIELD_SET(*this, FIX::StrikeCurrency);
    FIELD_SET_EX(std::string, StrikeCurrency);
    FIELD_SET(*this, FIX::OptAttribute);
    FIELD_SET_EX(char, OptAttribute);
    FIELD_SET(*this, FIX::ContractMultiplier);
    FIELD_SET_EX(double, ContractMultiplier);
    FIELD_SET(*this, FIX::CouponRate);
    FIELD_SET_EX(double, CouponRate);
    FIELD_SET(*this, FIX::SecurityExchange);
    FIELD_SET(*this, FIX::Issuer);
    FIELD_SET_EX(std::string, Issuer);
    FIELD_SET(*this, FIX::EncodedIssuerLen);
    FIELD_SET_EX(int, EncodedIssuerLen);
    FIELD_SET(*this, FIX::EncodedIssuer);
    FIELD_SET_EX(std::string, EncodedIssuer);
    FIELD_SET(*this, FIX::SecurityDesc);
    FIELD_SET_EX(std::string, SecurityDesc);
    FIELD_SET(*this, FIX::EncodedSecurityDescLen);
    FIELD_SET_EX(int, EncodedSecurityDescLen);
    FIELD_SET(*this, FIX::EncodedSecurityDesc);
    FIELD_SET_EX(std::string, EncodedSecurityDesc);
    FIELD_SET(*this, FIX::Pool);
    FIELD_SET_EX(std::string, Pool);
    FIELD_SET(*this, FIX::ContractSettlMonth);
    FIELD_SET(*this, FIX::CPProgram);
    FIELD_SET_EX(int, CPProgram);
    FIELD_SET(*this, FIX::CPRegType);
    FIELD_SET_EX(std::string, CPRegType);
    FIELD_SET(*this, FIX::DatedDate);
    FIELD_SET_EX(std::string, DatedDate);
    FIELD_SET(*this, FIX::InterestAccrualDate);
    FIELD_SET_EX(std::string, InterestAccrualDate);
    FIELD_SET(*this, FIX::NoSecurityAltID);
    FIELD_SET_EX(int, NoSecurityAltID);
    class NoSecurityAltID: public FIX::Group
    {
    public:
    NoSecurityAltID() : FIX::Group(454,455,FIX::message_order(455,456,0)) {}
      FIELD_SET(*this, FIX::SecurityAltID);
      FIELD_SET_EX(std::string, SecurityAltID);
      FIELD_SET(*this, FIX::SecurityAltIDSource);
      FIELD_SET_EX(std::string, SecurityAltIDSource);
    };
    FIELD_SET(*this, FIX::NoEvents);
    FIELD_SET_EX(int, NoEvents);
    class NoEvents: public FIX::Group
    {
    public:
    NoEvents() : FIX::Group(864,865,FIX::message_order(865,866,867,868,0)) {}
      FIELD_SET(*this, FIX::EventType);
      FIELD_SET_EX(int, EventType);
      FIELD_SET(*this, FIX::EventDate);
      FIELD_SET_EX(std::string, EventDate);
      FIELD_SET(*this, FIX::EventPx);
      FIELD_SET_EX(double, EventPx);
      FIELD_SET(*this, FIX::EventText);
      FIELD_SET_EX(std::string, EventText);
    };
    FIELD_SET(*this, FIX::PrevClosePx);
    FIELD_SET_EX(double, PrevClosePx);
    FIELD_SET(*this, FIX::LocateReqd);
    FIELD_SET_EX(bool, LocateReqd);
    FIELD_SET(*this, FIX::TransactTime);
    FIELD_SET_EX(FIX::UtcTimeStamp, TransactTime);
    FIELD_SET(*this, FIX::QtyType);
    FIELD_SET_EX(int, QtyType);
    FIELD_SET(*this, FIX::OrderQty);
    FIELD_SET_EX(double, OrderQty);
    FIELD_SET(*this, FIX::CashOrderQty);
    FIELD_SET_EX(double, CashOrderQty);
    FIELD_SET(*this, FIX::OrderPercent);
    FIELD_SET_EX(double, OrderPercent);
    FIELD_SET(*this, FIX::RoundingDirection);
    FIELD_SET_EX(char, RoundingDirection);
    FIELD_SET(*this, FIX::RoundingModulus);
    FIELD_SET_EX(double, RoundingModulus);
    FIELD_SET(*this, FIX::OrdType);
    FIELD_SET_EX(char, OrdType);
    FIELD_SET(*this, FIX::PriceType);
    FIELD_SET_EX(int, PriceType);
    FIELD_SET(*this, FIX::Price);
    FIELD_SET_EX(double, Price);
    FIELD_SET(*this, FIX::StopPx);
    FIELD_SET_EX(double, StopPx);
    FIELD_SET(*this, FIX::Currency);
    FIELD_SET_EX(std::string, Currency);
    FIELD_SET(*this, FIX::ComplianceID);
    FIELD_SET_EX(std::string, ComplianceID);
    FIELD_SET(*this, FIX::SolicitedFlag);
    FIELD_SET_EX(bool, SolicitedFlag);
    FIELD_SET(*this, FIX::IOIID);
    FIELD_SET_EX(std::string, IOIID);
    FIELD_SET(*this, FIX::QuoteID);
    FIELD_SET_EX(std::string, QuoteID);
    FIELD_SET(*this, FIX::TimeInForce);
    FIELD_SET_EX(char, TimeInForce);
    FIELD_SET(*this, FIX::EffectiveTime);
    FIELD_SET_EX(FIX::UtcTimeStamp, EffectiveTime);
    FIELD_SET(*this, FIX::ExpireDate);
    FIELD_SET_EX(std::string, ExpireDate);
    FIELD_SET(*this, FIX::ExpireTime);
    FIELD_SET_EX(FIX::UtcTimeStamp, ExpireTime);
    FIELD_SET(*this, FIX::GTBookingInst);
    FIELD_SET_EX(int, GTBookingInst);
    FIELD_SET(*this, FIX::Commission);
    FIELD_SET_EX(double, Commission);
    FIELD_SET(*this, FIX::CommType);
    FIELD_SET_EX(char, CommType);
    FIELD_SET(*this, FIX::CommCurrency);
    FIELD_SET_EX(std::string, CommCurrency);
    FIELD_SET(*this, FIX::FundRenewWaiv);
    FIELD_SET_EX(char, FundRenewWaiv);
    FIELD_SET(*this, FIX::OrderCapacity);
    FIELD_SET_EX(char, OrderCapacity);
    FIELD_SET(*this, FIX::OrderRestrictions);
    FIELD_SET(*this, FIX::CustOrderCapacity);
    FIELD_SET_EX(int, CustOrderCapacity);
    FIELD_SET(*this, FIX::ForexReq);
    FIELD_SET_EX(bool, ForexReq);
    FIELD_SET(*this, FIX::SettlCurrency);
    FIELD_SET_EX(std::string, SettlCurrency);
    FIELD_SET(*this, FIX::BookingType);
    FIELD_SET_EX(int, BookingType);
    FIELD_SET(*this, FIX::Text);
    FIELD_SET_EX(std::string, Text);
    FIELD_SET(*this, FIX::EncodedTextLen);
    FIELD_SET_EX(int, EncodedTextLen);
    FIELD_SET(*this, FIX::EncodedText);
    FIELD_SET_EX(std::string, EncodedText);
    FIELD_SET(*this, FIX::PositionEffect);
    FIELD_SET_EX(char, PositionEffect);
    FIELD_SET(*this, FIX::CoveredOrUncovered);
    FIELD_SET_EX(int, CoveredOrUncovered);
    FIELD_SET(*this, FIX::MaxShow);
    FIELD_SET_EX(double, MaxShow);
    FIELD_SET(*this, FIX::PegOffsetValue);
    FIELD_SET_EX(double, PegOffsetValue);
    FIELD_SET(*this, FIX::PegMoveType);
    FIELD_SET_EX(int, PegMoveType);
    FIELD_SET(*this, FIX::PegOffsetType);
    FIELD_SET_EX(int, PegOffsetType);
    FIELD_SET(*this, FIX::PegLimitType);
    FIELD_SET_EX(int, PegLimitType);
    FIELD_SET(*this, FIX::PegRoundDirection);
    FIELD_SET_EX(int, PegRoundDirection);
    FIELD_SET(*this, FIX::PegScope);
    FIELD_SET_EX(int, PegScope);
    FIELD_SET(*this, FIX::DiscretionInst);
    FIELD_SET_EX(char, DiscretionInst);
    FIELD_SET(*this, FIX::DiscretionOffsetValue);
    FIELD_SET_EX(double, DiscretionOffsetValue);
    FIELD_SET(*this, FIX::DiscretionMoveType);
    FIELD_SET_EX(int, DiscretionMoveType);
    FIELD_SET(*this, FIX::DiscretionOffsetType);
    FIELD_SET_EX(int, DiscretionOffsetType);
    FIELD_SET(*this, FIX::DiscretionLimitType);
    FIELD_SET_EX(int, DiscretionLimitType);
    FIELD_SET(*this, FIX::DiscretionRoundDirection);
    FIELD_SET_EX(int, DiscretionRoundDirection);
    FIELD_SET(*this, FIX::DiscretionScope);
    FIELD_SET_EX(int, DiscretionScope);
    FIELD_SET(*this, FIX::TargetStrategy);
    FIELD_SET_EX(int, TargetStrategy);
    FIELD_SET(*this, FIX::TargetStrategyParameters);
    FIELD_SET_EX(std::string, TargetStrategyParameters);
    FIELD_SET(*this, FIX::ParticipationRate);
    FIELD_SET_EX(double, ParticipationRate);
    FIELD_SET(*this, FIX::CancellationRights);
    FIELD_SET_EX(char, CancellationRights);
    FIELD_SET(*this, FIX::MoneyLaunderingStatus);
    FIELD_SET_EX(char, MoneyLaunderingStatus);
    FIELD_SET(*this, FIX::RegistID);
    FIELD_SET_EX(std::string, RegistID);
    FIELD_SET(*this, FIX::Designation);
    FIELD_SET_EX(std::string, Designation);
    FIELD_SET(*this, FIX::MultiLegRptTypeReq);
    FIELD_SET_EX(int, MultiLegRptTypeReq);
    FIELD_SET(*this, FIX::NoAllocs);
    FIELD_SET_EX(int, NoAllocs);
    class NoAllocs: public FIX::Group
    {
    public:
    NoAllocs() : FIX::Group(78,79,FIX::message_order(79,661,736,467,948,80,0)) {}
      FIELD_SET(*this, FIX::AllocAccount);
      FIELD_SET_EX(std::string, AllocAccount);
      FIELD_SET(*this, FIX::AllocAcctIDSource);
      FIELD_SET_EX(int, AllocAcctIDSource);
      FIELD_SET(*this, FIX::AllocSettlCurrency);
      FIELD_SET_EX(std::string, AllocSettlCurrency);
      FIELD_SET(*this, FIX::IndividualAllocID);
      FIELD_SET_EX(std::string, IndividualAllocID);
      FIELD_SET(*this, FIX::NoNested3PartyIDs);
      FIELD_SET_EX(int, NoNested3PartyIDs);
      class NoNested3PartyIDs: public FIX::Group
      {
      public:
      NoNested3PartyIDs() : FIX::Group(948,949,FIX::message_order(949,950,951,952,0)) {}
        FIELD_SET(*this, FIX::Nested3PartyID);
        FIELD_SET_EX(std::string, Nested3PartyID);
        FIELD_SET(*this, FIX::Nested3PartyIDSource);
        FIELD_SET_EX(char, Nested3PartyIDSource);
        FIELD_SET(*this, FIX::Nested3PartyRole);
        FIELD_SET_EX(int, Nested3PartyRole);
        FIELD_SET(*this, FIX::NoNested3PartySubIDs);
        FIELD_SET_EX(int, NoNested3PartySubIDs);
        class NoNested3PartySubIDs: public FIX::Group
        {
        public:
        NoNested3PartySubIDs() : FIX::Group(952,953,FIX::message_order(953,954,0)) {}
          FIELD_SET(*this, FIX::Nested3PartySubID);
          FIELD_SET_EX(std::string, Nested3PartySubID);
          FIELD_SET(*this, FIX::Nested3PartySubIDType);
          FIELD_SET_EX(int, Nested3PartySubIDType);
        };
      };
      FIELD_SET(*this, FIX::AllocQty);
      FIELD_SET_EX(double, AllocQty);
    };
    FIELD_SET(*this, FIX::NoTradingSessions);
    FIELD_SET_EX(int, NoTradingSessions);
    class NoTradingSessions: public FIX::Group
    {
    public:
    NoTradingSessions() : FIX::Group(386,336,FIX::message_order(336,625,0)) {}
      FIELD_SET(*this, FIX::TradingSessionID);
      FIELD_SET_EX(std::string, TradingSessionID);
      FIELD_SET(*this, FIX::TradingSessionSubID);
      FIELD_SET_EX(std::string, TradingSessionSubID);
    };
    FIELD_SET(*this, FIX::NoUnderlyings);
    FIELD_SET_EX(int, NoUnderlyings);
    class NoUnderlyings: public FIX::Group
    {
    public:
    NoUnderlyings() : FIX::Group(711,311,FIX::message_order(311,312,309,305,457,462,463,310,763,313,542,241,242,243,244,245,246,256,595,592,593,594,247,316,941,317,436,435,308,306,362,363,307,364,365,877,878,318,879,810,882,883,884,885,886,0)) {}
      FIELD_SET(*this, FIX::UnderlyingSymbol);
      FIELD_SET_EX(std::string, UnderlyingSymbol);
      FIELD_SET(*this, FIX::UnderlyingSymbolSfx);
      FIELD_SET_EX(std::string, UnderlyingSymbolSfx);
      FIELD_SET(*this, FIX::UnderlyingSecurityID);
      FIELD_SET_EX(std::string, UnderlyingSecurityID);
      FIELD_SET(*this, FIX::UnderlyingSecurityIDSource);
      FIELD_SET_EX(std::string, UnderlyingSecurityIDSource);
      FIELD_SET(*this, FIX::UnderlyingProduct);
      FIELD_SET_EX(int, UnderlyingProduct);
      FIELD_SET(*this, FIX::UnderlyingCFICode);
      FIELD_SET_EX(std::string, UnderlyingCFICode);
      FIELD_SET(*this, FIX::UnderlyingSecurityType);
      FIELD_SET_EX(std::string, UnderlyingSecurityType);
      FIELD_SET(*this, FIX::UnderlyingSecuritySubType);
      FIELD_SET_EX(std::string, UnderlyingSecuritySubType);
      FIELD_SET(*this, FIX::UnderlyingMaturityMonthYear);
      FIELD_SET(*this, FIX::UnderlyingMaturityDate);
      FIELD_SET_EX(std::string, UnderlyingMaturityDate);
      FIELD_SET(*this, FIX::UnderlyingCouponPaymentDate);
      FIELD_SET_EX(std::string, UnderlyingCouponPaymentDate);
      FIELD_SET(*this, FIX::UnderlyingIssueDate);
      FIELD_SET_EX(std::string, UnderlyingIssueDate);
      FIELD_SET(*this, FIX::UnderlyingRepoCollateralSecurityType);
      FIELD_SET_EX(int, UnderlyingRepoCollateralSecurityType);
      FIELD_SET(*this, FIX::UnderlyingRepurchaseTerm);
      FIELD_SET_EX(int, UnderlyingRepurchaseTerm);
      FIELD_SET(*this, FIX::UnderlyingRepurchaseRate);
      FIELD_SET_EX(double, UnderlyingRepurchaseRate);
      FIELD_SET(*this, FIX::UnderlyingFactor);
      FIELD_SET_EX(double, UnderlyingFactor);
      FIELD_SET(*this, FIX::UnderlyingCreditRating);
      FIELD_SET_EX(std::string, UnderlyingCreditRating);
      FIELD_SET(*this, FIX::UnderlyingInstrRegistry);
      FIELD_SET_EX(std::string, UnderlyingInstrRegistry);
      FIELD_SET(*this, FIX::UnderlyingCountryOfIssue);
      FIELD_SET(*this, FIX::UnderlyingStateOrProvinceOfIssue);
      FIELD_SET_EX(std::string, UnderlyingStateOrProvinceOfIssue);
      FIELD_SET(*this, FIX::UnderlyingLocaleOfIssue);
      FIELD_SET_EX(std::string, UnderlyingLocaleOfIssue);
      FIELD_SET(*this, FIX::UnderlyingRedemptionDate);
      FIELD_SET_EX(std::string, UnderlyingRedemptionDate);
      FIELD_SET(*this, FIX::UnderlyingStrikePrice);
      FIELD_SET_EX(double, UnderlyingStrikePrice);
      FIELD_SET(*this, FIX::UnderlyingStrikeCurrency);
      FIELD_SET_EX(std::string, UnderlyingStrikeCurrency);
      FIELD_SET(*this, FIX::UnderlyingOptAttribute);
      FIELD_SET_EX(char, UnderlyingOptAttribute);
      FIELD_SET(*this, FIX::UnderlyingContractMultiplier);
      FIELD_SET_EX(double, UnderlyingContractMultiplier);
      FIELD_SET(*this, FIX::UnderlyingCouponRate);
      FIELD_SET_EX(double, UnderlyingCouponRate);
      FIELD_SET(*this, FIX::UnderlyingSecurityExchange);
      FIELD_SET(*this, FIX::UnderlyingIssuer);
      FIELD_SET_EX(std::string, UnderlyingIssuer);
      FIELD_SET(*this, FIX::EncodedUnderlyingIssuerLen);
      FIELD_SET_EX(int, EncodedUnderlyingIssuerLen);
      FIELD_SET(*this, FIX::EncodedUnderlyingIssuer);
      FIELD_SET_EX(std::string, EncodedUnderlyingIssuer);
      FIELD_SET(*this, FIX::UnderlyingSecurityDesc);
      FIELD_SET_EX(std::string, UnderlyingSecurityDesc);
      FIELD_SET(*this, FIX::EncodedUnderlyingSecurityDescLen);
      FIELD_SET_EX(int, EncodedUnderlyingSecurityDescLen);
      FIELD_SET(*this, FIX::EncodedUnderlyingSecurityDesc);
      FIELD_SET_EX(std::string, EncodedUnderlyingSecurityDesc);
      FIELD_SET(*this, FIX::UnderlyingCPProgram);
      FIELD_SET_EX(std::string, UnderlyingCPProgram);
      FIELD_SET(*this, FIX::UnderlyingCPRegType);
      FIELD_SET_EX(std::string, UnderlyingCPRegType);
      FIELD_SET(*this, FIX::UnderlyingCurrency);
      FIELD_SET_EX(std::string, UnderlyingCurrency);
      FIELD_SET(*this, FIX::UnderlyingQty);
      FIELD_SET_EX(double, UnderlyingQty);
      FIELD_SET(*this, FIX::UnderlyingPx);
      FIELD_SET_EX(double, UnderlyingPx);
      FIELD_SET(*this, FIX::UnderlyingDirtyPrice);
      FIELD_SET_EX(double, UnderlyingDirtyPrice);
      FIELD_SET(*this, FIX::UnderlyingEndPrice);
      FIELD_SET_EX(double, UnderlyingEndPrice);
      FIELD_SET(*this, FIX::UnderlyingStartValue);
      FIELD_SET_EX(double, UnderlyingStartValue);
      FIELD_SET(*this, FIX::UnderlyingCurrentValue);
      FIELD_SET_EX(double, UnderlyingCurrentValue);
      FIELD_SET(*this, FIX::UnderlyingEndValue);
      FIELD_SET_EX(double, UnderlyingEndValue);
      FIELD_SET(*this, FIX::NoUnderlyingSecurityAltID);
      FIELD_SET_EX(int, NoUnderlyingSecurityAltID);
      class NoUnderlyingSecurityAltID: public FIX::Group
      {
      public:
      NoUnderlyingSecurityAltID() : FIX::Group(457,458,FIX::message_order(458,459,0)) {}
        FIELD_SET(*this, FIX::UnderlyingSecurityAltID);
        FIELD_SET_EX(std::string, UnderlyingSecurityAltID);
        FIELD_SET(*this, FIX::UnderlyingSecurityAltIDSource);
        FIELD_SET_EX(std::string, UnderlyingSecurityAltIDSource);
      };
    };
    FIELD_SET(*this, FIX::NoLegs);
    FIELD_SET_EX(int, NoLegs);
    class NoLegs: public FIX::Group
    {
    public:
    NoLegs() : FIX::Group(555,600,FIX::message_order(600,601,602,603,604,607,608,609,764,610,611,248,249,250,251,252,253,257,599,596,597,598,254,612,942,613,614,615,616,617,618,619,620,621,622,623,624,556,740,739,955,956,687,690,683,670,564,565,539,654,566,587,588,0)) {}
      FIELD_SET(*this, FIX::LegSymbol);
      FIELD_SET_EX(std::string, LegSymbol);
      FIELD_SET(*this, FIX::LegSymbolSfx);
      FIELD_SET_EX(std::string, LegSymbolSfx);
      FIELD_SET(*this, FIX::LegSecurityID);
      FIELD_SET_EX(std::string, LegSecurityID);
      FIELD_SET(*this, FIX::LegSecurityIDSource);
      FIELD_SET_EX(std::string, LegSecurityIDSource);
      FIELD_SET(*this, FIX::LegProduct);
      FIELD_SET_EX(int, LegProduct);
      FIELD_SET(*this, FIX::LegCFICode);
      FIELD_SET_EX(std::string, LegCFICode);
      FIELD_SET(*this, FIX::LegSecurityType);
      FIELD_SET_EX(std::string, LegSecurityType);
      FIELD_SET(*this, FIX::LegSecuritySubType);
      FIELD_SET_EX(std::string, LegSecuritySubType);
      FIELD_SET(*this, FIX::LegMaturityMonthYear);
      FIELD_SET(*this, FIX::LegMaturityDate);
      FIELD_SET_EX(std::string, LegMaturityDate);
      FIELD_SET(*this, FIX::LegCouponPaymentDate);
      FIELD_SET_EX(std::string, LegCouponPaymentDate);
      FIELD_SET(*this, FIX::LegIssueDate);
      FIELD_SET_EX(std::string, LegIssueDate);
      FIELD_SET(*this, FIX::LegRepoCollateralSecurityType);
      FIELD_SET_EX(int, LegRepoCollateralSecurityType);
      FIELD_SET(*this, FIX::LegRepurchaseTerm);
      FIELD_SET_EX(int, LegRepurchaseTerm);
      FIELD_SET(*this, FIX::LegRepurchaseRate);
      FIELD_SET_EX(double, LegRepurchaseRate);
      FIELD_SET(*this, FIX::LegFactor);
      FIELD_SET_EX(double, LegFactor);
      FIELD_SET(*this, FIX::LegCreditRating);
      FIELD_SET_EX(std::string, LegCreditRating);
      FIELD_SET(*this, FIX::LegInstrRegistry);
      FIELD_SET_EX(std::string, LegInstrRegistry);
      FIELD_SET(*this, FIX::LegCountryOfIssue);
      FIELD_SET(*this, FIX::LegStateOrProvinceOfIssue);
      FIELD_SET_EX(std::string, LegStateOrProvinceOfIssue);
      FIELD_SET(*this, FIX::LegLocaleOfIssue);
      FIELD_SET_EX(std::string, LegLocaleOfIssue);
      FIELD_SET(*this, FIX::LegRedemptionDate);
      FIELD_SET_EX(std::string, LegRedemptionDate);
      FIELD_SET(*this, FIX::LegStrikePrice);
      FIELD_SET_EX(double, LegStrikePrice);
      FIELD_SET(*this, FIX::LegStrikeCurrency);
      FIELD_SET_EX(std::string, LegStrikeCurrency);
      FIELD_SET(*this, FIX::LegOptAttribute);
      FIELD_SET_EX(char, LegOptAttribute);
      FIELD_SET(*this, FIX::LegContractMultiplier);
      FIELD_SET_EX(double, LegContractMultiplier);
      FIELD_SET(*this, FIX::LegCouponRate);
      FIELD_SET_EX(double, LegCouponRate);
      FIELD_SET(*this, FIX::LegSecurityExchange);
      FIELD_SET(*this, FIX::LegIssuer);
      FIELD_SET_EX(std::string, LegIssuer);
      FIELD_SET(*this, FIX::EncodedLegIssuerLen);
      FIELD_SET_EX(int, EncodedLegIssuerLen);
      FIELD_SET(*this, FIX::EncodedLegIssuer);
      FIELD_SET_EX(std::string, EncodedLegIssuer);
      FIELD_SET(*this, FIX::LegSecurityDesc);
      FIELD_SET_EX(std::string, LegSecurityDesc);
      FIELD_SET(*this, FIX::EncodedLegSecurityDescLen);
      FIELD_SET_EX(int, EncodedLegSecurityDescLen);
      FIELD_SET(*this, FIX::EncodedLegSecurityDesc);
      FIELD_SET_EX(std::string, EncodedLegSecurityDesc);
      FIELD_SET(*this, FIX::LegRatioQty);
      FIELD_SET_EX(double, LegRatioQty);
      FIELD_SET(*this, FIX::LegSide);
      FIELD_SET_EX(char, LegSide);
      FIELD_SET(*this, FIX::LegCurrency);
      FIELD_SET_EX(std::string, LegCurrency);
      FIELD_SET(*this, FIX::LegPool);
      FIELD_SET_EX(std::string, LegPool);
      FIELD_SET(*this, FIX::LegDatedDate);
      FIELD_SET_EX(std::string, LegDatedDate);
      FIELD_SET(*this, FIX::LegContractSettlMonth);
      FIELD_SET(*this, FIX::LegInterestAccrualDate);
      FIELD_SET_EX(std::string, LegInterestAccrualDate);
      FIELD_SET(*this, FIX::NoLegSecurityAltID);
      FIELD_SET_EX(std::string, NoLegSecurityAltID);
      class NoLegSecurityAltID: public FIX::Group
      {
      public:
      NoLegSecurityAltID() : FIX::Group(604,605,FIX::message_order(605,606,0)) {}
        FIELD_SET(*this, FIX::LegSecurityAltID);
        FIELD_SET_EX(std::string, LegSecurityAltID);
        FIELD_SET(*this, FIX::LegSecurityAltIDSource);
        FIELD_SET_EX(std::string, LegSecurityAltIDSource);
      };
      FIELD_SET(*this, FIX::LegQty);
      FIELD_SET_EX(double, LegQty);
      FIELD_SET(*this, FIX::LegSwapType);
      FIELD_SET_EX(int, LegSwapType);
      FIELD_SET(*this, FIX::NoLegStipulations);
      FIELD_SET_EX(int, NoLegStipulations);
      class NoLegStipulations: public FIX::Group
      {
      public:
      NoLegStipulations() : FIX::Group(683,688,FIX::message_order(688,689,0)) {}
        FIELD_SET(*this, FIX::LegStipulationType);
        FIELD_SET_EX(std::string, LegStipulationType);
        FIELD_SET(*this, FIX::LegStipulationValue);
        FIELD_SET_EX(std::string, LegStipulationValue);
      };
      FIELD_SET(*this, FIX::LegPositionEffect);
      FIELD_SET_EX(char, LegPositionEffect);
      FIELD_SET(*this, FIX::LegCoveredOrUncovered);
      FIELD_SET_EX(int, LegCoveredOrUncovered);
      FIELD_SET(*this, FIX::NoNestedPartyIDs);
      FIELD_SET_EX(int, NoNestedPartyIDs);
      class NoNestedPartyIDs: public FIX::Group
      {
      public:
      NoNestedPartyIDs() : FIX::Group(539,524,FIX::message_order(524,525,538,804,0)) {}
        FIELD_SET(*this, FIX::NestedPartyID);
        FIELD_SET_EX(std::string, NestedPartyID);
        FIELD_SET(*this, FIX::NestedPartyIDSource);
        FIELD_SET_EX(char, NestedPartyIDSource);
        FIELD_SET(*this, FIX::NestedPartyRole);
        FIELD_SET_EX(int, NestedPartyRole);
        FIELD_SET(*this, FIX::NoNestedPartySubIDs);
        FIELD_SET_EX(int, NoNestedPartySubIDs);
        class NoNestedPartySubIDs: public FIX::Group
        {
        public:
        NoNestedPartySubIDs() : FIX::Group(804,545,FIX::message_order(545,805,0)) {}
          FIELD_SET(*this, FIX::NestedPartySubID);
          FIELD_SET_EX(std::string, NestedPartySubID);
          FIELD_SET(*this, FIX::NestedPartySubIDType);
          FIELD_SET_EX(int, NestedPartySubIDType);
        };
      };
      FIELD_SET(*this, FIX::LegRefID);
      FIELD_SET_EX(std::string, LegRefID);
      FIELD_SET(*this, FIX::LegPrice);
      FIELD_SET_EX(double, LegPrice);
      FIELD_SET(*this, FIX::LegSettlType);
      FIELD_SET_EX(char, LegSettlType);
      FIELD_SET(*this, FIX::LegSettlDate);
      FIELD_SET_EX(std::string, LegSettlDate);
      FIELD_SET(*this, FIX::NoLegAllocs);
      FIELD_SET_EX(int, NoLegAllocs);
      class NoLegAllocs: public FIX::Group
      {
      public:
      NoLegAllocs() : FIX::Group(670,671,FIX::message_order(671,672,756,673,674,675,0)) {}
        FIELD_SET(*this, FIX::LegAllocAccount);
        FIELD_SET_EX(std::string, LegAllocAccount);
        FIELD_SET(*this, FIX::LegIndividualAllocID);
        FIELD_SET_EX(std::string, LegIndividualAllocID);
        FIELD_SET(*this, FIX::NoNested2PartyIDs);
        FIELD_SET_EX(int, NoNested2PartyIDs);
        class NoNested2PartyIDs: public FIX::Group
        {
        public:
        NoNested2PartyIDs() : FIX::Group(756,757,FIX::message_order(757,758,759,806,0)) {}
          FIELD_SET(*this, FIX::Nested2PartyID);
          FIELD_SET_EX(std::string, Nested2PartyID);
          FIELD_SET(*this, FIX::Nested2PartyIDSource);
          FIELD_SET_EX(char, Nested2PartyIDSource);
          FIELD_SET(*this, FIX::Nested2PartyRole);
          FIELD_SET_EX(int, Nested2PartyRole);
          FIELD_SET(*this, FIX::NoNested2PartySubIDs);
          FIELD_SET_EX(int, NoNested2PartySubIDs);
          class NoNested2PartySubIDs: public FIX::Group
          {
          public:
          NoNested2PartySubIDs() : FIX::Group(806,760,FIX::message_order(760,807,0)) {}
            FIELD_SET(*this, FIX::Nested2PartySubID);
            FIELD_SET_EX(std::string, Nested2PartySubID);
            FIELD_SET(*this, FIX::Nested2PartySubIDType);
            FIELD_SET_EX(int, Nested2PartySubIDType);
          };
        };
        FIELD_SET(*this, FIX::LegAllocQty);
        FIELD_SET_EX(double, LegAllocQty);
        FIELD_SET(*this, FIX::LegAllocAcctIDSource);
        FIELD_SET_EX(std::string, LegAllocAcctIDSource);
        FIELD_SET(*this, FIX::LegSettlCurrency);
        FIELD_SET_EX(std::string, LegSettlCurrency);
      };
    };
  };

}

#endif
