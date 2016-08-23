// This is always generated file. Do not change anything.

namespace
{
	void WriteDataHistoryRequest(const CFxDataHistoryRequest& arg, MemoryBuffer& buffer);
	CFxDataHistoryRequest ReadDataHistoryRequest(MemoryBuffer& buffer);
	void WriteAStringArray(const std::vector<std::string>& arg, MemoryBuffer& buffer);
	std::vector<std::string> ReadAStringArray(MemoryBuffer& buffer);
	void WriteTwoFactorReason(const FxTwoFactorReason& arg, MemoryBuffer& buffer);
	FxTwoFactorReason ReadTwoFactorReason(MemoryBuffer& buffer);
}

namespace
{
	void WriteDataHistoryRequest(const CFxDataHistoryRequest& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.Symbol, buffer);
		WriteTime(arg.Time, buffer);
		WriteInt32(arg.BarsNumber, buffer);
		WriteInt32(arg.PriceType, buffer);
		WriteAString(arg.GraphPeriod, buffer);
		WriteInt32(arg.ReportType, buffer);
		WriteInt32(arg.GraphType, buffer);
	}
	CFxDataHistoryRequest ReadDataHistoryRequest(MemoryBuffer& buffer)
	{
		CFxDataHistoryRequest result = CFxDataHistoryRequest();
		result.Symbol = ReadAString(buffer);
		result.Time = ReadTime(buffer);
		result.BarsNumber = ReadInt32(buffer);
		result.PriceType = ReadInt32(buffer);
		result.GraphPeriod = ReadAString(buffer);
		result.ReportType = ReadInt32(buffer);
		result.GraphType = ReadInt32(buffer);
		return result;
	}
	void WriteAStringArray(const std::vector<std::string>& arg, MemoryBuffer& buffer)
	{
		WriteUInt32((unsigned __int32)arg.size(), buffer);
		for each(const auto element in arg)
		{
			WriteAString(element, buffer);
		}
	}
	std::vector<std::string> ReadAStringArray(MemoryBuffer& buffer)
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
	void WriteTwoFactorReason(const FxTwoFactorReason& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	FxTwoFactorReason ReadTwoFactorReason(MemoryBuffer& buffer)
	{
		auto result = (FxTwoFactorReason)ReadInt32(buffer);
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
