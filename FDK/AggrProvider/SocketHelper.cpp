#include "stdafx.h"
#include "SocketHelper.h"

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
bool SendBuffer(SOCKET socket, const vector<char>& buffer)
{
	const int count = static_cast<int>(buffer.size());
	bool result = SendBufferInternal(socket, sizeof(int), &count);
	if (result && (count > 0))
	{
		result = SendBufferInternal(socket, count, &buffer.front());
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
bool ReceiveBuffer(SOCKET socket, vector<char>& buffer)
{
	buffer.clear();
	int32 count = 0;
	bool result = ReceiveBufferInternal(socket, sizeof(int), &count);
	if ((!result) || (count < 0))
	{
		return false;
	}
	if (0 == count)
	{
		return true;
	}
	buffer.insert(buffer.end(), static_cast<size_t>(count), 0);
	result = ReceiveBufferInternal(socket, count, &buffer.front());
	return result;
}
	
bool EnableKeepAlive(SOCKET socket, u_long keepalivetimeInMilliseconds /*= 10000*/, u_long keepaliveintervalInMilliseconds /*= 3000*/)
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
