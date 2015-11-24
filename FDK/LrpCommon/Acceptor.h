#pragma once

class CAcceptor
{
public:
	CAcceptor(int port, bool secure);
	~CAcceptor();
private:
	bool Construct(int port, bool secure, SOCKET& acceptor);
public:
	SOCKET Accept(const char* ceritificateFileName, const char* password);
	void Finalize();
private:
	bool m_continue;
	SOCKET m_acceptor;
	SOCKET m_server;
	SOCKET m_client;
};