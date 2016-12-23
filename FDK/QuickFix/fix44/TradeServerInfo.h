#ifndef FIX44_TRADESERVERINFO_H
#define FIX44_TRADESERVERINFO_H

#include "Message.h"

namespace FIX44
{

  class TradeServerInfo : public Message
  {
  public:
    TradeServerInfo() : Message(MsgType()) {}
    TradeServerInfo(const FIX::Message& m) : Message(m) {}
    TradeServerInfo(const Message& m) : Message(m) {}
    TradeServerInfo(const TradeServerInfo& m) : Message(m) {}
    static FIX::MsgType MsgType() { return FIX::MsgType("U1023"); }

  };

}

#endif
