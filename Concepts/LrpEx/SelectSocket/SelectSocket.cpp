#include "stdafx.h"


void PrintUsage()
{
	cout<<"Usage:"<<endl;
	cout<<"\tSelectSocket <sockets number> <iterations in millions>"<<endl;
	cout<<"\tsockets number should be not more than 64"<<endl;
	cout<<"Example:"<<endl;
	cout<<"\tSelectSocket 2 16"<<endl;
}

bool Parse(int argc, char** argv, size_t& socketsNumber, size_t& count)
{
	if (3 != argc)
	{
		return false;
	}
	socketsNumber = atoi(argv[1]);
	count = atoi(argv[2]);

	if ((socketsNumber > 64) || (0 == socketsNumber))
	{
		return false;
	}

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

void MakeMultiConnections(const size_t count, vector<SOCKET>& clients, vector<SOCKET>& servers)
{
	clients.clear();
	servers.clear();

	clients.reserve(count);
	servers.reserve(count);


	SOCKET acceptor = CreateAcceptor(0, 0, 1);


	for (size_t index = 0; index < count; ++index)
	{
		SOCKET client = socket(PF_INET, SOCK_STREAM, IPPROTO_TCP);
		if (INVALID_SOCKET == client)
		{
			throw runtime_error("couldn't create a new client socket");
		}
		clients.push_back(client);

		SOCKET server = Connect(client, acceptor);
		if (INVALID_SOCKET == server)
		{
			throw runtime_error("couldn't connect client socket to server");
		}
		if (!EnableNonBlockingMode(server))
		{
			throw runtime_error("couldn't enable non blocking mode");
		}
		servers.push_back(server);
	}
}

void SelectRead(const size_t count, const vector<SOCKET>& sockets)
{
	timeval zeroTime = { 0, 0 };

	const size_t socketsNumber = sockets.size();


	const uint64 start = GetTickCount64();

	for (size_t step = 0; step < count; ++step)
	{
		FD_SET reading;
		reading.fd_count = (u_int)sockets.size();

		for (size_t index = 0; index < socketsNumber; ++index)
		{
			reading.fd_array[index] = sockets[index];
		}
		select(0, &reading, nullptr, nullptr, &zeroTime);
	}

	const uint64 finish = GetTickCount64();

    double speed = (double)count;
	speed *= 1000;
	speed /= (finish - start);

	cout<<' '<<speed;
}

void SelectWrite(const size_t count, const vector<SOCKET>& sockets)
{
	timeval zeroTime = { 0, 0 };

	const size_t socketsNumber = sockets.size();


	const uint64 start = GetTickCount64();

	for (size_t step = 0; step < count; ++step)
	{
		FD_SET writing;
        writing.fd_count = (u_int)sockets.size();

		for (size_t index = 0; index < socketsNumber; ++index)
		{
			writing.fd_array[index] = sockets[index];
		}
		select(0, nullptr, &writing, nullptr, &zeroTime);
	}

	const uint64 finish = GetTickCount64();

    double speed = (double)count;
	speed *= 1000;
	speed /= (finish - start);

	cout<<' '<<speed;
}

void SelectReadWrite(const size_t count, const vector<SOCKET>& sockets)
{
	timeval zeroTime = { 0, 0 };

	const size_t socketsNumber = sockets.size();


	const uint64 start = GetTickCount64();

	for (size_t step = 0; step < count; ++step)
	{
		FD_SET reading;
		FD_SET writing;
		reading.fd_count = (u_int)sockets.size();
		writing.fd_count = (u_int)sockets.size();

		for (size_t index = 0; index < socketsNumber; ++index)
		{
			reading.fd_array[index] = sockets[index];
			writing.fd_array[index] = sockets[index];
		}
		select(0, &reading, &writing, nullptr, &zeroTime);
	}

	const uint64 finish = GetTickCount64();

	double speed = (double)count;
	speed *= 1000;
	speed /= (finish - start);

	cout<<' '<<speed;
}









int main(int argc, char* argv[])
{
	size_t socketsNumber = 0;
	size_t count = 0;
	if (!Parse(argc, argv, socketsNumber, count))
	{
		PrintUsage();
		return 1;
	}


	vector<SOCKET> clients;
	vector<SOCKET> servers;

	try
	{
		InitializeSocketsLibrary();
		MakeMultiConnections(socketsNumber, clients, servers);

		cout<<clients.size();
		SelectRead(count, servers);
		SelectWrite(count, servers);
		SelectReadWrite(count, servers);
		cout<<endl;
	}
	catch (const exception& ex)
	{
		cout<<ex.what()<<endl;
	}

	for each(auto element in clients)
	{
		closesocket(element);
	}
	clients.clear();

	for each(auto element in servers)
	{
		closesocket(element);
	}
	servers.clear();


	return 0;
}

