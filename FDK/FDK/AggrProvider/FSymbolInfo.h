#pragma once


class CBinaryReader;

class CFSymbolInfo
{
public:
	CFSymbolInfo();
public:
	int Color;
	int ContractMultiplier;
	int Precision;
	double RoundLot;
	double MinTradeVolume;
	double TradeVolumeStep;
	double MaxTradeVolume;

	string Name;
	string Currency;
	string SettlementCurrency;
private:
	friend bool operator < (const CFSymbolInfo& first, const CFSymbolInfo& second);
private:
	friend CBinaryReader& operator >> (CBinaryReader& stream, CFSymbolInfo& info);
	friend ostream& operator << (ostream& stream, const CFSymbolInfo& info);
};