#include "stdafx.h"
#include "SymbolEntry.h"
#include "TradeEntry.h"
#include "Functions.h"
#include "Formating.h"

namespace FDK
{
	CSymbolEntry::CSymbolEntry()
	{
		Initialize();
	}

	CSymbolEntry::CSymbolEntry(const CSymbolEntry& entry)
        : Tag(entry.Tag)
        , SymbolIndex(-1)
        , FromIndex(-1)
        , ToIndex(-1)
        , m_symbol(entry.m_symbol)
        , m_from(entry.m_from)
        , m_to(entry.m_to)
        , m_contractSize(entry.m_contractSize)
        , m_hedging(entry.m_hedging)
        , m_marginFactor(entry.m_marginFactor)
        , m_marginFactorOfPositions(entry.m_marginFactorOfPositions)
        , m_marginFactorOfLimitOrders(entry.m_marginFactorOfLimitOrders)
        , m_marginFactorOfStopOrders(entry.m_marginFactorOfStopOrders)
        , m_marginCalcMode(entry.m_marginCalcMode)
	{
	}

	CSymbolEntry& CSymbolEntry::operator = (const CSymbolEntry& entry)
	{
		if (this != &entry)
		{
			Tag = entry.Tag;
			m_symbol = entry.m_symbol;
			m_from = entry.m_from;
			m_to = entry.m_to;

			m_contractSize = entry.m_contractSize;
			m_hedging = entry.m_hedging;

			m_marginFactor = entry.m_marginFactor;
			m_marginFactorOfPositions = entry.m_marginFactorOfPositions;
			m_marginFactorOfLimitOrders = entry.m_marginFactorOfLimitOrders;
			m_marginFactorOfStopOrders = entry.m_marginFactorOfStopOrders;
            m_marginCalcMode = entry.m_marginCalcMode;
		}
		return *this;
	}

	CSymbolEntry::~CSymbolEntry()
	{
	}

	void CSymbolEntry::Initialize()
	{
		SymbolIndex = -1;
		FromIndex = -1;
		ToIndex = -1;

		m_contractSize = 1;

		m_hedging = 1;

        m_marginFactor = 1;
		m_marginFactorOfPositions = 1;
		m_marginFactorOfLimitOrders = 1;
		m_marginFactorOfStopOrders = 1;
        m_marginCalcMode = FxMarginCalcMode_Forex;

		ClearMargin();
	}

#ifdef new
	#pragma push_macro("new")
	#undef new
	#define FX_RESTORE_NEW
#endif

	void CSymbolEntry::Construct(const char* symbol, const char* from, const char* to)
	{
		new (&m_symbol)std::string(symbol);
		new (&m_from)std::string(from);
		new (&m_to)std::string(to);
		Initialize();
	}

	void CSymbolEntry::Construct(const char* symbol)
	{
		new (&m_symbol)std::string(symbol);

		const size_t index = m_symbol.find('/');

		if (string::npos != index)
		{
			new (&m_from)std::string(symbol + index + 1);
			new (&m_to)std::string(symbol, symbol + index);
		}

		if ((std::string::npos == index) || (std::string::npos != m_from.find('/')) || (std::string::npos != m_to.find('/')))
		{
			std::string message = "Symbol = " + m_symbol + " should has XXX/YYY format";
			throw std::runtime_error(message);
		}
		Initialize();
	}

#ifdef FX_RESTORE_NEW
	#pragma pop_macro("new")
	#undef FX_RESTORE_NEW
#endif


	const char* CSymbolEntry::DoGetSymbol() const
	{
		return m_symbol.c_str();
	}

	const char* CSymbolEntry::DoGetFrom() const
	{
		return m_from.c_str();
	}

	const char* CSymbolEntry::DoGetTo() const
	{
		return m_to.c_str();
	}

	bool CSymbolEntry::CollectMargin(const CTradeEntry& entry)
	{
		Nullable<double> value = entry.GetMargin();
		if (!value.HasValue())
		{
			return false;
		}

		double margin = value.Value();

		if (TradeType_Position == entry.GetType())
		{
			if (TradeSide_Buy == entry.GetSide())
			{
				m_buyPositionsMargin += margin;
			}
			else
			{
				m_sellPositionsMargin += margin;
			}
		}
		else if (TradeType_Limit == entry.GetType())
		{
			if (TradeSide_Buy == entry.GetSide())
			{
				m_buyLimitOrdersMargin += margin;
			}
			else
			{
				m_sellLimitOrdersMargin += margin;
			}
		}
		else if (TradeType_Stop == entry.GetType())
		{
			if (TradeSide_Buy == entry.GetSide())
			{
				m_buyStopOrdersMargin += margin;
			}
			else
			{
				m_sellStopOrdersMargin += margin;
			}
		}
		else
		{
			throw runtime_error("CSymbolEntry::CollectMargin() unknown type");
		}
		return true;
	}

