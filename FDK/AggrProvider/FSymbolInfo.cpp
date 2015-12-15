#include "stdafx.h"
#include "FSymbolInfo.h"
#include "BinaryReader.h"

CFSymbolInfo::CFSymbolInfo() : Color(), ContractMultiplier(), Precision(), RoundLot(), MinTradeVolume(), TradeVolumeStep(), MaxTradeVolume()
{
}

CBinaryReader& operator>>(CBinaryReader& stream, CFSymbolInfo& info)
{
	stream>>info.Color;
	stream>>info.ContractMultiplier;
	stream>>info.RoundLot;
	stream>>info.MinTradeVolume;
	stream>>info.TradeVolumeStep;
	stream>>info.MaxTradeVolume;
	stream>>info.Precision;

	stream>>info.Name;
	stream>>info.Currency;
	stream>>info.SettlementCurrency;
	return stream;
}
bool operator < (const CFSymbolInfo& first, const CFSymbolInfo& second)
{
	return (first.Name < second.Name);
}

ostream& operator << (ostream& stream, const CFSymbolInfo& info)
{
	stream<<"Color = "<<info.Color;
	stream<<"; ContractMultiplier = "<<info.ContractMultiplier;
	stream<<"; RoundLot = "<<info.RoundLot;
	stream<<"; MinTradeVolume = "<<info.MinTradeVolume;
	stream<<"; TradeVolumeStep = "<<info.TradeVolumeStep;
	stream<<"; MaxTradeVolume = "<<info.MaxTradeVolume;
	stream<<"; Precision = "<<info.Precision;

	stream<<"; Name = "<<info.Name;
	stream<<"; Currency = "<<info.Currency;
	stream<<"; SettlementCurrency = "<<info.SettlementCurrency;
	stream<<";";
	return stream;
}