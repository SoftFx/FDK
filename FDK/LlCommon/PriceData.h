#pragma once

namespace FDK
{
	class CPriceData
	{
	public:
		string Symbol;
		double Bid;
		double Ask;
	public:
		CPriceData();
		CPriceData(const std::string& symbol, double bid, double ask);
	};
}