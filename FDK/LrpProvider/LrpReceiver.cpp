#include "stdafx.h"
#include "LrpReceiver.h"
#include "LrpSender.h"


#define LrpSignature LrpClientSignature


typedef CLrpReceiver LrpChannel;

#include "Client_TypesSerializer.hpp"
#include "Client_Server.hpp"

#undef LrpSignature

CLrpReceiver::CLrpReceiver()
    : m_receiver()
    , m_sender()
{
}

CLrpReceiver::CLrpReceiver(IReceiver* pReceiver, CLrpSender* pSender)
    : m_receiver(pReceiver)
    , m_sender(pSender)
{
    m_codec.SetReceiver(pReceiver);
}

const char* CLrpReceiver::Signature()
{
    const char* result = LrpClientSignature();
    return result;
}

CLrpReceiver& CLrpReceiver::GetClient()
{
    return *this;
}

CSimpleCodec& CLrpReceiver::GetSimpleCodec()
{
    return m_codec;
}

void CLrpReceiver::OnHeartBeatRequest()
{
    m_sender->SendHeartBeatResponse();
}

void CLrpReceiver::OnHeartBeatResponse()
{
}

void CLrpReceiver::OnLogonMsg(const string& protocolVersion)
{
    CFxEventInfo info;
    m_receiver->VLogon(info, protocolVersion);
}

void CLrpReceiver::OnTwoFactorAuthMsg(const FxTwoFactorReason reason, const string& text, const CDateTime& expire)
{
    CFxEventInfo info;
    m_receiver->VTwoFactorAuth(info, reason, text, expire);
}

void CLrpReceiver::OnLogoutMsg(const FxLogoutReason reason, const string& description)
{
    CFxEventInfo info;
    m_receiver->VLogout(info, reason, description);
}

void CLrpReceiver::OnSessionInfoMsg(const string& requestId, const CFxSessionInfo& info)
{
    CFxSessionInfo temp = info;
    m_receiver->VSessionInfo(CFxEventInfo(requestId), temp);
}

void CLrpReceiver::OnSessionInfoMsg2(const string& requestId, const CFxSessionInfo& info)
{
    CFxSessionInfo temp = info;
    m_receiver->VSessionInfo(CFxEventInfo(requestId), temp);
}

void CLrpReceiver::AdjustSymbolsTradeAmounts(vector<CFxSymbolInfo>& symbols)
{
    for (auto& symbol : symbols)
    {
        symbol.MinTradeVolume *= symbol.RoundLot;
        symbol.TradeVolumeStep *= symbol.RoundLot;
        symbol.MaxTradeVolume *= symbol.RoundLot;
    }
}

void CLrpReceiver::OnCurrenciesInfoMsg(const string& requestId, const vector<CFxCurrencyInfo>& currencies)
{
    vector<CFxCurrencyInfo> temp = currencies;
    m_receiver->VGetCurrencies(CFxEventInfo(requestId), temp);
}

void CLrpReceiver::OnSymbolsInfoMsg(const string& requestId, const vector<CFxSymbolInfo>& symbols)
{
    vector<CFxSymbolInfo> temp = symbols;
    AdjustSymbolsTradeAmounts(temp);
    m_receiver->VGetSupportedSymbols(CFxEventInfo(requestId), temp);
}

void CLrpReceiver::OnSymbolsInfoMsg2(const string& requestId, const vector<CFxSymbolInfo>& symbols)
{
    vector<CFxSymbolInfo> temp = symbols;
    AdjustSymbolsTradeAmounts(temp);
    m_receiver->VGetSupportedSymbols(CFxEventInfo(requestId), temp);
}

void CLrpReceiver::OnSymbolsInfoMsg3(const string& requestId, const vector<CFxSymbolInfo>& symbols)
{
    vector<CFxSymbolInfo> temp = symbols;
    AdjustSymbolsTradeAmounts(temp);
    m_receiver->VGetSupportedSymbols(CFxEventInfo(requestId), temp);
}

void CLrpReceiver::OnSymbolsInfoMsg4(const string& requestId, const vector<CFxSymbolInfo>& symbols)
{
    vector<CFxSymbolInfo> temp = symbols;
    AdjustSymbolsTradeAmounts(temp);
    m_receiver->VGetSupportedSymbols(CFxEventInfo(requestId), temp);
}

void CLrpReceiver::OnSymbolsInfoMsg5(const string& requestId, const vector<CFxSymbolInfo>& symbols)
{
    vector<CFxSymbolInfo> temp = symbols;
    AdjustSymbolsTradeAmounts(temp);
    m_receiver->VGetSupportedSymbols(CFxEventInfo(requestId), temp);
}

