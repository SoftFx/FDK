#include "stdafx.h"
//#include "LrpMtServer.h"
//#include "MtServerImpl.h"
//#include "MtChannelImpl.h"
//#include "Functions.h"
//
//
//
//
//CMtServerImpl::CMtServerImpl(CLrpServer& server, const int port, const size_t numberOfThreadsInPool, const size_t maxJobsNumber, LrpInvokeHandler handler) :
//	m_server(server), m_handler(handler), m_socket(INVALID_SOCKET), m_thread(), m_continue(true), m_threads(numberOfThreadsInPool)
//{
//	if (nullptr == handler)
//	{
//		throw runtime_error("LrpInvokeHandler can not be null.");
//	}
//	if (0 == numberOfThreadsInPool)
//	{
//		throw runtime_error("Threads number can not be null");
//	}
//	if (!Construct(port))
//	{
//		throw runtime_error("Can not create/bind/listen socket");
//	}
//	m_thread = reinterpret_cast<HANDLE>(_beginthreadex(nullptr, 0, &CMtServerImpl::ThreadFunction, this, 0, nullptr));
//	if (nullptr == m_thread)
//	{
//		closesocket(m_socket);
//		throw runtime_error("Can not start a new thread");
//	}
//}
//bool CMtServerImpl::Construct(const int port)
//{
//	SOCKET socket = ::socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
//	if (INVALID_SOCKET == socket)
//	{
//		return false;
//	}
//	sockaddr_in service;
//	service.sin_family = AF_INET;
//	service.sin_addr.s_addr = INADDR_ANY;
//	service.sin_port = htons(port);
//
//	int status = bind(socket, (SOCKADDR*) &service, sizeof(service));
//	if (SOCKET_ERROR == status)
//	{
//		closesocket(socket);
//		return false;
//	}
//	status = listen(socket, SOMAXCONN);
//	if (SOCKET_ERROR == status)
//	{
//		closesocket(socket);
//		return false;
//	}
//	m_socket = socket;
//	return true;
//}
//CMtServerImpl::~CMtServerImpl()
//{
//	m_continue = false;
//	AsynchCloseSocketSafely(m_thread, m_socket);
//	m_socket = INVALID_SOCKET;
//
//	WaitForSingleObject(m_thread, INFINITE);
//	CloseHandle(m_thread);
//	m_thread = nullptr;
//	
//	set<Channel> channels;
//	{
//		CLock lock(m_synchronizer);
//		swap(m_channels, channels);
//	}
//	for each(auto element in channels)
//	{
//		element->Finalize();
//	}
//	channels.clear();
//}
//unsigned __stdcall CMtServerImpl::ThreadFunction(void* arg)
//{
//	CMtServerImpl* pServer = reinterpret_cast<CMtServerImpl*>(arg);
//	__try
//	{
//		pServer->Loop();
//	}
//	__except(LrpExceptionHandler(GetExceptionInformation()))
//	{
//		return 1;
//	}
//	return 0;
//}
//void CMtServerImpl::Loop()
//{
//	for (; m_continue;)
//	{
//		const SOCKET client = accept(m_socket, 0, 0);
//		if (INVALID_SOCKET != client)
//		{
//			CreateNewChannel(client);
//		}
//		else
//		{
//			Sleep(1000);
//		}
//	}
//}
//void CMtServerImpl::CreateNewChannel(SOCKET client)
//{
//	void* handle = ApproveNewClient(client);
//	if (nullptr != handle)
//	{
//		CreateNewChannel(client, handle);
//	}
//	else
//	{
//		closesocket(client);
//	}
//}
//void CMtServerImpl::CreateNewChannel(SOCKET client, void* handle)
//{
//	EnableKeepAlive(client);
//	CMtChannelImpl* pChannel = nullptr;
//	CLock lock(m_synchronizer);
//	try
//	{
//		pChannel = new CMtChannelImpl(*this, client, handle, m_handler);
//		auto_ptr<CMtChannelImpl> sentry(pChannel);
//		m_channels.insert(pChannel);
//		sentry.release();
//	}
//	catch (const exception&)
//	{
//		if (0 == pChannel)
//		{
//			closesocket(client);
//		}
//	}
//}
//void* CMtServerImpl::ApproveNewClient(SOCKET client)
//{
//	struct sockaddr_in addr;
//	int length = sizeof(addr);
//	const int status = getpeername(client, (struct sockaddr*)&addr, &length);
//	if (SOCKET_ERROR == status)
//	{
//		return nullptr;
//	}
//	const char* address = inet_ntoa(addr.sin_addr);
//	if (nullptr == address)
//	{
//		return nullptr;
//	}
//	void* result = m_server.CreateNewConnectionInternal(address);
//	return result;
//}
//
//void CMtServerImpl::ShutdownConnection(void* handle)
//{
//	m_server.ShutdownConnectionInternal(handle);
//}
//void CMtServerImpl::Process(const Channel& chanel)
//{
//	m_threads.Add(chanel);
//}
//
