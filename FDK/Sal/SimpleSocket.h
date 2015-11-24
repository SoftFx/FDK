#ifndef __Sal_Simple_Socket__
#define __Sal_Simple_Socket__
#include "Socket.h"

class SimpleSocket : public Socket
{
public:
	SimpleSocket(SOCKET s, bool enabledStats);
	SimpleSocket(SOCKET s, bool isAccepting, bool enabledStats);
	virtual ~SimpleSocket();
public:
	virtual Socket* PhysicalAccept(struct sockaddr* addr, socklen_t* addrlen, const char* ceritificateFileName, const char* password);
	virtual HRESULT LogicalAccept();
	virtual int Connect(const struct sockaddr* name, int namelen);
	virtual int Send(const char* buf, int len, int flags);
	virtual int RecvFrom(char* buf, int len, int flags, struct sockaddr* from, socklen_t* fromlen);
	virtual int Recv(char* buf, int len, int flags);
	virtual int SendTo(const char* buf, int len, int flags, const struct sockaddr* to, int tolen);
	virtual bool Pending();
};
#endif
