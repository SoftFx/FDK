#include "stdafx.h"
#include "Socket.h"


namespace
{
	SOCKET CreateAcceptor(int port, int mode, int /*backlog*/)
	{
		SOCKET result = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
		if (INVALID_SOCKET == result)
		{
			return INVALID_SOCKET;
		}
		sockaddr_in service;
		service.sin_family = AF_INET;
		service.sin_addr.s_addr = mode;
		service.sin_port = htons(static_cast<u_short>(port));

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
		status = connect(client, reinterpret_cast<sockaddr*>(&addr), sizeof(addr));
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


CSocket::CSocket(SOCKET socket) : m_continue(), m_socket(socket), m_server(INVALID_SOCKET), m_client(INVALID_SOCKET)
{
	SOCKET acceptor = INVALID_SOCKET;
	m_continue = Construct(acceptor);
	if (INVALID_SOCKET != acceptor)
	{
		closesocket(acceptor);
	}
	if (!m_continue)
	{
		const DWORD error = WSAGetLastError();
		Finalize();
		stringstream stream;
		stream<<"Couldn't create acceptor; WSAGetLastError() = "<<error;
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
bool CSocket::Construct(SOCKET& acceptor)
{
	acceptor = CreateAcceptor(0, 0, 1);
	if (INVALID_SOCKET == acceptor)
	{
		return false;
	}
	m_client = socket(PF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (INVALID_SOCKET == m_client)
	{
		return false;
	}
	m_server = Connect(m_client, acceptor);
	const bool result = (INVALID_SOCKET != m_server);
	return result;
}
void CSocket::Finalize()
{
	if (m_continue)
	{
		m_continue = false;
		char ch = 0;
		send(m_client, &ch, sizeof(ch), 0);
	}
}
int CSocket::Send(const char* buffer, const int length)
{
	timeval timeout;
	timeout.tv_sec = numeric_limits<long>::max();
	timeout.tv_usec = 0;

	return Send(buffer, length, timeout);
}
int CSocket::Send(const char* buffer, const int length, const timeval& timeout)
{
	if (!m_continue)
	{
		return -1;
	}

	fd_set reading;
	FD_ZERO(&reading);
	FD_SET(m_server, &reading);


	fd_set writing;
	FD_ZERO(&writing);
	FD_SET(m_socket, &writing);

	select(0, &reading, &writing, nullptr, &timeout);

	if (!m_continue)
	{
		return -1;
	}

	if (!FD_ISSET(m_socket, &writing))
	{
		return 0;
	}

	const int result = send(m_socket, buffer, length, 0);
	return result;
}
int CSocket::Receive(char* buffer, const int length)
{
	timeval timeout;
	timeout.tv_sec = numeric_limits<long>::max();
	timeout.tv_usec = 0;

	return Receive(buffer, length, timeout);
}
int CSocket::Receive(char* buffer, const int length, const timeval& timeout)
{
	if (!m_continue)
	{
		return -1;
	}

	fd_set reading;
	FD_ZERO(&reading);
	FD_SET(m_server, &reading);
	FD_SET(m_socket, &reading);

	select(0, &reading, nullptr, nullptr, &timeout);

	if (!m_continue)
	{
		return -1;
	}

	if (!FD_ISSET(m_socket, &reading))
	{
		return 0;
	}

	const int result = recv(m_socket, buffer, length, 0);
	return result;
}
void CSocket::ShutDown()
{
	shutdown(m_socket, SD_BOTH);
}
SOCKET CSocket::Handle()
{
	return m_socket;
}
