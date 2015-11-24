#include "stdafx.h"
#include "SimpleEncoder2.h"

namespace
{
	const uint32 CalculateSize(uint32 value)
	{
		uint32 result = 0;
		for (; value > 0; )
		{
			++result;
			value /= 2;
		}
		if (0 == result)
		{
			result = 1;
		}
		return result;
	}
}

CSimpleEncoder2::CSimpleEncoder2(const size_t precision, const double volumeStep) :
	m_priceFactor(), m_volumeDivisor(), m_priceSize(), m_volumeSize()
{
	m_priceFactor = pow(10, precision);
	m_volumeDivisor = volumeStep;
}
bool CSimpleEncoder2::TryEncode(const CFxQuote& quote, MemoryBuffer& buffer)
{
	m_bidsNumber = quote.Bids.size();
	m_asksNumber = quote.Asks.size();

	if ((0 == m_bidsNumber) || (0 == m_asksNumber)|| (m_bidsNumber > 16) || (m_asksNumber > 16))
	{
		return false;
	}
	if (!TryConvert(quote))
	{
		return false;
	}
	Write(buffer);
	return true;
}

bool CSimpleEncoder2::TryConvert(const CFxQuoteEntry& element, CSimpleEntry& entry)
{
	const double _price = element.Price * m_priceFactor + 0.5;
	if (_price > 0xFFFFFFFF)
	{
		return false;
	}
	const double _volume = element.Volume / m_volumeDivisor + 0.5;
	if (_volume > 0xFFFFFFFF)
	{
		return false;
	}

	entry.Price = static_cast<int32>(_price);
	entry.Volume = static_cast<int32>(_volume);

	return true;
}

bool CSimpleEncoder2::TryConvert(const CFxQuote& quote)
{
	CSimpleEntry* pCurrentBid = m_bids;

	if (!TryConvert(quote.Bids.back(), *pCurrentBid))
	{
		return false;
	}

	uint32 maxVolume = pCurrentBid->Volume;
	int32 maxDelta = 0;
	uint32 lastPrice = pCurrentBid->Price;
	

	for (size_t index = m_bidsNumber - 2; index < m_bidsNumber; --index)
	{
		++pCurrentBid;
		if (!TryConvert(quote.Bids[index], *pCurrentBid))
		{
			return false;
		}
		if (pCurrentBid->Volume > maxVolume)
		{
			maxVolume = pCurrentBid->Volume;
		}

		const int32 delta = static_cast<int32>(pCurrentBid->Price - lastPrice) - 1;
		lastPrice = pCurrentBid->Price;

		if ((delta < 0) || (delta > 0xFFFF))
		{
			return false;
		}

		pCurrentBid->Delta = delta;
		if (delta > maxDelta)
		{
			maxDelta = delta;
		}
	}



	CSimpleEntry* pCurrentAsk = m_asks;


	if (!TryConvert(quote.Asks.front(), *pCurrentAsk))
	{
		return false;
	}



	{
		if (pCurrentAsk->Volume > maxVolume)
		{
			maxVolume = pCurrentAsk->Volume;
		}

		pCurrentAsk->Delta = static_cast<int32>(pCurrentAsk->Price - lastPrice);
		lastPrice = pCurrentAsk->Price;

		const int32 delta = (pCurrentAsk->Delta >= 0) ? pCurrentAsk->Delta : (-pCurrentAsk->Delta);

		if (delta > 0xFFFF)
		{
			return false;
		}

		if (delta > maxDelta)
		{
			maxDelta = delta;
		}
	}


	for (size_t index = 1; index < m_asksNumber; ++index)
	{
		++pCurrentAsk;
		if (!TryConvert(quote.Asks[index], *pCurrentAsk))
		{
			return false;
		}
		if (pCurrentAsk->Volume > maxVolume)
		{
			maxVolume = pCurrentAsk->Volume;
		}

		const int32 delta = static_cast<int32>(pCurrentAsk->Price - lastPrice) - 1;
		lastPrice = pCurrentAsk->Price;

		if ((delta < 0) || (delta > 0xFFFF))
		{
			return false;
		}

		pCurrentAsk->Delta = delta;
		if (delta > maxDelta)
		{
			maxDelta = delta;
		}
	}

	m_volumeSize = CalculateSize(maxVolume);
	m_priceSize = CalculateSize(maxDelta);
	return true;
}




void CSimpleEncoder2::Write(MemoryBuffer& buffer)
{
	CBitWriter stream(buffer);

	stream.WriteUInt32(static_cast<unsigned>(m_bidsNumber - 1), 4);
	stream.WriteUInt32(static_cast<unsigned>(m_asksNumber - 1), 4);


	stream.WriteUInt32(m_priceSize - 1, 4);
	stream.WriteUInt32(m_volumeSize, 5);


	{
		const CSimpleEntry& element = m_bids[0];
		stream.WriteUInt32(element.Price, 32);
		stream.WriteUInt32(element.Volume, m_volumeSize);
	}

	for (uint32 index = 1; index < m_bidsNumber; ++index)
	{
		const CSimpleEntry& element = m_bids[index];
		stream.WriteUInt32(static_cast<uint32>(element.Delta), m_priceSize);
		stream.WriteUInt32(element.Volume, m_volumeSize);
	}

	{
		const CSimpleEntry& element = m_asks[0];
		stream.WriteInt32(element.Delta, 1 + m_priceSize);
		stream.WriteUInt32(element.Volume, m_volumeSize);
	}

	for (uint32 index = 1; index < m_asksNumber; ++index)
	{
		const CSimpleEntry& element = m_asks[index];
		stream.WriteUInt32(static_cast<uint32>(element.Delta), m_priceSize);
		stream.WriteUInt32(element.Volume, m_volumeSize);
	}

	stream.Flush();
}

