#include "stdafx.h"
#include "Functions.h"



void PrintUsage()
{
	cout<<"Usage:"<<endl;
	cout<<"\tReadWriteSocket <iterations in millions>"<<endl;
	cout<<"Example:"<<endl;
	cout<<"\tReadWriteSocket 16"<<endl;
}

bool Parse(int argc, char** argv, size_t& count)
{
	if (2 != argc)
	{
		return false;
	}
	count = atoi(argv[1]);

	size_t newCount = count * 1000000;
	if (newCount < count)
	{
		return false;
	}
	count = newCount;
	return true;
}

void InitializeSocketsLibrary()
{
	const WORD version = MAKEWORD(2, 2);
	WSADATA data;
	if (WSAStartup(version, &data))
	{
		throw runtime_error("couldn't initialize windows sockets.");
	}
}

bool EnableNonBlockingMode(SOCKET socket)
{
	u_long mode = 1;
	const int status = ioctlsocket(socket, FIONBIO, &mode);
	const bool result = (0 == status);

	return result;
}
SOCKET CreateAcceptor(int port, int mode, int backlog)
{
	SOCKET result = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
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
	status = connect(client, reinterpret_cast<sockaddr*>(&addr), sizeof(addr));
	if (SOCKET_ERROR == status)
	{
		return INVALID_SOCKET;
	}
	SOCKET result = accept(acceptor, nullptr, nullptr);
	return result;
}
