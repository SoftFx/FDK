#include "stdafx.h"
#include "Functions.h"
#include "Sockets.h"

CSockets::CSockets() : Continue(true), Server(INVALID_SOCKET), Client(INVALID_SOCKET), m_acceptor(INVALID_SOCKET)
{
}
void CSockets::Initialize()
{
	m_acceptor = CreateAcceptor(0, 0, 1);

	Client = socket(PF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (INVALID_SOCKET == Client)
	{
		throw runtime_error("couldn't create a new client socket");
	}

	Server = Connect(Client, m_acceptor);

	if (INVALID_SOCKET == Server)
	{
		throw runtime_error("couldn't connect client socket to server");
	}
	if (!EnableNonBlockingMode(Server))
	{
		throw runtime_error("couldn't enable non blocking mode");
	}
}
CSockets::~CSockets()
{
	if (INVALID_SOCKET != Server)
	{
		closesocket(Server);
		Server = INVALID_SOCKET;
	}
	if (INVALID_SOCKET != Client)
	{
		closesocket(Client);
		Client = INVALID_SOCKET;
	}
	if (INVALID_SOCKET != m_acceptor)
	{
		closesocket(m_acceptor);
		m_acceptor = INVALID_SOCKET;
	}
}
