#include "stdafx.h"
#include "SymbolToKey.h"

namespace
{
	CSymbolToKey gSymbolToKey;
}
namespace
{
	const char* cSymbols[] = 
	{
		"EURUSD",
		"EUR/USD",
		"EURAUD",
		"EUR/AUD",
		"USDCAD",
		"USD/CAD",
		"USDCHF",
		"USD/CHF",
		"EURCAD",
		"EUR/CAD",
		"CADJPY",
		"CAD/JPY",
		"EURJPY",
		"EUR/JPY",
		"GBPJPY",
		"GBP/JPY",
		"USDSEK",
		"USD/SEK",
		"USDNOK",
		"USD/NOK",
		"AUDUSD",
		"AUD/USD",
		"CHFJPY",
		"CHF/JPY",
		"GBPCHF",
		"GBP/CHF",
		"NZDUSD",
		"NZD/USD",
		"AUDNZD",
		"AUD/NZD",
		"EURCHF",
		"EUR/CHF",
		"GBPUSD",
		"GBP/USD",
		"USDJPY",
		"USD/JPY",
		"AUDJPY",
		"AUD/JPY",
		"EURGBP",
		"EUR/GBP",
		"EURNOK",
		"EUR/NOK",
		"EURSEK",
		"EUR/SEK",
		"AUDCHF",
		"AUD/CHF",
		"CADCHF",
		"CAD/CHF",
		"GBPAUD",
		"GBP/AUD",
		"GBPCAD",
		"GBP/CAD",
		"GBPNZD",
		"GBP/NZD",
		"NZDCAD",
		"NZD/CAD",
		"NZDCHF",
		"NZD/CHF",
		"NZDJPY",
		"NZD/JPY",
		"EURNZD",
		"EUR/NZD",
		"AUDCAD",
		"AUD/CAD",
		"USDRUB",
		"USD/RUB",
		"SGDJPY",
		"SGD/JPY",
		"EURDKK",
		"EUR/DKK",
		"USDTRY",
		"USD/TRY",
		"USDHKD",
		"USD/HKD",
		"EURTRY",
		"EUR/TRY",
		"USDSGD",
		"USD/SGD",
		"USDPLN",
		"USD/PLN",
		"HKDJPY",
		"HKD/JPY",
		"GBPSGD",
		"GBP/SGD",
		"NOKJPY",
		"NOK/JPY",
		"EURPLN",
		"EUR/PLN",
		"NZDSGD",
		"NZD/SGD",
		"NOKSEK",
		"NOK/SEK",
		"EURHKD",
		"EUR/HKD",
		"USDMXN",
		"USD/MXN",
		"USDDKK",
		"USD/DKK"
	};
}


CSymbolToKey::CSymbolToKey()
{
	m_static.max_load_factor(0.1F);
	m_dynamic.max_load_factor(0.1F);

	for each (const auto& element in cSymbols)
	{
		ptrdiff_t key = static_cast<ptrdiff_t>(m_static.size());
		m_static[element] = key;
	}
}
ptrdiff_t CSymbolToKey::GetKeyFromSymbol(const string& symbol)
{
	return gSymbolToKey.DoGetKeyFromSymbol(symbol);
}
ptrdiff_t CSymbolToKey::DoGetKeyFromSymbol(const string& symbol)
{
	{
		auto it = m_static.find(symbol);
		if (m_static.end() != it)
		{
			return it->second;
		}
	}
	{
		CSharedLocker lock(m_synchronizer);
		auto it = m_dynamic.find(symbol);
		if (m_dynamic.end() != it)
		{
			return it->second;
		}
	}
	CExclusiveLocker lock(m_synchronizer);
	auto it = m_dynamic.find(symbol);
	if (m_dynamic.end() != it)
	{
		return it->second;
	}
	ptrdiff_t result = static_cast<ptrdiff_t>(m_static.size() + m_dynamic.size());
	m_dynamic[symbol] = result;
	return result;
}
