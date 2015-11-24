#include "stdafx.h"
#include "Functions.h"
#include "MemoryBuffer.h"


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
bool SendEx(SOCKET socket, CTimeout timeout, const void* data, size_t size)
{
	const char* buffer = reinterpret_cast<const char*>(data);
	for (; size > 0; )
	{
		fd_set set;
		FD_ZERO(&set);
		FD_SET(socket, &set);

		timeval t = timeout.ToTimeValue();
		int status = select(1, nullptr, &set, nullptr, &t);
		if (1 != status)
		{
			return false;
		}
		status = send(socket, buffer, static_cast<int>(size), 0);
		if (SOCKET_ERROR == status)
		{
			return false;
		}
		buffer += status;
		size -= status;
	}
	return true;
}
bool SendEx(SOCKET socket, CTimeout timeout, const string& text)
{
	const uint32 size = static_cast<uint32>(text.length());
	if (!SendEx(socket, timeout, &size, sizeof(size)))
	{
		return false;
	}
	return SendEx(socket, timeout, text.c_str(), size);
}
bool SendEx(SOCKET socket, CTimeout timeout, HRESULT status)
{
	return SendEx(socket, timeout, &status, sizeof(status));
}
bool ReceiveEx(SOCKET socket, CTimeout timeout, void* data, size_t size)
{
	char* buffer = reinterpret_cast<char*>(data);
	for (; size > 0; )
	{
		fd_set set;
		FD_ZERO(&set);
		FD_SET(socket, &set);

		timeval t = timeout.ToTimeValue();
		int status = select(1, &set, nullptr, nullptr, &t);
		if (1 != status)
		{
			return false;
		}
		status = recv(socket, buffer, static_cast<int>(size), 0);
		if ((SOCKET_ERROR == status) || (0 == status))
		{
			return false;
		}
		buffer += status;
		size -= status;
	}
	return true;
}
bool ReceiveEx(SOCKET socket, CTimeout timeout, const size_t maximumLength, string& text)
{
	uint32 size = 0;
	if (!ReceiveEx(socket, timeout, &size, sizeof(size)))
	{
		return false;
	}
	if (size > maximumLength)
	{
		return false;
	}
	text.clear();
	text.insert(text.end(), size, '\0');
	return ReceiveEx(socket, timeout, const_cast<char*>(text.data()), size);
}
bool ReceiveEx(SOCKET socket, CTimeout timeout, string& text)
{
	uint32 size = 0;
	if (!ReceiveEx(socket, timeout, &size, sizeof(size)))
	{
		return false;
	}
	text.clear();
	text.insert(text.end(), size, '\0');
	return ReceiveEx(socket, timeout, const_cast<char*>(text.data()), size);
}
bool ReceiveEx(SOCKET socket, CTimeout timeout, MemoryBuffer& data)
{
	FD_SET set;
	FD_ZERO(&set);
	FD_SET(socket, &set);

	data.Reset();

	uint32 length = static_cast<uint32>(data.GetCapacity());
	char* buffer = reinterpret_cast<char*>(data.GetData());

	size_t total = 0;
	for (; total < 4;)
	{
		timeval interval = timeout.ToTimeValue();

		int status = select(1, &set, nullptr, nullptr, &interval);
		if (0 == status)
		{
			return false;
		}
		status = recv(socket, buffer, length, 0);
		if (status <= 0)
		{
			return false;
		}
		buffer += status;
		total += status;
		length -= status;
	}
	data.Construct(total);
	size_t size = ReadInt32(data);
	data.Construct(sizeof(uint32) + size);

	length = static_cast<uint32>(size - total + sizeof(uint32)); // remaining data length

	if (length < 0) // we read more than expected
	{
		return false;
	}
	if (0 == length) // we read all data
	{
		return true;
	}

	buffer = reinterpret_cast<char*>(data.GetData());
	buffer += total;
	for (; length > 0; )
	{
		timeval interval = timeout.ToTimeValue();

		int status = select(1, &set, nullptr, nullptr, &interval);
		if (0 == status)
		{
			return false;
		}
		status = recv(socket, buffer, length, 0);
		if (status <= 0)
		{
			return false;
		}
		buffer += status;
		length -= status;
	}
	return true;
}
namespace
{
	bool SendBufferInternal(SOCKET socket, int count, const void* buffer)
	{
		const char* data = reinterpret_cast<const char*>(buffer);
		while(count > 0)
		{
			int status = send(socket, data, count, 0);
			if (SOCKET_ERROR == status)
			{
				return false;
			}
			data += status;
			count -= status;
		}
		return true;
	}
}
bool SendEx(SOCKET socket, const char* text)
{
	const int count = static_cast<int>(strlen(text));
	bool result = SendBufferInternal(socket, sizeof(int), &count);
	if (result)
	{
		result = SendBufferInternal(socket, count, text);
	}
	return result;
}
namespace
{
	bool ReceiveBufferInternal(SOCKET socket, int count, void* buffer)
	{
		char* data = reinterpret_cast<char*>(buffer);
		while(count > 0)
		{
			int status = recv(socket, data, count, 0);
			if (status <= 0)
			{
				return false;
			}
			data += status;
			count -= status;
		}
		return true;
	}
}
bool ReceiveString16(SOCKET socket, CTimeout /*timeout*/, string& text)
{
	text.clear();
	int count = 0;
	bool result = ReceiveBufferInternal(socket, sizeof(int), &count);
	if ((!result) || (count < 0) || (count > numeric_limits<unsigned short>::max()))
	{
		return false;
	}
	if (0 == count)
	{
		return true;
	}
	text.insert(text.end(), static_cast<size_t>(count), 0);
	result = ReceiveBufferInternal(socket, count, &text.front());
	return result;
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
bool IsSocketConnected(SOCKET s)
{
	fd_set read;
	FD_ZERO(&read);
	FD_SET(s, &read);

	fd_set write;
	FD_ZERO(&write);
	FD_SET(s, &write);


	timeval timeout;
	timeout.tv_sec = 0;
	timeout.tv_usec = 0;

	const int status = select(1, &read, &write, nullptr, &timeout);
	return (status > 0);
}
