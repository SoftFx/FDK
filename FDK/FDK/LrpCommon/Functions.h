#pragma once


bool EnableNonBlockingMode(SOCKET socket);
bool EnableKeepAlive(SOCKET socket, u_long keepalivetimeInMilliseconds = 10000, u_long keepaliveintervalInMilliseconds = 3000);
bool ResolveConnectionString(const string& address, const int port, sockaddr& addr);


HRESULT Read(SOCKET socket, MemoryBuffer& buffer);
HRESULT SelectRead(SOCKET socket, MemoryBuffer& buffer);
HRESULT SelectReadUsername(SOCKET socket, MemoryBuffer& buffer);
HRESULT SelectReadPassword(SOCKET socket, MemoryBuffer& buffer);


HRESULT Write(SOCKET socket, MemoryBuffer& buffer);
HRESULT SelectWrite(SOCKET socket, MemoryBuffer& buffer);




