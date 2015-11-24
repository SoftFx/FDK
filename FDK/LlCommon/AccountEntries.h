#pragma once

#include "AccountEntry.h"

namespace FDK
{
	class CAccountEntries
	{
	public:
		typedef CAccountEntry value_type;
	public:
		LLCOMMON_API CAccountEntries();
		LLCOMMON_API CAccountEntries(const CAccountEntries& entries);
		LLCOMMON_API CAccountEntries& operator = (const CAccountEntries& entries);
		LLCOMMON_API ~CAccountEntries();
	public:
		#define LRP_STD_API LLCOMMON_API
		#include "LrpStdVector.h"
		#undef LRP_STD_API
	};
}