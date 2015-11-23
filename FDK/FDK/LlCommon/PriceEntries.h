#pragma once

#include "PriceEntry.h"

namespace FDK
{
	class CPriceEntries
	{
	public:
		LLCOMMON_API CPriceEntries();
		LLCOMMON_API CPriceEntries(const CPriceEntries& entries);
		LLCOMMON_API CPriceEntries& operator = (const CPriceEntries& entries);
		LLCOMMON_API ~CPriceEntries();
	public:
		inline void Update(const std::string& symbol, const double bid, const double ask)
		{
			Update(symbol.c_str(), bid, ask);
		}
		LLCOMMON_API void Update(const char* symbol, const double bid, const double ask);
		inline void Remove(const std::string& symbol)
		{
			Remove(symbol.c_str());
		}
		LLCOMMON_API void Remove(const char* symbol);
		LLCOMMON_API void Clear();
		inline Nullable<CPriceEntry> TryGetPriceEntry(const std::string& symbol) const { return TryGetPriceEntry(symbol.c_str()); }
		LLCOMMON_API Nullable<CPriceEntry> TryGetPriceEntry(const char* symbol) const;
	internal:
		const LrpMap(string, CPriceEntry)& GetEntries() const;
	private:
		LrpMap(string, CPriceEntry) m_entries;
	};
}