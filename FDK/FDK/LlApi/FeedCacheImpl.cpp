#include "stdafx.h"
#include "FeedCacheImpl.h"
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

vector<CFxSymbolInfo> CFeedCacheImpl::GetSymbols(void* handle)
{
	FxRef<CDataFeed> feed = FeedFromHandle(handle);
	return feed->Cache().GetSymbols();
}

vector<CFxCurrencyInfo> CFeedCacheImpl::GetCurrencies(void* handle)
{
    FxRef<CDataFeed> feed = FeedFromHandle(handle);
    return feed->Cache().GetCurrencies();
}

bool CFeedCacheImpl::TryGetBid(void* handle, const string& symbol, double& price, double& volume, CDateTime& creationTime)
{
	FxRef<CDataFeed> feed = FeedFromHandle(handle);
	const bool result = feed->Cache().TryGetBid(symbol, price, volume, creationTime);
	return result;
}

bool CFeedCacheImpl::TryGetAsk(void* handle, const string& symbol, double& price, double& volume, CDateTime& creationTime)
{
	FxRef<CDataFeed> feed = FeedFromHandle(handle);
	const bool result = feed->Cache().TryGetAsk(symbol, price, volume, creationTime);
	return result;
}

bool CFeedCacheImpl::TryGetQuote(void* handle, const string& symbol, CFxQuote& quote)
{
	FxRef<CDataFeed> feed = FeedFromHandle(handle);
	const bool result = feed->Cache().TryGetQuote(symbol, quote);
	return result;
}
