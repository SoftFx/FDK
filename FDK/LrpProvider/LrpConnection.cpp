#include "stdafx.h"
#include "LrpConnection.h"


namespace
{
    const int cInitialProtocolVersion = 0;
    const size_t cTimeoutInMs = 600000;
}
namespace
{
    const string cType = "Type";

    const string cAddress = "Address";
    const string cPort = "Port";
    const string cUsername = "Username";
    const string cPassword = "Password";
    const string cLogDirectory = "LogDirectory";
    const string cSecureConnection = "SecureConnection";
    const string cEnableQuotesLogging = "EnableQuotesLogging";
    const string cEnableNetworkStatistics = "EnableNetworkStatistics";
}

CLrpConnection::CLrpConnection(const CFxParams& params)
    : m_continue(false)
    , m_port()
    , m_enableQuotesLogging()
    , m_socket(params.GetBoolean(cSecureConnection), params.GetBoolean(cEnableNetworkStatistics))
    , m_sender(*this)
    , m_logger(params)
    , m_incommingBuffer(CHeap::Instance())
    , m_outgoingBuffer(CHeap::Instance())
{
    m_address = params.GetString(cAddress);
    m_port = params.GetInt32(cPort);
    m_username = params.GetString(cUsername);
    m_password = params.GetString(cPassword);
    m_enableQuotesLogging = params.GetBoolean(cEnableQuotesLogging);
}

CLrpConnection::~CLrpConnection()
{
}

void CLrpConnection::VReceiver(IReceiver* pReceiver)
{
    m_receiver = CLrpReceiver(pReceiver, &m_sender);
}

ISender* CLrpConnection::VSender()
{
    return &m_sender;
}

void CLrpConnection::VStart()
{
    CLogStream() << "Starting of LRP connection" >> m_logger;
    if (!m_continue)
    {
        m_continue = true;
        Delegate<void ()> func(this, &CLrpConnection::Run);
        func.DoAsynch(m_thread, &m_waiter);
    }
    CLogStream() << "LRP connection was started" >> m_logger;
}

void CLrpConnection::VShutdown()
{
    CLogStream() << "Stopping of LRP connection" >> m_logger;
    if (m_continue)
    {
        m_continue = false;
        m_socket.WakeUp();
        m_waiter.WaitForFinish();
    }
    CLogStream() << "LRP connection is stopped" >> m_logger;
}

void CLrpConnection::VStop()
{
}

void CLrpConnection::VGetActivity(uint64* pLogicalBytesSent, uint64* pPhysicalBytesSent, uint64* pLogicalBytesReceived, uint64* pPhysicalBytesReceived)
{
    m_socket.GetActivity(pLogicalBytesSent, pPhysicalBytesSent, pLogicalBytesReceived, pPhysicalBytesReceived);
}

HRESULT CLrpConnection::SendMessage(const CMessage& message)
{
    __try
    {
        DoSendMessage(message);
        return S_OK;
    }
    __except(EXCEPTION_EXECUTE_HANDLER)
    {
        return E_FAIL;
    }
}

void CLrpConnection::DoSendMessage(const CMessage& message)
{
    bool isEmpty = false;
    {
        CLock lock(m_synchronizer);
        isEmpty = m_messages.empty();
        m_messages.push_back(message);
    }
    if (isEmpty)
    {
        m_socket.WakeUp();
    }
}

void CLrpConnection::Run()
{
    for (; m_continue; Sleep(1000))
    {
        Step();
    }
}

void CLrpConnection::Step()
{
    if (Connect())
    {
        Loop();
    }
    m_receiver.OnLogoutMsg(FxLogoutReason_Unknown, string());
}

bool CLrpConnection::Connect()
{
    CLogStream() << "Connecting to " << m_address << " : " << m_port >> m_logger;
    if (!m_socket.Connect(m_address, m_port))
    {
        CLogStream() << "Couldn't establish new connection" >> m_logger;
        return false;
    }
    CLogStream() << "Physical connection was established" >> m_logger;

    CTimeout timeout(cTimeoutInMs);

    if (!DoSendClientVersion(timeout))
    {
        return false;
    }
    if (!DoReceiveVersionResponse(timeout))
    {
        CLogStream() << "Receiving was failed" >> m_logger;
        return false;
    }

    if (!DoSendClientSignagure(timeout))
    {
        return false;
    }

    if (!DoReceiveServerSignagure(timeout))
    {
        return false;
    }

    if (!DoSendUsernamePassword(timeout))
    {
        return false;
    }

    if (!DoReceiveVerificationResponse(timeout))
    {
        return false;
    }

    m_socket.EnableNonBlockingMode();
    m_socket.EnableKeepAlive();

    CLogStream() << "Connection was established successfully" >> m_logger;

    return true;
}

bool CLrpConnection::DoSendClientVersion(CTimeout timeout)
{
    CLogStream() << "Sending client LRP protocol version" >> m_logger;

    m_outgoingBuffer.Reset();
    WriteUInt16(0, m_outgoingBuffer);

    WriteInt32(cInitialProtocolVersion, m_outgoingBuffer);
    m_outgoingBuffer.SetPosition(0);

    if (Send(timeout))
    {
        CLogStream() << "LRP protocol version has been send successfully" >> m_logger;
        return true;
    }
    CLogStream() << "Couldn't send LRP protocol version due timeout or network error" >> m_logger;
    return false;
}

bool CLrpConnection::DoReceiveVersionResponse(CTimeout timeout)
{
    CLogStream() << "Receiving of server LRP protocol version response" >> m_logger;
    if (!Receive(timeout))
    {
        CLogStream() << "Couldn't receive LRP protocol version response due timeout or network error" >> m_logger;
        return false;
    }

    const HRESULT answer = ReadInt32(m_incommingBuffer);
    CLogStream() << "Server response = 0x" << hex << answer >> m_logger;
    const bool result = SUCCEEDED(answer);
    return result;
}

