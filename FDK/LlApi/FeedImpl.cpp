#include "stdafx.h"
#include "FeedImpl.h"
#include "DataFeed.h"

namespace
{
	FxRef<CDataFeed> FeedFromHandle(void* handle)
	{
		if (nullptr == handle)
		{
			throw CArgumentNullException("handle can not be null");
		}
		FxRef<CDataFeed> result = TypeFromHandle<CDataFeed>(handle);
		if (!result)
		{
			throw CInvalidHandleException("invalid handle");
		}
		return result;
	}
}

void* CFeedImpl::Create(const std::string& name, const std::string& connectionString)
{
	CDataFeed* result = new CDataFeed(name, connectionString);
	return result;
}

CDataHistoryInfo CFeedImpl::GetBarsHistoryFiles(void* handle, const string& symbol, int priceType, const string& period, CDateTime time, size_t timeoutInMilliseconds)
{
	FxRef<CDataFeed> feed = FeedFromHandle(handle);
    return feed->GetBarsHistoryFiles(symbol, priceType, period, time, static_cast<uint32>(timeoutInMilliseconds));
}

CDataHistoryInfo CFeedImpl::GetQuoteHistoryFiles(void* handle, const string& symbol, bool includeLevel2, CDateTime time, size_t timeoutInMilliseconds)
{
	FxRef<CDataFeed> feed = FeedFromHandle(handle);
    return feed->GetQuoteHistoryFiles(symbol, includeLevel2, time, static_cast<uint32>(timeoutInMilliseconds));
}

std::vector<CFxCurrencyInfo> CFeedImpl::GetCurrencies(void* handle, size_t timeoutInMilliseconds)
{
    FxRef<CDataFeed> feed = FeedFromHandle(handle);
    return feed->GetCurrencies(static_cast<uint32>(timeoutInMilliseconds));
}

std::vector<CFxSymbolInfo> CFeedImpl::GetSymbols(void* handle, size_t timeoutInMilliseconds)
{
	FxRef<CDataFeed> feed = FeedFromHandle(handle);
    return feed->GetSupportedSymbols(static_cast<uint32>(timeoutInMilliseconds));
}

void CFeedImpl::SubscribeToQuotes(void* handle, const vector<string>& symbols, int depth, size_t timeoutInMilliseconds)
{
	FxRef<CDataFeed> feed = FeedFromHandle(handle);
    feed->SubscribeToQuotes(symbols, depth, static_cast<uint32>(timeoutInMilliseconds));
}

void CFeedImpl::UnsubscribeQuotes(void* handle, const vector<string>& symbols, size_t timeoutInMilliseconds)
{
	FxRef<CDataFeed> feed = FeedFromHandle(handle);
    feed->UnsubscribeQuotes(symbols, static_cast<uint32>(timeoutInMilliseconds));
}

string CFeedImpl::GetBarsHistoryMetaInfoFile(void* handle, const string& symbol, FxPriceType priceType, const string& period, const size_t timeoutInMilliseconds)
{
	FxRef<CDataFeed> feed = FeedFromHandle(handle);
    string result = feed->GetBarsHistoryMetaInfoFile(symbol, priceType, period, static_cast<uint32>(timeoutInMilliseconds));
	return result;
}

string CFeedImpl::GetQuotesHistoryMetaInfoFile(void* handle, const string& symbol, bool includeLevel2, const size_t timeoutInMilliseconds)
{
	FxRef<CDataFeed> feed = FeedFromHandle(handle);
    string result = feed->GetTicksHistoryMetaInfoFile(symbol, includeLevel2, static_cast<uint32>(timeoutInMilliseconds));
	return result;
}

CDataHistoryInfo CFeedImpl::GetHistoryBars(void* handle, const string& symbol, CDateTime& time, int barsNumber, FxPriceType priceType, const string& period, const size_t timeoutInMilliseconds)
{
	FxRef<CDataFeed> feed = FeedFromHandle(handle);
    return feed->GetHistoryBars(symbol, time, barsNumber, priceType, period, static_cast<uint32>(timeoutInMilliseconds));
}

int CFeedImpl::GetQueueThreshold(void* handle)
{
	FxRef<CDataFeed> feed = FeedFromHandle(handle);
	const int result = static_cast<int>(feed->GetThreshold());
	return result;
}

void CFeedImpl::SetQueueThreshold(void* handle, int newSize)
{
	FxRef<CDataFeed> feed = FeedFromHandle(handle);
	feed->SetThreshold(static_cast<size_t>(newSize));
}

int CFeedImpl::GetQuotesHistoryVersion(void* handle, const uint32 timeoutInMilliseconds)
{
	FxRef<CDataFeed> feed = FeedFromHandle(handle);
	return feed->GetQuotesHistoryVersion(timeoutInMilliseconds);
}

