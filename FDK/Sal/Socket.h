#ifndef __Sal_Socket__
#define __Sal_Socket__
#ifndef __Sal_Critical_Section__
#include "CriticalSection.h"
#endif
#include "SocketState.h"
#include "ConnectType.h"

class Socket
{
public:
	Socket(SOCKET s, bool enabledStats);
	virtual ~Socket();
public:
	static void EnableKeepAlive(SOCKET socket, u_long keepalivetimeInMilliseconds = 10000, u_long keepaliveintervalInMilliseconds = 3000);
public:
	int Shutdown(int how);
	int Ioctlsocket(long cmd, u_long FAR* argp);
	int Bind(const struct sockaddr* name, int namelen);
	int Listen(int backlog);
	int GetPeerName(struct sockaddr* name, socklen_t* namelen);
	int GetSockName(struct sockaddr* name, socklen_t* namelen);
	int GetSockOpt(int level, int optname, char* optval, socklen_t* optlen);
	int SetSockOpt(int level, int optname, const sockoptval_t* optval, int optlen);    
	SOCKET Handle();
public:
	void GetActivity(uint64* pDataBytesSent, uint64* pSslBytesSent, uint64* pDataBytesReceived, uint64* pSslBytesReceived)const;
public:
	virtual Socket* PhysicalAccept(struct sockaddr* addr, socklen_t* addrlen, const char* ceritificateFileName, const char* password) = 0;
	virtual HRESULT LogicalAccept() = 0;
	virtual int Connect(ConnectType type, const sockaddr* address, int addressLen, const sockaddr* proxyAddress, int proxyAddressLen, const char* userName, const char* password);
	virtual int Recv(char* buf, int len, int flags) = 0;
	virtual int RecvFrom(char* buf, int len, int flags, struct sockaddr* from, socklen_t* fromlen) = 0;
	virtual int Send(const char* buf, int len, int flags) = 0;
	virtual int SendTo(const char* buf, int len, int flags, const struct sockaddr* to, int tolen) = 0;
	virtual bool Pending() = 0;
public:
	bool CanRead();
	bool CanWrite();
	CSocketState CanReadWrite();
	bool EnableKeepAlive(u_long keepalivetimeInMilliseconds, u_long keepaliveintervalInMilliseconds);
protected:
	void Finalize();
protected:
	void OnSent(int logicalBytesSent);
	void OnSent(int logicalBytesSent, int physicalBytesSent, int physicalBytesReceived);
	void OnReceived(int logicalBytesReceived);
	void OnReceived(int logicalBytesReceived, int physicalBytesSent, int physicalBytesReceived);
	void SetSocket(SOCKET newSocket);
	bool EnabledStats() const;
private:
	Socket(const Socket&);
	Socket& operator = (const Socket&);
protected:
	SOCKET m_socket;
	bool m_isAccepting;
private:
    int connectDirect(const sockaddr* address, int addressLen);
    int connectProxySocks4(const sockaddr* address, int addressLen, const sockaddr* proxyAddress, int proxyAddressLen, const char* userName);
    int connectProxySocks5(const sockaddr* address, int addressLen, const sockaddr* proxyAddress, int proxyAddressLen, const char* userName, const char* password);

	const bool m_enabledStats;
	atomic<LONGLONG> m_dataBytesSent;
	atomic<LONGLONG> m_sslBytesSent;
	atomic<LONGLONG> m_dataBytesReceived;
	atomic<LONGLONG> m_sslBytesReceived;
};
#endif
