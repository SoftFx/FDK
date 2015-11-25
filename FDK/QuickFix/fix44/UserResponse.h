#ifndef FIX44_USERRESPONSE_H
#define FIX44_USERRESPONSE_H

#include "Message.h"

namespace FIX44
{

  class UserResponse : public Message
  {
  public:
    UserResponse() : Message(MsgType()) {}
    UserResponse(const FIX::Message& m) : Message(m) {}
    UserResponse(const Message& m) : Message(m) {}
    UserResponse(const UserResponse& m) : Message(m) {}
    static FIX::MsgType MsgType() { return FIX::MsgType("BF"); }

    UserResponse(
      const FIX::UserRequestID& aUserRequestID,
      const FIX::Username& aUsername )
    : Message(MsgType())
    {
      set(aUserRequestID);
      set(aUsername);
    }

    FIELD_SET(*this, FIX::UserRequestID);
    FIELD_SET_EX(std::string, UserRequestID);
    FIELD_SET(*this, FIX::Username);
    FIELD_SET_EX(std::string, Username);
    FIELD_SET(*this, FIX::UserStatus);
    FIELD_SET_EX(int, UserStatus);
    FIELD_SET(*this, FIX::UserStatusText);
    FIELD_SET_EX(std::string, UserStatusText);
  };

}

#endif