#pragma once


class CSocket
{
public:
	CSocket(SOCKET socket);
	~CSocket();
private:
	CSocket(const CSocket&);
	CSocket& operator = (const CSocket&);
public:
	int Send(const char* buffer, const int length);
	int Send(const char* buffer, const int length, const timeval& timeout);
	int Receive(char* buffer, const int length);
	int Receive(char* buffer, const int length, const timeval& timeout);
	void ShutDown();
	void Finalize();
	SOCKET Handle();
private:
	bool Construct(SOCKET& acceptor);
private:
	bool m_continue;
	SOCKET m_socket;
	SOCKET m_server;
	SOCKET m_client;
};