#include "stdafx.h"
#include "LrpSender.h"
#include "LrpConnection.h"

#define LrpSignature LrpServerSignature

#include "Server_TypesSerializer.hpp"
#include "Server_Signature.hpp"
#include "Server.hpp"

#undef LrpSignature

namespace
{
    const int cClientQuoteHistoryVersion = 1;
}

CLrpSender::CLrpSender(CLrpConnection& connection)
    : m_connection(connection)
    , m_translators(LrpServerSignature())
{
}

void CLrpSender::Initialize(const string& remoteSignature)
{
    m_translators.Initialize(remoteSignature);
}

void CLrpSender::VSendGetCurrencies(const string& id)
{
    Server server(*this);
    server.OnCurrenciesInfoRequest(id);
}

void CLrpSender::VSendGetSupportedSymbols(const string& id)
{
    Server server(*this);
    server.OnSymbolsInfoRequest(id);
}

void CLrpSender::VSendGetSessionInfo(const string& id)
{
    Server server(*this);
    server.OnSessionInfoRequest(id);
}

void CLrpSender::VSendGetAccountInfo(const string& id)
{
    throw std::exception("CLrpSender::VSendGetAccountInfo() method is not implemented.");
}

void CLrpSender::VSendSubscribeToQuotes(const string& id, const vector<string>& symbols, int32 depth)
{
    Server server(*this);
    server.OnSubscribeToQuotesRequest(id, symbols, depth);
}

void CLrpSender::VSendUnsubscribeQuotes(const string& id, const vector<string>& symbols)
{
    Server server(*this);
    server.OnUnsubscribeQuotesRequest(id, symbols);
}

void CLrpSender::VSendDeleteOrder(const string& id, const string& orderID, const string& clientID, int32 side)
{
    throw std::exception("CLrpSender::VSendDeleteOrder() method is not implemented.");
}

void CLrpSender::VSendCloseOrder(const string& id, const string& orderId, Nullable<double> closingVolume)
{
    throw std::exception("CLrpSender::VSendCloseOrder() method is not implemented.");
}

void CLrpSender::VSendCloseByOrders(const string& id, const string& firstOrderId, const string& secondOrderId)
{
    throw std::exception("CLrpSender::VSendCloseByOrders() method is not implemented.");
}

void CLrpSender::VSendCloseAllOrders(const string& id)
{
    throw std::exception("CLrpSender::VSendCloseAllOrders() method is not implemented.");
}

void CLrpSender::VSendGetOrders(const string& id)
{
    throw std::exception("CLrpSender::VSendGetOrders() method is not implemented.");
}

void CLrpSender::VSendOpenNewOrder(const string& id, const CFxOrder& request)
{
    throw std::exception("CLrpSender::VSendOpenNewOrder() method is not implemented.");
}

void CLrpSender::VSendModifyOrder(const string& id, const CFxOrder& request)
{
    throw std::exception("CLrpSender::VSendModifyOrder() method is not implemented.");
}

void CLrpSender::VSendDataHistoryRequest(const string& id, const CFxDataHistoryRequest& request)
{
    Server server(*this);
    CFxDataHistoryRequest temp = request;
    temp.BarsNumber = - temp.BarsNumber;
    server.OnDataHistoryRequest(id, temp);
}

void CLrpSender::VSendGetFileChunk(const string& id, const string& fileId, const uint32 chunkId)
{
    Server server(*this);
    server.OnFileChunkRequest(id, fileId, chunkId);
}

void CLrpSender::VSendGetBarsHistoryMetaInfoFile(const string& id, const string& symbol, int32 priceType, const string& period)
{
    Server server(*this);
    server.OnBarsHistoryMetaInfoFileRequest(id, symbol, priceType, period);
}

void CLrpSender::VSendGetTicksHistoryMetaInfoFile(const string& id, const string& symbol, bool includeLevel2)
{
    Server server(*this);
    server.OnQuotesHistoryMetaInfoFileRequest(id, symbol, includeLevel2);
}

void CLrpSender::VSendGetTradeTransactionReportsAndSubscribeToNotifications(const string& id, FxTimeDirection direction, bool subscribe, const Nullable<CDateTime>& from, const Nullable<CDateTime>& to, uint32 bufferSize, const string& position)
{
    throw std::exception("CLrpSender::VSendGetTradeTransactionReportsAndSubscribeToNotifications() method is not implemented.");
}

void CLrpSender::VSendUnsubscribeTradeTransactionReports(const string& id)
{
    throw std::exception("CLrpSender::VSendUnsubscribeTradeTransactionReports() method is not implemented.");
}

void CLrpSender::VSendPositionReportRequest(const string& id, const string& account)
{
    throw std::exception("CLrpSender::VSendPositionReportRequest() method is not implemented.");
}

void CLrpSender::VSendQuotesHistoryRequest(const string& id)
{
    Server server(*this);
    server.OnComponentsInfoRequest(id, cClientQuoteHistoryVersion);
}

void CLrpSender::SendHeartBeatResponse()
{
    Server server(*this);
    server.OnHeartBeatResponse();
}

void CLrpSender::Initialize(MemoryBuffer& buffer)
{
    MemoryBuffer temp(CHeap::Instance());
    temp.SetPosition(sizeof(uint32));
    std::swap(buffer, temp);
}

HRESULT CLrpSender::Invoke(uint16 componentId, uint16 methodId, MemoryBuffer& buffer)
{
    size_t dataSize = buffer.GetSize() - sizeof(uint16);
    if (dataSize > 16777216)
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

    CMessage message(-1, componentId, methodId, buffer);
    const HRESULT result = m_connection.SendMessage(message);
    return result;
}

bool CLrpSender::IsSupported(uint16 componentId) const
{
    const bool result = m_translators.IsSupported(componentId);
    return result;
}

bool CLrpSender::IsSupported(uint16 componentId, uint16 methodId) const
{
    const bool result = m_translators.IsSupported(componentId, methodId);
    return result;
}
