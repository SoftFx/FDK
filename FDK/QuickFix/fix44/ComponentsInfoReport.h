#ifndef FIX44_COMPONENTSINFOREPORT_H
#define FIX44_COMPONENTSINFOREPORT_H

#include "Message.h"

namespace FIX44
{

  class ComponentsInfoReport : public Message
  {
  public:
    ComponentsInfoReport() : Message(MsgType()) {}
    ComponentsInfoReport(const FIX::Message& m) : Message(m) {}
    ComponentsInfoReport(const Message& m) : Message(m) {}
    ComponentsInfoReport(const ComponentsInfoReport& m) : Message(m) {}
    static FIX::MsgType MsgType() { return FIX::MsgType("U1019"); }

    ComponentsInfoReport(
      const FIX::CompReqID& aCompReqID )
    : Message(MsgType())
    {
      set(aCompReqID);
    }

    FIELD_SET(*this, FIX::CompReqID);
    FIELD_SET_EX(std::string, CompReqID);
    FIELD_SET(*this, FIX::ServerQuoteHistoryVersion);
    FIELD_SET_EX(int, ServerQuoteHistoryVersion);
  };

}

#endif
