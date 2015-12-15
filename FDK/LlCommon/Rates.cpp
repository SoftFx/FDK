#include "stdafx.h"
#include "Rates.h"
#include "SymbolEntries.h"
#include "PriceEntries.h"
#include "RateEntry.h"
#include "CurrencyEntries.h"

namespace FDK
{
	CRates::CRates()
        : m_handler()
        , m_userData()
        , m_prices()
        , m_size()
        , m_table()
        , m_data()
	{
	}

	CRates::CRates(const CRates& rates)
        : m_handler(rates.m_handler)
        , m_userData(rates.m_userData)
        , m_prices()
        , m_size()
        , m_table()
        , m_data()
	{
	}

	CRates& CRates::operator = (const CRates& rates)
	{
		if (this != &rates)
		{
			m_handler = rates.m_handler;
			m_userData = rates.m_userData;

			m_prices = nullptr;
			m_size = 0;
			m_table = nullptr;
			m_data = nullptr;
		}
		return *this;
	}

	CRates::CRates(ResolvePriceHandler handler, void* pUserData)
        : m_handler(handler)
        , m_userData(pUserData)
        , m_prices()
        , m_size()
        , m_table()
        , m_data()
	{
	}

	CRates::~CRates()
	{
		Finalize();
	}

	void CRates::Finalize()
	{
		if (nullptr != m_data)
		{
			delete[] m_data;
			m_data = nullptr;
		}
		if (nullptr != m_table)
		{
			delete[] m_table;
			m_table = nullptr;
		}
		m_prices = nullptr;
	}

	void CRates::Initialize(const CSymbolEntries& symbols, const CCurrencyEntries& currencies, CPriceEntries& prices)
	{
		Finalize();
		m_prices = &prices;
		const size_t size = symbols.GetNumberOfCurrencies();
		m_data = new CRateEntry[size * size];
		m_table = new CRateEntry*[size];

		for (size_t index = 0; index < size; ++index)
		{
			m_table[index] = m_data + index * size;
		}

		auto cReverseIterator = symbols.GetEntris().rbegin();
		for (cReverseIterator; cReverseIterator != symbols.GetEntris().rend(); cReverseIterator++)
		{
			const CSymbolEntry& symbol = cReverseIterator->second;
			CRateEntry& entry = m_table[symbol.ToIndex][symbol.FromIndex];
			entry.Price = prices.TryGetPriceEntry(symbol.GetSymbol());
			if (!entry.Price.HasValue() && (nullptr != m_handler))
			{
				entry.Symbol = &symbol;
			}
		}

		m_currencies.reserve(currencies.size());
		for each (const auto& element in currencies.GetEntries())
		{
			ptrdiff_t index = symbols.TryGetCurrencyIndex(element);
			if (index >= 0)
			{
				m_currencies.push_back(index);
			}
		}
	}

	Nullable<CPriceEntry> CRates::TryGetRate(ptrdiff_t to, ptrdiff_t from)
	{
		CRateEntry& entry = m_table[to][from];
		if (nullptr != entry.Symbol)
		{
			const string& symbol = entry.Symbol->GetSymbol();
			entry.Price = m_handler(symbol.c_str(), m_userData);
			entry.Symbol = nullptr;
			if(entry.Price.HasValue())
			{
				m_prices->Update(symbol, entry.Price.Value().Bid, entry.Price.Value().Ask);
			}
		}
		return entry.Price;
	}

	Nullable<CPriceEntry> CRates::TryGetRate(const CSymbolEntry& symbol)
	{
		return TryGetRate(symbol.ToIndex, symbol.FromIndex);
	}

