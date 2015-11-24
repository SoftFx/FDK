// This is always generated file. Do not change anything.

class FTrade
{
private:
	ILrpChannel* m_channel;
public:
	inline FTrade(ILrpChannel& channel) : m_channel(&channel)
	{
	}
	const static unsigned short LrpComponentId = 0;
	bool IsSupported() const
	{
		return m_channel->IsSupported(0);
	}
	const static unsigned short LrpMethod_Initialize_Id = 0;
	bool Is_Initialize_Supported() const
	{
		return m_channel->IsSupported(0, 0);
	}
	__int32 Initialize(const __int32& bankCode, const __int32& metaAccount, const std::string& password)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteInt32(bankCode, buffer);
		WriteInt32(metaAccount, buffer);
		WriteAString(password, buffer);

		const HRESULT _status = m_channel->Invoke(0, 0, buffer);
		Throw(_status, buffer);

		auto _result = ReadInt32(buffer);
		return _result;
	}
	const static unsigned short LrpMethod_ExecuteIOC_Id = 1;
	bool Is_ExecuteIOC_Supported() const
	{
		return m_channel->IsSupported(0, 1);
	}
	__int32 ExecuteIOC(const TradeSide& side, const std::string& symbol, const double& priceThreshold, const double& requestedVolume, double& executedPrice, double& executedVolume)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteFTradeSide(side, buffer);
		WriteAString(symbol, buffer);
		WriteDouble(priceThreshold, buffer);
		WriteDouble(requestedVolume, buffer);

		const HRESULT _status = m_channel->Invoke(0, 1, buffer);
		Throw(_status, buffer);

		executedPrice = ReadDouble(buffer);
		executedVolume = ReadDouble(buffer);
		auto _result = ReadInt32(buffer);
		return _result;
	}
};
