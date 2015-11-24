#pragma once
#include "ILrpChannel.h"



class LRPCORE_API CLrpStClient : public ILrpChannel
{
public:
	CLrpStClient();
	CLrpStClient(const char* localSignature, const char* address, int port, const char* username, const char* password, const unsigned __int32 operationTimeoutInMs);
	CLrpStClient(const char* localSignature, const char* address, int port, const char* username, const char* password, LrpLogHandler pLogHandler, void* pUserParam, const unsigned __int32 operationTimeoutInMs);
	CLrpStClient(const char* localSignature, const char* address, int port, const char* username, const char* password, const char* logPath, const unsigned __int32 operationTimeoutInMs);
	virtual ~CLrpStClient();
public:
	HRESULT Construct(const char* localSignature, const char* address, int port, const char* username, const char* password, const unsigned __int32 operationTimeoutInMs);
	HRESULT Construct(const char* localSignature, const char* address, int port, const char* username, const char* password, const char* logPath, const unsigned __int32 operationTimeoutInMs);
	HRESULT Construct(const char* localSignature, const char* address, int port, const char* username, const char* password, LrpLogHandler pLogHandler, void* pUserParam, const unsigned __int32 operationTimeoutInMs);
public:
	virtual void Initialize(MemoryBuffer& buffer);
	virtual HRESULT Invoke(unsigned __int16 componentId, unsigned __int16 methodId, MemoryBuffer& buffer);
	virtual bool IsSupported(unsigned __int16 componentId) const;
	virtual bool IsSupported(unsigned __int16 componentId, unsigned __int16 methodId) const;
	bool Ping(const unsigned int timeoutInMilliseconds);
public:
	void Translate(unsigned __int16& componentId, unsigned __int16& methodId) const;
	bool Connect(const unsigned int timeoutInMilliseconds) const;
	bool IsConnected() const;
private:
	CLrpStClientImpl* m_impl;
};
