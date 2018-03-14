#include "stdafx.h"
#include "Channel.h"


namespace
{
    CHandleImpl gHandleImpl;
    CParamsImpl gParmsImpl;
    CClientImpl gClientImpl;
    CClientCacheImpl gClientCacheImpl;
    CFeedImpl gFeedImpl;
    CFeedCacheImpl gFeedCacheImpl;
    CTradeImpl gTradeImpl;
    CTradeCacheImpl gTradeCacheImpl;
    CConverterImpl gConverterImpl;
    CTradeHistoryIteratorImpl gTradeHistoryIteratorImpl;
    CDailySnapshotsIteratorImpl gDailySnapshotsIteratorImpl;
    CLibraryImpl gLibraryImpl;
}

CHandleImpl& Channel::GetHandle()
{
    return gHandleImpl;
}
CParamsImpl& Channel::GetParams()
{
    return gParmsImpl;
}
CClientImpl& Channel::GetClientServer()
{
    return gClientImpl;
}
CClientCacheImpl& Channel::GetClientCache()
{
    return gClientCacheImpl;
}
CTradeImpl& Channel::GetTradeServer()
{
    return gTradeImpl;
}
CTradeCacheImpl& Channel::GetTradeCache()
{
    return gTradeCacheImpl;
}
CFeedImpl& Channel::GetFeedServer()
{
    return gFeedImpl;
}
CFeedCacheImpl& Channel::GetFeedCache()
{
    return gFeedCacheImpl;
}
CConverterImpl& Channel::GetConverter()
{
    return gConverterImpl;
}
CTradeHistoryIteratorImpl& Channel::GetTradeHistoryIterator()
{
    return gTradeHistoryIteratorImpl;
}
CDailySnapshotsIteratorImpl& Channel::GetDailySnapshotsIterator()
{
    return gDailySnapshotsIteratorImpl;
}
CLibraryImpl& Channel::GetLibrary()
{
    return gLibraryImpl;
}
