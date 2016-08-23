#ifndef __Native_Data_Feed__
#define __Native_Data_Feed__

#include "Client.h"
#include "DataFeedCache.h"

class CDataFeed : public CClient
{
public:
    CDataFeed(const string& connectionString);
    ~CDataFeed();

private:
    CDataFeed(const CDataFeed&);
    CDataFeed& operator = (const CDataFeed&);

public:
    vector<CFxCurrencyInfo> GetCurrencies(uint32 timeoutInMilliseconds);
    vector<CFxSymbolInfo> GetSupportedSymbols(uint32 timeoutInMilliseconds);
    HRESULT SubscribeToQuotes(const vector<string>& symbols, int32 depth, uint32 timeoutInMilliseconds);
    HRESULT UnsubscribeQuotes(const vector<string>& symbols, uint32 timeoutInMilliseconds);
    int GetQuotesHistoryVersion(const uint32 timeoutInMilliseconds);

public:
    CDataHistoryInfo GetHistoryBars(const string& symbol, CDateTime time, int32 barsNumber, FxPriceType priceType, const string& period, const uint32 timeoutInMilliseconds);
    CDataHistoryInfo GetQuoteHistoryFiles(const string& symbol, bool includeLevel2, CDateTime time, const uint32 timeoutInMillisecond);
    CDataHistoryInfo GetBarsHistoryFiles(const string& symbol, int32 priceType, const string& period, CDateTime time, const uint32 timeoutInMillisecond);
    string GetBarsHistoryMetaInfoFile(const string& symbol, FxPriceType priceType, const string& period, const uint32 timeoutInMilliseconds);
    string GetTicksHistoryMetaInfoFile(const string& symbol, bool includeLevel2, const uint32 timeoutInMilliseconds);

public:
    virtual void VLogon(const CFxEventInfo& eventInfo, const string& protocolVersion);
    virtual void VTwoFactorAuth(const CFxEventInfo& eventInfo, const FxTwoFactorReason reason, const std::string& text, const CDateTime& expire);
    virtual void VLogout(const CFxEventInfo& eventInfo, const FxLogoutReason reason, const string& description);

    virtual void VTick(const CFxEventInfo& eventInfo, const CFxQuote& quote);
    virtual void VGetCurrencies(const CFxEventInfo& eventInfo, const vector<CFxCurrencyInfo>& currencies);
    virtual void VGetSupportedSymbols(const CFxEventInfo& eventInfo, const vector<CFxSymbolInfo>& symbols);
    virtual void VNotify(const CFxEventInfo& eventInfo, const CNotification& notification);
    virtual void VQuotesHistoryResponse(const CFxEventInfo& eventInfo, const int version);

public:
    const CDataFeedCache& Cache() const;
    CDataFeedCache& Cache();

private:
    HANDLE m_serverQuotesHistoryEvent;
    CCriticalSection m_synchronizer;
    CDataFeedCache m_cache;
};


#pragma region inline methods

inline const CDataFeedCache& CDataFeed::Cache() const
{
    return m_cache;
}

inline CDataFeedCache& CDataFeed::Cache()
{
    return m_cache;
}

#pragma endregion

#endif
