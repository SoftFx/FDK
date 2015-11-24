#include "stdafx.h"
#include "SimpleDecoder.h"




void CSimpleDecoder::Decode(MemoryBuffer& buffer, CFxQuote& quote)
{
	CBitReader reader(buffer);

	const uint32 bidsNumber = reader.ReadUInt32(4);
	const uint32 asksNumber = reader.ReadUInt32(4);

	quote.Bids.clear();
	quote.Bids.reserve(bidsNumber);
	quote.Asks.clear();
	quote.Asks.reserve(asksNumber);


	const double priceExponent = reader.ReadUInt32(3);
	const double volumeExponent = reader.ReadInt32(4);

	const double priceDivisor = pow(10, priceExponent);
	const double volumeFactor = pow(10, volumeExponent);


	const uint32 priceSize = reader.ReadUInt32(4) + 1;
	const uint32 volumeSize = reader.ReadUInt32(5) + 1;

	uint32 price = 0;

	if (bidsNumber > 0)
	{
		{
			price = reader.ReadUInt32(32);
			uint32 volume = reader.ReadUInt32(volumeSize);

			quote.Bids.push_back(CFxQuoteEntry(price / priceDivisor, volume * volumeFactor));
		}

		for (uint32 index = 1; index < bidsNumber; ++index)
		{
			const uint32 priceDelta = reader.ReadUInt32(priceSize);
			price = price + priceDelta;
			const uint32 volume = reader.ReadUInt32(volumeSize);

			quote.Bids.push_back(CFxQuoteEntry(price / priceDivisor, volume * volumeFactor));
		}
	}

	if (asksNumber > 0)
	{
		if (bidsNumber > 0)
		{
			const int32 priceDelta = reader.ReadInt32(1 + priceSize);
			price = price + priceDelta;
			const uint32 volume = reader.ReadUInt32(volumeSize);

			quote.Asks.push_back(CFxQuoteEntry(price / priceDivisor, volume * volumeFactor));
		}
		else
		{
			price = reader.ReadUInt32(32);
			uint32 volume = reader.ReadUInt32(volumeSize);

			quote.Asks.push_back(CFxQuoteEntry(price / priceDivisor, volume * volumeFactor));
		}

		for(uint32 index = 1; index < asksNumber; ++index)
		{
			const uint32 priceDelta = reader.ReadUInt32(priceSize);
			price = price + priceDelta;
			const uint32 volume = reader.ReadUInt32(volumeSize);

			quote.Asks.push_back(CFxQuoteEntry(price / priceDivisor, volume * volumeFactor));
		}
	}
	std::reverse(quote.Bids.begin(), quote.Bids.end());

}
