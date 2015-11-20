#include "stdafx.h"
#include "LrpStServer.h"
#include "StServerImpl.h"


CLrpStServer::CLrpStServer() : m_impl()
{
}
CLrpStServer::CLrpStServer(const int port, const char* signature, LrpInvokeHandler handler) : m_impl()
{
	m_impl = new CStServerImpl(port, *this, signature, handler);
}
CLrpStServer::CLrpStServer(const int port, const char* signature, LrpInvokeHandler handler, LrpLogHandler pLogHandler, void* pUserParam) : m_impl()
{
	m_impl = new CStServerImpl(port, *this, signature, handler, pLogHandler, pUserParam);
}
CLrpStServer::CLrpStServer(const int port, const char* signature, LrpInvokeHandler handler, const char* logPath) : m_impl()
{
	m_impl = new CStServerImpl(port, *this, signature, handler, logPath);
}
CLrpStServer::~CLrpStServer()
{
	if (nullptr != m_impl)
	{
		delete m_impl;
	}
}
HRESULT CLrpStServer::Construct(const int port, const char* signature, LrpInvokeHandler handler)
{
	if (nullptr != m_impl)
	{
		return S_FALSE;
	}
	try
	{
		m_impl = new CStServerImpl(port, *this, signature, handler);
		return S_OK;
	}
	catch(const std::exception&)
	{
		return E_FAIL;
	}
}
HRESULT CLrpStServer::Construct(const int port, const char* signature, LrpInvokeHandler handler, LrpLogHandler pLogHandler, void* pUserParam)
{
	if (nullptr != m_impl)
	{
		return S_FALSE;
	}
	try
	{
		m_impl = new CStServerImpl(port, *this, signature, handler, pLogHandler, pUserParam);
		return S_OK;
	}
	catch(const std::exception&)
	{
		return E_FAIL;
	}
}
HRESULT CLrpStServer::Construct(const int port, const char* signature, LrpInvokeHandler handler, const char* logPath)
{
	if (nullptr != m_impl)
	{
		return S_FALSE;
	}
	try
	{
		m_impl = new CStServerImpl(port, *this, signature, handler, logPath);
		return S_OK;
	}
	catch(const std::exception&)
	{
		return E_FAIL;
	}
}
bool CLrpStServer::ValidateCredentialsInternal(const char* /*username*/, const char* /*password*/, void* /*handle*/)
{
	return true;
}
void CLrpStServer::ShutdownConnectionInternal(void* /*handle*/)
{
}
