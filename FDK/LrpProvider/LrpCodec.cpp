#include "stdafx.h"
#include "LrpCodec.h"
#include "Local_Server.h"
#include "../LrpCommon/SimpleEncoder2.h"

CLrpCodec::CLrpCodec() : m_size(), m_count(), m_time()
{
}
int64 CLrpCodec::GetSize()
{
	return m_size;
}
int64 CLrpCodec::GetCount()
{
	return m_count;
}
double CLrpCodec::GetTime()
{
	return m_time;
}

namespace
{
	bool Equals(const vector<CFxQuoteEntry>& first, const vector<CFxQuoteEntry>& second)
	{
		const size_t count = first.size();
		if (count != second.size())
		{
			return false;
		}

		for (size_t index = 0; index < count; ++index)
		{
			const CFxQuoteEntry& f = first[index];
			const CFxQuoteEntry& s = second[index];
			if (abs(f.Price - s.Price) > FLT_EPSILON)
			{
				return false;
			}
			if (abs(f.Volume - s.Volume) > FLT_EPSILON * max(f.Volume, s.Volume))
			{
				return false;
			}
		}
		return true;
	}
	bool Equals(const CFxQuote& first, const CFxQuote& second)
	{
		bool result = Equals(first.Bids, second.Bids) && Equals(first.Asks, second.Asks);
		return result;
	}
	bool ValidateCodec(const CFxQuote& quote, MemoryBuffer& buffer)
	{
		CSimpleDecoder decoder;

		MemoryBuffer buffer2(nullptr, buffer.GetData(), buffer.GetSize(), buffer.GetSize());

		CFxQuote quote2;
		decoder.Decode(buffer2, quote2);

		if (Equals(quote, quote2))
		{
			return true;
		}

		return false;
	}

}


void CLrpCodec::EncodeRaw(const vector<CFxQuote>& quotes)
{
	ptrdiff_t sequenceNumber = 0;
	uint64 start = FxGetTickCount();

	for (size_t index = 0; index < 256; ++index)
	{
		for each(const auto& element in quotes)
		{
			MemoryBuffer buffer(512);
			WriteQuoteToMemoryBuffer(element, buffer);
			++m_count;
			m_size += buffer.GetSize();
		}
	}
	uint64 finish = FxGetTickCount();
	m_time += (finish - start) / 1000.0;
}
void CLrpCodec::EncodeSlow(const vector<CFxQuote>& quotes)
{
	uint64 start = FxGetTickCount();
	for (size_t index = 0; index < 256; ++index)
	{
		for each(const auto& element in quotes)
		{
			MemoryBuffer buffer(512);
			if (m_encoder.TryEncode(element, buffer))
			{
				m_size += buffer.GetSize();
				++m_count;
				ValidateCodec(element, buffer);
			}
		}
	}
	uint64 finish = FxGetTickCount();
	m_time += (finish - start) / 1000.0;
}
void CLrpCodec::EncodeFast(size_t precision, double volumeStep, const vector<CFxQuote>& quotes)
{
	CSimpleEncoder2 encoder(precision, volumeStep);

	uint64 start = FxGetTickCount();
	for (size_t index = 0; index < 256; ++index)
	{
		for each(const auto& element in quotes)
		{
			MemoryBuffer buffer(512);
			if (encoder.TryEncode(element, buffer))
			{
				m_size += buffer.GetSize();
				++m_count;
			}
		}
	}
	uint64 finish = FxGetTickCount();
	m_time += (finish - start) / 1000.0;
}
void CLrpCodec::Clear()
{
	m_size = 0;
	m_count = 0;
	m_time = 0;
}
