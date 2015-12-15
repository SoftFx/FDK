#pragma once



void PrintUsage();
bool Parse(int argc, char** argv, size_t& count);
void InitializeSocketsLibrary();
bool EnableNonBlockingMode(SOCKET socket);
SOCKET CreateAcceptor(int port, int mode, int backlog);
SOCKET Connect(SOCKET client, SOCKET acceptor);
