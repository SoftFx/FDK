#pragma once
#include "FQuote.h"


class CFBank
{
public:
	int32 Code;
	vector<CFQuote> Bids;
	vector<CFQuote> Asks;
public:
	CFBank();
public:
	void Clear();
	void UpdateQuote(CFxQuote& quote) const;
public:
	friend CBinaryReader& operator >> (CBinaryReader& stream, CFBank& arg);
};