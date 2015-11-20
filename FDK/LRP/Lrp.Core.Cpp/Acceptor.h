#pragma once

class CAcceptor
{
public:
	CAcceptor(int port);
	~CAcceptor();
private:
	bool Construct(int port, SOCKET& acceptor);
public:
	SOCKET Accept();
	void Finalize();
private:
	bool m_continue;
	SOCKET m_acceptor;
	SOCKET m_server;
	SOCKET m_client;
};