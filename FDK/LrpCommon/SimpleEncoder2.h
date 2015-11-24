#pragma once


class CSimpleEncoder2
{
public:
	CSimpleEncoder2(const size_t precision, const double volumeStep);
public:
	bool TryEncode(const CFxQuote& quote, MemoryBuffer& buffer);
private:
	bool TryConvert(const CFxQuoteEntry& element, CSimpleEntry& entry);
	bool TryConvert(const CFxQuote& quote);
private:
	void Write(MemoryBuffer& buffer);
private:
	double m_priceFactor;
	double m_volumeDivisor;
private:
	size_t m_bidsNumber;
	size_t m_asksNumber;
private:
	uint32 m_priceSize;
	uint32 m_volumeSize;
private:
	CSimpleEntry m_bids[16];
	CSimpleEntry m_asks[16];
};