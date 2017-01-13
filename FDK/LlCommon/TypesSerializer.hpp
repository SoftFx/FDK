// This is always generated file. Do not change anything.

namespace
{
	void WriteMarginMode(const FDK::MarginMode& arg, MemoryBuffer& buffer);
	FDK::MarginMode ReadMarginMode(MemoryBuffer& buffer);
	void WriteTradeType(const FDK::TradeType& arg, MemoryBuffer& buffer);
	FDK::TradeType ReadTradeType(MemoryBuffer& buffer);
	void WriteTradeSide(const FDK::TradeSide& arg, MemoryBuffer& buffer);
	FDK::TradeSide ReadTradeSide(MemoryBuffer& buffer);
	void WriteTradeEntryStatus(const FDK::TradeEntryStatus& arg, MemoryBuffer& buffer);
	FDK::TradeEntryStatus ReadTradeEntryStatus(MemoryBuffer& buffer);
	void WriteAccountEntryStatus(const FDK::AccountEntryStatus& arg, MemoryBuffer& buffer);
	FDK::AccountEntryStatus ReadAccountEntryStatus(MemoryBuffer& buffer);
	void WriteAccountType(const FDK::AccountType& arg, MemoryBuffer& buffer);
	FDK::AccountType ReadAccountType(MemoryBuffer& buffer);
	void WriteAStringVector(const std::vector<std::string>& arg, MemoryBuffer& buffer);
	std::vector<std::string> ReadAStringVector(MemoryBuffer& buffer);
	void WritePriceData(const FDK::CPriceData& arg, MemoryBuffer& buffer);
	FDK::CPriceData ReadPriceData(MemoryBuffer& buffer);
	void WritePriceDataVector(const std::vector<FDK::CPriceData>& arg, MemoryBuffer& buffer);
	std::vector<FDK::CPriceData> ReadPriceDataVector(MemoryBuffer& buffer);
	void WriteSymbolData(const FDK::CSymbolData& arg, MemoryBuffer& buffer);
	FDK::CSymbolData ReadSymbolData(MemoryBuffer& buffer);
	void WriteSymbolDataVector(const std::vector<FDK::CSymbolData>& arg, MemoryBuffer& buffer);
	std::vector<FDK::CSymbolData> ReadSymbolDataVector(MemoryBuffer& buffer);
	void WriteTradeData(const FDK::CTradeData& arg, MemoryBuffer& buffer);
	FDK::CTradeData ReadTradeData(MemoryBuffer& buffer);
	void WriteTradeDataVector(const std::vector<FDK::CTradeData>& arg, MemoryBuffer& buffer);
	std::vector<FDK::CTradeData> ReadTradeDataVector(MemoryBuffer& buffer);
	void WriteAccountData(const FDK::CAccountData& arg, MemoryBuffer& buffer);
	FDK::CAccountData ReadAccountData(MemoryBuffer& buffer);
	void WriteAccountDataVector(const std::vector<FDK::CAccountData>& arg, MemoryBuffer& buffer);
	std::vector<FDK::CAccountData> ReadAccountDataVector(MemoryBuffer& buffer);
	void WriteCalculatorData(const FDK::CCalculatorData& arg, MemoryBuffer& buffer);
	FDK::CCalculatorData ReadCalculatorData(MemoryBuffer& buffer);
}