void CLrpReceiver::OnSymbolsInfoMsg6(const string& requestId, const vector<CFxSymbolInfo>& symbols)
{
    vector<CFxSymbolInfo> temp = symbols;
    AdjustSymbolsTradeAmounts(temp);
    m_receiver->VGetSupportedSymbols(CFxEventInfo(requestId), temp);
}

void CLrpReceiver::OnSymbolsInfoMsg7(const string& requestId, const vector<CFxSymbolInfo>& symbols)
{
    vector<CFxSymbolInfo> temp = symbols;
    AdjustSymbolsTradeAmounts(temp);
    m_receiver->VGetSupportedSymbols(CFxEventInfo(requestId), temp);
}

void CLrpReceiver::OnQuotesSubscriptionMsg(const string& requestId, int32 status, const string& message)
{
    if (SUCCEEDED(status))
    {
        m_receiver->VSubscribeToQuotes(CFxEventInfo(requestId), S_OK);
    }
    else
    {
        m_receiver->VSubscribeToQuotes(CFxEventInfo(requestId, message), FX_CODE_ERROR_REJECT);
    }
}

void CLrpReceiver::OnComponentsInfoMsg(const string& requestId, int version)
{
    m_receiver->VQuotesHistoryResponse(CFxEventInfo(requestId), version);
}

void CLrpReceiver::OnDataHistoryMetaInfoResponseMsg(const string& requestId, const int32 status, const string& field)
{
    CFxEventInfo info(requestId);
    info.Status = status;
    string temp = field;
    m_receiver->VMetaInfoFile(info, temp);
}

void CLrpReceiver::OnDataHistoryMetaInfoRejectMsg(const string& requestId, const int32 status, const string& field)
{
    CFxEventInfo info(requestId);
    info.Status = status;
    string temp = field;
    m_receiver->VMetaInfoFile(info, temp);
}

void CLrpReceiver::OnDataHistoryResponseMsg(const string& requestId, const CFxDataHistoryResponse& response)
{
    CFxDataHistoryResponse temp = response;
    m_receiver->VDataHistoryResponse(CFxEventInfo(requestId), temp);
}

void CLrpReceiver::OnDataHistoryRejectMsg(const string& requestId, FxMarketHistoryRejectType rejectType, const string& rejectReason)
{
    CFxEventInfo eventInfo;
    eventInfo.Status = FX_CODE_ERROR_REJECT;
    eventInfo.ID = requestId;
    eventInfo.Description = rejectReason;
    if (FxInvalidPeriodicity == rejectType)
    {
        eventInfo.Message = "Unsupported bar period";
    }
    else if (FxInvalidSymbol == rejectType)
    {
        eventInfo.Message = "Unsupported symbol";
    }
    else
    {
        eventInfo.Message = "Unknown error";
    }
    CFxDataHistoryResponse response;
    m_receiver->VDataHistoryResponse(eventInfo, response);
}

void CLrpReceiver::OnFileChunkMsg(const string& requestId, const CFxFileChunk& chunk)
{
    CFxFileChunk temp = chunk;
    m_receiver->VFileChunk(CFxEventInfo(requestId), temp);
}

void CLrpReceiver::OnQuoteRawMsg(MemoryBuffer& buffer)
{
    CFxEventInfo info;
    info.SendingTime = ReadTime(buffer);
    CFxQuote quote = ReadQuote(buffer);
    m_receiver->VTick(info, quote);
}

void CLrpReceiver::OnNotificationMsg(const CNotification& notification)
{
    CFxEventInfo info;
    CNotification temp = notification;
    m_receiver->VNotify(info, notification);
}

void CLrpReceiver::OnBusinessRejectMsg(const string& rejectReason, const string& rejectTag)
{
    CFxEventInfo eventInfo;
    if (rejectTag.empty())
        return;
    eventInfo.ID = rejectTag;
    eventInfo.Status = E_FAIL;
    eventInfo.Message = rejectReason;
    m_receiver->VBusinessReject(eventInfo);
}

HRESULT CLrpReceiver::Process(MemoryBuffer& buffer)
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

HRESULT CLrpReceiver::DoProcess(MemoryBuffer& buffer)
{
    const size_t componentId = ReadUInt8(buffer);
    const size_t methodId = ReadUInt8(buffer);

    const HRESULT result = LrpInvokeEx(sizeof(uint16), componentId, methodId, buffer, this);
    return result;
}

bool CLrpReceiver::ShouldBeLogged(const uint16 componentId, const uint16 methodId)
{
    if ((LrpComponent_Client_Id != componentId) || (LrpMethod_Client_OnQuoteRawMsg_Id != methodId))
    {
        return true;
    }
    return false;
}
