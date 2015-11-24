// This is always generated file. Do not change anything.

class Extended
{
private:
	CLrpLocalClient* m_channel;
public:
	inline Extended() : m_channel()
	{
	}
	inline Extended(CLrpLocalClient& client) : m_channel(&client)
	{
	}
	bool IsSupported() const
	{
		return m_channel->IsSupported(1);
	}
	const static unsigned short LrpMethod_Do_Id = 0;
	bool Is_Do_Supported() const
	{
		return m_channel->IsSupported(1, 0);
	}
	CReturnType Do(const CInType& inArg, CInOutType& inOutArg, COutType& outArg)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteInType(inArg, buffer);
		WriteInOutType(inOutArg, buffer);

		const HRESULT _status = m_channel->Invoke(1, 0, buffer);
		Throw(_status, buffer);

		inOutArg = ReadInOutType(buffer);
		outArg = ReadOutType(buffer);
		auto _result = ReadReturnType(buffer);
		return _result;
	}
	const static unsigned short LrpMethod_MarketBuy_Id = 1;
	bool Is_MarketBuy_Supported() const
	{
		return m_channel->IsSupported(1, 1);
	}
	__int32 MarketBuy(const std::string& symbol, const double& price, double& volume, double& amount)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(symbol, buffer);
		WriteDouble(price, buffer);
		WriteDouble(volume, buffer);

		const HRESULT _status = m_channel->Invoke(1, 1, buffer);
		Throw(_status, buffer);

		volume = ReadDouble(buffer);
		amount = ReadDouble(buffer);
		auto _result = ReadInt32(buffer);
		return _result;
	}
};
