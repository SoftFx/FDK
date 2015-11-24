#pragma once

#include "TradeEntry.h"

namespace FDK
{
	class CTradeEntries
	{
	public:
		typedef CTradeEntry value_type;
	public:
		LLCOMMON_API CTradeEntries();
		LLCOMMON_API CTradeEntries(const CTradeEntries& entries);
		LLCOMMON_API CTradeEntries& operator = (const CTradeEntries& entries);
		LLCOMMON_API ~CTradeEntries();
	public:
#define LRP_STD_API LLCOMMON_API
	#include "LrpStdVector.h"
#undef LRP_STD_API
	};
}