#include "stdafx.h"
#include "MemoryBuffer.h"
#include "LrpLocalClient.h"
#include "LrpLocalClientImpl.h"

CLrpLocalClient::CLrpLocalClient() : m_impl()
{
}
CLrpLocalClient::CLrpLocalClient(const char* localSignature, const char* dllPath, const char* typeName) : m_impl()
{
	m_impl = new CLrpLocalClientImpl(localSignature, dllPath, typeName);
}
CLrpLocalClient::~CLrpLocalClient()
{
	if (nullptr != m_impl)
	{
		delete m_impl;
		m_impl = nullptr;
	}
}
HRESULT CLrpLocalClient::Construct(const char* localSignature, const char* dllPath, const char* typeName)
{
	if (nullptr != m_impl)
	{
		return S_FALSE;
	}
	try
	{
		m_impl = new CLrpLocalClientImpl(localSignature, dllPath, typeName);
		return S_OK;
	}
	catch(const std::exception&)
	{
		return E_FAIL;
	}
}

void CLrpLocalClient::Initialize(MemoryBuffer& buffer)
{
	MemoryBuffer temp(2);
	std::swap(temp, buffer);
}
HRESULT CLrpLocalClient::Invoke(const unsigned __int16 componentId, const unsigned __int16 methodId, MemoryBuffer& buffer)
{
	return m_impl->Invoke(componentId, methodId, buffer);
}
bool CLrpLocalClient::IsSupported(const unsigned __int16 componentId) const
{
	const bool result = m_impl->IsSupported(componentId);
	return result;
}
bool CLrpLocalClient::IsSupported(const unsigned __int16 componentId, const unsigned __int16 methodId) const
{
	const bool result = m_impl->IsSupported(componentId, methodId);
	return result;
}