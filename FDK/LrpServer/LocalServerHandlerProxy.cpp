#include "stdafx.h"
#include "LocalServerHandlerProxy.h"
#include "LocalCSharp_TypesSerializer.hpp"
#include "LocalServerHandler.hpp"
#include "LocalCppLibrary.h"

CLocalServerHandlerProxy::CLocalServerHandlerProxy(void* handle) : m_handle(handle)
{
    const HRESULT status = InitializeLocalCSharpClient(m_client);
    if (FAILED(status))
    {
        throw runtime_error("Couldn't initialize local C# client");
    }
}
CLocalServerHandlerProxy::~CLocalServerHandlerProxy()
{
}
void CLocalServerHandlerProxy::BeginLogonRequest(const uint64 id, const string& address, const int port, const string& username, const string& password, const string& deviceid, const string& appsessionid)
{
    LocalServerHandler handler(m_client);
    handler.BeginLogonRequest(m_handle, id, address, port, username, password, deviceid, appsessionid);
}
void CLocalServerHandlerProxy::BeginTwoFactorAuthResponse(const uint64 id, const FxTwoFactorReason reason, const std::string& otp)
{
    LocalServerHandler handler(m_client);
    handler.BeginTwoFactorAuthResponse(m_handle, id, reason, otp);
}
void CLocalServerHandlerProxy::BeginCurrenciesInformationRequest(const uint64 id, const string& requestId)
{
    LocalServerHandler handler(m_client);
    handler.BeginCurrenciesInfoRequest(m_handle, id, requestId);
}
void CLocalServerHandlerProxy::BeginSymbolsInformationRequest(const uint64 id, const string& requestId)
{
    LocalServerHandler handler(m_client);
    handler.BeginSymbolsInfoRequest(m_handle, id, requestId);
}
void CLocalServerHandlerProxy::BeginSessionInformationRequest(const uint64 id, const string& requestId)
{
    LocalServerHandler handler(m_client);
    handler.BeginSessionInfoRequest(m_handle, id, requestId);
}
void CLocalServerHandlerProxy::BeginSubscribeToQuotesRequest(const uint64 id, const string& requestId, const vector<string>& symbols, int32 marketDepth)
{
    LocalServerHandler handler(m_client);
    handler.BeginSubscribeToQuotesRequest(m_handle, id, requestId, symbols, marketDepth);
}
void CLocalServerHandlerProxy::BeginUnsubscribeQuotesRequest(const uint64 id, const string& requestId, const vector<string>& symbols)
{
    LocalServerHandler handler(m_client);
    handler.BeginUnsubscribeQuotesRequest(m_handle, id, requestId, symbols);
}
void CLocalServerHandlerProxy::BeginComponentsInformationRequest(const uint64 id, const string& requestId, const int clientVersion)
{
    LocalServerHandler handler(m_client);
    handler.BeginComponentsInfoRequest(m_handle, id, requestId, clientVersion);
}
void CLocalServerHandlerProxy::BeginDataHistoryRequest(const uint64 id, const string& requestId, const CFxDataHistoryRequest& request)
{
    LocalServerHandler handler(m_client);
    handler.BeginDataHistoryRequest(m_handle, id, requestId, request);
}
void CLocalServerHandlerProxy::BeginFileChunkRequest(const uint64 id, const string& requestId, const string& fileId, const uint32 chunkId)
{
    LocalServerHandler handler(m_client);
    handler.BeginFileChunkRequest(m_handle, id, requestId, fileId, chunkId);
}
void CLocalServerHandlerProxy::BeginBarsHistoryMetaInformationFileRequest(const uint64 id, const string& requestId, const string& symbol, int32 priceType, const string& period)
{
    LocalServerHandler handler(m_client);
    handler.BeginBarsHistoryMetaInfoFileRequest(m_handle, id, requestId, symbol, priceType, period);
}
void CLocalServerHandlerProxy::BeginQuotesHistoryMetaInformationFileRequest(const uint64 id, const string& requestId, const string& symbol, bool includeLevel2)
{
    LocalServerHandler handler(m_client);
    handler.BeginQuotesHistoryMetaInfoFileRequest(m_handle, id, requestId, symbol, includeLevel2);
}

void CLocalServerHandlerProxy::BeginShutdownConnectionNotification(const uint64 id)
{
    LocalServerHandler handler(m_client);
    handler.BeginShutdownConnectionNotification(m_handle, id);
}
