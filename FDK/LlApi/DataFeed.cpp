#include "stdafx.h"
#include "DataFeed.h"
#include "Waiter.h"

#ifdef _MSC_VER
#pragma warning (disable : 4355)
#else
typedef CClient __super;
#endif

namespace
{
    const string cUnsupportedFeature = "Feature is not supported by this protocol version.";
}

CDataFeed::CDataFeed(const string& connectionString)
    : CClient(m_cache, connectionString)
    , m_cache(*this)
{
    m_serverQuotesHistoryEvent = CreateEvent(nullptr, true, false, nullptr);
}

CDataFeed::~CDataFeed()
{
    CloseHandle(m_serverQuotesHistoryEvent);
}

HRESULT CDataFeed::SubscribeToQuotes(const vector<string>& symbols, int32 depth, uint32 timeoutInMilliseconds)
{
    Waiter<HRESULT> waiter(timeoutInMilliseconds, cExternalSynchCall, *this);
    m_sender->VSendSubscribeToQuotes(waiter.Id(), symbols, depth);
    CFxEventInfo info;
    HRESULT result = waiter.WaitForResponse(info);
    if (FAILED(result))
    {
        throw CRejectException(info.Message, result);
    }
    return result;
}

HRESULT CDataFeed::UnsubscribeQuotes(const vector<string>& symbols, uint32 timeoutInMilliseconds)
{
    Waiter<HRESULT> waiter(timeoutInMilliseconds, cExternalSynchCall, *this);
    m_sender->VSendUnsubscribeQuotes(waiter.Id(), symbols);

    HRESULT result = waiter.WaitForResponse();

    return result;
}

vector<CFxCurrencyInfo> CDataFeed::GetCurrencies(uint32 timeoutInMilliseconds)
{
    if (!CheckProtocolVersion(CProtocolVersion(1, 24)))
    {
        throw CUnsupportedFeatureException(cUnsupportedFeature, "GetCurrencies");
    }

    Waiter<vector<CFxCurrencyInfo> > waiter(timeoutInMilliseconds, cExternalSynchCall, *this);
    m_sender->VSendGetCurrencies(waiter.Id());

    vector<CFxCurrencyInfo> result = waiter.WaitForResponse();
    return result;
}

vector<CFxSymbolInfo> CDataFeed::GetSupportedSymbols(uint32 timeoutInMilliseconds)
{
    Waiter<vector<CFxSymbolInfo> > waiter(timeoutInMilliseconds, cExternalSynchCall, *this);
    m_sender->VSendGetSupportedSymbols(waiter.Id());

    vector<CFxSymbolInfo> result = waiter.WaitForResponse();
    return result;
}

void CDataFeed::VTick(const CFxEventInfo& eventInfo, const CFxQuote& quote)
{
    m_cache.UpdateQuotes(quote);

    CFxMessage message(FX_MSG_TICK, eventInfo);
    message.Data = new CFxMsgTick(quote);
    ProcessMessage(quote.Symbol, message);
}

void CDataFeed::VLogon(const CFxEventInfo& eventInfo, const string& protocolVersion)
{
    __super::VLogon(eventInfo, protocolVersion);

    ResetEvent(m_serverQuotesHistoryEvent);
    m_cache.Clear();

    string id = NextId(cInternalASynchCall);
    m_sender->VSendGetSupportedSymbols(id);

    if (CheckProtocolVersion(CProtocolVersion(1, 24)))
    {
        id = NextId(cInternalASynchCall);
        m_sender->VSendGetCurrencies(id);
    }

    id = NextId(cInternalASynchCall);
    m_sender->VSendQuotesHistoryRequest(id);
}

void CDataFeed::VLogout(const CFxEventInfo& eventInfo, const FxLogoutReason reason, const string& description)
{
    m_cache.Clear();
    SetEvent(m_serverQuotesHistoryEvent);
    __super::VLogout(eventInfo, reason, description);
}

