#pragma once
#include "Timeout.h"


bool EnableKeepAlive(SOCKET socket, u_long keepalivetimeInMilliseconds = 10000, u_long keepaliveintervalInMilliseconds = 3000);


bool SendEx(SOCKET socket, CTimeout timeout, const void* data, size_t size);
bool SendEx(SOCKET socket, CTimeout timeout, const string& text);
bool SendEx(SOCKET socket, const char* text);

bool ReceiveEx(SOCKET socket, CTimeout timeout, void* data, size_t size);
bool ReceiveEx(SOCKET socket, CTimeout timeout, const size_t maximumLength, string& text);
bool ReceiveEx(SOCKET socket, CTimeout timeout, string& text);
bool ReceiveEx(SOCKET socket, CTimeout timeout, MemoryBuffer& buffer);

bool ResolveConnectionString(const string& address, const int port, sockaddr& addr);
bool IsSocketConnected(SOCKET s);
