#ifndef FIX44_ORDERMASSSTATUSREQUEST_H
#define FIX44_ORDERMASSSTATUSREQUEST_H

#include "Message.h"

namespace FIX44
{

  class OrderMassStatusRequest : public Message
  {
  public:
    OrderMassStatusRequest() : Message(MsgType()) {}
    OrderMassStatusRequest(const FIX::Message& m) : Message(m) {}
    OrderMassStatusRequest(const Message& m) : Message(m) {}
    OrderMassStatusRequest(const OrderMassStatusRequest& m) : Message(m) {}
    static FIX::MsgType MsgType() { return FIX::MsgType("AF"); }

    OrderMassStatusRequest(
      const FIX::MassStatusReqID& aMassStatusReqID,
      const FIX::MassStatusReqType& aMassStatusReqType )
    : Message(MsgType())
    {
      set(aMassStatusReqID);
      set(aMassStatusReqType);
    }

    FIELD_SET(*this, FIX::MassStatusReqID);
    FIELD_SET_EX(std::string, MassStatusReqID);
    FIELD_SET(*this, FIX::MassStatusReqType);
    FIELD_SET_EX(int, MassStatusReqType);
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
    FIELD_SET(*this, FIX::Account);
    FIELD_SET_EX(std::string, Account);
    FIELD_SET(*this, FIX::AcctIDSource);
    FIELD_SET_EX(int, AcctIDSource);
    FIELD_SET(*this, FIX::TradingSessionID);
    FIELD_SET_EX(std::string, TradingSessionID);
    FIELD_SET(*this, FIX::TradingSessionSubID);
    FIELD_SET_EX(std::string, TradingSessionSubID);
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
    FIELD_SET(*this, FIX::Side);
    FIELD_SET_EX(char, Side);
  };

}

#endif
