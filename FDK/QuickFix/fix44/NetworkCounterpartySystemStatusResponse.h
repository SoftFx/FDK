#ifndef FIX44_NETWORKCOUNTERPARTYSYSTEMSTATUSRESPONSE_H
#define FIX44_NETWORKCOUNTERPARTYSYSTEMSTATUSRESPONSE_H

#include "Message.h"

namespace FIX44
{

  class NetworkCounterpartySystemStatusResponse : public Message
  {
  public:
    NetworkCounterpartySystemStatusResponse() : Message(MsgType()) {}
    NetworkCounterpartySystemStatusResponse(const FIX::Message& m) : Message(m) {}
    NetworkCounterpartySystemStatusResponse(const Message& m) : Message(m) {}
    NetworkCounterpartySystemStatusResponse(const NetworkCounterpartySystemStatusResponse& m) : Message(m) {}
    static FIX::MsgType MsgType() { return FIX::MsgType("BD"); }

    NetworkCounterpartySystemStatusResponse(
      const FIX::NetworkStatusResponseType& aNetworkStatusResponseType,
      const FIX::NetworkResponseID& aNetworkResponseID )
    : Message(MsgType())
    {
      set(aNetworkStatusResponseType);
      set(aNetworkResponseID);
    }

    FIELD_SET(*this, FIX::NetworkStatusResponseType);
    FIELD_SET_EX(int, NetworkStatusResponseType);
    FIELD_SET(*this, FIX::NetworkRequestID);
    FIELD_SET_EX(std::string, NetworkRequestID);
    FIELD_SET(*this, FIX::NetworkResponseID);
    FIELD_SET_EX(std::string, NetworkResponseID);
    FIELD_SET(*this, FIX::LastNetworkResponseID);
    FIELD_SET_EX(std::string, LastNetworkResponseID);
    FIELD_SET(*this, FIX::NoCompIDs);
    FIELD_SET_EX(int, NoCompIDs);
    class NoCompIDs: public FIX::Group
    {
    public:
    NoCompIDs() : FIX::Group(936,930,FIX::message_order(930,931,283,284,928,929,0)) {}
      FIELD_SET(*this, FIX::RefCompID);
      FIELD_SET_EX(std::string, RefCompID);
      FIELD_SET(*this, FIX::RefSubID);
      FIELD_SET_EX(std::string, RefSubID);
      FIELD_SET(*this, FIX::LocationID);
      FIELD_SET_EX(std::string, LocationID);
      FIELD_SET(*this, FIX::DeskID);
      FIELD_SET_EX(std::string, DeskID);
      FIELD_SET(*this, FIX::StatusValue);
      FIELD_SET_EX(int, StatusValue);
      FIELD_SET(*this, FIX::StatusText);
      FIELD_SET_EX(std::string, StatusText);
    };
  };

}

#endif