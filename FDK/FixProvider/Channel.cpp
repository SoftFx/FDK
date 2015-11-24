#include "stdafx.h"
#include "Channel.h"

CFixCodecImpl& CChannel::GetFixCodec()
{
	return *static_cast<CFixCodecImpl*>(nullptr);
}
