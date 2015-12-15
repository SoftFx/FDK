#pragma once
//#include "LrpThreadPool.h"
//
//
//class CLrpServer;
//class CMtChannelImpl;
//class MemoryBuffer;
//
//
//
//class CMtServerImpl
//{
//public:
//	CMtServerImpl(CLrpServer& server, const int port, const size_t numberOfThreadsInPool, const size_t maxJobsNumber, LrpInvokeHandler handler);
//	~CMtServerImpl();
//public:
//	void ShutdownConnection(void* handle);
//	void Process(const Channel& pChannel);
//private:
//	bool Construct(const int port);
//private:
//	static unsigned __stdcall ThreadFunction(void* arg);
//	void Loop();
//	void CreateNewChannel(SOCKET client);
//	void CreateNewChannel(SOCKET client, void* handle);
//	void* ApproveNewClient(SOCKET client);
//private:
//	CMtServerImpl(const CMtServerImpl&);
//	CMtServerImpl& operator = (const CMtServerImpl&);
//private:
//	CLrpServer& m_server;
//	LrpInvokeHandler m_handler;
//	SOCKET m_socket;
//	HANDLE m_thread;
//	volatile bool m_continue;
//	CriticalSection m_synchronizer;
//	set<Channel> m_channels;
//	CLrpThreadPool m_threads;
//};