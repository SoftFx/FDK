#pragma once

#include "TradeEntryStatus.h"
#include "TradeType.h"
#include "TradeSide.h"
#include "TagEntry.h"

namespace FDK
{
	class CSymbolEntry;
	class CRates;
	class CTradeData;

	/// <summary>
	/// 
	/// </summary>
	class CTradeEntry
	{
	public:
		/// <summary>
		/// Creates a new instance of trade entry.
		/// </summary>
		LLCOMMON_API CTradeEntry();
		LLCOMMON_API CTradeEntry(const CTradeEntry& entry);
		LLCOMMON_API CTradeEntry& operator = (const CTradeEntry& entry);
		LLCOMMON_API ~CTradeEntry();
	public:
		inline CTradeEntry(TradeType type, TradeSide side, const char* symbol, double price, double volume, Nullable<double> staticMarginRate = nullptr)
		{
			Construct(type, side, symbol, price, volume,  0, 0, 0, staticMarginRate);
		}
		inline CTradeEntry(TradeType type, TradeSide side, const std::string& symbol, double price, double volume, Nullable<double> staticMarginRate = nullptr)
		{
			Construct(type, side, symbol.c_str(), price, volume, 0, 0, 0, staticMarginRate);
		}
	private:
		LLCOMMON_API void Construct(TradeType type, TradeSide side, const char* symbol, double price, double volume, double commission, double agentCommission, double swap, Nullable<double> staticMarginRate);
	public:
		LLCOMMON_API void Clear();
	public: // gets and sets of type
		inline TradeType GetType() const { return m_type; }
		LLCOMMON_API void SetType(const TradeType newType);
	public: // gets and sets of side
		inline TradeSide GetSide() const { return m_side; }
		LLCOMMON_API void SetSide(const TradeSide newSide);
	public: // gets and sets of symbol
		inline std::string GetSymbol() const { return DoGetSymbol(); }
		inline void SetSymbol(const std::string& symbol) { DoGetSymbol(symbol.c_str()); }
	private: // gets and sets of symbol
		LLCOMMON_API const char* DoGetSymbol() const;
		LLCOMMON_API void DoGetSymbol(const char* symbol);
	public: // gets and sets of price
		inline double GetPrice() const { return m_price; }
		LLCOMMON_API void SetPrice(const double newPrice);
	public: // gets and sets of volume
		inline double GetVolume() const { return m_volume; }
		LLCOMMON_API void SetVolume(const double newVolume);
	public: // gets and sets commission
		inline double GetCommission() const { return m_commission; }
		LLCOMMON_API void SetCommission(const double newCommission);
	public: // gets and sets agent commission
        inline double GetAgentCommission() const { return m_agentCommission; }
		LLCOMMON_API void SetAgentCommission(const double newAgentCommission);
	public: // gets and sets swap
		inline double GetSwap() const { return m_swap; }
		LLCOMMON_API void SetSwap(const double newSwap);
	public: // gets and sets of static margin rate
		Nullable<double> GetStaitcMarinRate() const { return m_staticMarginRate; }
		LLCOMMON_API void SetStaticMarginRate(const Nullable<double> newStaticMarginRate);
	public:
		inline TradeEntryStatus GetProfitStatus() const { return m_profitStatus; }
		inline TradeEntryStatus GetMarginStatus() const {return m_marginStatus; }
		inline Nullable<double> GetProfit() const { return m_profit; }
		inline Nullable<double> GetMargin() const { return m_margin; }
	internal:
		bool CaculateProlog();
		void CalculateProfit(const ptrdiff_t zzz, CRates& rates);
		void CalculateStaticMargin(double leverageFactor);
		void CalculateDynamicMargin(const ptrdiff_t zzz, double leverageFactor, CRates& rates);
		void CalculateStaticIfPossibleMargin(const ptrdiff_t zzz, double leverageFactor, CRates& rates);
		void CalculateMargin(double leverageFactor, Nullable<double> rate);
	private:
		TradeEntryStatus DoCalculateProfit(ptrdiff_t zzz, CRates& rates);
		double GetNativeVolume() const;
	public:
		CTagEntry Tag;
	internal:
		CSymbolEntry* SymbolEntry;
	private:
		TradeType m_type;
		TradeSide m_side;
		LrpString m_symbol;
		double m_price;
		double m_volume;
        double m_commission;
        double m_agentCommission;
        double m_swap;
		Nullable<double> m_staticMarginRate;
	private:
		TradeEntryStatus m_profitStatus;
		TradeEntryStatus m_marginStatus;
		Nullable<double> m_profit;
		Nullable<double> m_margin;
	private:
#ifdef LLCOMMON_EXPORTS
		friend std::ostream& operator << (std::ostream& stream, const CTradeEntry& entry);
		friend std::istream& operator >> (std::istream& stream, CTradeEntry& entry);
		friend class CTradeData;
#endif
	};
}