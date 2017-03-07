#include "stdafx.h"
#include "Socket.h"


namespace
{
	SOCKET CreateAcceptor(int port, int mode, int backlog)
	{
		SOCKET result = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP, FX_SOCKET_MODE_SIMPLE);
		if (INVALID_SOCKET == result)
		{
			return INVALID_SOCKET;
		}
		sockaddr_in service;
		service.sin_family = AF_INET;
		service.sin_addr.s_addr = mode;
		service.sin_port = htons(port);

		int status = bind(result, (SOCKADDR*) &service, sizeof(service));
		if (SOCKET_ERROR == status)
		{
			closesocket(result);
			return INVALID_SOCKET;
		}
		status = listen(result, SOMAXCONN);
		if (SOCKET_ERROR == status)
		{
			closesocket(result);
			return INVALID_SOCKET;
		}
		return result;
	}
	SOCKET Connect(SOCKET client, SOCKET acceptor)
	{
		sockaddr_in addr;
		socklen_t len = sizeof(addr);
		int status = getsockname(acceptor, reinterpret_cast<sockaddr*>(&addr), &len);
		if (SOCKET_ERROR == status)
		{
			return INVALID_SOCKET;
		}
		addr.sin_addr.s_addr = inet_addr("127.0.0.1");
		status = connect(client, ConnectType_Direct, reinterpret_cast<sockaddr*>(&addr), sizeof(addr), 0, 0, 0, 0);
		if (SOCKET_ERROR == status)
		{
			return INVALID_SOCKET;
		}
		SOCKET result = accept(acceptor, nullptr, nullptr);
		return result;
	}
	void ThrowException(const char* message, const DWORD error)
	{
		stringstream stream;
		stream<<message<<error;
		string st = stream.str();
		throw runtime_error(st);
	}
}

CSocket::CSocket(bool secureConnection, bool enableStats)
    : m_secureConnection(secureConnection)
    , m_enableStats(enableStats)
    , m_wokeUp()
    , m_socket(INVALID_SOCKET)
    , m_server(INVALID_SOCKET)
    , m_client(INVALID_SOCKET)
{
	SOCKET acceptor = INVALID_SOCKET;
	const bool isConstructed = DoConstruct(acceptor);
	if (INVALID_SOCKET != acceptor)
	{
		closesocket(acceptor);
	}
	if (!isConstructed)
	{
		const DWORD error = WSAGetLastError();
		//Finalize();
		stringstream stream;
		stream << "Couldn't create acceptor; WSAGetLastError() = " << error;
		string message = stream.str();
		throw runtime_error(message);
	}
}

CSocket::CSocket(bool secureConnection)
    : m_secureConnection(secureConnection)
    , m_enableStats(false)
    , m_wokeUp()
    , m_socket(INVALID_SOCKET)
    , m_server(INVALID_SOCKET)
    , m_client(INVALID_SOCKET)
{
	SOCKET acceptor = INVALID_SOCKET;
	const bool isConstructed = DoConstruct(acceptor);
	if (INVALID_SOCKET != acceptor)
	{
		closesocket(acceptor);
	}
	if (!isConstructed)
	{
		const DWORD error = WSAGetLastError();
		//Finalize();
		stringstream stream;
		stream << "Couldn't create acceptor; WSAGetLastError() = " << error;
		string message = stream.str();
		throw runtime_error(message);
	}
}

CSocket::~CSocket()
{
	if (INVALID_SOCKET != m_socket)
	{
		closesocket(m_socket);
		m_socket = INVALID_SOCKET;
	}
	if (INVALID_SOCKET != m_server)
	{
		closesocket(m_server);
		m_server = INVALID_SOCKET;
	}
	if (INVALID_SOCKET != m_client)
	{
		closesocket(m_client);
		m_client = INVALID_SOCKET;
	}
}

