#ifndef __Sal_Secure_Socket__
#define __Sal_Secure_Socket__
#include "Socket.h"



class SecureSocket : public Socket
{
public:
	SecureSocket(SOCKET s, int af, int type, int protocol, bool enabledStats);
	virtual ~SecureSocket();
private:
	SecureSocket(bool enabledStats);
	bool Initialize(SOCKET s, const char* ceritificateFileName, const char* password);
public:
	virtual Socket* PhysicalAccept(struct sockaddr* addr, socklen_t* addrlen, const char* ceritificateFileName, const char* password);
	virtual HRESULT LogicalAccept();
    virtual int Connect(ConnectType type, const sockaddr* address, int addressLen, const sockaddr* proxyAddress, int proxyAddressLen, const char* userName, const char* password);
	virtual int Send(const char* buf, int len, int flags);
	virtual int RecvFrom(char* buf, int len, int flags, struct sockaddr* from, socklen_t* fromlen);
	virtual int Recv(char* buf, int len, int flags );
	virtual int SendTo(const char* buf, int len, int flags, const struct sockaddr* to, int tolen);
	virtual bool Pending();
private:
	int DoConnect(ConnectType type, const sockaddr* address, int addressLen, const sockaddr* proxyAddress, int proxyAddressLen, const char* userName, const char* password, int& timeout);
	int DoPhysicalConnect(ConnectType type, const sockaddr* address, int addressLen, const sockaddr* proxyAddress, int proxyAddressLen, const char* userName, const char* password, int& timeout);
	bool DoReinitialize();
private:
	int m_af;
	int m_type;
	int m_protocol;
private:
	const SSL_METHOD* m_method;
	SSL_CTX* m_context;
	SSL* m_ssl;
	BIO* m_bio;
};
#endif
