#pragma once
#include "MemoryBuffer.h"
#include "Socket.h"


class CStServerImpl;
class CStChannelImpl
{
public:
	CStChannelImpl(CStServerImpl& server, SOCKET socket, void* handle);
	~CStChannelImpl();
public:
	void Finalize(bool isSelfDestrcution);
private:
	static unsigned __stdcall ThreadFunction(void* arg0);
	void ThreadMethod();
	bool Header();
	void Loop();
	void Invoke(LrpInvokeHandler handler);
	bool Receive();
	bool Send();
private:
	CStChannelImpl(const CStChannelImpl&);
	CStChannelImpl& operator = (const CStChannelImpl&);
private:
	CStServerImpl& m_server;
	CSocket m_socket;
	void* m_handle;
	volatile bool m_continue;
	MemoryBuffer m_buffer;
	HANDLE m_thread;
};