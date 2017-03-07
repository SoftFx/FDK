#include "stdafx.h"
#include "Acceptor.h"

namespace
{
	SOCKET CreateAcceptor(int port, int mode, int backlog, bool secure)
	{
		SOCKET result = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP, secure? FX_SOCKET_MODE_SECURE : FX_SOCKET_MODE_SIMPLE);
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
CAcceptor::CAcceptor(int port, bool secure) : m_continue(true), m_acceptor(INVALID_SOCKET), m_server(INVALID_SOCKET), m_client(INVALID_SOCKET)
{
	SOCKET acceptor = INVALID_SOCKET;
	const bool status = Construct(port, secure, acceptor);
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
bool CAcceptor::Construct(int port, bool secure, SOCKET& acceptor)
{
	m_acceptor = CreateAcceptor(port, INADDR_ANY, SOMAXCONN, secure);
	if (INVALID_SOCKET == m_acceptor)
	{
		return false;
	}
	acceptor = CreateAcceptor(0, 0, 1, false);
	if (INVALID_SOCKET == acceptor)
	{
		return false;
	}
	m_client = socket(PF_INET, SOCK_STREAM, IPPROTO_TCP, FX_SOCKET_MODE_SIMPLE);
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
SOCKET CAcceptor::Accept(const char* ceritificateFileName, const char* password)
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
	SOCKET result = FxPhysicalAccept(m_acceptor, nullptr, nullptr, ceritificateFileName, password);
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

