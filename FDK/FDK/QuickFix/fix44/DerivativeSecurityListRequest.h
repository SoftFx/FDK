#ifndef FIX44_DERIVATIVESECURITYLISTREQUEST_H
#define FIX44_DERIVATIVESECURITYLISTREQUEST_H

#include "Message.h"

namespace FIX44
{

  class DerivativeSecurityListRequest : public Message
  {
  public:
    DerivativeSecurityListRequest() : Message(MsgType()) {}
    DerivativeSecurityListRequest(const FIX::Message& m) : Message(m) {}
    DerivativeSecurityListRequest(const Message& m) : Message(m) {}
    DerivativeSecurityListRequest(const DerivativeSecurityListRequest& m) : Message(m) {}
    static FIX::MsgType MsgType() { return FIX::MsgType("z"); }

    DerivativeSecurityListRequest(
      const FIX::SecurityReqID& aSecurityReqID,
      const FIX::SecurityListRequestType& aSecurityListRequestType )
    : Message(MsgType())
    {
      set(aSecurityReqID);
      set(aSecurityListRequestType);
    }

    FIELD_SET(*this, FIX::SecurityReqID);
    FIELD_SET_EX(std::string, SecurityReqID);
    FIELD_SET(*this, FIX::SecurityListRequestType);
    FIELD_SET_EX(int, SecurityListRequestType);
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
    FIELD_SET(*this, FIX::SecuritySubType);
    FIELD_SET_EX(std::string, SecuritySubType);
    FIELD_SET(*this, FIX::Currency);
    FIELD_SET_EX(std::string, Currency);
    FIELD_SET(*this, FIX::Text);
    FIELD_SET_EX(std::string, Text);
    FIELD_SET(*this, FIX::EncodedTextLen);
    FIELD_SET_EX(int, EncodedTextLen);
    FIELD_SET(*this, FIX::EncodedText);
    FIELD_SET_EX(std::string, EncodedText);
    FIELD_SET(*this, FIX::TradingSessionID);
    FIELD_SET_EX(std::string, TradingSessionID);
    FIELD_SET(*this, FIX::TradingSessionSubID);
    FIELD_SET_EX(std::string, TradingSessionSubID);
    FIELD_SET(*this, FIX::SubscriptionRequestType);
    FIELD_SET_EX(char, SubscriptionRequestType);
  };

}

#endif
