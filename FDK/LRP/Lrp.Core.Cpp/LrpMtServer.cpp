#include "stdafx.h"
//#include "LrpMtServer.h"
//
//CLrpServer::CLrpServer() : m_impl(nullptr)
//{
//}
//CLrpServer::CLrpServer(const int port, const size_t numberOfThreadsInPool, const size_t maxJobsNumber, LrpInvokeHandler handler) : m_impl(nullptr)
//{
//	Construct(port, numberOfThreadsInPool, maxJobsNumber, handler);
//}
//void CLrpServer::Construct(const int port, const size_t numberOfThreadsInPool, const size_t maxJobsNumber, LrpInvokeHandler handler)
//{
//	if (nullptr != m_impl)
//	{
//		throw runtime_error("Can not initialize LRP server twice.");
//	}
//	m_impl = new CMtServerImpl(*this, port, numberOfThreadsInPool, maxJobsNumber, handler);
//}
//CLrpServer::~CLrpServer()
//{
//	delete m_impl;
//}
//bool CLrpServer::ValidateCredentialsInternal(const char* /*username*/, const char* /*password*/)
//{
//	return true;
//}
//void CLrpServer::ShutdownConnectionInternal(void* /*handle*/)
//{
//}
//