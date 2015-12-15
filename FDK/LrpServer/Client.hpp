// This is always generated file. Do not change anything.

class Client
{
private:
	ILrpChannel* m_channel;
public:
	inline Client(ILrpChannel& channel) : m_channel(&channel)
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
	const static unsigned short LrpMethod_OnLogonMsg_Id = 2;
	bool Is_OnLogonMsg_Supported() const
	{
		return m_channel->IsSupported(0, 2);
	}
	void OnLogonMsg(const std::string& protocolVersion)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(protocolVersion, buffer);

		const HRESULT _status = m_channel->Invoke(0, 2, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnLogoutMsg_Id = 3;
	bool Is_OnLogoutMsg_Supported() const
	{
		return m_channel->IsSupported(0, 3);
	}
	void OnLogoutMsg(const FxLogoutReason& reason, const std::string& description)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteLogoutReason(reason, buffer);
		WriteAString(description, buffer);

		const HRESULT _status = m_channel->Invoke(0, 3, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnSessionInfoMsg_Id = 4;
	bool Is_OnSessionInfoMsg_Supported() const
	{
		return m_channel->IsSupported(0, 4);
	}
	void OnSessionInfoMsg(const std::string& requestId, const CFxSessionInfo& sessionInfo)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(requestId, buffer);
		WriteLrpSessionInfo(sessionInfo, buffer);

		const HRESULT _status = m_channel->Invoke(0, 4, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnSessionInfoMsg2_Id = 5;
	bool Is_OnSessionInfoMsg2_Supported() const
	{
		return m_channel->IsSupported(0, 5);
	}
	void OnSessionInfoMsg2(const std::string& requestId, const CFxSessionInfo& sessionInfo)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(requestId, buffer);
		WriteLrpSessionInfo2(sessionInfo, buffer);

		const HRESULT _status = m_channel->Invoke(0, 5, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnCurrenciesInfoMsg_Id = 6;
	bool Is_OnCurrenciesInfoMsg_Supported() const
	{
		return m_channel->IsSupported(0, 6);
	}
	void OnCurrenciesInfoMsg(const std::string& requestId, const std::vector<CFxCurrencyInfo>& currencies)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(requestId, buffer);
		WriteCurrencyInfoArray(currencies, buffer);

		const HRESULT _status = m_channel->Invoke(0, 6, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnSymbolsInfoMsg_Id = 7;
	bool Is_OnSymbolsInfoMsg_Supported() const
	{
		return m_channel->IsSupported(0, 7);
	}
	void OnSymbolsInfoMsg(const std::string& requestId, const std::vector<CFxSymbolInfo>& symbols)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(requestId, buffer);
		WriteSymbolInfoArray(symbols, buffer);

		const HRESULT _status = m_channel->Invoke(0, 7, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnSymbolsInfoMsg2_Id = 8;
	bool Is_OnSymbolsInfoMsg2_Supported() const
	{
		return m_channel->IsSupported(0, 8);
	}
	void OnSymbolsInfoMsg2(const std::string& requestId, const std::vector<CFxSymbolInfo>& symbols)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(requestId, buffer);
		WriteSymbolInfoArray2(symbols, buffer);

		const HRESULT _status = m_channel->Invoke(0, 8, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnSymbolsInfoMsg3_Id = 9;
	bool Is_OnSymbolsInfoMsg3_Supported() const
	{
		return m_channel->IsSupported(0, 9);
	}
	void OnSymbolsInfoMsg3(const std::string& requestId, const std::vector<CFxSymbolInfo>& symbols)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(requestId, buffer);
		WriteSymbolInfoArray3(symbols, buffer);

		const HRESULT _status = m_channel->Invoke(0, 9, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnSymbolsInfoMsg4_Id = 10;
	bool Is_OnSymbolsInfoMsg4_Supported() const
	{
		return m_channel->IsSupported(0, 10);
	}
	void OnSymbolsInfoMsg4(const std::string& requestId, const std::vector<CFxSymbolInfo>& symbols)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(requestId, buffer);
		WriteSymbolInfoArray4(symbols, buffer);

		const HRESULT _status = m_channel->Invoke(0, 10, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnSymbolsInfoMsg5_Id = 11;
	bool Is_OnSymbolsInfoMsg5_Supported() const
	{
		return m_channel->IsSupported(0, 11);
	}
	void OnSymbolsInfoMsg5(const std::string& requestId, const std::vector<CFxSymbolInfo>& symbols)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(requestId, buffer);
		WriteSymbolInfoArray5(symbols, buffer);

		const HRESULT _status = m_channel->Invoke(0, 11, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnSymbolsInfoMsg6_Id = 12;
	bool Is_OnSymbolsInfoMsg6_Supported() const
	{
		return m_channel->IsSupported(0, 12);
	}
	void OnSymbolsInfoMsg6(const std::string& requestId, const std::vector<CFxSymbolInfo>& symbols)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(requestId, buffer);
		WriteSymbolInfoArray6(symbols, buffer);

		const HRESULT _status = m_channel->Invoke(0, 12, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnSymbolsInfoMsg7_Id = 13;
	bool Is_OnSymbolsInfoMsg7_Supported() const
	{
		return m_channel->IsSupported(0, 13);
	}
	void OnSymbolsInfoMsg7(const std::string& requestId, const std::vector<CFxSymbolInfo>& symbols)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(requestId, buffer);
		WriteSymbolInfoArray7(symbols, buffer);

		const HRESULT _status = m_channel->Invoke(0, 13, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnComponentsInfoMsg_Id = 14;
	bool Is_OnComponentsInfoMsg_Supported() const
	{
		return m_channel->IsSupported(0, 14);
	}
	void OnComponentsInfoMsg(const std::string& requestId, const __int32& quotesStorageVersion)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(requestId, buffer);
		WriteInt32(quotesStorageVersion, buffer);

		const HRESULT _status = m_channel->Invoke(0, 14, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnQuotesSubscriptionMsg_Id = 15;
	bool Is_OnQuotesSubscriptionMsg_Supported() const
	{
		return m_channel->IsSupported(0, 15);
	}
	void OnQuotesSubscriptionMsg(const std::string& requestId, const __int32& status, const std::string& message)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(requestId, buffer);
		WriteInt32(status, buffer);
		WriteAString(message, buffer);

		const HRESULT _status = m_channel->Invoke(0, 15, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnFileChunkMsg_Id = 16;
	bool Is_OnFileChunkMsg_Supported() const
	{
		return m_channel->IsSupported(0, 16);
	}
	void OnFileChunkMsg(const std::string& requestId, const CFxFileChunk& chunk)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(requestId, buffer);
		WriteFileChunk(chunk, buffer);

		const HRESULT _status = m_channel->Invoke(0, 16, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnDataHistoryMsg_Id = 17;
	bool Is_OnDataHistoryMsg_Supported() const
	{
		return m_channel->IsSupported(0, 17);
	}
	void OnDataHistoryMsg(const std::string& requestId, const CFxDataHistoryResponse& response)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(requestId, buffer);
		WriteDataHistoryResponse(response, buffer);

		const HRESULT _status = m_channel->Invoke(0, 17, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnDataHistoryMetaInfoMsg_Id = 18;
	bool Is_OnDataHistoryMetaInfoMsg_Supported() const
	{
		return m_channel->IsSupported(0, 18);
	}
	void OnDataHistoryMetaInfoMsg(const std::string& requestId, const __int32& status, const std::string& field)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(requestId, buffer);
		WriteInt32(status, buffer);
		WriteAString(field, buffer);

		const HRESULT _status = m_channel->Invoke(0, 18, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnQuoteRawMsg_Id = 19;
	bool Is_OnQuoteRawMsg_Supported() const
	{
		return m_channel->IsSupported(0, 19);
	}
	void OnQuoteRawMsg(const MemoryBuffer& data)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteRaw(data, buffer);

		const HRESULT _status = m_channel->Invoke(0, 19, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnNotificationMsg_Id = 20;
	bool Is_OnNotificationMsg_Supported() const
	{
		return m_channel->IsSupported(0, 20);
	}
	void OnNotificationMsg(const CNotification& notification)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteNotification(notification, buffer);

		const HRESULT _status = m_channel->Invoke(0, 20, buffer);
		Throw(_status, buffer);

	}
};
