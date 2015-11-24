#pragma once

#include "SymbolEntry.h"

namespace FDK
{
	class CSymbolData
	{
	public:
		CSymbolData();
		CSymbolData(const CSymbolEntry& entry);
	public:
		string Tag;
		string Symbol;
		string From;
		string To;
        double ContractSize;
		double Hedging;
		double MarginFactorOfPositions;
		double MarginFactorOfLimitOrders;
		double MarginFactorOfStopOrders;
	public:
		CSymbolEntry ToSymbolEntry() const;
	};
}