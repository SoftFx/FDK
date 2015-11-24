#include "stdafx.h"
//#include "MtChannelImpl.h"
//#include "LrpMtServer.h"
//#include "MemoryBuffer.h"
//
//
//CMtChannelImpl::CMtChannelImpl(CMtServerImpl& server, SOCKET socket, void* handle, LrpInvokeHandler handler) :
//	m_server(server), m_handler(handler), m_socket(socket), m_handle(handle), m_continue(true),
//	m_receivingThread(), m_sendingThread(), m_sendingEvent(), m_counter(3)
//{
//	if (!Construct())
//	{
//		Finalize();
//		throw runtime_error("Can not create a new channel");
//	}
//}
//bool CMtChannelImpl::Construct()
//{
//	m_sendingEvent = CreateEvent(nullptr, FALSE, FALSE, nullptr);
//	if (nullptr == m_sendingEvent)
//	{
//		return false;
//	}
//	m_sendingThread  = reinterpret_cast<HANDLE>(_beginthreadex(nullptr, 0, &CMtChannelImpl::SendingThread, this, 0, nullptr));
//	if (nullptr == m_sendingThread)
//	{
//		return false;
//	}
//	m_receivingThread = reinterpret_cast<HANDLE>(_beginthreadex(nullptr, 0, &CMtChannelImpl::ReceivingThread, this, 0, nullptr));
//	if (nullptr == m_receivingThread)
//	{
//		return false;
//	}
//	return true;
//}
//CMtChannelImpl::~CMtChannelImpl()
//{
//	Finalize();
//}
//void CMtChannelImpl::Finalize()
//{
//	m_continue = false;
//	if (nullptr != m_sendingEvent)
//	{
//		SetEvent(m_sendingEvent);
//	}
//	shutdown(m_socket, SD_BOTH);
//	if (nullptr != m_receivingThread)
//	{
//		WaitForSingleObject(m_receivingThread, INFINITE);
//		CloseHandle(m_receivingThread);
//		m_receivingThread = nullptr;
//	}
//	if (nullptr != m_sendingThread)
//	{
//		WaitForSingleObject(m_sendingThread, INFINITE);
//		CloseHandle(m_sendingThread);
//		m_sendingThread = nullptr;
//	}
//	if (nullptr != m_sendingEvent)
//	{
//		CloseHandle(m_sendingEvent);
//		m_sendingEvent = nullptr;
//	}
//	if (nullptr != m_handle)
//	{
//		m_server.ShutdownConnection(m_handle);
//		m_handle = nullptr;
//	}
//	for each(auto element in m_sendingData)
//	{
//		DeleteMemoryBuffer(element);
//	}
//	for each(auto element in m_sendingData2)
//	{
//		DeleteMemoryBuffer(element);
//	}
//}
//
//
//unsigned __stdcall CMtChannelImpl::ReceivingThread(void* arg)
//{
//	CMtChannelImpl* pChannel = reinterpret_cast<CMtChannelImpl*>(arg);
//	__try
//	{
//		pChannel->ReceivingLoop();
//	}
//	__except(LrpExceptionHandler(GetExceptionInformation()))
//	{
//		return 1;
//	}
//	return 0;
//}
//
//unsigned __stdcall CMtChannelImpl::SendingThread(void* arg)
//{
//	CMtChannelImpl* pChannel = reinterpret_cast<CMtChannelImpl*>(arg);
//	__try
//	{
//		pChannel->SendingLoop();
//	}
//	__except(LrpExceptionHandler(GetExceptionInformation()))
//	{
//		return 1;
//	}
//	return 0;
//}
//
//void CMtChannelImpl::ReceivingLoop()
//{
//	Channel guard(this);
//	for (; m_continue;)
//	{
//		if (!ReceiveData())
//		{
//			break;
//		}
//	}
//}
//namespace
//{
//	bool ReceiveData(SOCKET socket, int count, void* buffer)
//	{
//		char* data = reinterpret_cast<char*>(buffer);
//		while(count > 0)
//		{
//			int status = recv(socket, data, count, 0);
//			if (status <= 0)
//			{
//				return false;
//			}
//			data += status;
//			count -= status;
//		}
//		return true;
//	}
//}
//bool CMtChannelImpl::ReceiveData()
//{
//	int count = 0;
//	if (!::ReceiveData(m_socket, sizeof(unsigned int), &count))
//	{
//		return false;
//	}
//	MemoryBuffer* buffer = CreateMemoryBuffer(count);
//
//	if (!::ReceiveData(m_socket, count, buffer->GetData()))
//	{
//		DeleteMemoryBuffer(buffer);
//		return false;
//	}
//	try
//	{
//		Channel channel(this);
//		this->Acquire();
//		CLock lock(m_receivingSynchronizer);
//		m_server.Process(channel);
//		m_receivingData.push_back(buffer);
//	}
//	catch (const std::bad_alloc&)
//	{
//		DeleteMemoryBuffer(buffer);
//		return false;
//	}
//	return true;
//}
//void CMtChannelImpl::SendingLoop()
//{
//	Channel guard(this);
//	for (WaitForSingleObject(m_sendingEvent, INFINITE); m_continue; WaitForSingleObject(m_sendingEvent, INFINITE))
//	{
//		{
//			CLock lock(m_sendingSynchronizer);
//			std::swap(m_sendingData, m_sendingData2);
//		}
//		if (!SendData())
//		{
//			break;
//		}
//	}
//}
//namespace
//{
//	bool SendData(SOCKET socket, int count, const void* buffer)
//	{
//		const char* data = reinterpret_cast<const char*>(buffer);
//		while(count > 0)
//		{
//			int status = send(socket, data, count, 0);
//			if (SOCKET_ERROR == status)
//			{
//				return false;
//			}
//			data += status;
//			count -= status;
//		}
//		return true;
//	}
//}
//bool CMtChannelImpl::SendData()
//{
//	auto it = m_sendingData2.begin();
//	auto end = m_sendingData2.end();
//
//	bool result = true;
//
//	for (; m_continue && (it != end); ++it)
//	{
//		MemoryBuffer* buffer = *it;
//		result = SendData(*buffer);
//		if (!result)
//		{
//			break;
//		}
//		DeleteMemoryBuffer(buffer);
//	}
//	for (; it != end; ++it)
//	{
//		MemoryBuffer* buffer = *it;
//		DeleteMemoryBuffer(buffer);
//	}
//	m_sendingData2.clear();
//	return result;
//}
//
//bool CMtChannelImpl::SendData(MemoryBuffer& buffer)
//{
//	int count = static_cast<int>(buffer.GetSize());
//	const char* data = reinterpret_cast<const char*>(buffer.GetData());
//	while(count > 0)
//	{
//		const int status = send(m_socket, data, count, 0);
//		if (SOCKET_ERROR == status)
//		{
//			return false;
//		}
//		data += status;
//		count -= status;
//	}
//	return true;
//}
//void CMtChannelImpl::Process()
//{
//	/*MemoryBuffer* buffer = nullptr;
//	{
//	CLock lock(m_receivingSynchronizer);
//	if (m_receivingData.empty())
//	{
//	return;
//	}
//	buffer = m_receivingData.front();
//	m_receivingData.pop_front();
//	}
//
//	unsigned __int64 id = 0;
//	const HRESULT status = m_handler(m_handle, id, *buffer);
//	{
//	CLock lock(m_sendingSynchronizer);
//	m_sendingData.push_back(buffer);
//	}
//	SetEvent(m_sendingEvent);*/
//}
//
//void CMtChannelImpl::Acquire()
//{
//	InterlockedIncrement(&m_counter);
//}
//
//void CMtChannelImpl::Release()
//{
//	LONG counter = InterlockedDecrement(&m_counter);
//	if (0 == counter)
//	{
//		delete this;
//	}
//}
