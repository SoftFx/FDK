// This is always generated file. Do not change anything.

class Server
{
private:
	ILrpChannel* m_channel;
public:
	inline Server(ILrpChannel& channel) : m_channel(&channel)
	{
	}
	const static unsigned short LrpComponentId = 0;
	bool IsSupported() const
	{
		return m_channel->IsSupported(0);
	}
	const static unsigned short LrpMethod_OnHeartBeatRequest_Id = 0;
	bool Is_OnHeartBeatRequest_Supported() const
	{
		return m_channel->IsSupported(0, 0);
	}
	void OnHeartBeatRequest()
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);


		const HRESULT _status = m_channel->Invoke(0, 0, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnHeartBeatResponse_Id = 1;
	bool Is_OnHeartBeatResponse_Supported() const
	{
		return m_channel->IsSupported(0, 1);
	}
	void OnHeartBeatResponse()
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);


		const HRESULT _status = m_channel->Invoke(0, 1, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnCurrenciesInfoRequest_Id = 2;
	bool Is_OnCurrenciesInfoRequest_Supported() const
	{
		return m_channel->IsSupported(0, 2);
	}
	void OnCurrenciesInfoRequest(const std::string& id)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(id, buffer);

		const HRESULT _status = m_channel->Invoke(0, 2, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnSymbolsInfoRequest_Id = 3;
	bool Is_OnSymbolsInfoRequest_Supported() const
	{
		return m_channel->IsSupported(0, 3);
	}
	void OnSymbolsInfoRequest(const std::string& id)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(id, buffer);

		const HRESULT _status = m_channel->Invoke(0, 3, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnTwoFactorAuthResponse_Id = 4;
	bool Is_OnTwoFactorAuthResponse_Supported() const
	{
		return m_channel->IsSupported(0, 4);
	}
	void OnTwoFactorAuthResponse(const FxTwoFactorReason& reason, const std::string& otp)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteTwoFactorReason(reason, buffer);
		WriteAString(otp, buffer);

		const HRESULT _status = m_channel->Invoke(0, 4, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnSessionInfoRequest_Id = 5;
	bool Is_OnSessionInfoRequest_Supported() const
	{
		return m_channel->IsSupported(0, 5);
	}
	void OnSessionInfoRequest(const std::string& id)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(id, buffer);

		const HRESULT _status = m_channel->Invoke(0, 5, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnSubscribeToQuotesRequest_Id = 6;
	bool Is_OnSubscribeToQuotesRequest_Supported() const
	{
		return m_channel->IsSupported(0, 6);
	}
	void OnSubscribeToQuotesRequest(const std::string& id, const std::vector<std::string>& symbols, const __int32& marketDepth)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(id, buffer);
		WriteAStringArray(symbols, buffer);
		WriteInt32(marketDepth, buffer);

		const HRESULT _status = m_channel->Invoke(0, 6, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnUnsubscribeQuotesRequest_Id = 7;
	bool Is_OnUnsubscribeQuotesRequest_Supported() const
	{
		return m_channel->IsSupported(0, 7);
	}
	void OnUnsubscribeQuotesRequest(const std::string& id, const std::vector<std::string>& symbols)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(id, buffer);
		WriteAStringArray(symbols, buffer);

		const HRESULT _status = m_channel->Invoke(0, 7, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnComponentsInfoRequest_Id = 8;
	bool Is_OnComponentsInfoRequest_Supported() const
	{
		return m_channel->IsSupported(0, 8);
	}
	void OnComponentsInfoRequest(const std::string& id, const __int32& clientVersion)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(id, buffer);
		WriteInt32(clientVersion, buffer);

		const HRESULT _status = m_channel->Invoke(0, 8, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnDataHistoryRequest_Id = 9;
	bool Is_OnDataHistoryRequest_Supported() const
	{
		return m_channel->IsSupported(0, 9);
	}
	void OnDataHistoryRequest(const std::string& id, const CFxDataHistoryRequest& request)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(id, buffer);
		WriteDataHistoryRequest(request, buffer);

		const HRESULT _status = m_channel->Invoke(0, 9, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnFileChunkRequest_Id = 10;
	bool Is_OnFileChunkRequest_Supported() const
	{
		return m_channel->IsSupported(0, 10);
	}
	void OnFileChunkRequest(const std::string& id, const std::string& fieldId, const unsigned __int32& chunkId)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(id, buffer);
		WriteAString(fieldId, buffer);
		WriteUInt32(chunkId, buffer);

		const HRESULT _status = m_channel->Invoke(0, 10, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnBarsHistoryMetaInfoFileRequest_Id = 11;
	bool Is_OnBarsHistoryMetaInfoFileRequest_Supported() const
	{
		return m_channel->IsSupported(0, 11);
	}
	void OnBarsHistoryMetaInfoFileRequest(const std::string& id, const std::string& symbol, const __int32& priceType, const std::string& period)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(id, buffer);
		WriteAString(symbol, buffer);
		WriteInt32(priceType, buffer);
		WriteAString(period, buffer);

		const HRESULT _status = m_channel->Invoke(0, 11, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnQuotesHistoryMetaInfoFileRequest_Id = 12;
	bool Is_OnQuotesHistoryMetaInfoFileRequest_Supported() const
	{
		return m_channel->IsSupported(0, 12);
	}
	void OnQuotesHistoryMetaInfoFileRequest(const std::string& id, const std::string& symbol, const bool& includeLevel2)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(id, buffer);
		WriteAString(symbol, buffer);
		WriteBoolean(includeLevel2, buffer);

		const HRESULT _status = m_channel->Invoke(0, 12, buffer);
		Throw(_status, buffer);

	}
};
