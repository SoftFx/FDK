#include "stdafx.h"
#include "Channel.h"
#include "Server.h"




namespace
{
    LONG ExceptionHandler()
    {
        return EXCEPTION_EXECUTE_HANDLER;
    }
    void WriteSize(MemoryBuffer& buffer)
    {
        const size_t size = buffer.GetSize();
        if (size < sizeof(uint16))
        {
            throw overflow_error("Memory buffer is too small");
        }
        if (size > sizeof(uint16) + numeric_limits<uint16>::max())
        {
            throw overflow_error("Memory buffer is too big");
        }
        const uint16 dataSize = static_cast<uint16>(size - sizeof(uint16));
        buffer.SetPosition(0);
        WriteUInt16(dataSize, buffer);
        buffer.SetPosition(0);
    }
}

CChannel::CChannel(CServer& server, const CParameters& params, uint64 id, SOCKET socket) :
    m_server(server), m_logger(server.GetLogger()), m_params(params), m_id(id), m_transport(socket), m_useCodec(false), m_state(*this),
    m_behaviour(), m_handShake(*this), m_connected(*this),
    m_incomming(*this, m_server.GetProxy()), m_outgoing(*this), m_sender(*this)
{
    m_behaviour = &m_handShake;
    CLogStream()<<"CChannel::CChannel(id = "<<id<<", socket = "<<socket<<")">>m_logger;
}
CChannel::~CChannel()
{
    CLogStream()<<"CChannel::~CChannel(id = "<<m_id<<")">>m_logger;
}
const uint64 CChannel::GetId() const
{
    return m_id;
}
CServer& CChannel::GetServer()
{
    return m_server;
}
CChannelState& CChannel::GetState()
{
    return m_state;
}
CTransport& CChannel::GetTransport()
{
    return m_transport;
}
void CChannel::Finalize()
{
    CLogStream()<<"CChannel::Finalize(id = "<<m_id<<"): finalizing">>m_logger;
    CLock lock(m_synchronizer);
    m_transport.Finalize();
    m_server.ShutdownConnection(m_id);
    CLogStream()<<"CChannel::Finalize(id = "<<m_id<<"): finalized">>m_logger;
}
void CChannel::Connect(const HRESULT /*status*/)
{
    //CLock lock(m_synchronizer);
}
void CChannel::Logon(const HRESULT status, const string& message)
{
    CLock lock(m_synchronizer);
    m_handShake.Logon(status, message);
}
HRESULT CChannel::SendMessage(const CMessage& message)
{
    __try
    {
        const HRESULT result = DoSendMessage(message);
        return result;
    }
    __except(ExceptionHandler())
    {
        return E_FAIL;
    }
}
HRESULT CChannel::DoSendMessage(const CMessage& message)
{
    CLock lock(m_synchronizer);

    const HRESULT result = m_state.Messages.Add(message);
    return result;
}
HRESULT CChannel::Process()
{
    HRESULT result = S_OK;
    uint64 now = GetTickCount64();

    m_synchronizer.Acquire();

    __try
    {
        result = DoProcess(now);
    }
    __except(ExceptionHandler())
    {
        result = E_FAIL;
    }

    m_synchronizer.Release();
    return result;
}
HRESULT CChannel::DoProcess(const uint64 now)
{
    if (nullptr == m_behaviour)
    {
        return E_FAIL;
    }
    const HRESULT result = m_behaviour->VProcess(now);
    if (S_OK != result)
    {
        return result;
    }
    if (m_behaviour == &m_handShake)
    {
        m_state.Buffer.Construct(CChannelState::DefaultBufferSizeInBytes);
        m_sender.Initialize();
        m_behaviour = &m_connected;
    }
    else if (m_behaviour == &m_connected)
    {
        m_behaviour = nullptr;
    }
    return result;
}
CLrpLogger& CChannel::GetLogger()
{
    return m_server.GetLogger();
}
COutgoing& CChannel::Outgoing()
{
    return m_outgoing;
}
CIncomming& CChannel::Incomming()
{
    return m_incomming;
}
void CChannel::WriteOutgoingMessage(CMessage& message)
{
    const ptrdiff_t key = message.GetKey();
    if (-1 != key)
    {
        return;
    }
    try
    {
        const uint16 componentId = message.GetComponentId();
        const uint16 methodId = message.GetMethodId();
        MemoryBuffer&  buffer = message.GetBuffer();
        buffer.SetPosition(sizeof(uint16) + sizeof(uint8) + sizeof(uint8));
        m_logger.WriteOutgoingMessage(m_id, componentId, methodId, buffer);
    }
    catch (const std::exception&)
    {
    }
}
void CChannel::ProcessHeartBeatResponse()
{
    m_connected.ProcessHeartBeatResponse();
}

const CParameters& CChannel::GetParameters() const
{
    return m_params;
}
void CChannel::SendQuote(const CFxQuote& quote)
{
    m_sender.SendQuote(quote);
}
