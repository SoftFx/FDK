#include "stdafx.h"
#include "SimpleEncoder.h"
#include "SimpleDecoder.h"


namespace
{
	const double cRelativePrecision = 1E-14;

}

CSimpleEncoder::CSimpleEncoder()
{
}
bool CSimpleEncoder::TryEncode(const CFxQuote& quote, MemoryBuffer& buffer)
{
	Clear();
	if ((quote.Bids.size() > 15) || (quote.Asks.size() > 15))
	{
		return false;
	}
	if (!TryConvertPrice(quote))
	{
		return false;
	}
	if (!TryConvertVolume(quote))
	{
		return false;
	}
	if (!TryCalcPriceSize())
	{
		return false;
	}
	if (!TryCalcVolumeSize())
	{
		return false;
	}
	Write(buffer);
	return true;
}
bool CSimpleEncoder::TryConvertPrice(const CFxQuote& quote)
{
	assert(0 == m_priceExp);
	double factor = 1;

	for each(const auto& element in quote.Bids)
	{
		for (; m_priceExp < 8; ++m_priceExp, factor *= 10)
		{
			const double price = element.Price * factor;
			const double value = floor(price + 0.5);
			if (abs(value - price) < FLT_EPSILON)
			{
				break;
			}
		}
	}

	for each(const auto& element in quote.Asks)
	{
		for (; m_priceExp < 8; ++m_priceExp, factor *= 10)
		{
			const double price = element.Price * factor;
			const double value = floor(price + 0.5);
			if (abs(value - price) < FLT_EPSILON)
			{
				break;
			}
		}
	}

	if (8 == m_priceExp)
	{
		return false;
	}

	const size_t count = quote.Bids.size();

	for(size_t index = count - 1; index < count; --index)
	{
		const CFxQuoteEntry& element = quote.Bids[index];
		const double value = floor(element.Price * factor + 0.5);
		if (value > 0xFFFFFFFF)
		{
			return false;
		}
		const uint32 price = static_cast<int32>(value);
		m_bids.push_back(CSimpleEntry(price, 0));
	}

	for each(const auto& element in quote.Asks)
	{
		const double value = floor(element.Price * factor + 0.5);
		if (value > 0xFFFFFFFF)
		{
			return false;
		}
		const uint32 price = static_cast<int32>(value);
		m_asks.push_back(CSimpleEntry(price, 0));
	}

	return true;
}
bool CSimpleEncoder::TryConvertVolume(const CFxQuote& quote)
{
	assert(6 == m_volumeExp);
	double divisor = 1000000;

	for each(const auto& element in quote.Bids)
	{
		for (; m_volumeExp > -9; --m_volumeExp, divisor /= 10)
		{
			const double volume = element.Volume / divisor;
			const double value = floor(volume + 0.5);
			if (abs(value - volume) < cRelativePrecision * value)
			{
				break;
			}
		}
	}

	for each(const auto& element in quote.Asks)
	{
		for (; m_volumeExp > -9; --m_volumeExp, divisor /= 10)
		{
			const double volume = element.Volume / divisor;
			const double value = floor(volume + 0.5);
			if (abs(value - volume) < cRelativePrecision * value)
			{
				break;
			}
		}
	}

	if (-9 == m_volumeExp)
	{
		return false;
	}

	size_t count = quote.Bids.size();

	for(size_t index = count - 1; index < count; --index)
	{
		const CFxQuoteEntry& element = quote.Bids[index];
		const double value = floor(element.Volume / divisor + 0.5);
		if (value > 0xFFFFFFFF)
		{
			return false;
		}
		m_bids[count - index - 1].Volume = static_cast<uint32>(value);
	}

	count = quote.Asks.size();

	for(size_t index = 0; index < count; ++index)
	{
		const CFxQuoteEntry& element = quote.Asks[index];
		const double value = floor(element.Volume / divisor + 0.5);
		if (value > 0xFFFFFFFF)
		{
			return false;
		}
		m_asks[index].Volume = static_cast<uint32>(value);
	}

	return true;
}
void CSimpleEncoder::Clear()
{
	m_priceExp = 0;
	m_volumeExp = 6;
	m_priceSize = 0;
	m_volumeSize = 0;
	m_bids.clear();
	m_asks.clear();
}
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
		return result;
	}
}


