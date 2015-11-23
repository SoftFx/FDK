#ifndef FIX44_SECURITYTYPES_H
#define FIX44_SECURITYTYPES_H

#include "Message.h"

namespace FIX44
{

  class SecurityTypes : public Message
  {
  public:
    SecurityTypes() : Message(MsgType()) {}
    SecurityTypes(const FIX::Message& m) : Message(m) {}
    SecurityTypes(const Message& m) : Message(m) {}
    SecurityTypes(const SecurityTypes& m) : Message(m) {}
    static FIX::MsgType MsgType() { return FIX::MsgType("w"); }

    SecurityTypes(
      const FIX::SecurityReqID& aSecurityReqID,
      const FIX::SecurityResponseID& aSecurityResponseID,
      const FIX::SecurityResponseType& aSecurityResponseType )
    : Message(MsgType())
    {
      set(aSecurityReqID);
      set(aSecurityResponseID);
      set(aSecurityResponseType);
    }

    FIELD_SET(*this, FIX::SecurityReqID);
    FIELD_SET_EX(std::string, SecurityReqID);
    FIELD_SET(*this, FIX::SecurityResponseID);
    FIELD_SET_EX(std::string, SecurityResponseID);
    FIELD_SET(*this, FIX::SecurityResponseType);
    FIELD_SET_EX(int, SecurityResponseType);
    FIELD_SET(*this, FIX::TotNoSecurityTypes);
    FIELD_SET_EX(int, TotNoSecurityTypes);
    FIELD_SET(*this, FIX::LastFragment);
    FIELD_SET_EX(bool, LastFragment);
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
    FIELD_SET(*this, FIX::NoSecurityTypes);
    FIELD_SET_EX(int, NoSecurityTypes);
    class NoSecurityTypes: public FIX::Group
    {
    public:
    NoSecurityTypes() : FIX::Group(558,167,FIX::message_order(167,762,460,461,0)) {}
      FIELD_SET(*this, FIX::SecurityType);
      FIELD_SET_EX(std::string, SecurityType);
      FIELD_SET(*this, FIX::SecuritySubType);
      FIELD_SET_EX(std::string, SecuritySubType);
      FIELD_SET(*this, FIX::Product);
      FIELD_SET_EX(int, Product);
      FIELD_SET(*this, FIX::CFICode);
      FIELD_SET_EX(std::string, CFICode);
    };
  };

}

#endif