void CDataFeed::VGetCurrencies(const CFxEventInfo& eventInfo, const vector<CFxCurrencyInfo>& currencies)
{
    if (eventInfo.IsInternalAsynchCall())
    {
        m_cache.UpdateCurrencies(currencies);
    }
    __super::VGetCurrencies(eventInfo, currencies);
}

void CDataFeed::VGetSupportedSymbols(const CFxEventInfo& eventInfo, const vector<CFxSymbolInfo>& symbols)
{
    if (eventInfo.IsInternalAsynchCall())
    {
        m_cache.UpdateSymbols(symbols);

        if (!CheckProtocolVersion(CProtocolVersion(1, 24)))
        {
            map<string, CFxCurrencyInfo> currenciesMap;

            for (const auto& s : symbols)
            {
                if (currenciesMap.find(s.Currency) == currenciesMap.end())
                    currenciesMap[s.Currency] = CFxCurrencyInfo(s.Currency, "", s.CurrencySortOrder, s.CurrencyPrecision);

                if (currenciesMap.find(s.SettlementCurrency) == currenciesMap.end())
                    currenciesMap[s.SettlementCurrency] = CFxCurrencyInfo(s.SettlementCurrency, "", s.SettlementCurrencySortOrder, s.SettlementCurrencyPrecision);
            }

            vector<CFxCurrencyInfo> currencies;

            for (const auto& c : currenciesMap)
            {
                currencies.push_back(c.second);
            }

            m_cache.UpdateCurrencies(currencies);
        }
    }
    __super::VGetSupportedSymbols(eventInfo, symbols);
}

CDataHistoryInfo CDataFeed::GetHistoryBars(const string& symbol, CDateTime time, int32 barsNumber, FxPriceType priceType, const string& period, const uint32 timeoutInMilliseconds)
{
    Waiter<CFxDataHistoryResponse> waiter(timeoutInMilliseconds, cExternalSynchCall, *this);

    CFxDataHistoryRequest request(symbol, period);
    request.ReportType = FX_REPORT_TYPE_GROUPS;
    request.GraphType = FX_GRAPH_TYPE_BARS;
    request.BarsNumber = barsNumber;
    request.Time = time;
    request.PriceType = priceType;
    m_sender->VSendDataHistoryRequest(waiter.Id(), request);

    CFxEventInfo info;
    CFxDataHistoryResponse response = waiter.WaitForResponse(info);
    if (FAILED(info.Status))
    {
        throw runtime_error(info.Message);
    }

    if (barsNumber > 0)
    {
        response.SortForward();
    }
    else
    {
        response.SortBackward();
    }

    CDataHistoryInfo result;
    result.FromAll = response.FromAll;
    result.ToAll = response.ToAll;
    result.From = response.From;
    result.To = response.To;
    result.LastTickId = response.LastTickId;
    std::swap(response.Bars, result.Bars);

    return result;
}

CDataHistoryInfo CDataFeed::GetBarsHistoryFiles(const string& symbol, int32 priceType, const string& period, CDateTime time, uint32 timeoutInMilliseconds)
{
    Waiter<CFxDataHistoryResponse> waiter(timeoutInMilliseconds, cExternalSynchCall, *this);

    CFxDataHistoryRequest request(symbol, period);
    request.BarsNumber = 0;
    request.Time = time;
    request.PriceType = priceType;
    request.ReportType = FX_REPORT_TYPE_FILE;
    request.GraphType = FX_GRAPH_TYPE_BARS;
    m_sender->VSendDataHistoryRequest(waiter.Id(), request);

    CFxDataHistoryResponse response = waiter.WaitForResponse();

    CDataHistoryInfo result;

    std::swap(result.Files, response.Files);

    result.FromAll = response.FromAll;
    result.ToAll = response.ToAll;
    if (!result.Files.empty())
    {
        result.From = response.From;
        result.To = response.To;
    }
    result.LastTickId = response.LastTickId;

    return result;
}

