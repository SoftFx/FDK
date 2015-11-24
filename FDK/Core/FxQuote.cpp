#include "stdafx.h"
#include "FxQuote.h"
#include "BidComparator.h"
#include "AskComparator.h"


CFxQuote::CFxQuote()
{
}

CFxQuote::CFxQuote(const string& symbol, const CDateTime creatingTime) : Symbol(symbol), CreatingTime(creatingTime)
{
}

CFxQuote::CFxQuote(CFxQuote&& arg)
    : Symbol(std::move(arg.Symbol))
    , CreatingTime(std::move(arg.CreatingTime))
    , Bids(std::move(arg.Bids))
    , Asks(std::move(arg.Asks))
{
}

bool CFxQuote::TryGetBid(double& price, double& volume, CDateTime& creationTime) const
{
	if (Bids.empty())
	{
		return false;
	}
	const CFxQuoteEntry& entry = Bids.front();
	price = entry.Price;
	volume = entry.Volume;
	creationTime = this->CreatingTime;
	return true;
}

bool CFxQuote::TryGetAsk(double& price, double& volume, CDateTime& creationTime) const
{
	if (Asks.empty())
	{
		return false;
	}
	const CFxQuoteEntry& entry = Asks.front();
	price = entry.Price;
	volume = entry.Volume;
	creationTime = this->CreatingTime;
	return true;
}

bool CFxQuote::TryGetBidAsk(double& bid, double& ask) const
{
	if (Bids.empty())
	{
		return false;
	}
	if (Asks.empty())
	{
		return false;
	}
	const CFxQuoteEntry& pBid = Bids.front();
	const CFxQuoteEntry& pAsk = Asks.front();
	bid = pBid.Price;
	ask = pAsk.Price;
	return true;
}

void CFxQuote::AddBid(const double price, const double volume)
{
	CFxQuoteEntry entry(price, volume);
	Bids.push_back(entry);
}
void CFxQuote::AddAsk(const double price, const double volume)
{
	CFxQuoteEntry entry(price, volume);
	Asks.push_back(entry);
}

void CFxQuote::Sort()
{
	sort(Bids.begin(), Bids.end(), CBidComparator());
	sort(Asks.begin(), Asks.end(), CAskComparator());
}

namespace
{
	bool CompareQuoteEntries(const vector<CFxQuoteEntry>& first, const vector<CFxQuoteEntry>& second)
	{
		const size_t count = first.size();
		if (count != second.size())
		{
			return false;
		}
		for (size_t index = 0; index < count; ++index)
		{
			const CFxQuoteEntry& f = first[index];
			const CFxQuoteEntry& s = first[index];
			if ((f.Price != s.Price) || (f.Volume != s.Volume))
			{
				return false;
			}
		}
		return true;
	}
}

bool operator == (const CFxQuote& first, const CFxQuote& second)
{
	if (first.Symbol != second.Symbol)
	{
		return false;
	}
	if (first.CreatingTime != second.CreatingTime)
	{
		return false;
	}
	if (!CompareQuoteEntries(first.Bids, second.Bids))
	{
		return false;
	}
	return CompareQuoteEntries(first.Asks, second.Asks);
}

bool operator != (const CFxQuote& first, const CFxQuote& second)
{
	if (first.Symbol != second.Symbol)
	{
		return true;
	}
	if (first.CreatingTime != second.CreatingTime)
	{
		return true;
	}
	if (!CompareQuoteEntries(first.Bids, second.Bids))
	{
		return true;
	}
	return !CompareQuoteEntries(first.Asks, second.Asks);
}