bool CSimpleEncoder::TryCalcPriceSize()
{
	assert(0 == m_priceSize);

	uint32 price = 0;
	int32 delta = 0;

	const size_t bidsNumber = m_bids.size();
	const size_t asksNumber = m_asks.size();

	if (bidsNumber > 0)
	{
		price = m_bids.front().Price;

		for (uint32 index = 1; index < bidsNumber; ++index)
		{
			const uint32 value = m_bids[index].Price;
			if (value < price)
			{
				return false;
			}
			const int32 _delta = static_cast<int32>(value - price);
			if ((_delta < 0) || (_delta > 0xFFFF))
			{
				return false;
			}
			if (_delta > delta)
			{
				delta = _delta;
			}
			price = value;
		}
	}

	if (asksNumber > 0)
	{
		if (bidsNumber > 0)
		{
			const uint32 value = m_asks.front().Price;
			const int32 _delta = (value >= price) ? static_cast<int32>(value - price) : static_cast<int32>(price - value);
			if ((_delta < 0) || (_delta > 0xFFFF))
			{
				return false;
			}
			if (_delta > delta)
			{
				delta = _delta;
			}
			price = value;
		}
		else
		{
			price = m_asks.front().Price;
		}

		for(uint32 index = 1; index < asksNumber; ++index)
		{
			const uint32 value = m_asks[index].Price;
			if (value < price)
			{
				return false;
			}
			const int32 _delta = static_cast<int32>(value - price);
			if ((_delta < 0) || (_delta > 0xFFFF))
			{
				return false;
			}
			if (_delta > delta)
			{
				delta = _delta;
			}
			price = value;
		}
	}
	m_priceSize = CalculateSize(delta);
	return true;
}
bool CSimpleEncoder::TryCalcVolumeSize()
{
	uint32 value = 0;
	for each (const auto& element in m_bids)
	{
		if (element.Volume > value)
		{
			value = element.Volume;
		}
	}
	for each (const auto& element in m_asks)
	{
		if (element.Volume > value)
		{
			value = element.Volume;
		}
	}
	m_volumeSize = CalculateSize(value);
	if (0 == m_volumeSize)
	{
		return false;
	}
	return true;
}
void CSimpleEncoder::Write(MemoryBuffer& buffer)
{
	CBitWriter stream(buffer);

	const uint32 bidsNumber = static_cast<uint32>(m_bids.size());
	const uint32 asksNumber = static_cast<uint32>(m_asks.size());

	stream.WriteUInt32(bidsNumber, 4);
	stream.WriteUInt32(asksNumber, 4);

	stream.WriteUInt32(m_priceExp, 3);
	stream.WriteInt32(m_volumeExp, 4);

	if (0 == m_priceSize)
	{
		m_priceSize = 1;
	}
	if (0 == m_volumeSize)
	{
		m_volumeSize = 1;
	}


	stream.WriteUInt32(m_priceSize - 1, 4);
	stream.WriteUInt32(m_volumeSize - 1, 5);



	uint32 price = 0;


	if (bidsNumber > 0)
	{
		{
			const CSimpleEntry& element = m_bids.front();
			price = element.Price;
			stream.WriteUInt32(price, 32);
			stream.WriteUInt32(element.Volume, m_volumeSize);
		}

		for (uint32 index = 1; index < bidsNumber; ++index)
		{
			const CSimpleEntry& element = m_bids[index];
			const uint32 value = element.Price;
			const uint32 delta = static_cast<uint32>(value - price);
			stream.WriteUInt32(delta, m_priceSize);
			stream.WriteUInt32(element.Volume, m_volumeSize);
			price = value;
		}
	}

	if (asksNumber > 0)
	{
		if (bidsNumber > 0)
		{
			const CSimpleEntry& element = m_asks.front();
			const int32 delta = element.Price - price;
			stream.WriteInt32(delta, 1 + m_priceSize);
			stream.WriteUInt32(element.Volume, m_volumeSize);
			price = element.Price;
		}
		else
		{
			const CSimpleEntry& element = m_asks.front();
			stream.WriteUInt32(price, 32);
			stream.WriteUInt32(element.Volume, m_volumeSize);
			price = element.Price;
		}

		for (uint32 index = 1; index < asksNumber; ++index)
		{
			const CSimpleEntry& element = m_asks[index];
			const uint32 value = element.Price;
			const uint32 delta = static_cast<uint32>(value - price);
			stream.WriteUInt32(delta, m_priceSize);
			stream.WriteUInt32(element.Volume, m_volumeSize);
			price = value;
		}
	}

	stream.Flush();
}
