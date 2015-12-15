#pragma once
#include "LrpSender.h"
#include "LrpReceiver.h"
#include "Client.h"
#include "Socket.h"


class CLrpConnection : public IConnection
{
public:
	CLrpConnection(const CFxParams& params);
	~CLrpConnection();
public:
	virtual void VReceiver(IReceiver* pReceiver);
	virtual ISender* VSender();
	virtual void VStart();
	virtual void VShutdown();
	virtual void VStop();
	virtual void VGetActivity(uint64* pLogicalBytesSent, uint64* pPhysicalBytesSent, uint64* pLogicalBytesReceived, uint64* pPhysicalBytesReceived);
public:
	HRESULT SendMessage(const CMessage& message);
private:
	void DoSendMessage(const CMessage& message);
private:
	void Run();
	void Step();
	bool Connect();
	void Loop();
	bool DoRead();
	bool DoWrite();
	CSocketState WaitFor();
private:
	bool DoSendClientVersion(CTimeout timeout);
	bool DoReceiveVersionResponse(CTimeout timeout);
	bool DoSendUsernamePassword(CTimeout timeout);
	bool DoReceiveVerificationResponse(CTimeout timeout);
	bool DoSendClientSignagure(CTimeout timeout);
	bool DoReceiveServerSignagure(CTimeout timeout);
private:
	bool Send(CTimeout timeout);
	bool Receive(CTimeout timeout);
private:
	string m_address;
	int m_port;
	string m_username;
	string m_password;
	bool m_enableQuotesLogging;
private:
	CLrpSender m_sender;
	CLrpReceiver m_receiver; 
	CLrpLogger m_logger;
private:
	volatile bool m_continue;
	CThreadPool m_thread;
	CCallsWaiter m_waiter;
	CCriticalSection m_synchronizer;
private:
	CSocket m_socket;
	MemoryBuffer m_incommingBuffer;
	MemoryBuffer m_outgoingBuffer;
private:
	deque<CMessage> m_messages;
};