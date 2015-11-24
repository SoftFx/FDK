// This is always generated file. Do not change anything.

namespace
{
	void WriteQuoteEntry(const CFxQuoteEntry& arg, MemoryBuffer& buffer);
	CFxQuoteEntry ReadQuoteEntry(MemoryBuffer& buffer);
	void WriteQuoteEntryArray(const std::vector<CFxQuoteEntry>& arg, MemoryBuffer& buffer);
	std::vector<CFxQuoteEntry> ReadQuoteEntryArray(MemoryBuffer& buffer);
	void WriteQuote(const CFxQuote& arg, MemoryBuffer& buffer);
	CFxQuote ReadQuote(MemoryBuffer& buffer);
	void WriteQuoteArray(const std::vector<CFxQuote>& arg, MemoryBuffer& buffer);
	std::vector<CFxQuote> ReadQuoteArray(MemoryBuffer& buffer);
}

namespace
{
	void WriteQuoteEntry(const CFxQuoteEntry& arg, MemoryBuffer& buffer)
	{
		WriteDouble(arg.Price, buffer);
		WriteDouble(arg.Volume, buffer);
	}
	CFxQuoteEntry ReadQuoteEntry(MemoryBuffer& buffer)
	{
		CFxQuoteEntry result = CFxQuoteEntry();
		result.Price = ReadDouble(buffer);
		result.Volume = ReadDouble(buffer);
		return result;
	}
	void WriteQuoteEntryArray(const std::vector<CFxQuoteEntry>& arg, MemoryBuffer& buffer)
	{
		WriteUInt32((unsigned __int32)arg.size(), buffer);
		for each(const auto element in arg)
		{
			WriteQuoteEntry(element, buffer);
		}
	}
	std::vector<CFxQuoteEntry> ReadQuoteEntryArray(MemoryBuffer& buffer)
	{
		const size_t count = buffer.ReadCount();
		std::vector<CFxQuoteEntry> result;
		result.reserve(count);
		for(size_t index = 0; index < count; ++index)
		{
			result.push_back(ReadQuoteEntry(buffer));
		}
		return result;
	}
	void WriteQuote(const CFxQuote& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.Symbol, buffer);
		WriteTime(arg.CreatingTime, buffer);
		WriteQuoteEntryArray(arg.Bids, buffer);
		WriteQuoteEntryArray(arg.Asks, buffer);
	}
	CFxQuote ReadQuote(MemoryBuffer& buffer)
	{
		CFxQuote result = CFxQuote();
		result.Symbol = ReadAString(buffer);
		result.CreatingTime = ReadTime(buffer);
		result.Bids = ReadQuoteEntryArray(buffer);
		result.Asks = ReadQuoteEntryArray(buffer);
		return result;
	}
	void WriteQuoteArray(const std::vector<CFxQuote>& arg, MemoryBuffer& buffer)
	{
		WriteUInt32((unsigned __int32)arg.size(), buffer);
		for each(const auto element in arg)
		{
			WriteQuote(element, buffer);
		}
	}
	std::vector<CFxQuote> ReadQuoteArray(MemoryBuffer& buffer)
	{
		const size_t count = buffer.ReadCount();
		std::vector<CFxQuote> result;
		result.reserve(count);
		for(size_t index = 0; index < count; ++index)
		{
			result.push_back(ReadQuote(buffer));
		}
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
