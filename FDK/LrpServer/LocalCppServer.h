#pragma once


class CLocalCppServer
{
public:
    static void* Constructor(void* channels, int port, const string& sertificateFilename, const string& sertificatePassword, void* handler);
    static void Destructor(void* handle);
    static void Start(void* handle);
    static void Stop(void* handle);
public:
    static void EndConnection(void* handle, int64 id, int32 status);
    static void EndLogon(void* handle, int64 id, int32 status, const string& message);
    static void SendSessionInfo(void* handle, int64 id, const string& requestId, const CFxSessionInfo& sessionInfo);
    static void SendCurrenciesInfo(void* handle, int64 id, const string& requestId, const vector<CFxCurrencyInfo>& currenciesInfo);
    static void SendSymbolsInfo(void* handle, int64 id, const string& requestId, const vector<CFxSymbolInfo>& symbolsInfo);
    static void SendQuotesSubscriptionConfirm(void* handle, int64 id, const string& requestId);
    static void SendQuotesSubscriptionReject(void* handle, int64 id, const string& requestId, const string& message);
    static void SendQuotesHistoryVersion(void* handle, int64 id, const string& requestId, const int32 version);
    static void SendQuote(void* handle, int64 id, const CFxQuote& quote);
    static void SendMarketHistoryMetadataResponse(void* handle, int64 id, const string& requestId, int32 status, const string& field);
    static void SendMarketHistoryMetadataReject(void* handle, int64 id, const string& requestId, int32 status, const string& field);
    static void SendDataHistoryResponse(void* handle, int64 id, const string& requestId, const CFxDataHistoryResponse& response);
    static void SendDataHistoryReject(void* handle, int64 id, const string& requestId, FxMarketHistoryRejectType rejectType, const string& rejectReason);
    static void SendFileChunk(void* handle, int64 id, const string& requestId, const CFxFileChunk& chunk);
    static void SendNotification(void* handle, int64 id, const CNotification& notification);
    static void SendBusinessReject(void* handle, int64 id, const string& rejectReason, const string& rejectTag);
};