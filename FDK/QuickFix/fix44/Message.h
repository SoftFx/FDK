#ifndef FIX44_MESSAGES_H
#define FIX44_MESSAGES_H

#include "../Message.h"
#include "../Group.h"

namespace FIX44
{
  class Header : public FIX::Header
  {
  public:
    FIELD_SET(*this, FIX::BeginString);
    FIELD_SET_EX(std::string, BeginString);
    FIELD_SET(*this, FIX::BodyLength);
    FIELD_SET_EX(int, BodyLength);
    FIELD_SET(*this, FIX::MsgType);
    FIELD_SET_EX(std::string, MsgType);
    FIELD_SET(*this, FIX::SenderCompID);
    FIELD_SET_EX(std::string, SenderCompID);
    FIELD_SET(*this, FIX::TargetCompID);
    FIELD_SET_EX(std::string, TargetCompID);
    FIELD_SET(*this, FIX::OnBehalfOfCompID);
    FIELD_SET_EX(std::string, OnBehalfOfCompID);
    FIELD_SET(*this, FIX::DeliverToCompID);
    FIELD_SET_EX(std::string, DeliverToCompID);
    FIELD_SET(*this, FIX::SecureDataLen);
    FIELD_SET_EX(int, SecureDataLen);
    FIELD_SET(*this, FIX::SecureData);
    FIELD_SET_EX(std::string, SecureData);
    FIELD_SET(*this, FIX::MsgSeqNum);
    FIELD_SET(*this, FIX::SenderSubID);
    FIELD_SET_EX(std::string, SenderSubID);
    FIELD_SET(*this, FIX::SenderLocationID);
    FIELD_SET_EX(std::string, SenderLocationID);
    FIELD_SET(*this, FIX::TargetSubID);
    FIELD_SET_EX(std::string, TargetSubID);
    FIELD_SET(*this, FIX::TargetLocationID);
    FIELD_SET_EX(std::string, TargetLocationID);
    FIELD_SET(*this, FIX::OnBehalfOfSubID);
    FIELD_SET_EX(std::string, OnBehalfOfSubID);
    FIELD_SET(*this, FIX::OnBehalfOfLocationID);
    FIELD_SET_EX(std::string, OnBehalfOfLocationID);
    FIELD_SET(*this, FIX::DeliverToSubID);
    FIELD_SET_EX(std::string, DeliverToSubID);
    FIELD_SET(*this, FIX::DeliverToLocationID);
    FIELD_SET_EX(std::string, DeliverToLocationID);
    FIELD_SET(*this, FIX::PossDupFlag);
    FIELD_SET_EX(bool, PossDupFlag);
    FIELD_SET(*this, FIX::PossResend);
    FIELD_SET_EX(bool, PossResend);
    FIELD_SET(*this, FIX::SendingTime);
    FIELD_SET_EX(FIX::UtcTimeStamp, SendingTime);
    FIELD_SET(*this, FIX::OrigSendingTime);
    FIELD_SET_EX(FIX::UtcTimeStamp, OrigSendingTime);
    FIELD_SET(*this, FIX::XmlDataLen);
    FIELD_SET_EX(int, XmlDataLen);
    FIELD_SET(*this, FIX::XmlData);
    FIELD_SET_EX(std::string, XmlData);
    FIELD_SET(*this, FIX::MessageEncoding);
    FIELD_SET_EX(std::string, MessageEncoding);
    FIELD_SET(*this, FIX::LastMsgSeqNumProcessed);
    FIELD_SET(*this, FIX::NoHops);
    FIELD_SET_EX(int, NoHops);
    class NoHops: public FIX::Group
    {
    public:
    NoHops() : FIX::Group(627,628,FIX::message_order(628,629,630,0)) {}
      FIELD_SET(*this, FIX::HopCompID);
      FIELD_SET_EX(std::string, HopCompID);
      FIELD_SET(*this, FIX::HopSendingTime);
      FIELD_SET_EX(FIX::UtcTimeStamp, HopSendingTime);
      FIELD_SET(*this, FIX::HopRefID);
    };
  };

  class Trailer : public FIX::Trailer
  {
  public:
    FIELD_SET(*this, FIX::SignatureLength);
    FIELD_SET_EX(int, SignatureLength);
    FIELD_SET(*this, FIX::Signature);
    FIELD_SET_EX(std::string, Signature);
    FIELD_SET(*this, FIX::CheckSum);
    FIELD_SET_EX(int, CheckSum);
  };

  class Message : public FIX::Message
  {
  public:
    Message( const FIX::MsgType& msgtype )
    : FIX::Message(
      FIX::BeginString("FIX.4.4"), msgtype ) {}

    Message(const FIX::Message& m) : FIX::Message(m) {}
    Message(const Message& m) : FIX::Message(m) {}
    Header& getHeader() { return (Header&)m_header; }
    const Header& getHeader() const { return (Header&)m_header; }
    Trailer& getTrailer() { return (Trailer&)m_trailer; }
    const Trailer& getTrailer() const { return (Trailer&)m_trailer; }
  };

}

#endif
