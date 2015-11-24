#pragma once

#include "HandleImpl.h"
#include "ParamsImpl.h"
#include "ClientImpl.h"
#include "ClientCacheImpl.h"
#include "FeedImpl.h"
#include "FeedCacheImpl.h"
#include "TradeImpl.h"
#include "TradeCacheImpl.h"
#include "ConverterImpl.h"
#include "IteratorImpl.h"
#include "LibraryImpl.h"

class Channel
{
public:
	static CHandleImpl& GetHandle();
	static CParamsImpl& GetParams();
	static CClientImpl& GetClientServer();
	static CClientCacheImpl& GetClientCache();
	static CTradeImpl& GetTradeServer();
	static CTradeCacheImpl& GetTradeCache();
	static CFeedImpl& GetFeedServer();
	static CFeedCacheImpl& GetFeedCache();
	static CConverterImpl& GetConverter();
	static CIteratorImpl& GetIterator();
	static CLibraryImpl& GetLibrary();
};