	Nullable<double> CRates::CalculateProfitRate(double profit, ptrdiff_t xxx, ptrdiff_t yyy, ptrdiff_t zzz)
	{
		// if xxx == zzz => 1
		if (yyy == zzz)
		{
			return 1;
		}

		// if X = Z => 1 / XY_Price2
		if (xxx == zzz)
		{
			Nullable<CPriceEntry> xxxyyy = TryGetRate(xxx, yyy);
			if (xxxyyy.HasValue())
			{
				return 1 / xxxyyy.Value().PriceDivisorFromProfit(profit);
			}
		}

		// if YZ exists => YZ_Price1
		Nullable<CPriceEntry> entry = TryGetRate(yyy, zzz);
		if (entry.HasValue())
		{
			return entry.Value().PriceMultiplierFromProfit(profit);
		}

		// if ZY exists => 1 / ZY_Price2
		entry = TryGetRate(zzz, yyy);
		if (entry.HasValue())
		{
			return 1 / entry.Value().PriceDivisorFromProfit(profit);
		}

		// if ZX exists => 1 / XY_Price2 / ZX_Price2
        entry = TryGetRate(zzz, xxx);
		if (entry.HasValue())
		{
			Nullable<CPriceEntry> xxxyyy = TryGetRate(xxx, yyy);
			if (xxxyyy.HasValue())
			{
				return 1 / xxxyyy.Value().PriceDivisorFromProfit(profit) / entry.Value().PriceDivisorFromProfit(profit);
			}
		}

		// if XZ exists => 1 / XY_Price2 * XZ_Price1
		entry = TryGetRate(xxx, zzz);
		if (entry.HasValue())
		{
			Nullable<CPriceEntry> xxxyyy = TryGetRate(xxx, yyy);
			if (xxxyyy.HasValue())
			{
				return 1 / xxxyyy.Value().PriceDivisorFromProfit(profit) * entry.Value().PriceMultiplierFromProfit(profit);
			}
		}

		for each (auto element in m_currencies)
		{
		    // if YC && ZC exist => YC_Price1 / ZC_Price2
			Nullable<CPriceEntry> yyyEntry = TryGetRate(yyy, element);
			Nullable<CPriceEntry> zzzEntry = TryGetRate(zzz, element);

			if (yyyEntry.HasValue() && zzzEntry.HasValue())
			{
    			return yyyEntry.Value().PriceMultiplierFromProfit(profit) / zzzEntry.Value().PriceDivisorFromProfit(profit);
            }

		    // if CY && ZC exist => 1 / CY_Price2 / ZC_Price2
			yyyEntry = TryGetRate(element, yyy);
			zzzEntry = TryGetRate(zzz, element);

			if (yyyEntry.HasValue() && zzzEntry.HasValue())
			{
                return 1 / yyyEntry.Value().PriceDivisorFromProfit(profit) / zzzEntry.Value().PriceDivisorFromProfit(profit);
            }

		    // if YC && CZ exist => YC_Price1 * CZ_Price1
			yyyEntry = TryGetRate(yyy, element);
			zzzEntry = TryGetRate(element, zzz);

			if (yyyEntry.HasValue() && zzzEntry.HasValue())
			{
			    return yyyEntry.Value().PriceMultiplierFromProfit(profit) * zzzEntry.Value().PriceMultiplierFromProfit(profit);
            }

		    // if CY && CZ exist => 1 / CY_Price2 * CZ_Price1
			yyyEntry = TryGetRate(element, yyy);
			zzzEntry = TryGetRate(element, zzz);

			if (yyyEntry.HasValue() && zzzEntry.HasValue())
			{
    			return zzzEntry.Value().PriceMultiplierFromProfit(profit) / yyyEntry.Value().PriceDivisorFromProfit(profit);
            }
		}

		return nullptr;
	}