CDataHistoryInfo CDataFeed::GetQuoteHistoryFiles(const string& symbol, bool includeLevel2, CDateTime time, const uint32 timeoutInMilliseconds)
{
    Waiter<CFxDataHistoryResponse> waiter(timeoutInMilliseconds, cExternalSynchCall, *this);

    CFxDataHistoryRequest request(symbol);
    request.BarsNumber = 0;
    request.Time = time;
    request.PriceType = FxPriceType_None;
    request.ReportType = FX_REPORT_TYPE_FILE;
    request.GraphType = includeLevel2 ? FX_GRAPH_TYPE_LEVEL2 : FX_GRAPH_TYPE_TICKS;
    m_sender->VSendDataHistoryRequest(waiter.Id(), request);

    CFxDataHistoryResponse response = waiter.WaitForResponse();

    CDataHistoryInfo result;
    std::swap(result.Files, response.Files);
    if ((response.From > time) || (response.To < time))
    {
        result.Files.clear();
    }
    result.FromAll = response.FromAll;
    result.ToAll = response.ToAll;
    if (!result.Files.empty())
    {
        result.From = response.From;
        result.To = response.To;
    }
    std::swap(result.LastTickId, response.LastTickId);

    return result;
}

string CDataFeed::GetBarsHistoryMetaInfoFile(const string& symbol, FxPriceType priceType, const string& period, const uint32 timeoutInMilliseconds)
{
    Waiter<string> waiter(timeoutInMilliseconds, cExternalSynchCall, *this);
    m_sender->VSendGetBarsHistoryMetaInfoFile(waiter.Id(), symbol, priceType, period);

    string result = waiter.WaitForResponse();
    return result;
}

string CDataFeed::GetTicksHistoryMetaInfoFile(const string& symbol, bool includeLevel2, const uint32 timeoutInMilliseconds)
{
    Waiter<string> waiter(timeoutInMilliseconds, cExternalSynchCall, *this);
    m_sender->VSendGetTicksHistoryMetaInfoFile(waiter.Id(), symbol, includeLevel2);
    string result = waiter.WaitForResponse();
    return result;
}

void CDataFeed::VNotify(const CFxEventInfo& eventInfo, const CNotification& notification)
{
    if (NotificationType_ConfigUpdated == notification.Type)
    {
        string id = NextId(cInternalASynchCall);
        m_sender->VSendGetSupportedSymbols(id);

        if (CheckProtocolVersion(CProtocolVersion(1, 24)))
        {
            id = NextId(cInternalASynchCall);
            m_sender->VSendGetCurrencies(id);
        }

    }
    __super::VNotify(eventInfo, notification);
}

void CDataFeed::VQuotesHistoryResponse(const CFxEventInfo& /*eventInfo*/, const int version)
{
    m_cache.SetServerQuotesHistoryVersion(version);
    SetEvent(m_serverQuotesHistoryEvent);
}

int CDataFeed::GetQuotesHistoryVersion(const uint32 timeoutInMilliseconds)
{
    Nullable<int> version;

    DWORD status = WaitForSingleObject(m_serverQuotesHistoryEvent, 0);
    version = m_cache.GetServerQuotesHistoryVersion();

    string id;

    if (!version.HasValue())
    {
        id = NextId(cExternalSynchCall);
        m_sender->VSendQuotesHistoryRequest(id);

        status = WaitForSingleObject(m_serverQuotesHistoryEvent, timeoutInMilliseconds);
        version = m_cache.GetServerQuotesHistoryVersion();
    }

    if (version.HasValue())
    {
        return version.Value();
    }

    if (WAIT_OBJECT_0 == status)
    {
        throw runtime_error("Couldn't get server quotes storage version due to logout");
    }
    else
    {
        throw CTimeoutException(id, 0);
    }
}