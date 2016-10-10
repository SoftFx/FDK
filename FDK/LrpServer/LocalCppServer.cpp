#include "stdafx.h"
#include "LocalCppServer.h"
#include "Server.h"

void* CLocalCppServer::Constructor(void* channels, int port, const string& sertificateFilename, const string& sertificatePassword, void* handler)
{
    CChannelsPool* pChannels = reinterpret_cast<CChannelsPool*>(channels);
    CServer* result = new CServer(*pChannels, port, sertificateFilename, sertificatePassword, handler);
    return result;
}

void CLocalCppServer::Destructor(void* handle)
{
    CServer* pServer = reinterpret_cast<CServer*>(handle);
    if (nullptr != pServer)
    {
        delete pServer;
    }
}

void CLocalCppServer::Start(void* handle)
{
    CServer* pServer = reinterpret_cast<CServer*>(handle);
    pServer->Start();
}

void CLocalCppServer::Stop(void* handle)
{
    CServer* pServer = reinterpret_cast<CServer*>(handle);
    pServer->Stop();
}

void CLocalCppServer::EndConnection(void* handle, int64 id, int32 status)
{
    CServer* pServer = reinterpret_cast<CServer*>(handle);
    pServer->EndConnection(id, status);
}

void CLocalCppServer::EndLogon(void* handle, int64 id, int32 status, const string& message, bool twofactor)
{
    CServer* pServer = reinterpret_cast<CServer*>(handle);
    pServer->EndLogon(id, status, message, twofactor);
}

void CLocalCppServer::SendTwoFactorAuth(void* handle, int64 id, const FxTwoFactorReason reason, const string& text, const CDateTime& expire)
{
    CServer* pServer = reinterpret_cast<CServer*>(handle);
    pServer->SendTwoFactorAuth(id, reason, text, expire);
}

void CLocalCppServer::SendSessionInfo(void* handle, int64 id, const string& requestId, const CFxSessionInfo& sessionInfo)
{
    CServer* pServer = reinterpret_cast<CServer*>(handle);
    pServer->SendSessionInfo(id, requestId, sessionInfo);
}

void CLocalCppServer::SendCurrenciesInfo(void* handle, int64 id, const string& requestId, const vector<CFxCurrencyInfo>& currenciesInfo)
{
    CServer* pServer = reinterpret_cast<CServer*>(handle);
    pServer->SendCurrenciesInfo(id, requestId, currenciesInfo);
}

void CLocalCppServer::SendSymbolsInfo(void* handle, int64 id, const string& requestId, const vector<CFxSymbolInfo>& symbolsInfo)
{
    CServer* pServer = reinterpret_cast<CServer*>(handle);
    pServer->SendSymbolsInfo(id, requestId, symbolsInfo);
}

void CLocalCppServer::SendQuotesSubscriptionConfirm(void* handle, int64 id, const string& requestId)
{
    CServer* pServer = reinterpret_cast<CServer*>(handle);
    pServer->SendQuotesSubscriptionConfirm(id, requestId);
}

void CLocalCppServer::SendQuotesSubscriptionReject(void* handle, int64 id, const string& requestId, const string& message)
{
    CServer* pServer = reinterpret_cast<CServer*>(handle);
    pServer->SendQuotesSubscriptionReject(id, requestId, message);
}

void CLocalCppServer::SendQuotesHistoryVersion(void* handle, int64 id, const string& requestId, const int32 version)
{
    CServer* pServer = reinterpret_cast<CServer*>(handle);
    pServer->SendQuotesHistoryVersion(id, requestId, version);
}

void CLocalCppServer::SendQuote(void* handle, int64 id, const CFxQuote& quote)
{
    CServer* pServer = reinterpret_cast<CServer*>(handle);
    pServer->SendQuote(id, quote);
}

void CLocalCppServer::SendMarketHistoryMetadataResponse(void* handle, int64 id, const string& requestId, int32 status, const string& field)
{
    CServer* pServer = reinterpret_cast<CServer*>(handle);
    pServer->SendMarketHistoryMetadataResponse(id, requestId, status, field);
}

void CLocalCppServer::SendMarketHistoryMetadataReject(void* handle, int64 id, const string& requestId, int32 status, const string& field)
{
    CServer* pServer = reinterpret_cast<CServer*>(handle);
    pServer->SendMarketHistoryMetadataReject(id, requestId, status, field);
}

void CLocalCppServer::SendDataHistoryResponse(void* handle, int64 id, const string& requestId, const CFxDataHistoryResponse& response)
{
    CServer* pServer = reinterpret_cast<CServer*>(handle);
    pServer->SendDataHistoryResponse(id, requestId, response);
}

void CLocalCppServer::SendDataHistoryReject(void* handle, int64 id, const string& requestId, FxMarketHistoryRejectType rejectType, const string& rejectReason)
{
    CServer* pServer = reinterpret_cast<CServer*>(handle);
    pServer->SendDataHistoryReject(id, requestId, rejectType, rejectReason);
}

void CLocalCppServer::SendFileChunk(void* handle, int64 id, const string& requestId, const CFxFileChunk& chunk)
{
    CServer* pServer = reinterpret_cast<CServer*>(handle);
    pServer->SendFileChunk(id, requestId, chunk);
}

void CLocalCppServer::SendNotification(void* handle, int64 id, const CNotification& notification)
{
    CServer* pServer = reinterpret_cast<CServer*>(handle);
    pServer->SendNotification(id, notification);
}

void CLocalCppServer::SendBusinessReject(void* handle, int64 id, const string& rejectReason, const string& rejectTag)
{
    CServer* pServer = reinterpret_cast<CServer*>(handle);
    pServer->SendBusinessReject(id, rejectReason, rejectTag);
}
