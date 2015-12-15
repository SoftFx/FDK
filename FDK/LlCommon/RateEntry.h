#pragma once

#include "PriceEntry.h"
#include "SymbolEntry.h"


namespace FDK
{
	class CRateEntry
	{
	public:
		const CSymbolEntry* Symbol;
		Nullable<CPriceEntry> Price;
	public:
		inline CRateEntry()
            : Symbol()
		{
		}
	};
}