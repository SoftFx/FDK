#include "stdafx.h"
#include "Transport.h"

CTransport::CTransport(SOCKET socket) : m_socket(socket)
{
	EnableNonBlockingMode(socket);
	ResolveAddress();
}
void CTransport::ResolveAddress()
{
	try
	{
		sockaddr_in addr;
		int length = sizeof(addr);
		const int status = getpeername(m_socket, reinterpret_cast<sockaddr*>(&addr), &length);
		if (SOCKET_ERROR != status)
		{
			const char* address = inet_ntoa(addr.sin_addr);
			if (nullptr != address)
			{
				m_address = address;
			}
		}
	}
	catch (const std::bad_alloc&)
	{
	}
}

CTransport::~CTransport()
{
	Finalize();
}
HRESULT CTransport::LogicalAccept()
{
	const HRESULT result = FxLogicalAccept(m_socket);
	return result;
}
void CTransport::Finalize()
{
	if (INVALID_SOCKET != m_socket)
	{
		shutdown(m_socket, SD_BOTH);
		closesocket(m_socket);
		m_socket = INVALID_SOCKET;
	}
}
const string& CTransport::GetAddress() const
{
	return m_address;
}
HRESULT CTransport::SelectWrite(MemoryBuffer& buffer)
{
	return ::SelectWrite(m_socket, buffer);
}
bool CTransport::CanRead() const
{
	return FxCanRead(m_socket);
}
bool CTransport::CanWrite() const
{
	return FxCanWrite(m_socket);
}
CSocketState CTransport::CanReadWrite() const
{
	return FxCanReadWrite(m_socket);
}
HRESULT CTransport::Read(MemoryBuffer& buffer)
{
	return ::Read(m_socket, buffer);
}
HRESULT CTransport::Write(MemoryBuffer& buffer)
{
	return ::Write(m_socket, buffer);
}

