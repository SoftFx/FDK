#pragma once
#include "LrpClientImpl.h"
#include "Logger.h"


class CLrpStClientImpl : public CLrpClientImpl
{
public:
	CLrpStClientImpl(const char* localSignature, const char* address, int port, const char* username, const char* password, const uint32 operationTimeoutInMs);
	CLrpStClientImpl(const char* localSignature, const char* address, int port, const char* username, const char* password, LrpLogHandler pLogHandler, void* pUserParam, const uint32 operationTimeoutInMs);
	CLrpStClientImpl(const char* localSignature, const char* address, int port, const char* username, const char* password, const char* logPath, const uint32 operationTimeoutInMs);
	~CLrpStClientImpl();
public:
	bool Connect(const uint32 timeoutInMilliseconds);
	HRESULT Invoke(MemoryBuffer& buffer);
	bool IsConnected() const;
	bool Ping(const uint32 timeoutInMilliseconds);
private:
	static void OnOutput(void* pUserParam, const char* message);
private:
	SOCKET m_socket;
	string m_address;
	int m_port;
	string m_username;
	string m_password;
	const uint32 m_operationTimeoutInMs;
private:
	auto_ptr<ofstream> m_logStream;
	CLogger m_logger;
};