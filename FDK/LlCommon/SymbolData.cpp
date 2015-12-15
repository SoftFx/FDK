#include "stdafx.h"
#include "SymbolData.h"


namespace FDK
{
	CSymbolData::CSymbolData()
        : ContractSize()
        , Hedging()
        , MarginFactorOfPositions()
        , MarginFactorOfLimitOrders()
        , MarginFactorOfStopOrders()
	{
	}

	CSymbolData::CSymbolData(const CSymbolEntry& entry)
	{
		stringstream stream;
		stream << entry.Tag;
		Tag = stream.str();

		Symbol = entry.GetSymbol();
		From = entry.GetFrom();
		To = entry.GetTo();
        ContractSize = entry.GetContractSize();
		Hedging = entry.GetHedging();
		MarginFactorOfPositions = entry.GetMarginFactorOfPositions();
		MarginFactorOfLimitOrders = entry.GetMarginFactorOfLimitOrders();
		MarginFactorOfStopOrders = entry.GetMarginFactorOfStopOrders();
	}

	CSymbolEntry CSymbolData::ToSymbolEntry() const
	{
		CSymbolEntry result(Symbol, From, To);
		result.Tag = Tag;
        result.SetContractSize(ContractSize);
		result.SetHedging(Hedging);
		result.SetMarginFactorOfPositions(MarginFactorOfPositions);
		result.SetMarginFactorOfLimitOrders(MarginFactorOfLimitOrders);
		result.SetMarginFactorOfStopOrders(MarginFactorOfStopOrders);

		return result;
	}

}