namespace
{
	void WriteMarginMode(const FDK::MarginMode& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	FDK::MarginMode ReadMarginMode(MemoryBuffer& buffer)
	{
		auto result = (FDK::MarginMode)ReadInt32(buffer);
		return result;
	}
	void WriteTradeType(const FDK::TradeType& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	FDK::TradeType ReadTradeType(MemoryBuffer& buffer)
	{
		auto result = (FDK::TradeType)ReadInt32(buffer);
		return result;
	}
	void WriteTradeSide(const FDK::TradeSide& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	FDK::TradeSide ReadTradeSide(MemoryBuffer& buffer)
	{
		auto result = (FDK::TradeSide)ReadInt32(buffer);
		return result;
	}
	void WriteTradeEntryStatus(const FDK::TradeEntryStatus& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	FDK::TradeEntryStatus ReadTradeEntryStatus(MemoryBuffer& buffer)
	{
		auto result = (FDK::TradeEntryStatus)ReadInt32(buffer);
		return result;
	}
	void WriteAccountEntryStatus(const FDK::AccountEntryStatus& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	FDK::AccountEntryStatus ReadAccountEntryStatus(MemoryBuffer& buffer)
	{
		auto result = (FDK::AccountEntryStatus)ReadInt32(buffer);
		return result;
	}
	void WriteAccountType(const FDK::AccountType& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	FDK::AccountType ReadAccountType(MemoryBuffer& buffer)
	{
		auto result = (FDK::AccountType)ReadInt32(buffer);
		return result;
	}
	void WriteAStringVector(const std::vector<std::string>& arg, MemoryBuffer& buffer)
	{
		WriteUInt32((unsigned __int32)arg.size(), buffer);
		for each(const auto element in arg)
		{
			WriteAString(element, buffer);
		}
	}
	std::vector<std::string> ReadAStringVector(MemoryBuffer& buffer)
	{
		const size_t count = buffer.ReadCount();
		std::vector<std::string> result;
		result.reserve(count);
		for(size_t index = 0; index < count; ++index)
		{
			result.push_back(ReadAString(buffer));
		}
		return result;
	}
	void WritePriceData(const FDK::CPriceData& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.Symbol, buffer);
		WriteDouble(arg.Bid, buffer);
		WriteDouble(arg.Ask, buffer);
	}
	FDK::CPriceData ReadPriceData(MemoryBuffer& buffer)
	{
		FDK::CPriceData result = FDK::CPriceData();
		result.Symbol = ReadAString(buffer);
		result.Bid = ReadDouble(buffer);
		result.Ask = ReadDouble(buffer);
		return result;
	}
	void WritePriceDataVector(const std::vector<FDK::CPriceData>& arg, MemoryBuffer& buffer)
	{
		WriteUInt32((unsigned __int32)arg.size(), buffer);
		for each(const auto element in arg)
		{
			WritePriceData(element, buffer);
		}
	}
	std::vector<FDK::CPriceData> ReadPriceDataVector(MemoryBuffer& buffer)
	{
		const size_t count = buffer.ReadCount();
		std::vector<FDK::CPriceData> result;
		result.reserve(count);
		for(size_t index = 0; index < count; ++index)
		{
			result.push_back(ReadPriceData(buffer));
		}
		return result;
	}
	void WriteSymbolData(const FDK::CSymbolData& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.Tag, buffer);
		WriteAString(arg.Symbol, buffer);
		WriteAString(arg.From, buffer);
		WriteAString(arg.To, buffer);
		WriteDouble(arg.ContractSize, buffer);
		WriteDouble(arg.Hedging, buffer);
		WriteDouble(arg.MarginFactorOfPositions, buffer);
		WriteDouble(arg.MarginFactorOfLimitOrders, buffer);
		WriteDouble(arg.MarginFactorOfStopOrders, buffer);
	}
	FDK::CSymbolData ReadSymbolData(MemoryBuffer& buffer)
	{
		FDK::CSymbolData result = FDK::CSymbolData();
		result.Tag = ReadAString(buffer);
		result.Symbol = ReadAString(buffer);
		result.From = ReadAString(buffer);
		result.To = ReadAString(buffer);
		result.ContractSize = ReadDouble(buffer);
		result.Hedging = ReadDouble(buffer);
		result.MarginFactorOfPositions = ReadDouble(buffer);
		result.MarginFactorOfLimitOrders = ReadDouble(buffer);
		result.MarginFactorOfStopOrders = ReadDouble(buffer);
		return result;
	}
	void WriteSymbolDataVector(const std::vector<FDK::CSymbolData>& arg, MemoryBuffer& buffer)
	{
		WriteUInt32((unsigned __int32)arg.size(), buffer);
		for each(const auto element in arg)
		{
			WriteSymbolData(element, buffer);
		}
	}
	std::vector<FDK::CSymbolData> ReadSymbolDataVector(MemoryBuffer& buffer)
	{
		const size_t count = buffer.ReadCount();
		std::vector<FDK::CSymbolData> result;
		result.reserve(count);
		for(size_t index = 0; index < count; ++index)
		{
			result.push_back(ReadSymbolData(buffer));
		}
		return result;
	}
	void WriteTradeData(const FDK::CTradeData& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.Tag, buffer);
		WriteTradeType(arg.Type, buffer);
		WriteTradeSide(arg.Side, buffer);
		WriteAString(arg.Symbol, buffer);
		WriteDouble(arg.Volume, buffer);
		WriteNullDouble(arg.Price, buffer);
		WriteNullDouble(arg.StopPrice, buffer);
		WriteDouble(arg.Commission, buffer);
		WriteDouble(arg.AgentCommission, buffer);
		WriteDouble(arg.Swap, buffer);
		WriteNullDouble(arg.Rate, buffer);
		WriteNullDouble(arg.Profit, buffer);
		WriteTradeEntryStatus(arg.ProfitStatus, buffer);
		WriteNullDouble(arg.Margin, buffer);
		WriteTradeEntryStatus(arg.MarginStatus, buffer);
	}
	FDK::CTradeData ReadTradeData(MemoryBuffer& buffer)
	{
		FDK::CTradeData result = FDK::CTradeData();
		result.Tag = ReadAString(buffer);
		result.Type = ReadTradeType(buffer);
		result.Side = ReadTradeSide(buffer);
		result.Symbol = ReadAString(buffer);
		result.Volume = ReadDouble(buffer);
		result.Price = ReadNullDouble(buffer);
		result.StopPrice = ReadNullDouble(buffer);
		result.Commission = ReadDouble(buffer);
		result.AgentCommission = ReadDouble(buffer);
		result.Swap = ReadDouble(buffer);
		result.Rate = ReadNullDouble(buffer);
		result.Profit = ReadNullDouble(buffer);
		result.ProfitStatus = ReadTradeEntryStatus(buffer);
		result.Margin = ReadNullDouble(buffer);
		result.MarginStatus = ReadTradeEntryStatus(buffer);
		return result;
	}
	void WriteTradeDataVector(const std::vector<FDK::CTradeData>& arg, MemoryBuffer& buffer)
	{
		WriteUInt32((unsigned __int32)arg.size(), buffer);
		for each(const auto element in arg)
		{
			WriteTradeData(element, buffer);
		}
	}
	std::vector<FDK::CTradeData> ReadTradeDataVector(MemoryBuffer& buffer)
	{
		const size_t count = buffer.ReadCount();
		std::vector<FDK::CTradeData> result;
		result.reserve(count);
		for(size_t index = 0; index < count; ++index)
		{
			result.push_back(ReadTradeData(buffer));
		}
		return result;
	}
	void WriteAccountData(const FDK::CAccountData& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.Tag, buffer);
		WriteAccountType(arg.Type, buffer);
		WriteDouble(arg.Leverage, buffer);
		WriteDouble(arg.Balance, buffer);
		WriteAString(arg.Currency, buffer);
		WriteNullDouble(arg.Profit, buffer);
		WriteAccountEntryStatus(arg.ProfitStatus, buffer);
		WriteNullDouble(arg.Margin, buffer);
		WriteAccountEntryStatus(arg.MarginStatus, buffer);
		WriteTradeDataVector(arg.Trades, buffer);
	}
	FDK::CAccountData ReadAccountData(MemoryBuffer& buffer)
	{
		FDK::CAccountData result = FDK::CAccountData();
		result.Tag = ReadAString(buffer);
		result.Type = ReadAccountType(buffer);
		result.Leverage = ReadDouble(buffer);
		result.Balance = ReadDouble(buffer);
		result.Currency = ReadAString(buffer);
		result.Profit = ReadNullDouble(buffer);
		result.ProfitStatus = ReadAccountEntryStatus(buffer);
		result.Margin = ReadNullDouble(buffer);
		result.MarginStatus = ReadAccountEntryStatus(buffer);
		result.Trades = ReadTradeDataVector(buffer);
		return result;
	}
	void WriteAccountDataVector(const std::vector<FDK::CAccountData>& arg, MemoryBuffer& buffer)
	{
		WriteUInt32((unsigned __int32)arg.size(), buffer);
		for each(const auto element in arg)
		{
			WriteAccountData(element, buffer);
		}
	}
	std::vector<FDK::CAccountData> ReadAccountDataVector(MemoryBuffer& buffer)
	{
		const size_t count = buffer.ReadCount();
		std::vector<FDK::CAccountData> result;
		result.reserve(count);
		for(size_t index = 0; index < count; ++index)
		{
			result.push_back(ReadAccountData(buffer));
		}
		return result;
	}
	void WriteCalculatorData(const FDK::CCalculatorData& arg, MemoryBuffer& buffer)
	{
		WriteMarginMode(arg.MarginMode, buffer);
		WritePriceDataVector(arg.Prices, buffer);
		WriteSymbolDataVector(arg.Symbols, buffer);
		WriteAccountDataVector(arg.Accounts, buffer);
		WriteAStringVector(arg.Currencies, buffer);
	}
	FDK::CCalculatorData ReadCalculatorData(MemoryBuffer& buffer)
	{
		FDK::CCalculatorData result = FDK::CCalculatorData();
		result.MarginMode = ReadMarginMode(buffer);
		result.Prices = ReadPriceDataVector(buffer);
		result.Symbols = ReadSymbolDataVector(buffer);
		result.Accounts = ReadAccountDataVector(buffer);
		result.Currencies = ReadAStringVector(buffer);
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
