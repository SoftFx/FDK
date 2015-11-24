#include "stdafx.h"
#include "FxMessage.h"


CFxMessage::CFxMessage()
    : Type()
    , Data()
{
}

CFxMessage::CFxMessage(const CFxEventInfo& info)
    : Type()
    , Data()
{
	this->SendingTime = info.SendingTime;
	this->ReceivingTime = info.ReceivingTime;
}

CFxMessage::CFxMessage(int32 type, const CFxEventInfo& info)
    : Type(type)
    , Data()
{
	this->SendingTime = info.SendingTime;
	this->ReceivingTime = info.ReceivingTime;
}
