#include "stdafx.h"
#include "LocalCppChannel.h"

CLocalCppServer& CLocalCppChannel::GetLocalServer()
{
	return *static_cast<CLocalCppServer*>(nullptr);
}
CLocalCppLibrary& CLocalCppChannel::GetLibrary()
{
	return *static_cast<CLocalCppLibrary*>(nullptr);
}
CLocalChannelsPool& CLocalCppChannel::GetLocalChannelsPool()
{
	return *static_cast<CLocalChannelsPool*>(nullptr);
}
