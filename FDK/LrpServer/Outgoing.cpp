#include "stdafx.h"
#include "Outgoing.h"
#include "Channel.h"
#include "SymbolToKey.h"

#define LrpSignature LrpClientSignature

#include "Client_Signature.hpp"
#include "Client_TypesSerializer.hpp"
#include "Client.hpp"

COutgoing::COutgoing(CChannel& channel)
    : m_channel(channel)
    , m_translators(LrpClientSignature())
{
}

COutgoing::~COutgoing()
{
}

void COutgoing::Initialize(const string& remoteSignature)
{
    m_translators.Initialize(remoteSignature);
}

void COutgoing::Initialize(MemoryBuffer& buffer)
{
    MemoryBuffer temp(CHeap::Instance());
    temp.SetPosition(sizeof(uint32));
    std::swap(buffer, temp);
}

HRESULT COutgoing::Invoke(const uint16 componentId, const uint16 methodId, MemoryBuffer& buffer)
{
    return DoInvoke(-1, componentId, methodId, buffer);
}

HRESULT COutgoing::DoInvoke(const ptrdiff_t key, uint16 componentId, uint16 methodId, MemoryBuffer& buffer)
{
    size_t dataSize = buffer.GetSize() - sizeof(uint16);
    if (dataSize > numeric_limits<uint16>::max())
    {
        return E_FAIL;
    }

    uint16 _componentId = componentId;
    uint16 _methodId = methodId;

    m_translators.Translate(_componentId, _methodId);

    assert(_componentId <= numeric_limits<uint8>::max());
    assert(_methodId <= numeric_limits<uint8>::max());

    buffer.SetPosition(0);

    WriteUInt16(static_cast<uint16>(dataSize), buffer);
    WriteUInt8(static_cast<uint8>(_componentId), buffer);
    WriteUInt8(static_cast<uint8>(_methodId), buffer);
    buffer.SetPosition(0);

    CMessage message(key, componentId, methodId, buffer);
    const HRESULT result = m_channel.SendMessage(message);
    return result;
}

bool COutgoing::IsSupported(uint16 componentId) const
{
    const bool result = m_translators.IsSupported(componentId);
    return result;
}

bool COutgoing::IsSupported(uint16 componentId, uint16 methodId) const
{
    const bool result = m_translators.IsSupported(componentId, methodId);
    return result;
}

void COutgoing::SendLogon(const string& protocolVersion)
{
    Client client(*this);
    client.OnLogonMsg(protocolVersion);
}

void COutgoing::SendLogout(const FxLogoutReason reason, const string& description)
{
    Client client(*this);
    client.OnLogoutMsg(reason, description);
}

void COutgoing::SendTwoFactorAuth(const FxTwoFactorReason reason, const string& text, const CDateTime& expire)
{
    Client client(*this);
    client.OnTwoFactorAuthMsg(reason, text, expire);
}

void COutgoing::SendSessionInfo(const string& requestId, const CFxSessionInfo& sessionInfo)
{
    Client client(*this);

    if (client.Is_OnSessionInfoMsg2_Supported())
        client.OnSessionInfoMsg2(requestId, sessionInfo);
    else
        client.OnSessionInfoMsg(requestId, sessionInfo);
}

void COutgoing::SendCurrenciesInfo(const string& requestId, const vector<CFxCurrencyInfo>& currenciesInfo)
{
    Client client(*this);
    client.OnCurrenciesInfoMsg(requestId, currenciesInfo);
}

void COutgoing::SendSymbolsInfo(const string& requestId, const vector<CFxSymbolInfo>& symbolsInfo)
{
    Client client(*this);

    if (client.Is_OnSymbolsInfoMsg7_Supported())
        client.OnSymbolsInfoMsg7(requestId, symbolsInfo);
    else if (client.Is_OnSymbolsInfoMsg6_Supported())
        client.OnSymbolsInfoMsg6(requestId, symbolsInfo);
    else if (client.Is_OnSymbolsInfoMsg5_Supported())
        client.OnSymbolsInfoMsg5(requestId, symbolsInfo);
    else if (client.Is_OnSymbolsInfoMsg4_Supported())
        client.OnSymbolsInfoMsg4(requestId, symbolsInfo);
    else if (client.Is_OnSymbolsInfoMsg3_Supported())
        client.OnSymbolsInfoMsg3(requestId, symbolsInfo);
    else if (client.Is_OnSymbolsInfoMsg2_Supported())
        client.OnSymbolsInfoMsg2(requestId, symbolsInfo);
    else
        client.OnSymbolsInfoMsg(requestId, symbolsInfo);
}

void COutgoing::SendSubscribeToQuotesResponse(const string& requestId, const int32 status, const string& message)
{
    Client client(*this);
    client.OnQuotesSubscriptionMsg(requestId, status, message);
}

void COutgoing::SendUnsubscribeQuotesResponse(const string& requestId, const int32 status, const string& message)
{
    Client client(*this);
    client.OnQuotesSubscriptionMsg(requestId, status, message);
}

void COutgoing::SendQuotesHistoryVersion(const string& requestId, const int32 version)
{
    Client client(*this);
    client.OnComponentsInfoMsg(requestId, version);
}

void COutgoing::SendQuote(const ptrdiff_t key, const CFxQuote& quote)
{
    MemoryBuffer buffer;
    Initialize(buffer);

    WriteTime(FxUtcNow(), buffer);
    WriteQuote(quote, buffer);

    const HRESULT _status = DoInvoke(key, Client::LrpComponentId, Client::LrpMethod_OnQuoteRawMsg_Id, buffer);
    Throw(_status, buffer);
}

void COutgoing::SendMarketHistoryMetadataResponse(const string& requestId, const int32 status, const string& field)
{
    Client client(*this);
    client.OnDataHistoryMetaInfoResponseMsg(requestId, status, field);
}

void COutgoing::SendMarketHistoryMetadataReject(const string& requestId, const int32 status, const string& field)
{
    Client client(*this);
    client.OnDataHistoryMetaInfoRejectMsg(requestId, status, field);
}

void COutgoing::SendDataHistoryResponse(const string& requestId, const CFxDataHistoryResponse& response)
{
    Client client(*this);
    client.OnDataHistoryResponseMsg(requestId, response);
}

void COutgoing::SendDataHistoryReject(const string& requestId, FxMarketHistoryRejectType rejectType, const string& rejectReason)
{
    Client client(*this);
    client.OnDataHistoryRejectMsg(requestId, rejectType, rejectReason);
}

void COutgoing::SendFileChunk(const string& requestId, const CFxFileChunk& chunk)
{
    Client client(*this);
    client.OnFileChunkMsg(requestId, chunk);
}

void COutgoing::SendHeartBeatRequest()
{
    Client client(*this);
    client.OnHeartBeatRequest();
}

void COutgoing::SendNotification(const CNotification& notification)
{
    Client client(*this);

    if (client.Is_OnNotificationMsg_Supported())
        client.OnNotificationMsg(notification);
}

void COutgoing::SendBusinessReject(const string& rejectReason, const string& rejectTag)
{
    Client client(*this);

    if (client.Is_OnBusinessRejectMsg_Supported())
        client.OnBusinessRejectMsg(rejectReason, rejectTag);
}
