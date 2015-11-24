#include "stdafx.h"
#include "Acceptor.h"

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
CAcceptor::CAcceptor(int port) : m_continue(true), m_acceptor(INVALID_SOCKET), m_server(INVALID_SOCKET), m_client(INVALID_SOCKET)
{
	SOCKET acceptor = INVALID_SOCKET;
	const bool status = Construct(port, acceptor);
	if (INVALID_SOCKET != acceptor)
	{
		closesocket(acceptor);
	}
	if (!status)
	{
		const DWORD error = WSAGetLastError();
		Finalize();
		stringstream stream;
		stream<<"Couldn't create acceptor; WSAGetLastError() = "<<error;
		string message = stream.str();
		throw runtime_error(message);
	}
}
bool CAcceptor::Construct(int port, SOCKET& acceptor)
{
	m_acceptor = CreateAcceptor(port, INADDR_ANY, SOMAXCONN);
	if (INVALID_SOCKET == m_acceptor)
	{
		return false;
	}
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

CAcceptor::~CAcceptor()
{
	if (INVALID_SOCKET != m_acceptor)
	{
		closesocket(m_acceptor);
		m_acceptor = INVALID_SOCKET;
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
SOCKET CAcceptor::Accept()
{
	if (!m_continue)
	{
		return INVALID_SOCKET;
	}
	fd_set set;
	FD_ZERO(&set);
	FD_SET(m_server, &set);
	FD_SET(m_acceptor, &set);
	const int status = select(0, &set, nullptr, nullptr, nullptr);
	if (SOCKET_ERROR == status)
	{
		return INVALID_SOCKET;
	}
	if (FD_ISSET(m_server, &set))
	{
		m_continue = false;
		char ch = 0;
		send(m_server, &ch, sizeof(ch), 0);
		return INVALID_SOCKET;
	}
	SOCKET result = accept(m_acceptor, nullptr, nullptr);
	return result;
}
void CAcceptor::Finalize()
{
	if (m_continue)
	{
		char ch = 0;
		send(m_client, &ch, sizeof(ch), 0);
		recv(m_client, &ch, sizeof(ch), 0);
	}
}

