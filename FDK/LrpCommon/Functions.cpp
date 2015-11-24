#include "stdafx.h"
#include "Functions.h"


namespace
{
	timeval gZeroTimeout = { 0, 0 };
}


bool EnableNonBlockingMode(SOCKET socket)
{
	u_long mode = 1;
	const int status = ioctlsocket(socket, FIONBIO, &mode);
	const bool result = (0 == status);
	return result;
}
bool EnableKeepAlive(SOCKET socket, u_long keepalivetimeInMilliseconds /* = 10000 */, u_long keepaliveintervalInMilliseconds /* = 3000 */)
{
	DWORD size = 0;
	tcp_keepalive keepAlive = {1, keepalivetimeInMilliseconds, keepaliveintervalInMilliseconds};
	const int status = WSAIoctl(socket, SIO_KEEPALIVE_VALS, &keepAlive, sizeof(keepAlive), NULL, 0, &size, NULL, NULL);
	if (SOCKET_ERROR == status)
	{
		return false;
	}
	return true;
}
HRESULT Read(SOCKET socket, MemoryBuffer& buffer)
{
	const size_t position = buffer.GetPosition();
	const size_t size = buffer.GetSize();

	int length = static_cast<int>(size - position);

	char* data = reinterpret_cast<char*>(buffer.GetData()) + position;

	const int status = recv(socket, data, length, 0);

	if (status <= 0)
	{
		const DWORD code = WSAGetLastError();
		const HRESULT result = (WSAEWOULDBLOCK == code) ? S_FALSE : E_FAIL;
		return result;
	}
	length -= status;
	buffer.SetPosition(position + status);

	if (length > 0)
	{
		return S_FALSE;
	}
	return S_OK;
}
HRESULT SelectRead(SOCKET socket, MemoryBuffer& buffer)
{
	if (!FxCanRead(socket))
	{
		return S_FALSE;
	}
	return Read(socket, buffer);
}
HRESULT Write(SOCKET socket, MemoryBuffer& buffer)
{
	const size_t position = buffer.GetPosition();
	const size_t size = buffer.GetSize();

	int length = static_cast<int>(size - position);

	const char* data = reinterpret_cast<const char*>(buffer.GetData()) + position;

	const int status = send(socket, data, length, 0);

	if (status < 0)
	{
		return E_FAIL;
	}
	length -= status;
	buffer.SetPosition(position + status);

	if (length > 0)
	{
		return S_FALSE;
	}
	return S_OK;
}
HRESULT SelectWrite(SOCKET socket, MemoryBuffer& buffer)
{
	if (!FxCanWrite(socket))
	{
		return S_FALSE;
	}
	return Write(socket, buffer);
}
bool ResolveConnectionString(const string& address, const int port, sockaddr& addr)
{
	char buffer[1 + 8 * sizeof(int)] = "";
	_itoa_s(port, buffer, 10);

	addrinfo hints;
	ZeroMemory(&hints, sizeof(hints));

	hints.ai_family = AF_INET;
	hints.ai_socktype = SOCK_STREAM;
	hints.ai_protocol = IPPROTO_TCP;

	addrinfo* list = nullptr;
	const int status = getaddrinfo(address.c_str(), buffer, &hints, &list);
	if ((0 != status) || (nullptr == list->ai_addr))
	{
		return false;
	}
	addr = *(list->ai_addr);
	freeaddrinfo(list);
	return true;
}
