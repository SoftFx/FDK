#include "stdafx.h"
#include "Socket.h"
#include <iostream>

namespace
{
	timeval gZeroTimeout = { 0, 0 };
}



Socket::Socket(SOCKET s, bool enabledStats) : m_socket(s), m_isAccepting(false), m_enabledStats(enabledStats), m_dataBytesSent(), m_sslBytesSent(), m_dataBytesReceived(), m_sslBytesReceived()
{
}
Socket::~Socket()
{
	Finalize();
}
/// <summary>
/// 
/// </summary>
/// <returns></returns>
void Socket::Finalize()
{
	if (INVALID_SOCKET == m_socket)
	{
		return;
	}
	#ifdef _MSC_VER
	const int status = closesocket(m_socket);
	#else
	const int status = close(m_socket);
	#endif
	m_socket = INVALID_SOCKET;
	assert(0 == status);
	UNREFERENCED_PARAMETER(status);
}


int Socket::Shutdown(int how)
{
	const int result = ::shutdown(m_socket, how);
	return result;
}
int Socket::Bind(const struct sockaddr* name, int namelen)
{
	const int result = ::bind(m_socket, name, namelen);
	return result;
}
int Socket::Listen(int backlog)
{
	const int result = ::listen(m_socket, backlog);
	return result;
}
int Socket::GetPeerName(struct sockaddr* name, socklen_t* namelen)
{
	const int result = ::getpeername(m_socket, name, namelen);
	return result;
}
int Socket::GetSockName(struct sockaddr* name, socklen_t* namelen)
{
	const int result = ::getsockname(m_socket, name, namelen);
	return result;
}
int Socket::GetSockOpt(int level, int optname, char* optval, socklen_t* optlen)
{
	const int result = ::getsockopt(m_socket, level, optname, optval, optlen);
	return result;
}
int Socket::SetSockOpt(int level, int optname, const sockoptval_t* optval, int optlen)
{
	const int result = ::setsockopt(m_socket, level, optname, optval, optlen);
	return result;
}
SOCKET Socket::Handle()
{
	return m_socket;
}
void Socket::EnableKeepAlive(SOCKET socket, u_long keepalivetimeInMilliseconds /* = 10000 */, u_long keepaliveintervalInMilliseconds /* = 3000 */)
{
	#ifdef _MSC_VER
	DWORD size = 0;
	tcp_keepalive keepAlive = {1, keepalivetimeInMilliseconds, keepaliveintervalInMilliseconds};
	const int status = WSAIoctl(socket, SIO_KEEPALIVE_VALS, &keepAlive, sizeof(keepAlive), NULL, 0, &size, NULL, NULL);
	assert(SOCKET_ERROR != status);
	UNREFERENCED_PARAMETER(status);
	#endif
}
void Socket::GetActivity(uint64* pDataBytesSent, uint64* pSslBytesSent, uint64* pDataBytesReceived, uint64* pSslBytesReceived) const
{
	if (nullptr != pDataBytesSent)
	{
		*pDataBytesSent = m_dataBytesSent;
	}
	if (nullptr != pDataBytesReceived)
	{
		*pDataBytesReceived = m_dataBytesReceived;
	}
	if (nullptr != pSslBytesSent)
	{
		*pSslBytesSent = m_sslBytesSent;
	}
	if (nullptr != pSslBytesReceived)
	{
		*pSslBytesReceived = m_sslBytesReceived;
	}
}
void Socket::OnSent(int logicalBytesSent)
{
	if (m_enabledStats)
	{
		m_dataBytesSent += logicalBytesSent;
	}
}
void Socket::OnSent(int logicalBytesSent, int physicalBytesSent, int physicalBytesReceived)
{
	if (m_enabledStats)
	{
		m_sslBytesSent += physicalBytesSent;
		m_sslBytesReceived += physicalBytesReceived;
		m_dataBytesSent += logicalBytesSent;
	}
}
void Socket::OnReceived(int logicalBytesReceived)
{
	if (m_enabledStats)
	{
		m_dataBytesReceived += logicalBytesReceived;
	}
}
void Socket::OnReceived(int logicalBytesReceived, int physicalBytesSent, int physicalBytesReceived)
{
	if (m_enabledStats)
	{
		m_sslBytesSent += physicalBytesSent;
		m_sslBytesReceived += physicalBytesReceived;
		m_dataBytesReceived += logicalBytesReceived;
	}
}
void Socket::SetSocket(SOCKET newSocket)
{
	#ifdef _MSC_VER
	const int status = closesocket(m_socket);
	#else
	const int status = close(m_socket);
	#endif
	assert(0 == status);
	UNREFERENCED_PARAMETER(status);
	m_socket = newSocket;
}
int Socket::Ioctlsocket(long cmd, u_long FAR* argp)
{
	return ioctlsocket(m_socket, cmd, argp);
}
bool Socket::EnabledStats() const
{
	return m_enabledStats;
}

bool Socket::CanRead()
{
	if (Pending())
	{
		return true;
	}

	fd_set set;
	set.fd_count = 1;
	set.fd_array[0] = m_socket;
	const int status = select(1, &set, nullptr, nullptr, &gZeroTimeout);
	const bool result = (status > 0);
	return result;
}
bool Socket::CanWrite()
{
	fd_set set;
	set.fd_count = 1;
	set.fd_array[0] = m_socket;
	const int status = select(1, nullptr, &set, nullptr, &gZeroTimeout);
	const bool result = (status > 0);
	return result;
}
CSocketState Socket::CanReadWrite()
{
	CSocketState result;
	result.CanRead = Pending();
	if (result.CanRead)
	{
		result.CanWrite = CanWrite();
	}
	else
	{
		fd_set readSet;
		readSet.fd_count = 1;
		readSet.fd_array[0] = m_socket;

		FD_SET writeSet;
		writeSet.fd_count = 1;
		writeSet.fd_array[0] = m_socket;

		select(1, &readSet, &writeSet, nullptr, &gZeroTimeout);

		result.CanRead = (readSet.fd_count > 0);
		result.CanWrite = (writeSet.fd_count > 0);
	}
	return result;
}
bool Socket::EnableKeepAlive(u_long keepalivetimeInMilliseconds, u_long keepaliveintervalInMilliseconds)
{
	DWORD size = 0;
	tcp_keepalive keepAlive = {1, keepalivetimeInMilliseconds, keepaliveintervalInMilliseconds};
	const int status = WSAIoctl(m_socket, SIO_KEEPALIVE_VALS, &keepAlive, sizeof(keepAlive), NULL, 0, &size, NULL, NULL);
	if (SOCKET_ERROR == status)
	{
		return false;
	}
	return true;
}
