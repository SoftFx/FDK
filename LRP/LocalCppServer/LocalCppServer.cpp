#include "stdafx.h"
#include "LocalCppServer.h"
#include "Channel.h"
#include "Level2.h"
typedef CChannel LrpChannel;
#include "TypesSerializer.hpp"
#include "Server.hpp"





extern "C"
{
	int __stdcall MarketBuy(const char* symbol, double price, double& volume, double& totalAmount)
	{
		totalAmount = price * volume;
		volume = 0;
		return 0;
	}
}