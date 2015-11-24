// This is always generated file. Do not change anything.

class SimpleCodec
{
private:
	ILrpChannel* m_channel;
public:
	inline SimpleCodec(ILrpChannel& channel) : m_channel(&channel)
	{
	}
	const static unsigned short LrpComponentId = 1;
	bool IsSupported() const
	{
		return m_channel->IsSupported(1);
	}
	const static unsigned short LrpMethod_OnSymbolIndexMsg_Id = 0;
	bool Is_OnSymbolIndexMsg_Supported() const
	{
		return m_channel->IsSupported(1, 0);
	}
	void OnSymbolIndexMsg(const std::string& symbol, const unsigned __int32& index)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(symbol, buffer);
		WriteUInt32(index, buffer);

		const HRESULT _status = m_channel->Invoke(1, 0, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnTimeSynchMsg_Id = 1;
	bool Is_OnTimeSynchMsg_Supported() const
	{
		return m_channel->IsSupported(1, 1);
	}
	void OnTimeSynchMsg(const CDateTime& time)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteTime(time, buffer);

		const HRESULT _status = m_channel->Invoke(1, 1, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnSymbol8Time32Level2Msg_Id = 2;
	bool Is_OnSymbol8Time32Level2Msg_Supported() const
	{
		return m_channel->IsSupported(1, 2);
	}
	void OnSymbol8Time32Level2Msg(const unsigned __int8& symbolIndex, const __int32& timeOffset, const MemoryBuffer& data)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteUInt8(symbolIndex, buffer);
		WriteInt32(timeOffset, buffer);
		WriteRaw(data, buffer);

		const HRESULT _status = m_channel->Invoke(1, 2, buffer);
		Throw(_status, buffer);

	}
	const static unsigned short LrpMethod_OnQuoteZipRawMsg_Id = 3;
	bool Is_OnQuoteZipRawMsg_Supported() const
	{
		return m_channel->IsSupported(1, 3);
	}
	void OnQuoteZipRawMsg(const MemoryBuffer& data)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteRaw(data, buffer);

		const HRESULT _status = m_channel->Invoke(1, 3, buffer);
		Throw(_status, buffer);

	}
};
