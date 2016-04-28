#include "stdafx.h"
#include "HandShakeBehaviour.h"
#include "Server.h"
#include "Channel.h"


namespace
{
    const int cInitialVersion = 0;
    const string cProtocolVersion = "ext.1.31";
}

namespace
{
    void WriteSize(MemoryBuffer& buffer)
    {
        const size_t size = buffer.GetSize();
        if (size < sizeof(uint16))
        {
            throw overflow_error("Memory buffer is too small");
        }
        if (size > sizeof(uint16) + 16777216)
        {
            throw overflow_error("Memory buffer is too big");
        }
        const uint16 dataSize = static_cast<uint16>(size - sizeof(uint16));
        buffer.SetPosition(0);
        WriteUInt16(dataSize, buffer);
        buffer.SetPosition(0);
    }
}
CHandShakeBehaviour::CHandShakeBehaviour(CChannel& channel) : CBaseBehaviour(channel), m_method(), m_outgoing(channel.Outgoing())
{
    StartLogicalAccept();
}
HRESULT CHandShakeBehaviour::VProcess(const uint64 now)
{
    const size_t handShakeTimeoutInMs = m_params.HandshakeTimeout;
    if (m_state.LastIncommingEvent + handShakeTimeoutInMs < now)
    {
        CLogStream()<<"CHandShakeBehaviour::VProcess(id = "<<m_id<<"): handshake timeout has been reached">>m_logger;
        return E_FAIL;
    }

    if (nullptr == m_method)
    {
        return S_FALSE;
    }

    const HRESULT result = (this->*m_method)();
    return result;
}
void CHandShakeBehaviour::Logon(const HRESULT status, const string& message)
{
    CLogStream()<<"CHandShakeBehaviour::Logon(id = "<<m_id<<"): status = "<<status<<"; message = "<<message>>m_logger;
    if (nullptr == m_method)
    {
        m_state.Buffer.Reset();
        WriteUInt16(0, m_state.Buffer);
        WriteInt32(status, m_state.Buffer);
        WriteAString(message, m_state.Buffer);
        WriteSize(m_state.Buffer);
        m_method = SUCCEEDED(status) ? &CHandShakeBehaviour::DoLogon : &CHandShakeBehaviour::DoLogout;
    }
}
HRESULT CHandShakeBehaviour::DoLogon()
{
    const HRESULT result = m_transport.SelectWrite(m_state.Buffer);
    if (S_OK != result)
    {
        return result;
    }
    m_state.LastIncommingEvent = GetTickCount64();
    m_state.LastOutgoingEvent = m_state.LastIncommingEvent;

    m_outgoing.SendLogon(cProtocolVersion);
    m_state.Buffer.Reset();

    CLogStream()<<"CHandShakeBehaviour::Logon(id = "<<m_id<<") - ok">>m_logger;
    return S_OK;
}
HRESULT CHandShakeBehaviour::DoLogout()
{
    m_transport.SelectWrite(m_state.Buffer);
    CLogStream()<<"CHandShakeBehaviour::Logon(id = "<<m_id<<") - ok">>m_logger;
    return E_FAIL;
}
void CHandShakeBehaviour::StartLogicalAccept()
{
    m_method = &CHandShakeBehaviour::DoLogicalAccept;
    CLogStream()<<"CHandShakeBehaviour::StartLogicalAccept(id = "<<m_id<<")">>m_logger;
}
HRESULT CHandShakeBehaviour::DoLogicalAccept()
{
    const HRESULT result = m_transport.LogicalAccept();
    if (S_OK == result)
    {
        StartClientVersion();
        return S_FALSE;
    }
    return result;
}
void CHandShakeBehaviour::StartClientVersion()
{
    m_state.Buffer.Reset();
    m_method = &CHandShakeBehaviour::DoClientVersion;
    CLogStream()<<"CHandShakeBehaviour::StartClientVersion(id = "<<m_id<<")">>m_logger;
}
HRESULT CHandShakeBehaviour::DoClientVersion()
{
    const HRESULT result = SelectReceive();
    if (S_OK != result)
    {
        return result;
    }
    const int version = ReadInt32(m_state.Buffer);

    CLogStream()<<"CHandShakeBehaviour::DoClientVersion(id = "<<m_id<<"): version = "<<version>>m_logger;

    if (cInitialVersion == version)
    {
        StartVersionResponse();
        return S_FALSE;
    }
    else
    {
        CLogStream()<<"CHandShakeBehaviour::DoClientVersion(id = "<<m_id<<"): disconnect due to invalid version">>m_logger;
        return E_FAIL;
    }
}
void CHandShakeBehaviour::StartVersionResponse()
{
    m_state.Buffer.Construct(sizeof(uint16) + sizeof(HRESULT));

    WriteUInt16(sizeof(HRESULT), m_state.Buffer);
    WriteInt32(S_OK, m_state.Buffer);

    m_state.Buffer.SetPosition(0);
    m_method = &CHandShakeBehaviour::DoVersionResponse;
    CLogStream()<<"CHandShakeBehaviour::StartVersionResponse(id = "<<m_id<<")">>m_logger;
}
HRESULT CHandShakeBehaviour::DoVersionResponse()
{
    const HRESULT result = m_transport.SelectWrite(m_state.Buffer);
    if (S_OK != result)
    {
        return result;
    }
    CLogStream()<<"CHandShakeBehaviour::DoVersionResponse(id = "<<m_id<<") - ok">>m_logger;
    StartClientSignature();
    return S_FALSE;
}
void CHandShakeBehaviour::StartClientSignature()
{
    m_state.Buffer.Reset();
    m_method = &CHandShakeBehaviour::DoClientSignature;
    CLogStream()<<"CHandShakeBehaviour::StartClientSignature(id = "<<m_id<<")">>m_logger;
}
HRESULT CHandShakeBehaviour::DoClientSignature()
{
    const HRESULT result = Receive();
    if (S_OK != result)
    {
        return result;
    }

    string remoteSignature = ReadAString(m_state.Buffer);
    CLogStream()<<"CHandShakeBehaviour::DoClientSignature(id = "<<m_id<<"): client signature = "<<endl<<remoteSignature>>m_logger;
    m_outgoing.Initialize(remoteSignature);

    StartServerSignature();

    return S_FALSE;
}
void CHandShakeBehaviour::StartServerSignature()
{
    m_state.Buffer.Reset();
    WriteUInt16(0, m_state.Buffer);
    WriteAString(CIncomming::Signature(), m_state.Buffer);
    WriteSize(m_state.Buffer);
    m_method = &CHandShakeBehaviour::DoServerSignature;
    CLogStream()<<"CHandShakeBehaviour::StartServerSignature(id = "<<m_id<<")">>m_logger;
}
HRESULT CHandShakeBehaviour::DoServerSignature()
{
    const HRESULT result = m_transport.SelectWrite(m_state.Buffer);
    if (S_OK != result)
    {
        return result;
    }
    CLogStream()<<"CHandShakeBehaviour::DoServerSignature(id = "<<m_id<<") - ok">>m_logger;
    StartUsernamePassword();

    return S_FALSE;
}
void CHandShakeBehaviour::StartUsernamePassword()
{
    m_state.Buffer.Reset();
    m_method = &CHandShakeBehaviour::DoUsernamePassword;
    CLogStream()<<"CHandShakeBehaviour::StartUsernamePassword(id = "<<m_id<<")">>m_logger;
}
HRESULT CHandShakeBehaviour::DoUsernamePassword()
{
    const HRESULT result = SelectReceive();
    if (S_OK != result)
    {
        return result;
    }

    string username = ReadAString(m_state.Buffer);
    string password = ReadAString(m_state.Buffer);


    CLogStream()<<"CHandShakeBehaviour::DoUsernamePassword(id = "<<m_id<<"): username/password = "<<username<<"/"<<password>>m_logger;

    m_method = nullptr;
    const string& address = m_transport.GetAddress();
    m_server.BeginLogon(m_id, address, username, password);

    CLogStream()<<"CHandShakeBehaviour::DoUsernamePassword(id = "<<m_id<<"): begin logon">>m_logger;

    return S_FALSE;
}


