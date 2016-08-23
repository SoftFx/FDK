#include "stdafx.h"
#include "Incomming.h"
#include "Channel.h"
#include "LocalServerHandlerProxy.h"


#define LrpSignature LrpServerSignature
typedef CIncomming LrpChannel;

#include "Server_TypesSerializer.hpp"
#include "Server_Server.hpp"

CIncomming::CIncomming(CChannel& channel, CLocalServerHandlerProxy& proxy) : m_channel(channel), m_proxy(proxy), m_id(channel.GetId())
{
}
const char* CIncomming::Signature()
{
    return LrpServerSignature();
}
CIncomming& CIncomming::GetServer()
{
    return *this;
}
HRESULT CIncomming::Process(MemoryBuffer& buffer)
{
    __try
    {
        return DoProcess(buffer);
    }
    __except(EXCEPTION_EXECUTE_HANDLER)
    {
        return E_FAIL;
    }
}
HRESULT CIncomming::DoProcess(MemoryBuffer& buffer)
{
    const size_t componentId = ReadUInt8(buffer);
    const size_t methodId = ReadUInt8(buffer);

    const HRESULT result = LrpInvokeEx(sizeof(uint16), componentId, methodId, buffer, this);
    return result;
}
void CIncomming::OnHeartBeatRequest()
{
}
void CIncomming::OnHeartBeatResponse()
{
    m_channel.ProcessHeartBeatResponse();
}
void CIncomming::OnTwoFactorAuthRequest(const FxTwoFactorReason reason, const std::string& otp)
{
    m_proxy.BeginTwoFactorAuthRequest(m_id, reason, otp);
}
void CIncomming::OnSessionInfoRequest(const string& requestId)
{
    m_proxy.BeginSessionInformationRequest(m_id, requestId);
}
void CIncomming::OnCurrenciesInfoRequest(const string& requestId)
{
    m_proxy.BeginCurrenciesInformationRequest(m_id, requestId);
}
void CIncomming::OnSymbolsInfoRequest(const string& requestId)
{
    m_proxy.BeginSymbolsInformationRequest(m_id, requestId);
}
void CIncomming::OnSubscribeToQuotesRequest(const string& requestId, const vector<string>& symbols, int32 marketDepth)
{
    m_proxy.BeginSubscribeToQuotesRequest(m_id, requestId, symbols, marketDepth);
}
void CIncomming::OnUnsubscribeQuotesRequest(const string& requestId, const vector<string>& symbols)
{
    m_proxy.BeginUnsubscribeQuotesRequest(m_id, requestId, symbols);
}
void CIncomming::OnComponentsInfoRequest(const string& requestId, const int clientVersion)
{
    m_proxy.BeginComponentsInformationRequest(m_id, requestId, clientVersion);
}
void CIncomming::OnDataHistoryRequest(const string& requestId, const CFxDataHistoryRequest& request)
{
    m_proxy.BeginDataHistoryRequest(m_id, requestId, request);
}
void CIncomming::OnFileChunkRequest(const string& requestId, const string& fileId, const uint32 chunkId)
{
    m_proxy.BeginFileChunkRequest(m_id, requestId, fileId, chunkId);
}
void CIncomming::OnBarsHistoryMetaInfoFileRequest(const string& requestId, const string& symbol, int32 priceType, const string& period)
{
    m_proxy.BeginBarsHistoryMetaInformationFileRequest(m_id, requestId, symbol, priceType, period);
}
void CIncomming::OnQuotesHistoryMetaInfoFileRequest(const string& requestId, const string& symbol, bool includeLevel2)
{
    m_proxy.BeginQuotesHistoryMetaInformationFileRequest(m_id, requestId, symbol, includeLevel2);
}
