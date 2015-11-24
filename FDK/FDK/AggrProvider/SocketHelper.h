#pragma once

bool SendBuffer(SOCKET socket, const vector<char>& buffer);
bool ReceiveBuffer(SOCKET socket, vector<char>& buffer);
void AsynchCloseSocketSafely(HANDLE thread, SOCKET socket);
bool EnableKeepAlive(SOCKET socket, u_long keepalivetimeInMilliseconds = 10000, u_long keepaliveintervalInMilliseconds = 3000);