HRESULT CHandShakeBehaviour::SelectReceive()
{
    if (!m_transport.CanRead())
    {
        return S_FALSE;
    }

    const HRESULT result = Receive();
    return result;
}
HRESULT CHandShakeBehaviour::Receive()
{
    const size_t position = m_state.Buffer.GetPosition();

    if (position < sizeof(uint16))
    {
        return ReceiveSize();
    }
    const HRESULT result = m_transport.Read(m_state.Buffer);
    if (S_OK == result)
    {
        m_state.LastIncommingEvent = GetTickCount64();
        m_state.Buffer.SetPosition(sizeof(uint16));
    }
    return result;
}
HRESULT CHandShakeBehaviour::ReceiveSize()
{
    const size_t size = m_state.Buffer.GetSize();
    assert(size <= sizeof(uint32));
    if (size < sizeof(uint16))
    {
        m_state.Buffer.Construct(sizeof(uint16));
    }

    HRESULT result = m_transport.Read(m_state.Buffer);
    if (S_OK != result)
    {
        return result;
    }
    m_state.Buffer.SetPosition(0);
    const size_t dataSize = ReadUInt16(m_state.Buffer);
    m_state.Buffer.Construct(sizeof(uint16) + dataSize);

    m_state.Buffer.SetPosition(sizeof(uint16));

    return S_FALSE;
}
