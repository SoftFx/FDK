#pragma once

#include "TagEntry.h"
#include "MarginCalcMode.h"

namespace FDK
{
	class CTradeEntry;

	class CSymbolEntry
	{
	public:
		CTagEntry Tag;
	public:
		LLCOMMON_API CSymbolEntry();
		inline CSymbolEntry(const std::string& symbol)
		{
			Construct(symbol.c_str());
		}
		inline CSymbolEntry(const std::string& symbol, const std::string& profitCurrency, const std::string& tradeCurrency)
		{
			Construct(symbol.c_str(), profitCurrency.c_str(), tradeCurrency.c_str());
		}
		LLCOMMON_API CSymbolEntry(const CSymbolEntry& entry);
		LLCOMMON_API CSymbolEntry& operator = (const CSymbolEntry& entry);
		LLCOMMON_API ~CSymbolEntry();
	private:
		LLCOMMON_API void Construct(const char* symbol);
		LLCOMMON_API void Construct(const char* symbol, const char* from, const char* to);
		void Initialize();
	public:
#ifdef LLCOMMON_EXPORTS
		inline const std::string& GetSymbol() const { return m_symbol; }
		inline const std::string& GetFrom() const { return m_from; }
		inline const std::string& GetTo() const { return m_to; }
#else
		inline std::string GetSymbol() const { return DoGetSymbol(); }
		inline std::string GetProfitCurrency() const { return DoGetFrom(); }
		inline std::string GetTradeCurrency() const { return DoGetTo(); }
#endif
	public:
		inline double GetContractSize() const { return m_contractSize; }
		inline double GetHedging() const { return m_hedging; }
        inline double GetMarginFactor() const { return m_marginFactor; }
		inline double GetMarginFactorOfPositions() const { return m_marginFactorOfPositions; }
		inline double GetMarginFactorOfLimitOrders() const { return m_marginFactorOfLimitOrders; }
		inline double GetMarginFactorOfStopOrders() const { return m_marginFactorOfStopOrders; }
        inline MarginCalcMode GetMarginCalcMode() const { return m_marginCalcMode; }

		LLCOMMON_API void SetContractSize(const double newContractSize);
		LLCOMMON_API void SetHedging(const double newHedging);
        LLCOMMON_API void SetMarginFactor(const double newMarginFactor);
		LLCOMMON_API void SetMarginFactorOfPositions(const double newMarginFactorOfPositions);
		LLCOMMON_API void SetMarginFactorOfLimitOrders(const double newMarginFactorOfLimitOrders);
		LLCOMMON_API void SetMarginFactorOfStopOrders(const double newMarginFactorOfStopOrders);
		LLCOMMON_API void SetMarginCalcMode(const MarginCalcMode newMarginCalcMode);
	internal:
		bool CollectMargin(const CTradeEntry& entry);
		void ClearMargin();
		double CalculateGrossMargin() const;
		double CalculateNetMargin() const;
	private:
		double DoCalculateMargin(double accountTypeCoefficient) const;
	internal:
		ptrdiff_t SymbolIndex;
		ptrdiff_t FromIndex;
		ptrdiff_t ToIndex;
	private:
		LLCOMMON_API const char* DoGetSymbol() const;
		LLCOMMON_API const char* DoGetFrom() const;
		LLCOMMON_API const char* DoGetTo() const;
	private:
		LrpString m_symbol;
		LrpString m_from;
		LrpString m_to;
	private:
		double m_contractSize;
		double m_hedging;
        double m_marginFactor;
		double m_marginFactorOfPositions;
		double m_marginFactorOfLimitOrders;
		double m_marginFactorOfStopOrders;
        MarginCalcMode m_marginCalcMode;
	private:
		double m_buyPositionsMargin;
		double m_sellPositionsMargin;
		double m_buyLimitOrdersMargin;
		double m_sellLimitOrdersMargin;
		double m_buyStopOrdersMargin;
		double m_sellStopOrdersMargin;
	private:
#ifdef LLCOMMON_EXPORTS
		friend std::ostream& operator << (std::ostream& stream, const CSymbolEntry& entry);
		friend std::istream& operator >> (std::istream& stream, CSymbolEntry& entry);
#endif
	};
}