#pragma once

#include "AccountType.h"
#include "AccountEntryStatus.h"
#include "TradeEntries.h"
#include "TagEntry.h"
#include "SymbolEntries.h"

namespace FDK
{
	class CFinancialCalculator;
	class CRates;
	class CAccountData;

	class CAccountEntry
	{
	public:
		LLCOMMON_API CAccountEntry();
		inline CAccountEntry(AccountType type, const char* currency, double leverage, double balance = 0)
		{
			Construct(type, currency, leverage, balance);
		}
		inline CAccountEntry(AccountType type, const std::string& currency, double leverage, double balance = 0)
		{
			Construct(type, currency.c_str(), leverage, balance);
		}
		LLCOMMON_API CAccountEntry(const CAccountEntry& entry);
		LLCOMMON_API CAccountEntry& operator = (const CAccountEntry& entry);
		LLCOMMON_API ~CAccountEntry();
	private:
		LLCOMMON_API void Construct(AccountType type, const char* currency, double leverage, double balance);
	public: // gets and sets account type
		inline AccountType GetAccountType() const { return m_type; }
		LLCOMMON_API void SetAccountType(const AccountType newAccounType);
	public: // gets and sets leverage
		inline double GetLeverage() const { return m_leverage; }
		LLCOMMON_API void SetLeverage(const double newLeverage);
	public: // gets and sets balance
		inline double GetBalance() const { return m_balance; }
		LLCOMMON_API void SetBalance(const double newBalance);
	public: // gets and sets account currency
		inline std::string GetCurrency() const { return DoGetCurrency(); }
		inline void SetCurrency(const std::string& newCurrency) { DoSetCurrency(newCurrency.c_str()); }
	private: // gets and sets account currency
		LLCOMMON_API const char* DoGetCurrency() const;
		LLCOMMON_API void DoSetCurrency(const char* newCurrency);
	public: // gets calculated parameters
		inline Nullable<double> GetProfit() const { return m_profit; }
		inline Nullable<double> GetMargin() const { return m_margin; }
		LLCOMMON_API Nullable<double> GetEquity() const;
		LLCOMMON_API Nullable<double> GetMarginLevel() const;
		LLCOMMON_API double GetCommission() const;
		LLCOMMON_API double GetAgentCommission() const;
		LLCOMMON_API double GetSwap() const;

		inline AccountEntryStatus GetProfitStatus() const { return m_profitStatus; }
		inline AccountEntryStatus GetMarginStatus() const { return m_marginStatus; }
		inline AccountEntryStatus GetEquityStatus() const { return m_profitStatus; }
		LLCOMMON_API AccountEntryStatus GetMarginLevelStatus() const;
	public:
		LLCOMMON_API void Clear();
		void Calcualte(CFinancialCalculator& owner);
		void DoCalculate(const ptrdiff_t zzz, CFinancialCalculator& owner);
		void DoCalculateDynamic(const ptrdiff_t zzz, CRates& rates);
		void DoCalculateStatic(const ptrdiff_t zzz, CRates& rates);
		void DoCalculateStaticIfPossible(const ptrdiff_t zzz, CRates& rates);
		void DoCollectProfit();
		void DoCollectMargin(CFinancialCalculator& owner);
	public:
		CTagEntry Tag;
		CTradeEntries Trades;
	private: // input parameters
		AccountType m_type;
		double m_leverage;
		double m_balance;
		LrpString m_currency;
	private: // calculated parameters
		Nullable<double> m_profit;
		AccountEntryStatus m_profitStatus;
		Nullable<double> m_margin;
		AccountEntryStatus m_marginStatus;
	private:
#ifdef LLCOMMON_EXPORTS
		friend std::ostream& operator << (std::ostream& stream, const CAccountEntry& entry);
		friend std::istream& operator >> (std::istream& stream, CAccountEntry& entry);
		friend class CAccountData;
#endif
	};
}
