#pragma once
#include "StChannelImpl.h"
#include "Acceptor.h"
#include "Logger.h"

class CLrpStServer;
class CStServerImpl
{
public:
	CStServerImpl(const int port, CLrpStServer& server, const char* signature, LrpInvokeHandler handler);
	CStServerImpl(const int port, CLrpStServer& server, const char* signature, LrpInvokeHandler handler, LrpLogHandler pLogHandler, void* pUserParam);
	CStServerImpl(const int port, CLrpStServer& server, const char* signature, LrpInvokeHandler handler, const char* logPath);
	~CStServerImpl();
public:
	bool RemoveChannel(CStChannelImpl* pChannel);
	const char* Signature() const;
	LrpInvokeHandler Handler() const;
	bool ValidateCredentials(const string& username, const string& password, void* handle);
	void ShutdownChannel(void* handle);
private:
	bool Construct(const int port);
private:
	static unsigned __stdcall ThreadFunction(void* arg);
	void Loop();
	void CreateNewChannel(SOCKET client);
	void CreateNewChannel(SOCKET client, void* handle);
	void* ApproveNewClient(SOCKET client);
private:
	CStServerImpl(const CStServerImpl&);
	void Construct(LrpInvokeHandler handler);
	CStServerImpl& operator = (const CStServerImpl&);
private:
	static void OnOutput(void* pUserParam, const char* message);
private:
	CLrpStServer& m_server;
	const char* m_signature;
	LrpInvokeHandler m_handler;
	CAcceptor m_acceptor;
	HANDLE m_thread;
	volatile bool m_continue;
	CriticalSection m_synchronizer;
	set<CStChannelImpl*> m_channels;
private:
	CriticalSection m_loggerSynchronizer;
	auto_ptr<ofstream> m_logStream;
	CLogger m_logger;
};