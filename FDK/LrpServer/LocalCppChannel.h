#pragma once
#include "LocalCppServer.h"
#include "LocalCppLibrary.h"
#include "LocalChannelsPool.h"


class CLocalCppChannel
{
public:
	static CLocalCppServer& GetLocalServer();
	static CLocalCppLibrary& GetLibrary();
	static CLocalChannelsPool& GetLocalChannelsPool();
};