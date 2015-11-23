#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)
#pragma warning (disable : 4251)

class CORE_API CFxDataHistoryRequest
{
public:
	string Symbol;
	CDateTime Time;
	int32 BarsNumber;
	int32 PriceType;
	string GraphPeriod;
	int32 ReportType;
	int32 GraphType;
public:
	CFxDataHistoryRequest();
	CFxDataHistoryRequest(const string& symbol);
	CFxDataHistoryRequest(const string& symbol, const string& period);
public:
	const string& GetSymbol()const;
	const string& GetPeriod()const;
};

#pragma warning (pop)
