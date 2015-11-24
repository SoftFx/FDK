// This is always generated file. Do not change anything.

namespace
{
	void WriteUsedType(const CUsedType& arg, MemoryBuffer& buffer);
	CUsedType ReadUsedType(MemoryBuffer& buffer);
	void WriteInType(const CInType& arg, MemoryBuffer& buffer);
	CInType ReadInType(MemoryBuffer& buffer);
	void WriteInOutType(const CInOutType& arg, MemoryBuffer& buffer);
	CInOutType ReadInOutType(MemoryBuffer& buffer);
	void WriteOutType(const COutType& arg, MemoryBuffer& buffer);
	COutType ReadOutType(MemoryBuffer& buffer);
	void WriteReturnType(const CReturnType& arg, MemoryBuffer& buffer);
	CReturnType ReadReturnType(MemoryBuffer& buffer);
	void WritePositionReports(const std::map<std::string, double>& arg, MemoryBuffer& buffer);
	std::map<std::string, double> ReadPositionReports(MemoryBuffer& buffer);
	void WritePositionReports2(const std::unordered_map<std::string, double>& arg, MemoryBuffer& buffer);
	std::unordered_map<std::string, double> ReadPositionReports2(MemoryBuffer& buffer);
}

namespace
{
	void WriteUsedType(const CUsedType& arg, MemoryBuffer& buffer)
	{
		WriteInt32(arg.Code, buffer);
		WriteAString(arg.Description, buffer);
	}
	CUsedType ReadUsedType(MemoryBuffer& buffer)
	{
		CUsedType result = CUsedType();
		result.Code = ReadInt32(buffer);
		result.Description = ReadAString(buffer);
		return result;
	}
	void WriteInType(const CInType& arg, MemoryBuffer& buffer)
	{
		WriteUsedType(arg.Used, buffer);
		WriteDouble(arg.Value, buffer);
	}
	CInType ReadInType(MemoryBuffer& buffer)
	{
		CInType result = CInType();
		result.Used = ReadUsedType(buffer);
		result.Value = ReadDouble(buffer);
		return result;
	}
	void WriteInOutType(const CInOutType& arg, MemoryBuffer& buffer)
	{
		WriteUsedType(arg.Used, buffer);
		WriteDouble(arg.Value2, buffer);
	}
	CInOutType ReadInOutType(MemoryBuffer& buffer)
	{
		CInOutType result = CInOutType();
		result.Used = ReadUsedType(buffer);
		result.Value2 = ReadDouble(buffer);
		return result;
	}
	void WriteOutType(const COutType& arg, MemoryBuffer& buffer)
	{
		WriteUsedType(arg.Used, buffer);
		WriteDouble(arg.Value3, buffer);
	}
	COutType ReadOutType(MemoryBuffer& buffer)
	{
		COutType result = COutType();
		result.Used = ReadUsedType(buffer);
		result.Value3 = ReadDouble(buffer);
		return result;
	}
	void WriteReturnType(const CReturnType& arg, MemoryBuffer& buffer)
	{
		WriteUsedType(arg.Used, buffer);
		WriteDouble(arg.Value4, buffer);
	}
	CReturnType ReadReturnType(MemoryBuffer& buffer)
	{
		CReturnType result = CReturnType();
		result.Used = ReadUsedType(buffer);
		result.Value4 = ReadDouble(buffer);
		return result;
	}
	void WritePositionReports(const std::map<std::string, double>& arg, MemoryBuffer& buffer)
	{
		WriteUInt32((unsigned __int32)arg.size(), buffer);
		for each(const auto element in arg)
		{
			WriteAString(element.first, buffer);
			WriteDouble(element.second, buffer);
		}
	}
	std::map<std::string, double> ReadPositionReports(MemoryBuffer& buffer)
	{
		const size_t count = buffer.ReadCount();
		std::map<std::string, double> result;
		for(size_t index = 0; index < count; ++index)
		{
			auto key = ReadAString(buffer);
			auto value = ReadDouble(buffer);
			result[key] = value;
		}
		return result;
	}
	void WritePositionReports2(const std::unordered_map<std::string, double>& arg, MemoryBuffer& buffer)
	{
		WriteUInt32((unsigned __int32)arg.size(), buffer);
		for each(const auto element in arg)
		{
			WriteAString(element.first, buffer);
			WriteDouble(element.second, buffer);
		}
	}
	std::unordered_map<std::string, double> ReadPositionReports2(MemoryBuffer& buffer)
	{
		const size_t count = buffer.ReadCount();
		std::unordered_map<std::string, double> result;
		for(size_t index = 0; index < count; ++index)
		{
			auto key = ReadAString(buffer);
			auto value = ReadDouble(buffer);
			result[key] = value;
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
