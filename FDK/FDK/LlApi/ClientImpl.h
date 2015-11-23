#pragma once



class CClientImpl
{
public:
	bool Start(void* handle);
	bool WaitForLogon(void* handle, size_t timeoutInMilliseconds);
	HRESULT Shutdown(void* handle);
	HRESULT Stop(void* handle);
	string NextId(void* handle);
	string GetProtocolVersion(void* handle);
	HRESULT GetNextMessage(void* handle, int& type, void*& data);
	void GetNetworkActivity(void* handle, uint64& dataBytesSent, uint64& sslBytesSent, uint64& dataBytesReceived, uint64& sslBytesReceived);
	CFxSessionInfo GetSessionInfo(void* handle, size_t timeoutInMilliseconds);
	CFxFileChunk GetFileChunk(void* handle, const string& fileId, uint32 chunkId, const size_t timeoutInMilliseconds);
	bool GetNextMessage(void* handle, CFxMessage& message);
	void DispatchMessage(void* handle, const CFxMessage& message);
};


