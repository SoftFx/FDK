// This is always generated file. Do not change anything.

class LocalServerHandler
{
private:
	CLrpLocalClient* m_channel;
public:
	inline LocalServerHandler() : m_channel()
	{
	}
	inline LocalServerHandler(CLrpLocalClient& client) : m_channel(&client)
	{
	}
	bool IsSupported() const
	{
		return m_channel->IsSupported(0);
	}
	const static unsigned short LrpMethod_BeginNewConnectionRequest_Id = 0;
	bool Is_BeginNewConnectionRequest_Supported() const
	{
		return m_channel->IsSupported(0, 0);
	}
	void BeginNewConnectionRequest(void* handle, const __int64& id, const std::string& address, const __int32& port)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteLocalPointer(handle, buffer);
		WriteInt64(id, buffer);
		WriteAString(address, buffer);
		WriteInt32(port, buffer);

		const HRESULT _status = m_channel->Invoke(0, 0, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_BeginShutdownConnectionNotification_Id = 1;
	bool Is_BeginShutdownConnectionNotification_Supported() const
	{
		return m_channel->IsSupported(0, 1);
	}
	void BeginShutdownConnectionNotification(void* handle, const __int64& id)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteLocalPointer(handle, buffer);
		WriteInt64(id, buffer);

		const HRESULT _status = m_channel->Invoke(0, 1, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_BeginLogonRequest_Id = 2;
	bool Is_BeginLogonRequest_Supported() const
	{
		return m_channel->IsSupported(0, 2);
	}
	void BeginLogonRequest(void* handle, const __int64& id, const std::string& address, const __int32& port, const std::string& username, const std::string& password)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteLocalPointer(handle, buffer);
		WriteInt64(id, buffer);
		WriteAString(address, buffer);
		WriteInt32(port, buffer);
		WriteAString(username, buffer);
		WriteAString(password, buffer);

		const HRESULT _status = m_channel->Invoke(0, 2, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_BeginLogoutRequest_Id = 3;
	bool Is_BeginLogoutRequest_Supported() const
	{
		return m_channel->IsSupported(0, 3);
	}
	void BeginLogoutRequest(void* handle, const __int64& id)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteLocalPointer(handle, buffer);
		WriteInt64(id, buffer);

		const HRESULT _status = m_channel->Invoke(0, 3, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_BeginCurrenciesInfoRequest_Id = 4;
	bool Is_BeginCurrenciesInfoRequest_Supported() const
	{
		return m_channel->IsSupported(0, 4);
	}
	void BeginCurrenciesInfoRequest(void* handle, const __int64& id, const std::string& requestId)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteLocalPointer(handle, buffer);
		WriteInt64(id, buffer);
		WriteAString(requestId, buffer);

		const HRESULT _status = m_channel->Invoke(0, 4, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_BeginSymbolsInfoRequest_Id = 5;
	bool Is_BeginSymbolsInfoRequest_Supported() const
	{
		return m_channel->IsSupported(0, 5);
	}
	void BeginSymbolsInfoRequest(void* handle, const __int64& id, const std::string& requestId)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteLocalPointer(handle, buffer);
		WriteInt64(id, buffer);
		WriteAString(requestId, buffer);

		const HRESULT _status = m_channel->Invoke(0, 5, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_BeginSessionInfoRequest_Id = 6;
	bool Is_BeginSessionInfoRequest_Supported() const
	{
		return m_channel->IsSupported(0, 6);
	}
	void BeginSessionInfoRequest(void* handle, const __int64& id, const std::string& requestId)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteLocalPointer(handle, buffer);
		WriteInt64(id, buffer);
		WriteAString(requestId, buffer);

		const HRESULT _status = m_channel->Invoke(0, 6, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_BeginSubscribeToQuotesRequest_Id = 7;
	bool Is_BeginSubscribeToQuotesRequest_Supported() const
	{
		return m_channel->IsSupported(0, 7);
	}
	void BeginSubscribeToQuotesRequest(void* handle, const __int64& id, const std::string& requestId, const std::vector<std::string>& symbols, const __int32& depth)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteLocalPointer(handle, buffer);
		WriteInt64(id, buffer);
		WriteAString(requestId, buffer);
		WriteAStringArray(symbols, buffer);
		WriteInt32(depth, buffer);

		const HRESULT _status = m_channel->Invoke(0, 7, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_BeginUnsubscribeQuotesRequest_Id = 8;
	bool Is_BeginUnsubscribeQuotesRequest_Supported() const
	{
		return m_channel->IsSupported(0, 8);
	}
	void BeginUnsubscribeQuotesRequest(void* handle, const __int64& id, const std::string& requestId, const std::vector<std::string>& symbols)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteLocalPointer(handle, buffer);
		WriteInt64(id, buffer);
		WriteAString(requestId, buffer);
		WriteAStringArray(symbols, buffer);

		const HRESULT _status = m_channel->Invoke(0, 8, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_BeginComponentsInfoRequest_Id = 9;
	bool Is_BeginComponentsInfoRequest_Supported() const
	{
		return m_channel->IsSupported(0, 9);
	}
	void BeginComponentsInfoRequest(void* handle, const __int64& id, const std::string& requestId, const __int32& clientVersion)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteLocalPointer(handle, buffer);
		WriteInt64(id, buffer);
		WriteAString(requestId, buffer);
		WriteInt32(clientVersion, buffer);

		const HRESULT _status = m_channel->Invoke(0, 9, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_BeginDataHistoryRequest_Id = 10;
	bool Is_BeginDataHistoryRequest_Supported() const
	{
		return m_channel->IsSupported(0, 10);
	}
	void BeginDataHistoryRequest(void* handle, const __int64& id, const std::string& requestId, const CFxDataHistoryRequest& request)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteLocalPointer(handle, buffer);
		WriteInt64(id, buffer);
		WriteAString(requestId, buffer);
		WriteDataHistoryRequest(request, buffer);

		const HRESULT _status = m_channel->Invoke(0, 10, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_BeginFileChunkRequest_Id = 11;
	bool Is_BeginFileChunkRequest_Supported() const
	{
		return m_channel->IsSupported(0, 11);
	}
	void BeginFileChunkRequest(void* handle, const __int64& id, const std::string& requestId, const std::string& fieldId, const unsigned __int32& chunkId)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteLocalPointer(handle, buffer);
		WriteInt64(id, buffer);
		WriteAString(requestId, buffer);
		WriteAString(fieldId, buffer);
		WriteUInt32(chunkId, buffer);

		const HRESULT _status = m_channel->Invoke(0, 11, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_BeginBarsHistoryMetaInfoFileRequest_Id = 12;
	bool Is_BeginBarsHistoryMetaInfoFileRequest_Supported() const
	{
		return m_channel->IsSupported(0, 12);
	}
	void BeginBarsHistoryMetaInfoFileRequest(void* handle, const __int64& id, const std::string& requestId, const std::string& symbol, const __int32& priceType, const std::string& period)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteLocalPointer(handle, buffer);
		WriteInt64(id, buffer);
		WriteAString(requestId, buffer);
		WriteAString(symbol, buffer);
		WriteInt32(priceType, buffer);
		WriteAString(period, buffer);

		const HRESULT _status = m_channel->Invoke(0, 12, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_BeginQuotesHistoryMetaInfoFileRequest_Id = 13;
	bool Is_BeginQuotesHistoryMetaInfoFileRequest_Supported() const
	{
		return m_channel->IsSupported(0, 13);
	}
	void BeginQuotesHistoryMetaInfoFileRequest(void* handle, const __int64& id, const std::string& requestId, const std::string& symbol, const bool& includeLevel2)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteLocalPointer(handle, buffer);
		WriteInt64(id, buffer);
		WriteAString(requestId, buffer);
		WriteAString(symbol, buffer);
		WriteBoolean(includeLevel2, buffer);

		const HRESULT _status = m_channel->Invoke(0, 13, buffer);
		Throw(_status, buffer);

	}
};
