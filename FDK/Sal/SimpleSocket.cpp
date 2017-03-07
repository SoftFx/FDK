#include "stdafx.h"
#include "SimpleSocket.h"

SimpleSocket::SimpleSocket(SOCKET s, bool enabledStats) : Socket(s, enabledStats)
{
}
SimpleSocket::SimpleSocket(SOCKET s, bool isAccepting, bool enabledStats) : Socket(s, enabledStats)
{
	m_isAccepting = isAccepting;
}
SimpleSocket::~SimpleSocket()
{
}
Socket* SimpleSocket::PhysicalAccept(struct sockaddr* addr, socklen_t* addrlen, const char* /*ceritificateFileName*/, const char* /*password*/)
{
	SOCKET s = ::accept(m_socket, addr, addrlen);
	if (INVALID_SOCKET == s)
	{
		return nullptr;
	}
	Socket* result = new SimpleSocket(s, true, false);
	return result;
}
HRESULT SimpleSocket::LogicalAccept()
{
	if (!m_isAccepting)
	{
		return E_FAIL;
	}
	m_isAccepting = false;
	return S_OK;
}
int SimpleSocket::Connect(ConnectType type, const sockaddr* address, int addressLen, const sockaddr* proxyAddress, int proxyAddressLen, const char* userName, const char* password)
{
	const int result = Socket::Connect(type, address, addressLen, proxyAddress, proxyAddressLen, userName, password);
	if (0 == result)
	{
		EnableKeepAlive(m_socket);
	}
	return result;
}
int SimpleSocket::Send(const char* buf, int len, int flags)
{
	const int result = ::send(m_socket, buf, len, flags);
	if (result > 0)
	{
		OnSent(result);
	}
	return result;
}
int SimpleSocket::RecvFrom(char* buf, int len, int flags, struct sockaddr* from, socklen_t* fromlen)
{
	const int result = ::recvfrom(m_socket, buf, len, flags, from, fromlen);
	if (result > 0)
	{
		OnReceived(result);
	}
	if (0 == result)
	{
		return -1;
	}
	return result;
}
int SimpleSocket::Recv(char* buf, int len, int flags)
{
	const int result = ::recv(m_socket, buf, len, flags);
	if (result > 0)
	{
		OnReceived(result);
		return result;
	}
	const DWORD status = WSAGetLastError();
	if (WSAEWOULDBLOCK == status)
	{
		return 0;
	}
	return -1;
}
int SimpleSocket::SendTo(const char* buf, int len, int flags, const struct sockaddr* to, int tolen)
{
	const int result = ::sendto(m_socket, buf, len, flags, to, tolen);
	if (result > 0)
	{
		OnSent(result);
	}
	return result;
}
bool SimpleSocket::Pending()
{
	return false;
}
