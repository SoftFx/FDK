#pragma once

class CSockets
{
public:
	volatile bool Continue;
	SOCKET Server;
	SOCKET Client;
private:
	SOCKET m_acceptor;
public:
	CSockets();
	~CSockets();
public:
	void Initialize();
private:
	CSockets(const CSockets&);
	CSockets& operator = (const CSockets&);
};