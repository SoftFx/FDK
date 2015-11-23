// This is always generated file. Do not change anything.

namespace
{
	void LrpWriteDataHistoryRequest(const char* name, const CFxDataHistoryRequest& arg, std::ostream& _stream);
	void LrpWriteAStringArray(const char* name, const std::vector<std::string>& arg, std::ostream& _stream);
	template<size_t count> void LrpWriteAStringArray(const char* name, const std::string(&arg)[count], std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream << "[" << count << "]{";
		const std::string* it = arg;
		const std::string* end = it + count;
		if (it != end)
		{
			LrpWriteAString(nullptr, *it, _stream);
			_stream << ";";
			++it;
		}
		for (; it != end; ++it)
		{
			_stream << " ";
			LrpWriteAString(nullptr, *it, _stream);
			_stream << ";";
		}
		_stream << "}";
	}
}
namespace
{
	void LrpWriteDataHistoryRequest(const char* name, const CFxDataHistoryRequest& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream<<"{";
		LrpWriteAString("Symbol", arg.Symbol, _stream);
		_stream << ';';
		LrpWriteTime("Time", arg.Time, _stream);
		_stream << ';';
		LrpWriteInt32("BarsNumber", arg.BarsNumber, _stream);
		_stream << ';';
		LrpWriteInt32("PriceType", arg.PriceType, _stream);
		_stream << ';';
		LrpWriteAString("GraphPeriod", arg.GraphPeriod, _stream);
		_stream << ';';
		LrpWriteInt32("ReportType", arg.ReportType, _stream);
		_stream << ';';
		LrpWriteInt32("GraphType", arg.GraphType, _stream);
		_stream << ';';
		_stream<<"}";
	}
	void LrpWriteAStringArray(const char* name, const std::vector<std::string>& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream << "[" << arg.size() << "]{";
		auto it = LrpBeginIterator(arg);
		auto end = LrpEndIterator(arg);
		if (it != end)
		{
			LrpWriteAString(nullptr, *it, _stream);
			_stream << ";";
			++it;
		}
		for (; it != end; ++it)
		{
			_stream << " ";
			LrpWriteAString(nullptr, *it, _stream);
			_stream << ";";
		}
		_stream << "}";
	}
}
Server::Server(ILrpTextStream* pStream) : m_stream(pStream)
{
}
void Server::OnHeartBeatRequest()
{
	std::stringstream _stream;
	_stream << "[0]Server[0]OnHeartBeatRequest(";
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Server::OnHeartBeatResponse()
{
	std::stringstream _stream;
	_stream << "[0]Server[1]OnHeartBeatResponse(";
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Server::OnCurrenciesInfoRequest(const std::string& id)
{
	std::stringstream _stream;
	_stream << "[0]Server[2]OnCurrenciesInfoRequest(";
	LrpWriteAString("id", id, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Server::OnSymbolsInfoRequest(const std::string& id)
{
	std::stringstream _stream;
	_stream << "[0]Server[3]OnSymbolsInfoRequest(";
	LrpWriteAString("id", id, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Server::OnSessionInfoRequest(const std::string& id)
{
	std::stringstream _stream;
	_stream << "[0]Server[4]OnSessionInfoRequest(";
	LrpWriteAString("id", id, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Server::OnSubscribeToQuotesRequest(const std::string& id, const std::vector<std::string>& symbols, const __int32& marketDepth)
{
	std::stringstream _stream;
	_stream << "[0]Server[5]OnSubscribeToQuotesRequest(";
	LrpWriteAString("id", id, _stream);
	_stream<<", ";
	LrpWriteAStringArray("symbols", symbols, _stream);
	_stream<<", ";
	LrpWriteInt32("marketDepth", marketDepth, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Server::OnUnsubscribeQuotesRequest(const std::string& id, const std::vector<std::string>& symbols)
{
	std::stringstream _stream;
	_stream << "[0]Server[6]OnUnsubscribeQuotesRequest(";
	LrpWriteAString("id", id, _stream);
	_stream<<", ";
	LrpWriteAStringArray("symbols", symbols, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Server::OnComponentsInfoRequest(const std::string& id, const __int32& clientVersion)
{
	std::stringstream _stream;
	_stream << "[0]Server[7]OnComponentsInfoRequest(";
	LrpWriteAString("id", id, _stream);
	_stream<<", ";
	LrpWriteInt32("clientVersion", clientVersion, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Server::OnDataHistoryRequest(const std::string& id, const CFxDataHistoryRequest& request)
{
	std::stringstream _stream;
	_stream << "[0]Server[8]OnDataHistoryRequest(";
	LrpWriteAString("id", id, _stream);
	_stream<<", ";
	LrpWriteDataHistoryRequest("request", request, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Server::OnFileChunkRequest(const std::string& id, const std::string& fieldId, const unsigned __int32& chunkId)
{
	std::stringstream _stream;
	_stream << "[0]Server[9]OnFileChunkRequest(";
	LrpWriteAString("id", id, _stream);
	_stream<<", ";
	LrpWriteAString("fieldId", fieldId, _stream);
	_stream<<", ";
	LrpWriteUInt32("chunkId", chunkId, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Server::OnBarsHistoryMetaInfoFileRequest(const std::string& id, const std::string& symbol, const __int32& priceType, const std::string& period)
{
	std::stringstream _stream;
	_stream << "[0]Server[10]OnBarsHistoryMetaInfoFileRequest(";
	LrpWriteAString("id", id, _stream);
	_stream<<", ";
	LrpWriteAString("symbol", symbol, _stream);
	_stream<<", ";
	LrpWriteInt32("priceType", priceType, _stream);
	_stream<<", ";
	LrpWriteAString("period", period, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Server::OnQuotesHistoryMetaInfoFileRequest(const std::string& id, const std::string& symbol, const bool& includeLevel2)
{
	std::stringstream _stream;
	_stream << "[0]Server[11]OnQuotesHistoryMetaInfoFileRequest(";
	LrpWriteAString("id", id, _stream);
	_stream<<", ";
	LrpWriteAString("symbol", symbol, _stream);
	_stream<<", ";
	LrpWriteBoolean("includeLevel2", includeLevel2, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
