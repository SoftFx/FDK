#include "stdafx.h"
#include "StChannelImpl.h"
#include "StServerImpl.h"
#include "MemoryBuffer.h"
#include "Functions.h"

CStChannelImpl::CStChannelImpl(CStServerImpl& server, SOCKET socket, void* handle) :
	m_server(server), m_socket(socket), m_handle(handle), m_continue(true), m_buffer(4), m_thread(nullptr)
{
	m_thread = reinterpret_cast<HANDLE>(_beginthreadex(nullptr, 0, CStChannelImpl::ThreadFunction, this, 0, nullptr));
	if (nullptr == m_thread)
	{
		throw runtime_error("Couldn't create a new thread");
	}
}
CStChannelImpl::~CStChannelImpl()
{
	m_server.ShutdownChannel(m_handle);
	CloseHandle(m_thread);
	m_thread = nullptr;
}
void CStChannelImpl::Finalize(bool isSelfDestrcution)
{
	m_continue = false;
	if (isSelfDestrcution)
	{
		delete this;
	}
	else
	{
		m_socket.ShutDown();
		m_socket.Finalize();
		WaitForSingleObject(m_thread, INFINITE);
	}
}

unsigned __stdcall CStChannelImpl::ThreadFunction(void* arg)
{
	CStChannelImpl* pChannel = reinterpret_cast<CStChannelImpl*>(arg);
	__try
	{
		pChannel->ThreadMethod();
		return 0;
	}
	__except(LrpExceptionHandler(GetExceptionInformation()))
	{
		return 1;
	}
}

void CStChannelImpl::ThreadMethod()
{
	if (Header())
	{
		Loop();
	}
	if (m_server.RemoveChannel(this))
	{
		Finalize(true);
	}
}
bool CStChannelImpl::Header()
{
	CTimeout timeout(60000);
	uint32 version = 0;
	if (!ReceiveEx(m_socket.Handle(), timeout, &version, sizeof(version)))
	{
		return false;
	}
	HRESULT answer = (cInitialProtocolVersion == version) ? S_OK : E_FAIL;
	if (!SendEx(m_socket.Handle(), timeout, &answer, sizeof(answer)))
	{
		return false;
	}
	string username;
	if (!ReceiveEx(m_socket.Handle(), timeout, cMaximumStringLength, username))
	{
		return false;
	}
	string password;
	if (!ReceiveEx(m_socket.Handle(), timeout, cMaximumStringLength, password))
	{
		return false;
	}
	bool result = m_server.ValidateCredentials(username, password, m_handle);
	answer = result ? S_OK : E_FAIL;
	if (!SendEx(m_socket.Handle(), timeout, &answer, sizeof(answer)))
	{
		return false;
	}
	if (result)
	{
		const char* signature = m_server.Signature();
		result = SendEx(m_socket.Handle(), signature);
	}
	return result;
}
void CStChannelImpl::Loop()
{
	LrpInvokeHandler handler = m_server.Handler();
	for (; m_continue; )
	{
		Invoke(handler);
	}
}
void CStChannelImpl::Invoke(LrpInvokeHandler handler)
{
	bool status = Receive();
	if (status)
	{
		status = m_buffer.GetSize() >= 12;
	}
	if (status)
	{
		m_buffer.SetPosition(12);
		size_t componentId = ReadUInt16(m_buffer);
		size_t methodId = ReadUInt16(m_buffer);
		handler(componentId, methodId, m_buffer, m_handle);
		status = Send();
	}
	m_continue = status;
}
bool CStChannelImpl::Receive()
{
	// receiving package size and any additional data, if it is available

	char* buffer = reinterpret_cast<char*>(m_buffer.GetData());
	int length = static_cast<int>(m_buffer.GetCapacity());

	int total = 0;
	for (; total < 4;)
	{
		const int status = m_socket.Receive(buffer, length);
		if (status <= 0)
		{
			return false;
		}
		buffer += status;
		total += status;
		length -= status;
	}
	// receive remaining data
	m_buffer.Construct(total);

	int size = m_buffer.ReadImpl<__int32>(); // package size
	length = size - total + sizeof(int); // remaining data length
	if (length < 0) // we read more than expected
	{
		return false;
	}
	if (0 == length) // we read all data
	{
		return true;
	}
	m_buffer.Construct(size + sizeof(int));
	buffer = reinterpret_cast<char*>(m_buffer.GetData());
	buffer += total;
	for (; length > 0;)
	{
		const int status = m_socket.Receive(buffer, length);
		if (status <= 0)
		{
			return false;
		}
		buffer += status;
		length -= status;
	}
	return true;
}
bool CStChannelImpl::Send()
{
	m_buffer.SetPosition(0);
	WriteInt32(static_cast<int32>(m_buffer.GetSize() - 4), m_buffer);
	const char* buffer = reinterpret_cast<const char*>(m_buffer.GetData());
	int length = static_cast<int>(m_buffer.GetSize());
	for (; length > 0; )
	{
		const int status = m_socket.Send(buffer, length);
		if (status <= 0)
		{
			return false;
		}
		buffer += status;
		length -= status;
	}
	return true;
}


