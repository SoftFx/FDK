#pragma once
#include "SimpleEntry.h"


class MemoryBuffer;
class CSimpleEncoder
{
public:
	CSimpleEncoder();
	bool TryEncode(const CFxQuote& quote, MemoryBuffer& buffer);
private:
	void Clear();
	bool TryConvertPrice(const CFxQuote& quote);
	bool TryConvertVolume(const CFxQuote& quote);
	bool TryCalcPriceSize();
	bool TryCalcVolumeSize();
private:
	void Write(MemoryBuffer& buffer);
private:
	uint32 m_priceExp;
	int32 m_volumeExp;
	uint32 m_priceSize;
	uint32 m_volumeSize;
private:
	vector<CSimpleEntry> m_bids;
	vector<CSimpleEntry> m_asks;
};