bool CSocket::DoConstruct(SOCKET& acceptor)
{
	acceptor = CreateAcceptor(0, 0, 1);
	if (INVALID_SOCKET == acceptor)
	{
		return false;
	}
	m_client = socket(PF_INET, SOCK_STREAM, IPPROTO_TCP, FX_SOCKET_MODE_SIMPLE);
	if (INVALID_SOCKET == m_client)
	{
		return false;
	}
	m_server = ::Connect(m_client, acceptor);
	const bool result = (INVALID_SOCKET != m_server);
	return result;
}

bool CSocket::Connect(const string& address, const int port)
{
	if (INVALID_SOCKET != m_socket)
	{
		closesocket(m_socket);
		m_socket = INVALID_SOCKET;
	}
	const int mode = (m_enableStats ? FX_SOCKET_ENABLE_STATS : 0) | (m_secureConnection ? FX_SOCKET_MODE_SECURE : FX_SOCKET_MODE_SIMPLE);
	m_socket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP, mode);
	if (INVALID_SOCKET == m_socket)
	{
		return false;
	}

	sockaddr addr;
	ZeroMemory(&addr, sizeof(addr));
	if (!ResolveConnectionString(address, port, addr))
	{
		return false;
	}

	const int status = connect(m_socket, ConnectType_Direct, &addr, sizeof(addr), 0, 0, 0, 0);
	if (SOCKET_ERROR == status)
	{
		return false;
	}

	return true;
}

void CSocket::EnableNonBlockingMode()
{
	::EnableNonBlockingMode(m_socket);
}

void CSocket::EnableKeepAlive()
{
	::EnableKeepAlive(m_socket);
}

void CSocket::GetActivity(uint64* pLogicalBytesSent, uint64* pPhysicalBytesSent, uint64* pLogicalBytesReceived, uint64* pPhysicalBytesReceived)
{
	FxGetSocketActivity(m_socket, pLogicalBytesSent, pPhysicalBytesSent, pLogicalBytesReceived, pPhysicalBytesReceived);
}

bool CSocket::CanRead() const
{
	const bool result = FxCanRead(m_socket);
	return result;
}

bool CSocket::CanWrite() const
{
	const bool result = FxCanWrite(m_socket);
	return result;
}
namespace
{
	timeval TimeoutFromMs(DWORD timeoutInMs)
	{
		timeval timeout = {0, 0};
		if (INFINITE == timeoutInMs)
		{
			timeout.tv_sec =  std::numeric_limits<long>::max();
		}
		else
		{
			timeout.tv_sec = timeoutInMs / 1000;
			timeout.tv_usec = timeoutInMs % 1000;
			timeout.tv_usec *= 1000;
		}
		return timeout;
	}
}

bool CSocket::WaitForRead(DWORD timeoutInMs /* = INFINITE */)
{
	timeval timeout = TimeoutFromMs(timeoutInMs);

	fd_set set;
	set.fd_count = 2;
	set.fd_array[0] = m_socket;
	set.fd_array[1] = m_server;

	select(0, &set, nullptr, nullptr, &timeout);

	const bool result = (FD_ISSET(m_socket, &set)) != 0;
	Reset();
	return result;
}

bool CSocket::WaitForWrite(DWORD timeoutInMs /* = INFINITE */)
{
	timeval timeout = TimeoutFromMs(timeoutInMs);

	fd_set reading;
	reading.fd_count = 1;
	reading.fd_array[0] = m_server;

	fd_set writing;
	writing.fd_count = 1;
	writing.fd_array[0] = m_socket;


	select(0, &reading, &writing, nullptr, &timeout);

	const bool result = (FD_ISSET(m_socket, &writing) != 0);
	Reset();
	return result;
}

