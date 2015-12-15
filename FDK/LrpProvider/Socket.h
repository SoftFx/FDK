#pragma once


class CSocket
{
public:
	CSocket(bool secureConnection);
    CSocket(bool secureConnection, bool enableStats);
public:
	~CSocket();
private:
	CSocket(const CSocket&);
	CSocket& operator = (const CSocket&);
private:
	bool DoConstruct(SOCKET& acceptor);
public:
	bool Connect(const string& address, const int port);
	void EnableNonBlockingMode();
	void EnableKeepAlive();
	void GetActivity(uint64* pLogicalBytesSent, uint64* pPhysicalBytesSent, uint64* pLogicalBytesReceived, uint64* pPhysicalBytesReceived);
public:
	bool CanRead() const;
	bool CanWrite() const;
public:
	HRESULT Write(MemoryBuffer& buffer);
	bool Write(CTimeout timeout, MemoryBuffer& buffer);
	HRESULT Read(MemoryBuffer& buffer);
	bool Read(CTimeout timeout, MemoryBuffer& buffer);
private:
	bool DoRead(CTimeout timeout, MemoryBuffer& buffer);
	HRESULT DoReadSize(MemoryBuffer& buffer);
public:
	bool WaitForRead(DWORD timeoutInMs = INFINITE);
	bool WaitForWrite(DWORD timeoutInMs = INFINITE);
	CSocketState WaitForReadWrite(DWORD timeoutInMs = INFINITE);
	void WakeUp();
private:
	void Reset();
private:
	const bool m_secureConnection;
    const bool m_enableStats;
private:
	bool m_wokeUp;
	SOCKET m_socket;
	SOCKET m_server;
	SOCKET m_client;
	CCriticalSection m_synchronizer;
};