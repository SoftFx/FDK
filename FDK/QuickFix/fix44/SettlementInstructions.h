#ifndef FIX44_SETTLEMENTINSTRUCTIONS_H
#define FIX44_SETTLEMENTINSTRUCTIONS_H

#include "Message.h"

namespace FIX44
{

  class SettlementInstructions : public Message
  {
  public:
    SettlementInstructions() : Message(MsgType()) {}
    SettlementInstructions(const FIX::Message& m) : Message(m) {}
    SettlementInstructions(const Message& m) : Message(m) {}
    SettlementInstructions(const SettlementInstructions& m) : Message(m) {}
    static FIX::MsgType MsgType() { return FIX::MsgType("T"); }

    SettlementInstructions(
      const FIX::SettlInstMsgID& aSettlInstMsgID,
      const FIX::SettlInstMode& aSettlInstMode,
      const FIX::TransactTime& aTransactTime )
    : Message(MsgType())
    {
      set(aSettlInstMsgID);
      set(aSettlInstMode);
      set(aTransactTime);
    }

    FIELD_SET(*this, FIX::SettlInstMsgID);
    FIELD_SET_EX(std::string, SettlInstMsgID);
    FIELD_SET(*this, FIX::SettlInstReqID);
    FIELD_SET_EX(std::string, SettlInstReqID);
    FIELD_SET(*this, FIX::SettlInstMode);
    FIELD_SET_EX(char, SettlInstMode);
    FIELD_SET(*this, FIX::SettlInstReqRejCode);
    FIELD_SET_EX(int, SettlInstReqRejCode);
    FIELD_SET(*this, FIX::Text);
    FIELD_SET_EX(std::string, Text);
    FIELD_SET(*this, FIX::EncodedTextLen);
    FIELD_SET_EX(int, EncodedTextLen);
    FIELD_SET(*this, FIX::EncodedText);
    FIELD_SET_EX(std::string, EncodedText);
    FIELD_SET(*this, FIX::ClOrdID);
    FIELD_SET_EX(std::string, ClOrdID);
    FIELD_SET(*this, FIX::TransactTime);
    FIELD_SET_EX(FIX::UtcTimeStamp, TransactTime);
    FIELD_SET(*this, FIX::NoSettlInst);
    FIELD_SET_EX(int, NoSettlInst);
    class NoSettlInst: public FIX::Group
    {
    public:
    NoSettlInst() : FIX::Group(778,162,FIX::message_order(162,163,214,453,54,460,167,461,168,126,779,172,169,170,171,85,492,476,488,489,503,490,491,504,505,0)) {}
      FIELD_SET(*this, FIX::SettlInstID);
      FIELD_SET_EX(std::string, SettlInstID);
      FIELD_SET(*this, FIX::SettlInstTransType);
      FIELD_SET_EX(char, SettlInstTransType);
      FIELD_SET(*this, FIX::SettlInstRefID);
      FIELD_SET_EX(std::string, SettlInstRefID);
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
      FIELD_SET(*this, FIX::Side);
      FIELD_SET_EX(char, Side);
      FIELD_SET(*this, FIX::Product);
      FIELD_SET_EX(int, Product);
      FIELD_SET(*this, FIX::SecurityType);
      FIELD_SET_EX(std::string, SecurityType);
      FIELD_SET(*this, FIX::CFICode);
      FIELD_SET_EX(std::string, CFICode);
      FIELD_SET(*this, FIX::EffectiveTime);
      FIELD_SET_EX(FIX::UtcTimeStamp, EffectiveTime);
      FIELD_SET(*this, FIX::ExpireTime);
      FIELD_SET_EX(FIX::UtcTimeStamp, ExpireTime);
      FIELD_SET(*this, FIX::LastUpdateTime);
      FIELD_SET_EX(FIX::UtcTimeStamp, LastUpdateTime);
      FIELD_SET(*this, FIX::SettlDeliveryType);
      FIELD_SET_EX(int, SettlDeliveryType);
      FIELD_SET(*this, FIX::StandInstDbType);
      FIELD_SET_EX(int, StandInstDbType);
      FIELD_SET(*this, FIX::StandInstDbName);
      FIELD_SET_EX(std::string, StandInstDbName);
      FIELD_SET(*this, FIX::StandInstDbID);
      FIELD_SET_EX(std::string, StandInstDbID);
      FIELD_SET(*this, FIX::NoDlvyInst);
      FIELD_SET_EX(int, NoDlvyInst);
      class NoDlvyInst: public FIX::Group
      {
      public:
      NoDlvyInst() : FIX::Group(85,165,FIX::message_order(165,787,781,0)) {}
        FIELD_SET(*this, FIX::SettlInstSource);
        FIELD_SET_EX(char, SettlInstSource);
        FIELD_SET(*this, FIX::DlvyInstType);
        FIELD_SET_EX(char, DlvyInstType);
        FIELD_SET(*this, FIX::NoSettlPartyIDs);
        FIELD_SET_EX(int, NoSettlPartyIDs);
        class NoSettlPartyIDs: public FIX::Group
        {
        public:
        NoSettlPartyIDs() : FIX::Group(781,782,FIX::message_order(782,783,784,801,0)) {}
          FIELD_SET(*this, FIX::SettlPartyID);
          FIELD_SET_EX(std::string, SettlPartyID);
          FIELD_SET(*this, FIX::SettlPartyIDSource);
          FIELD_SET_EX(char, SettlPartyIDSource);
          FIELD_SET(*this, FIX::SettlPartyRole);
          FIELD_SET_EX(int, SettlPartyRole);
          FIELD_SET(*this, FIX::NoSettlPartySubIDs);
          FIELD_SET_EX(int, NoSettlPartySubIDs);
          class NoSettlPartySubIDs: public FIX::Group
          {
          public:
          NoSettlPartySubIDs() : FIX::Group(801,785,FIX::message_order(785,786,0)) {}
            FIELD_SET(*this, FIX::SettlPartySubID);
            FIELD_SET_EX(std::string, SettlPartySubID);
            FIELD_SET(*this, FIX::SettlPartySubIDType);
            FIELD_SET_EX(int, SettlPartySubIDType);
          };
        };
      };
      FIELD_SET(*this, FIX::PaymentMethod);
      FIELD_SET_EX(int, PaymentMethod);
      FIELD_SET(*this, FIX::PaymentRef);
      FIELD_SET_EX(std::string, PaymentRef);
      FIELD_SET(*this, FIX::CardHolderName);
      FIELD_SET_EX(std::string, CardHolderName);
      FIELD_SET(*this, FIX::CardNumber);
      FIELD_SET_EX(std::string, CardNumber);
      FIELD_SET(*this, FIX::CardStartDate);
      FIELD_SET_EX(std::string, CardStartDate);
      FIELD_SET(*this, FIX::CardExpDate);
      FIELD_SET_EX(std::string, CardExpDate);
      FIELD_SET(*this, FIX::CardIssNum);
      FIELD_SET_EX(std::string, CardIssNum);
      FIELD_SET(*this, FIX::PaymentDate);
      FIELD_SET_EX(std::string, PaymentDate);
      FIELD_SET(*this, FIX::PaymentRemitterID);
      FIELD_SET_EX(std::string, PaymentRemitterID);
    };
  };

}

#endif
