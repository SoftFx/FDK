#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)
#pragma warning (disable : 4251)

#include "FxQuoteEntry.h"


class CORE_API CFxQuote
{
public:
	CFxQuote();
	CFxQuote(const string& symbol, const CDateTime creatingTime);
	CFxQuote(const CFxQuote& arg) = default;
	CFxQuote(CFxQuote&& arg);
public:
	CFxQuote& operator=(const CFxQuote& arg) = default;
public:
	string Symbol;
	CDateTime CreatingTime;
	vector<CFxQuoteEntry> Bids;
	vector<CFxQuoteEntry> Asks;
	string Id;
public:
	bool TryGetBidAsk(double& bid, double& ask) const;
	bool TryGetBid(double& price, double& volume, CDateTime& creationTime) const;
	bool TryGetAsk(double& price, double& volume, CDateTime& creationTime) const;
public:
	void AddBid(const double price, const double volume);
	void AddAsk(const double price, const double volume);
	void Sort();
private:
	CORE_API friend bool operator == (const CFxQuote& first, const CFxQuote& second);
	CORE_API friend bool operator != (const CFxQuote& first, const CFxQuote& second);
};

namespace std
{
	inline void swap(CFxQuote& first, CFxQuote& second)
	{
		swap(first.Symbol, second.Symbol);
		swap(first.CreatingTime, second.CreatingTime);
		swap(first.Bids, second.Bids);
		swap(first.Asks, second.Asks);
	}
}

#pragma warning (pop)
