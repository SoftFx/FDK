// This is always generated file. Do not change anything.

namespace
{
	void WriteFTradeCommand(const TradeCommand& arg, MemoryBuffer& buffer);
	TradeCommand ReadFTradeCommand(MemoryBuffer& buffer);
	void WriteFTradeSide(const TradeSide& arg, MemoryBuffer& buffer);
	TradeSide ReadFTradeSide(MemoryBuffer& buffer);
}

namespace
{
	void WriteFTradeCommand(const TradeCommand& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	TradeCommand ReadFTradeCommand(MemoryBuffer& buffer)
	{
		auto result = (TradeCommand)ReadInt32(buffer);
		return result;
	}
	void WriteFTradeSide(const TradeSide& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	TradeSide ReadFTradeSide(MemoryBuffer& buffer)
	{
		auto result = (TradeSide)ReadInt32(buffer);
		return result;
	}
	void Throw(HRESULT status, MemoryBuffer& buffer)
	{
		if(status >= 0)
		{
			return;
		}
		if(LRP_EXCEPTION != status)
		{
			throw std::exception("Unexpected exception has been encountered");
		}
		const int _id = ReadInt32(buffer);
		_id;
		string _message = ReadAString(buffer);
		throw std::exception(_message.c_str());
	}
}