CSocketState CSocket::WaitForReadWrite(DWORD timeoutInMs /* = INFINITE */)
{
	timeval timeout = TimeoutFromMs(timeoutInMs);

	fd_set reading;
	reading.fd_count = 2;
	reading.fd_array[0] = m_socket;
	reading.fd_array[1] = m_server;

	fd_set writing;
	writing.fd_count = 2;
	writing.fd_array[0] = m_socket;
	writing.fd_array[1] = m_server;

	select(0, &reading, &writing, nullptr, &timeout);

	CSocketState result;
	result.CanRead = (FD_ISSET(m_socket, &reading)) != 0 ;
	result.CanWrite = (FD_ISSET(m_socket, &writing)) != 0;
	Reset();
	return result;
}

void CSocket::WakeUp()
{
	CLock lock(m_synchronizer);
	if (!m_wokeUp)
	{
		char ch = 0;
		send(m_client, &ch, sizeof(ch), 0);
		m_wokeUp = true;
	}
}

void CSocket::Reset()
{
	CLock lock(m_synchronizer);
	if (m_wokeUp)
	{
		char ch = 0;
		recv(m_server, &ch, sizeof(ch), 0);
		m_wokeUp = false;
	}
}

HRESULT CSocket::Write(MemoryBuffer& buffer)
{
	const HRESULT result = ::Write(m_socket, buffer);
	return result;
}

bool CSocket::Write(CTimeout timeout, MemoryBuffer& buffer)
{
	const char* data = reinterpret_cast<const char*>(buffer.GetData());
	size_t position = buffer.GetPosition();
	const size_t size = buffer.GetSize();
	for (; position < size; )
	{
		if (!WaitForWrite(static_cast<DWORD>(timeout.ToMilliseconds())))
		{
			return false;
		}
		const int status = send(m_socket, data + position, static_cast<int>(size - position), 0);
		if (SOCKET_ERROR == status)
		{
			return false;
		}
		position += status;
	}
	return true;
}

HRESULT CSocket::Read(MemoryBuffer& buffer)
{
	const size_t position = buffer.GetPosition();

	if (position < sizeof(uint16))
	{
		return DoReadSize(buffer);
	}
	const HRESULT result = ::Read(m_socket, buffer);
	if (S_OK == result)
	{
		buffer.SetPosition(sizeof(uint16));
	}
	return result;
}

HRESULT CSocket::DoReadSize(MemoryBuffer& buffer)
{
	const size_t size = buffer.GetSize();
	assert(size <= sizeof(uint32));
	if (size < sizeof(uint16))
	{
		buffer.Construct(sizeof(uint16));
	}

	HRESULT result = ::Read(m_socket, buffer);
	if (S_OK != result)
	{
		return result;
	}
	buffer.SetPosition(0);
	const size_t dataSize = ReadUInt16(buffer);
	buffer.Construct(sizeof(uint16) + dataSize);

	buffer.SetPosition(sizeof(uint16));

	return S_FALSE;
}

bool CSocket::Read(CTimeout timeout, MemoryBuffer& buffer)
{
	buffer.Construct(sizeof(uint16));

	if (!DoRead(timeout, buffer))
	{
		return false;
	}
	size_t dataSize = ReadUInt16(buffer);
	buffer.Construct(dataSize);
	const bool result = DoRead(timeout, buffer);
	return result;
}

bool CSocket::DoRead(CTimeout timeout, MemoryBuffer& buffer)
{
	char* data = reinterpret_cast<char*>(buffer.GetData());
	size_t position = buffer.GetPosition();
	const size_t size = buffer.GetSize();

	for (; position < size;)
	{
		if (!WaitForRead(static_cast<DWORD>(timeout.ToMilliseconds())))
		{
			return false;
		}
		const int status = recv(m_socket, data + position, static_cast<int>(size - position), 0);
		if (0 == status)
		{
			return 0;
		}
		if (status < 0)
		{
			const DWORD code = WSAGetLastError();
			if (WSAEWOULDBLOCK == code)
			{
				continue;
			}
			return false;
		}
		position += status;
	}
	return true;
}