	void CSymbolEntry::ClearMargin()
	{
		m_buyPositionsMargin = 0;
		m_sellPositionsMargin = 0;
		m_buyLimitOrdersMargin = 0;
		m_sellLimitOrdersMargin = 0;
		m_buyStopOrdersMargin = 0;
		m_sellStopOrdersMargin = 0;
	}

	double CSymbolEntry::CalculateGrossMargin() const
	{
		return DoCalculateMargin(0);
	}

	double CSymbolEntry::CalculateNetMargin() const
	{
		return DoCalculateMargin(1);
	}

	double CSymbolEntry::DoCalculateMargin(double accountTypeCoefficient) const
	{
		double minSidePositionsMargin = min(m_buyPositionsMargin, m_sellPositionsMargin);
		double buyMargin = m_marginFactorOfPositions * (m_buyPositionsMargin - accountTypeCoefficient * minSidePositionsMargin) +
			m_marginFactorOfLimitOrders * m_buyLimitOrdersMargin + m_marginFactorOfStopOrders * m_buyStopOrdersMargin;

		double sellMargin = m_marginFactorOfPositions * (m_sellPositionsMargin - accountTypeCoefficient * minSidePositionsMargin) +
			m_marginFactorOfLimitOrders * m_sellLimitOrdersMargin + m_marginFactorOfStopOrders * m_sellStopOrdersMargin;

		double result = max(buyMargin, sellMargin) + (2 * m_hedging - 1) * min(buyMargin, sellMargin);
		return result;
	}

	void CSymbolEntry::SetContractSize(const double newContractSize)
	{
		m_contractSize = ValidatePositiveValue(__FUNCTION__, "newContractSize", newContractSize);
	}

	void CSymbolEntry::SetHedging(const double newHedging)
	{
		m_hedging = ValidateValueFromZeroToOne(__FUNCTION__, "newHedging", newHedging);
	}


	void CSymbolEntry::SetMarginFactor(const double newMarginFactor)
	{
		m_marginFactor = newMarginFactor;
	}

	void CSymbolEntry::SetMarginFactorOfPositions(const double newMarginFactorOfPositions)
	{
		m_marginFactorOfPositions = ValidateValueFromZeroToOne(__FUNCTION__, "newMarginFactorOfPositions", newMarginFactorOfPositions);
	}

	void CSymbolEntry::SetMarginFactorOfLimitOrders(const double newMarginFactorOfLimitOrders)
	{
		m_marginFactorOfLimitOrders = ValidateValueFromZeroToOne(__FUNCTION__, "newMarginFactorOfLimitOrders", newMarginFactorOfLimitOrders);
	}

	void CSymbolEntry::SetMarginFactorOfStopOrders(const double newMarginFactorOfStopOrders)
	{
		m_marginFactorOfStopOrders = ValidateValueFromZeroToOne(__FUNCTION__, "newMarginFactorOfStopOrders", newMarginFactorOfStopOrders);
	}

    void CSymbolEntry::SetMarginCalcMode(const MarginCalcMode newMarginCalcMode)
    {
        m_marginCalcMode = newMarginCalcMode;
    }

	std::ostream& operator << (std::ostream& stream, const CSymbolEntry& entry)
	{
		Process("Tag", entry.Tag, stream);
		Process("Symbol", entry.m_symbol, stream);
		Process("From", entry.m_from, stream);
		Process("To", entry.m_to, stream);
		Process("ContractSize", entry.m_contractSize, stream);
		Process("Hedging", entry.m_hedging, stream);
		Process("MarginFactorOfPositions", entry.m_marginFactorOfPositions, stream);
		Process("MarginFactorOfLimitOrders", entry.m_marginFactorOfLimitOrders, stream);
		Process("MarginFactorOfStopOrders", entry.m_marginFactorOfStopOrders, stream);

		return stream;
	}

	std::istream& operator >> (std::istream& stream, CSymbolEntry& entry)
	{
		Process("Tag", entry.Tag, stream);
		Process("Symbol", entry.m_symbol, stream);
		Process("From", entry.m_from, stream);
		Process("To", entry.m_to, stream);
		Process("ContractSize", entry.m_contractSize, stream);
		Process("Hedging", entry.m_hedging, stream);
		Process("MarginFactorOfPositions", entry.m_marginFactorOfPositions, stream);
		Process("MarginFactorOfLimitOrders", entry.m_marginFactorOfLimitOrders, stream);
		Process("MarginFactorOfStopOrders", entry.m_marginFactorOfStopOrders, stream);

		return stream;
	}
}