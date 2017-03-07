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

#pragma pack(push)
#pragma pack(1)

struct socks4_request_header_t
{
    uint8_t vn;
    uint8_t cd;
    uint16_t dstport;
    uint32_t dstip;
};

struct socks4_response_header_t
{
    uint8_t vn;
    uint8_t cd;
    uint16_t dstport;
    uint32_t dstip;
};

struct socks5_method_request_header_t
{
    uint8_t ver;
    uint8_t methods;
    uint8_t method;
};

struct socks5_method_response_header_t
{
    uint8_t ver;
    uint8_t method;
};

struct socks5_logon_request_header_t
{
    uint8_t ver;
};

struct socks5_logon_response_header_t
{
    uint8_t ver;
    uint8_t status;
};

struct socks5_connect_request_header_t
{
    uint8_t ver;
    uint8_t cmd;
    uint8_t rsv;
    uint8_t atyp;
    uint32_t addr;
    uint16_t port;
};

struct socks5_connect_response_header_t
{
    uint8_t ver;
    uint8_t rep;
    uint8_t rsv;
    uint8_t atyp;
    uint32_t addr;
    uint16_t port;
};

#pragma pack(pop)

int Socket::Connect(ConnectType type, const sockaddr* address, int addressLen, const sockaddr* proxyAddress, int proxyAddressLen, const char* userName, const char* password)
{
    switch (type)
    {
    case ConnectType_Direct:
        return connectDirect(address, addressLen);

    case ConnectType_ProxySocks4:
        return connectProxySocks4(address, addressLen, proxyAddress, proxyAddressLen, userName);

    case ConnectType_ProxySocks5:
        return connectProxySocks5(address, addressLen, proxyAddress, proxyAddressLen, userName, password);

    default:
        return -1;
    }
}


int Socket::connectDirect(const sockaddr* address, int addressLen)
{
    return ::connect(m_socket, address, addressLen);
}

int Socket::connectProxySocks4(const sockaddr* address, int addressLen, const sockaddr* proxyAddress, int proxyAddressLen, const char* userName) 
{
    int result = ::connect(m_socket, proxyAddress, proxyAddressLen);

    if (result < 0)
        return result;

    size_t user_name_size = strlen(userName) + 1;
    size_t socks_request_size = sizeof(socks4_request_header_t) + user_name_size;

    socks4_request_header_t* socks_request_header = (socks4_request_header_t*)malloc(socks_request_size);
    socks_request_header->vn = 4;
    socks_request_header->cd = 1;
    socks_request_header->dstport = ((const sockaddr_in*)address)->sin_port;
    socks_request_header->dstip = ((const sockaddr_in*)address)->sin_addr.S_un.S_addr;
    memcpy(socks_request_header + 1, userName, user_name_size);

    result = ::send(m_socket, (const char*)socks_request_header, (int)socks_request_size, 0);

    if (result < 0)
    {
        free(socks_request_header);
        return result;
    }

    free(socks_request_header);

    socks4_response_header_t socks_response_header;

    char* data = (char*)&socks_response_header;
    int len = sizeof(socks4_response_header_t);

    while (true)
    {
        result = ::recv(m_socket, data, len, 0);

        if (result < 0)
            return result;

        if (result == 0)
            return -1;

        data += result;
        len -= result;

        if (!len)
            break;
    }

    if (socks_response_header.vn != 0)
        return -1;

    if (socks_response_header.cd != 90)
        return -1;

    return 0;
}

int Socket::connectProxySocks5(const sockaddr* address, int addressLen, const sockaddr* proxyAddress, int proxyAddressLen, const char* userName, const char* password)
{
    int result = ::connect(m_socket, proxyAddress, proxyAddressLen);

    if (result < 0)
        return result;

    socks5_method_request_header_t socks5_method_request_header;
    socks5_method_request_header.ver = 0x05;
    socks5_method_request_header.methods = 1;
    socks5_method_request_header.method = 0x02;

    result = ::send(m_socket, (const char*)&socks5_method_request_header, sizeof(socks5_method_request_header_t), 0);

    if (result < 0)
        return result;

    socks5_method_response_header_t socks5_method_response_header;

    char* data = (char*)&socks5_method_response_header;
    int len = sizeof(socks5_method_response_header_t);

    while (true)
    {
        result = ::recv(m_socket, data, len, 0);

        if (result < 0)
            return result;

        if (result == 0)
            return -1;

        data += result;
        len -= result;

        if (!len)
            break;
    }

    if (socks5_method_response_header.ver != 0x05)
        return -1;

    if (socks5_method_response_header.method != 0x02)
        return -1;

    size_t userNameSize = strlen(userName);
    size_t passwordSize = strlen(password);
    size_t logonRequestSize = sizeof(socks5_logon_request_header_t) + 1 + userNameSize + 1 + passwordSize;

    socks5_logon_request_header_t* socks5_logon_request_header = (socks5_logon_request_header_t*) malloc(logonRequestSize);
    socks5_logon_request_header->ver = 0x01;
    data = (char*)(socks5_logon_request_header + 1);
    *data = userNameSize;
    data += 1;
    memcpy(data, userName, userNameSize);
    data += userNameSize;
    *data = passwordSize;
    data += 1;
    memcpy(data, password, passwordSize);
    data += passwordSize;

    result = ::send(m_socket, (const char*) socks5_logon_request_header, logonRequestSize, 0);

    if (result < 0)
    {
        free(socks5_logon_request_header);
        return result;
    }

    free(socks5_logon_request_header);

    socks5_logon_response_header_t socks5_logon_response_header;

    data = (char*) &socks5_logon_response_header;
    len = sizeof(socks5_logon_response_header_t);

    while (true)
    {
        result = ::recv(m_socket, data, len, 0);

        if (result < 0)
            return result;

        if (result == 0)
            return -1;

        data += result;
        len -= result;

        if (!len)
            break;
    }

    if (socks5_logon_response_header.ver != 0x01)
        return -1;

    if (socks5_logon_response_header.status != 0x00)
        return -1;

    socks5_connect_request_header_t socks5_connect_request_header;
    socks5_connect_request_header.ver = 0x05;
    socks5_connect_request_header.cmd = 0x01;
    socks5_connect_request_header.rsv = 0x00;
    socks5_connect_request_header.atyp = 0x01;
    socks5_connect_request_header.addr = ((const sockaddr_in*)address)->sin_addr.S_un.S_addr;
    socks5_connect_request_header.port = ((const sockaddr_in*)address)->sin_port;

    result = ::send(m_socket, (const char*)&socks5_connect_request_header, sizeof(socks5_connect_request_header_t), 0);

    if (result < 0)
        return result;

    socks5_connect_response_header_t socks5_connect_response_header;

    data = (char*)&socks5_connect_response_header;
    len = sizeof(socks5_connect_response_header_t);

    while (true)
    {
        result = ::recv(m_socket, data, len, 0);

        if (result < 0)
            return result;

        if (result == 0)
            return -1;

        data += result;
        len -= result;

        if (!len)
            break;
    }

    if (socks5_connect_response_header.ver != 0x05)
        return -1;

    if (socks5_connect_response_header.rep != 0x00)
        return -1;

    return 0;
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
