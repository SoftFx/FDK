#include "stdafx.h"
#include "LrpStClientImpl.h"
#include "Functions.h"
#include "MemoryBuffer.h"
#include "Formatting.h"
#include "SocketGuad.h"


CLrpStClientImpl::CLrpStClientImpl(const char* localSignature, const char* address, int port, const char* username, const char* password, const uint32 operationTimeoutInMs) :
	CLrpClientImpl(localSignature), m_socket(INVALID_SOCKET), m_address(address), m_port(port), m_username(username), m_password(password), m_logger(nullptr, nullptr), m_operationTimeoutInMs(operationTimeoutInMs)
{
}
CLrpStClientImpl::CLrpStClientImpl(const char* localSignature, const char* address, int port, const char* username, const char* password, LrpLogHandler pLogHandler, void* pUserParam, const uint32 operationTimeoutInMs) :
	CLrpClientImpl(localSignature), m_socket(INVALID_SOCKET), m_address(address), m_port(port), m_username(username), m_password(password), m_logger(pLogHandler, pUserParam), m_operationTimeoutInMs(operationTimeoutInMs)
{
	m_logger.Output("Local signature = {0}", localSignature);
}
CLrpStClientImpl::CLrpStClientImpl(const char* localSignature, const char* address, int port, const char* username, const char* password, const char* logPath, const uint32 operationTimeoutInMs) :
	CLrpClientImpl(localSignature), m_socket(INVALID_SOCKET), m_address(address), m_port(port), m_username(username), m_password(password), m_logger(&CLrpStClientImpl::OnOutput, this), m_operationTimeoutInMs(operationTimeoutInMs)
{
	m_logStream.reset(new ofstream(logPath));
	m_logger.Output("Local signature = {0}", localSignature);
}
CLrpStClientImpl::~CLrpStClientImpl()
{
	if (INVALID_SOCKET != m_socket)
	{
		closesocket(m_socket);
		m_socket = INVALID_SOCKET;
	}
}
bool CLrpStClientImpl::Connect(uint32 timeoutInMilliseconds)
{
	if (INVALID_SOCKET != m_socket)
	{
		m_logger.Output("Closing socket");
		closesocket(m_socket);
		m_socket = INVALID_SOCKET;
		m_logger.Output("Socket has been closed");
	}
	m_logger.Output("Creating a new socket");
	SOCKET s = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (INVALID_SOCKET == s)
	{
		m_logger.Output("Couldn't create a new socket: WSAGetLastError() = ", WSAGetLastError());
		return false;
	}
	CSocketGuard guard(s);
	m_logger.Output("New socket has been created");

	m_logger.Output("Resolving connection string");
	sockaddr addr;
	ZeroMemory(&addr, sizeof(addr));
	if (!ResolveConnectionString(m_address, m_port, addr))
	{
		m_logger.Output("Could not resolve connection string: WSAGetLastError() = ", WSAGetLastError());
	}
	m_logger.Output("Connection string has been resolved");

	m_logger.Output("Connectiong to {0}:{1}", m_address, m_port);

	const int status = connect(s, &addr, sizeof(addr));
	if (SOCKET_ERROR == status)
	{
		m_logger.Output("Could not establish new connection: WSAGetLastError() = {0}", WSAGetLastError());
		return false;
	}
	m_logger.Output("New connection has been established");
	m_logger.Output("Enabling keep alive");
	EnableKeepAlive(s);
	CTimeout timeout(timeoutInMilliseconds);
	// send protocol version
	m_logger.Output("Sending protocol version = {0}", cInitialProtocolVersion);
	if (!SendEx(s, timeout, &cInitialProtocolVersion, sizeof(cInitialProtocolVersion)))
	{
		m_logger.Output("Could not send protocol version");
		return false;
	}
	m_logger.Output("Protocol version has been sent");
	HRESULT answer = S_OK;
	m_logger.Output("Receiving server protocol answer");
	if (!ReceiveEx(s, timeout, &answer, sizeof(answer)))
	{
		m_logger.Output("Could not receive server protocol answer");
		return false;
	}
	m_logger.Output("Server protocol answer: code = {0}; status = {1}", answer, SUCCEEDED(answer));
	if (FAILED(answer))
	{
		return false;
	}
	m_logger.Output("Sending username = {0}", m_username);
	if (!SendEx(s, timeout, m_username))
	{
		m_logger.Output("Could not send username");
		return false;
	}
	m_logger.Output("Username has been sent");
	m_logger.Output("Sending password = {0}", m_password);
	if (!SendEx(s, timeout, m_password))
	{
		m_logger.Output("Could not send password");
		return false;
	}
	m_logger.Output("Password has been sent");
	answer = S_OK;
	m_logger.Output("Receiving server authorization answer");
	if(!ReceiveEx(s, timeout, &answer, sizeof(answer)))
	{
		m_logger.Output("Could not receive server authorization answer");
		return false;
	}
	m_logger.Output("Server authorization answer: code = {0}; status = {1}", answer, SUCCEEDED(answer));
	if (FAILED(answer))
	{
		return false;
	}
	m_logger.Output("Receiving remote signature");
	string remoteSignature;
	if (!ReceiveEx(s, timeout, remoteSignature))
	{
		m_logger.Output("Could not receive remote signature");
		return false;
	}
	m_logger.Output("Remote signature has been received = {0}", remoteSignature);
	m_logger.Output("Initializing translators");
	Initialize(remoteSignature);
	m_logger.Output("Translators have been initialized");
	m_socket = guard.Release();
	FlushTranlators(m_logger);
	return true;
}
bool CLrpStClientImpl::IsConnected() const
{
	if (INVALID_SOCKET == m_socket)
	{
		return false;
	}
	const bool result = IsSocketConnected(m_socket);
	return result;
}
bool CLrpStClientImpl::Ping(const uint32 timeoutInMilliseconds)
{
	if (INVALID_SOCKET == m_socket)
	{
		return false;
	}
	MemoryBuffer buffer(numeric_limits<uint16>::max(), numeric_limits<uint16>::max());
	buffer.SetPosition(0);
	WriteUInt32(static_cast<uint32>(buffer.GetSize() - sizeof(uint32)), buffer);

	CTimeout timeout(timeoutInMilliseconds);

	if (!SendEx(m_socket, timeout, buffer.GetData(), buffer.GetSize()))
	{
		closesocket(m_socket);
		m_socket = INVALID_SOCKET;
		return false;
	}
	if (!ReceiveEx(m_socket, timeout, buffer))
	{
		closesocket(m_socket);
		m_socket = INVALID_SOCKET;
		return false;
	}
	return true;
}
HRESULT CLrpStClientImpl::Invoke(MemoryBuffer& buffer)
{
	uint32 size = static_cast<uint32>(buffer.GetSize());
	if (size < sizeof(uint32))
	{
		throw runtime_error("Input buffer is too small");
	}
	buffer.SetPosition(0);
	WriteInt32(size - 4, buffer);

	buffer.SetPosition(12);

	uint16 componentId = ReadUInt16(buffer);
	uint16 methodId = ReadUInt16(buffer);
	Translate(componentId, methodId);

	buffer.SetPosition(12);
	WriteUInt16(componentId, buffer);
	WriteUInt16(methodId, buffer);

	CTimeout timeout(m_operationTimeoutInMs);

	if (!SendEx(m_socket, timeout, buffer.GetData(), buffer.GetSize()))
	{
		closesocket(m_socket);
		m_socket = INVALID_SOCKET;
		throw runtime_error("Couldn't send data");
	}
	if (!ReceiveEx(m_socket, timeout, buffer))
	{
		closesocket(m_socket);
		m_socket = INVALID_SOCKET;
		throw runtime_error("Couldn't receive data");
	}
	buffer.SetPosition(12);
	const HRESULT result = ReadInt32(buffer);
	return result;
}
void CLrpStClientImpl::OnOutput(void* pUserParam, const char* message)
{
	CLrpStClientImpl* pImpl = reinterpret_cast<CLrpStClientImpl*>(pUserParam);
	ofstream& stream = *(pImpl->m_logStream);

	SYSTEMTIME utcNow;
	ZeroMemory(&utcNow, sizeof(utcNow));
	GetSystemTime(&utcNow);
	stream<<utcNow<<", "<<GetCurrentThreadId()<<">: "<<message<<endl<<flush;
}