	Nullable<double> CRates::CalculateMarginRate(TradeSide /*side*/, ptrdiff_t xxx, ptrdiff_t yyy, ptrdiff_t zzz)
	{
        // if X == Z => 1
		if (xxx == zzz)
		{
			return 1;
		}

        Nullable<CPriceEntry> xxxyyy = TryGetRate(xxx, yyy);

		// if Y == Z => XY_Ask
		if (yyy == zzz)
        {
            if (xxxyyy.HasValue())
                return xxxyyy.Value().Ask;
        }

        // if XZ exists => XZ_Ask
        Nullable<CPriceEntry> entry = TryGetRate(xxx, zzz);
        if (entry.HasValue())
        {
            return entry.Value().Ask;
        }

		// if ZX exists => 1 / ZX_Bid
		entry = TryGetRate(zzz, xxx);
		if (entry.HasValue())
		{
            return 1 / entry.Value().Bid;
		}

		if (xxxyyy.HasValue())
		{
			// if YZ exists => XY_Ask * YZ_Ask
			entry = TryGetRate(yyy, zzz);
			if (entry.HasValue())
			{
				double result = xxxyyy.Value().Ask * entry.Value().Ask;
				return result;
			}

			// if ZY exists => XY_Ask / ZY_Bid
			entry = TryGetRate(zzz, yyy);
			if (entry.HasValue())
			{
				double result = xxxyyy.Value().Ask / entry.Value().Bid;
				return result;
			}
		}


		for each (auto element in m_currencies)
		{
            {
    		    // if XC && ZC exists => XC_Ask / ZC_Bid
			    Nullable<CPriceEntry> xxxEntry = TryGetRate(xxx, element);
			    Nullable<CPriceEntry> zzzEntry = TryGetRate(zzz, element);

			    if (xxxEntry.HasValue() && zzzEntry.HasValue())
                {
                    return xxxEntry.Value().Ask / zzzEntry.Value().Bid;
                }

		        // if CX && ZC exists => 1 / CX_Bid / ZC_Bid
                xxxEntry = TryGetRate(element, xxx);
                zzzEntry = TryGetRate(zzz, element);

			    if (xxxEntry.HasValue() && zzzEntry.HasValue())
                {
                    return 1 / xxxEntry.Value().Bid / zzzEntry.Value().Bid;
                }

		        // if XC && CZ exists => XC_Ask * CZ_Ask
			    xxxEntry = TryGetRate(xxx, element);
			    zzzEntry = TryGetRate(element, zzz);

			    if (xxxEntry.HasValue() && zzzEntry.HasValue())
                {
                    return xxxEntry.Value().Ask * zzzEntry.Value().Ask;
                }

		        // if CX && CZ exists => 1 / CX_Bid * CZ_Ask
			    xxxEntry = TryGetRate(element, xxx);
			    zzzEntry = TryGetRate(element, zzz);

			    if (xxxEntry.HasValue() && zzzEntry.HasValue())
                {
                    return 1 / xxxEntry.Value().Bid * zzzEntry.Value().Ask;
                }
            }


            if (xxxyyy.HasValue())
            {
    		    // if YC && ZC exists => (YC_Ask / ZC_Bid) * XY_Ask
			    Nullable<CPriceEntry> yyyEntry = TryGetRate(yyy, element);
			    Nullable<CPriceEntry> zzzEntry = TryGetRate(zzz, element);

			    if (yyyEntry.HasValue() && zzzEntry.HasValue())
                {
                    return (yyyEntry.Value().Ask / zzzEntry.Value().Bid) * xxxyyy.Value().Ask;
                }

		        // if CY && ZC exists => (1 / (CY_Bid * ZC_Bid)) * XY_Ask
                yyyEntry = TryGetRate(element, yyy);
                zzzEntry = TryGetRate(zzz, element);

			    if (yyyEntry.HasValue() && zzzEntry.HasValue())
                {
                    return (1 / yyyEntry.Value().Bid / zzzEntry.Value().Bid) * xxxyyy.Value().Ask;
                }

		        // if YC && CZ exists => (YC_Ask * CZ_Ask) * XY_Ask
			    yyyEntry = TryGetRate(yyy, element);
			    zzzEntry = TryGetRate(element, zzz);

			    if (yyyEntry.HasValue() && zzzEntry.HasValue())
                {
                    return yyyEntry.Value().Ask * zzzEntry.Value().Ask * xxxyyy.Value().Ask;
                }

		        // if CY && CZ exists => (1 / CY_Bid * CZ_Ask) * XY_Ask
			    yyyEntry = TryGetRate(element, yyy);
			    zzzEntry = TryGetRate(element, zzz);

			    if (yyyEntry.HasValue() && zzzEntry.HasValue())
                {
                    return 1 / yyyEntry.Value().Bid * zzzEntry.Value().Ask * xxxyyy.Value().Ask;
                }
            }
		}

		return nullptr;
	}

	std::ostream& operator << (std::ostream& stream, const CRates& /*rates*/)
	{
		return stream;
	}
}