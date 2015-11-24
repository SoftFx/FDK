#include "stdafx.h"
#include "LrpStClient.h"
#include "LrpStClientImpl.h"
#include "MemoryBuffer.h"

CLrpStClient::CLrpStClient() : m_impl()
{
}
CLrpStClient::CLrpStClient(const char* localSignature, const char* address, int port, const char* username, const char* password, const unsigned __int32 operationTimeoutInMs)
{
	m_impl = new CLrpStClientImpl(localSignature, address, port, username, password, operationTimeoutInMs);
}
CLrpStClient::CLrpStClient(const char* localSignature, const char* address, int port, const char* username, const char* password, LrpLogHandler pLogHandler, void* pUserParam, const unsigned __int32 operationTimeoutInMs)
{
	m_impl = new CLrpStClientImpl(localSignature, address, port, username, password, pLogHandler, pUserParam, operationTimeoutInMs);
}
CLrpStClient::CLrpStClient(const char* localSignature, const char* address, int port, const char* username, const char* password, const char* logPath, const unsigned __int32 operationTimeoutInMs)
{
	m_impl = new CLrpStClientImpl(localSignature, address, port, username, password, logPath, operationTimeoutInMs);
}
CLrpStClient::~CLrpStClient()
{
	delete m_impl;
}
HRESULT CLrpStClient::Construct(const char* localSignature, const char* address, int port, const char* username, const char* password, const unsigned __int32 operationTimeoutInMs)
{
	if (nullptr != m_impl)
	{
		return S_FALSE;
	}
	try
	{
		m_impl = new CLrpStClientImpl(localSignature, address, port, username, password, operationTimeoutInMs);
		return S_OK;
	}
	catch(const std::exception&)
	{
		return E_FAIL;
	}
}
HRESULT CLrpStClient::Construct(const char* localSignature, const char* address, int port, const char* username, const char* password, const char* logPath, const unsigned __int32 operationTimeoutInMs)
{
	if (nullptr != m_impl)
	{
		return S_FALSE;
	}
	try
	{
		m_impl = new CLrpStClientImpl(localSignature, address, port, username, password, logPath, operationTimeoutInMs);
		return S_OK;
	}
	catch(const std::exception&)
	{
		return E_FAIL;
	}
}
HRESULT CLrpStClient::Construct(const char* localSignature, const char* address, int port, const char* username, const char* password, LrpLogHandler pLogHandler, void* pUserParam, const unsigned __int32 operationTimeoutInMs)
{
	if (nullptr != m_impl)
	{
		return S_FALSE;
	}
	try
	{
		m_impl = new CLrpStClientImpl(localSignature, address, port, username, password, pLogHandler, pUserParam, operationTimeoutInMs);
		return S_OK;
	}
	catch(const std::exception&)
	{
		return E_FAIL;
	}
}
void CLrpStClient::Translate(unsigned __int16& componentId, unsigned __int16& methodId) const
{
	m_impl->Translate(componentId, methodId);
}
void CLrpStClient::Initialize(MemoryBuffer& buffer)
{
	MemoryBuffer temp(0, 0);
	std::swap(buffer, temp);
}
HRESULT CLrpStClient::Invoke(const unsigned __int16 componentId, const unsigned __int16 methodId, MemoryBuffer& buffer)
{
	buffer.SetPosition(sizeof(int64) + sizeof(int32));
	WriteUInt16(componentId, buffer);
	WriteUInt16(methodId, buffer);
	return m_impl->Invoke(buffer);
}
bool CLrpStClient::Connect(const unsigned int timeoutInMilliseconds) const
{
	return m_impl->Connect(timeoutInMilliseconds);
}
bool CLrpStClient::IsConnected() const
{
	return m_impl->IsConnected();
}
bool CLrpStClient::IsSupported(const unsigned __int16 componentId) const
{
	return m_impl->IsSupported(componentId);
}
bool CLrpStClient::IsSupported(const unsigned __int16 componentId, const unsigned __int16 methodId) const
{
	return m_impl->IsSupported(componentId, methodId);
}
bool CLrpStClient::Ping(const unsigned int timeoutInMilliseconds)
{
	return m_impl->Ping(timeoutInMilliseconds);
}
namespace
{
	CLrpStClient gClient;
}