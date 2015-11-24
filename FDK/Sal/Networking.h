#ifndef __Sal_Networking__
#define __Sal_Networking__


#define FX_SOCKET_MODE_SIMPLE  0
#define FX_SOCKET_MODE_SECURE  1
#define FX_SOCKET_ENABLE_STATS 2

#include "SocketState.h"


extern "C"
{
	SOCKET FxAccept(SOCKET s, struct sockaddr* addr, socklen_t* addrlen, const char* ceritificateFileName = nullptr, const char* password = nullptr);  // implemented
	SOCKET FxPhysicalAccept(SOCKET s, struct sockaddr* addr, socklen_t* addrlen, const char* ceritificateFileName = nullptr, const char* password = nullptr);  // implemented
	HRESULT FxLogicalAccept(SOCKET s);  // implemented
	int FxBind(SOCKET s, const struct sockaddr* name, int namelen); // implemented
	int FxCloseSocket(SOCKET s); // implemented
	int FxConnect(SOCKET s, const struct sockaddr* name, int namelen); // implemented
	int FxGetPeerName(SOCKET s, struct sockaddr* name, socklen_t* namelen); // implemented
	int FxGetSockName(SOCKET s, struct sockaddr* name, socklen_t* namelen); // implemented
	int FxGetSockOpt(SOCKET s, int level, int optname, char* optval, socklen_t* optlen); // implemented
	int FxListen(SOCKET s, int backlog); // implemented
	int FxRecv(SOCKET s, char* buf, int len, int flags);
	int FxRecvFrom(SOCKET s, char* buf, int len, int flags, struct sockaddr* from, socklen_t* fromlen);
	int FxSelect(int nfds, fd_set* readfds, fd_set* writefds, fd_set* exceptfds, const struct timeval* timeout);
	int FxSend(SOCKET s, const char* buf, int len, int flags);
	int FxSendTo(SOCKET s, const char* buf, int len, int flags, const struct sockaddr* to, int tolen);
	int FxSetSockOpt(SOCKET s, int level, int optname, const sockoptval_t* optval, int optlen);
	int FxShutdown(SOCKET s, int how); // implemented
	SOCKET FxSocket(int af, int type, int protocol, int mode); // implemented
	int FxGetSocketActivity(SOCKET s, uint64* pDataBytesSent, uint64* pSslBytesSent, uint64* pDataBytesReceived, uint64* pSslBytesReceived);
	int FxIoctlsocket(SOCKET s, long cmd, u_long FAR* argp);
}


SAL_API bool FxCanRead(SOCKET s);
SAL_API bool FxCanWrite(SOCKET s);
SAL_API CSocketState FxCanReadWrite(SOCKET s);
SAL_API bool FxEnableKeepAlive(SOCKET s, u_long keepalivetimeInMilliseconds = 10000, u_long keepaliveintervalInMilliseconds = 3000);



#ifdef FX_OVERRIDE_WINSOCKS
#define accept FxAccept
#define bind FxBind
#define closesocket FxCloseSocket
#define connect FxConnect
#define getpeername FxGetPeerName
#define getsockname FxGetSockName
#define getsockopt FxGetSockOpt
#define listen FxListen
#define recv FxRecv
#define recvfrom FxRecvFrom
#define select FxSelect
#define send FxSend
#define sendto FxSendTo
#define setsockopt FxSetSockOpt
#define shutdown FxShutdown
#define socket FxSocket
#define ioctlsocket FxIoctlsocket
#endif
#endif
