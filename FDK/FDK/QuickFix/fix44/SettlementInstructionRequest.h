#ifndef FIX44_SETTLEMENTINSTRUCTIONREQUEST_H
#define FIX44_SETTLEMENTINSTRUCTIONREQUEST_H

#include "Message.h"

namespace FIX44
{

  class SettlementInstructionRequest : public Message
  {
  public:
    SettlementInstructionRequest() : Message(MsgType()) {}
    SettlementInstructionRequest(const FIX::Message& m) : Message(m) {}
    SettlementInstructionRequest(const Message& m) : Message(m) {}
    SettlementInstructionRequest(const SettlementInstructionRequest& m) : Message(m) {}
    static FIX::MsgType MsgType() { return FIX::MsgType("AV"); }

    SettlementInstructionRequest(
      const FIX::SettlInstReqID& aSettlInstReqID,
      const FIX::TransactTime& aTransactTime )
    : Message(MsgType())
    {
      set(aSettlInstReqID);
      set(aTransactTime);
    }

    FIELD_SET(*this, FIX::SettlInstReqID);
    FIELD_SET_EX(std::string, SettlInstReqID);
    FIELD_SET(*this, FIX::TransactTime);
    FIELD_SET_EX(FIX::UtcTimeStamp, TransactTime);
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
    FIELD_SET(*this, FIX::AllocAccount);
    FIELD_SET_EX(std::string, AllocAccount);
    FIELD_SET(*this, FIX::AllocAcctIDSource);
    FIELD_SET_EX(int, AllocAcctIDSource);
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
    FIELD_SET(*this, FIX::StandInstDbType);
    FIELD_SET_EX(int, StandInstDbType);
    FIELD_SET(*this, FIX::StandInstDbName);
    FIELD_SET_EX(std::string, StandInstDbName);
    FIELD_SET(*this, FIX::StandInstDbID);
    FIELD_SET_EX(std::string, StandInstDbID);
  };

}

#endif
