#pragma once


class CTransport
{
public:
	CTransport(SOCKET socket);
	~CTransport();
private:
	CTransport(const CTransport&);
	CTransport& operator = (const CTransport&);
private:
	void ResolveAddress();
public:
	HRESULT LogicalAccept();
	void Finalize();
public: // state
	bool CanRead() const;
	bool CanWrite() const;
	CSocketState CanReadWrite() const;
public: // write
	HRESULT SelectWrite(MemoryBuffer& buffer);
	HRESULT Write(MemoryBuffer& buffer);
public: // read
	HRESULT Read(MemoryBuffer& buffer);
public:
	const string& GetAddress() const;
private:
	SOCKET m_socket;
	string m_address;
};