bool CLrpConnection::DoSendUsernamePassword(CTimeout timeout)
{
    CLogStream() << "Sending of username and password" >> m_logger;
    m_outgoingBuffer.Reset();
    WriteUInt16(0, m_outgoingBuffer);
    WriteAString(m_username, m_outgoingBuffer);
    WriteAString(m_password, m_outgoingBuffer);
    m_outgoingBuffer.SetPosition(0);

    if(Send(timeout))
    {
        CLogStream() << "Username and password have been send successfully" >> m_logger;
        return true;
    }
    CLogStream() << "Couldn't send username and password due timeout or network error" >> m_logger;
    return false;
}

bool CLrpConnection::DoReceiveVerificationResponse(CTimeout timeout)
{
    CLogStream() << "Receiving of username/password verification response" >> m_logger;
    if (!Receive(timeout))
    {
        CLogStream() << "Couldn't receive of username/password verification reponse due to timeout or network error" >> m_logger;
        return false;
    }

    const HRESULT answer = ReadInt32(m_incommingBuffer);
    CLogStream() << "Server response = 0x" << hex << answer >> m_logger;
    const bool result = SUCCEEDED(answer);
    return result;
}

bool CLrpConnection::DoSendClientSignagure(CTimeout timeout)
{
    CLogStream() << "Sending of client signature" >> m_logger;
    m_outgoingBuffer.Reset();
    WriteUInt16(0, m_outgoingBuffer);
    WriteAString(CLrpReceiver::Signature(), m_outgoingBuffer);
    m_outgoingBuffer.SetPosition(0);

    if (Send(timeout))
    {
        CLogStream() << "Client signature been send successfully" >> m_logger;
        return true;
    }
    CLogStream() << "Couldn't send client signature due timeout or network error" >> m_logger;
    return false;
}

bool CLrpConnection::DoReceiveServerSignagure(CTimeout timeout)
{
    CLogStream() << "Receiving of server signature" >> m_logger;
    if (!Receive(timeout))
    {
        CLogStream() << "Couldn't receive server signature due timeout or network error" >> m_logger;
        return false;
    }

    string remoteSignature = ReadAString(m_incommingBuffer);
    CLogStream() << "Server signature = " << endl << remoteSignature >> m_logger;
    m_sender.Initialize(remoteSignature);
    return true;
}

CSocketState CLrpConnection::WaitFor()
{
    CSocketState result;
    bool shouldWrite = false;

    {
        CLock lock(m_synchronizer);
        shouldWrite = !m_messages.empty();
    }

    if (shouldWrite)
    {
        result = m_socket.WaitForReadWrite();
    }
    else
    {
        result.CanRead = m_socket.WaitForRead();
    }

    return result;
}

void CLrpConnection::Loop()
{
    m_incommingBuffer.Reset();

    for (CSocketState state = WaitFor(); m_continue; state = WaitFor())
    {
        if (state.CanWrite)
        {
            if (!DoWrite())
            {
                break;
            }
        }
        if (state.CanRead)
        {
            if (!DoRead())
            {
                break;
            }
        }
    }
}

bool CLrpConnection::DoRead()
{
    HRESULT status = m_socket.Read(m_incommingBuffer);
    if (FAILED(status))
    {
        return false;
    }
    if (S_OK != status)
    {
        return true;
    }
    {
        // logging

        const size_t position = m_incommingBuffer.GetPosition();

        const uint16 componentId = ReadUInt8(m_incommingBuffer);
        const uint16 methodId = ReadUInt8(m_incommingBuffer);

        if (m_enableQuotesLogging || CLrpReceiver::ShouldBeLogged(componentId, methodId))
        {
            m_logger.WriteIncommingMessage(componentId, methodId, m_incommingBuffer);
        }
        m_incommingBuffer.SetPosition(position);
    }
    status = m_receiver.Process(m_incommingBuffer);

    if (FAILED(status))
    {
        return false;
    }

    m_incommingBuffer.Reset();
    return true;
}

bool CLrpConnection::DoWrite()
{
    assert(!m_messages.empty());
    CMessage& message = m_messages.front();
    MemoryBuffer& buffer = message.GetBuffer();

    const HRESULT status = m_socket.Write(buffer);
    if (S_OK == status)
    {
        CLock lock(m_synchronizer);
        const uint16 componentId = message.GetComponentId();
        const uint16 methodId = message.GetMethodId();
        buffer.SetPosition(sizeof(uint16) + sizeof(uint8) + sizeof(uint8));
        m_logger.WriteOutgoingMessage(componentId, methodId, buffer);
        m_messages.pop_front();
    }
    const bool result = SUCCEEDED(status);
    return result;
}

bool CLrpConnection::Send(CTimeout timeout)
{
    const size_t size = m_outgoingBuffer.GetSize();
    if (size < sizeof(uint16))
    {
        throw runtime_error("Size of outgoing buffer is too small");
    }
    if (size - sizeof(uint16) > 16777216)
    {
        throw runtime_error("Size of outgoing buffer is too large");
    }
    m_outgoingBuffer.SetPosition(0);
    const uint16 dataSize = static_cast<uint16>(size - sizeof(uint16));
    WriteUInt16(dataSize, m_outgoingBuffer);
    m_outgoingBuffer.SetPosition(0);

    const bool result = m_socket.Write(timeout, m_outgoingBuffer);
    return result;
}

bool CLrpConnection::Receive(CTimeout timeout)
{
    m_incommingBuffer.Reset();
    const bool result = m_socket.Read(timeout, m_incommingBuffer);
    return result;
}
