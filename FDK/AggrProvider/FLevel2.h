#pragma once
#include "FBank.h"


class CFLevel2
{
public:
	uint16 Version;
	bool Filtered;
	string Symbol;
	map<int32, CFBank> Banks;
public:
	CFLevel2(uint16 version = 0);
public:
	void Clear();
	bool CopyTo(int32 bankCode, CFxQuote& quote) const;
private:
	friend CBinaryReader& operator >> (CBinaryReader& stream, CFLevel2& arg);
};