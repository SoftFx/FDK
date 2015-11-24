#pragma once
#include "Request.h"

class CBridgeClient
{
public:
	CBridgeClient(int bankCode, int metaAccount, const string& metaPassword, const string& address, int port, const string& username, const string& password, const string& logPath);
	~CBridgeClient();
public:
	void SetReceiver(IReceiver* pReceiver);
	void Finalize();
	void SendOrder(const string& id, const CFxOrder& order);
private:
	void InitializeLog(const string& logPath, const string& address, int port, int bankCode);
	void InitializeLrpClient(const string& address, int port, const string& username, const string& password);
private:
	void Loop();
	void SafeStep();
	void Step();
	void Connect();
	void Execute(const string& id, const CFxOrder& order);
	void SendNewOrder(const string& id, CDateTime cretionTime, const CFxOrder& order);
	void SendReject(const string& id, CDateTime creationTime, const CFxOrder& order);
	void SendFilled(const string& id, CDateTime creationTime, const CFxOrder& order, double executedPrice, double executedVolume);
	void SendFirstFilled(const string& id, CDateTime cretionTime, CDateTime modificcationTime, const CFxOrder& order, double executedPrice, double executedVolume);
	void SendSecondFilled(const string& id, CDateTime cretionTime, CDateTime modificcationTime, const CFxOrder& order, double executedPrice, double executedVolume);
private:
	volatile bool m_continue;
	int m_bankCode;
	int m_metaAccount;
	string m_metaPassword;
	IReceiver* m_receiver;
	CSemaphore m_semaphore;
	CCriticalSection m_synchronizer;
	CThreadPool m_thread;
	auto_ptr<CLogger> m_logger;
	auto_ptr<CLrpStClient> m_client;
	deque<CRequest> m_requests;
};