#pragma once
#include "BinaryReader.h"

class CFQuote
{
public:
	double Price;
	double Volume;
	FIX::UtcTimeStamp Setting;
	FIX::UtcTimeStamp Expiration;
	string EntryId;
public:
	CFQuote();
private:
	friend CBinaryReader& operator >> (CBinaryReader& stream, CFQuote& arg);
};