#include "stdafx.h"
#include "QuotesSender.h"
#include "Channel.h"


namespace
{
	#include "Client_TypesSerializer.hpp"
	#include "SimpleCodec.hpp"
	#include "Client.hpp"
}

CQuotesSender::CQuotesSender(CChannel& owner) : m_owner(owner), m_outgoing(owner.Outgoing()), m_enableCodec(false),
												m_smallCodecSupported(false), m_largeCodecSupported(false), m_validateCodec(false)
{
	const CParameters& params = owner.GetParameters();
	m_validateCodec = params.ValidateCodec;
	m_symbols.max_load_factor(0.1F);
}
void CQuotesSender::Initialize()
{
	const CParameters& params = m_owner.GetParameters();
	SimpleCodec codec(m_outgoing);
	m_enableCodec = params.EnableCodec;
	m_smallCodecSupported = codec.Is_OnSymbol8Time32Level2Msg_Supported() && codec.Is_OnTimeSynchMsg_Supported() && codec.Is_OnSymbolIndexMsg_Supported();
	m_largeCodecSupported = codec.Is_OnQuoteZipRawMsg_Supported();
}
void CQuotesSender::SendQuote(const CFxQuote& quote)
{
	if (!TrySendThroughSimpleCodec(quote))
	{
		const ptrdiff_t key = KeyFromSymbol(quote.Symbol);
		m_outgoing.SendQuote(key, quote);
	}
}
bool CQuotesSender::TrySendThroughSimpleCodec(const CFxQuote& quote)
{
	if (!m_enableCodec)
	{
		return false;
	}
	if (!TrySendAsSmall(quote))
	{
		return false;
	}
	const bool result = TrySendAsLarge(quote);
	return result;
}
bool CQuotesSender::TrySendAsSmall(const CFxQuote &quote)
{
	if (!m_smallCodecSupported)
	{
		return false;
	}
	MemoryBuffer buffer;
	m_outgoing.Initialize(buffer);
	ptrdiff_t key = -1;

	CLock lock(m_synchronizer);
	key = DoKeyFromSymbol(quote.Symbol);
	if (key > numeric_limits<uint8>::max())
	{
		return false;
	}

	WriteUInt8(static_cast<uint8>(key), buffer);
	int32 timeOffset = DoTimeSynch(quote.CreatingTime);
	WriteInt32(timeOffset, buffer);

	bool result = m_encoder.TryEncode(quote, buffer);

	if (result && m_validateCodec)
	{
		result = ValidateCodec(quote, buffer);
	}
	if (result)
	{
		const HRESULT _status = m_outgoing.DoInvoke(key, SimpleCodec::LrpComponentId, SimpleCodec::LrpMethod_OnSymbol8Time32Level2Msg_Id, buffer);
		Throw(_status, buffer);
	}
	return result;
}
bool CQuotesSender::TrySendAsLarge(const CFxQuote& quote)
{
	if (!m_largeCodecSupported)
	{
		return false;
	}
	MemoryBuffer buffer;
	m_outgoing.Initialize(buffer);

	MemoryBuffer stream(CHeap::Instance());


	ptrdiff_t key = -1;

	{
		CLock lock(m_synchronizer);
		key = DoKeyFromSymbol(quote.Symbol);
	}
	
	WriteQuote(quote, stream);
	ZipCompress(L"0", stream.GetData(), stream.GetSize(), buffer);

	const HRESULT _status = m_outgoing.DoInvoke(key, SimpleCodec::LrpComponentId, SimpleCodec::LrpMethod_OnQuoteZipRawMsg_Id, buffer);
	Throw(_status, buffer);

	return true;
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
}
bool CQuotesSender::ValidateCodec(const CFxQuote& quote, MemoryBuffer& buffer)
{
	CSimpleDecoder decoder;

	MemoryBuffer buffer2(nullptr, buffer.GetData(), buffer.GetSize(), buffer.GetSize());
	buffer2.SetPosition(sizeof(uint32) + sizeof(uint8) + sizeof(int32));

	CFxQuote quote2;
	decoder.Decode(buffer2, quote2);

	if (Equals(quote, quote2))
	{
		return true;
	}
	m_enableCodec = false;

	std::stringstream stream;
	FormatQuote(quote, stream);
	string st = stream.str();

	CLogStream()<<"CQuotesSender::ValidateCodec(): codec error for "<<st>>m_owner.GetLogger();

	return false;
}
ptrdiff_t CQuotesSender::KeyFromSymbol(const string& symbol)
{
	CLock lock(m_synchronizer);
	return DoKeyFromSymbol(symbol);
}
ptrdiff_t CQuotesSender::DoKeyFromSymbol(const string& symbol)
{
	auto it = m_symbols.find(symbol);
	if (m_symbols.end() != it)
	{
		return it->second;
	}

	ptrdiff_t result = static_cast<ptrdiff_t>(m_symbols.size());
	m_symbols[symbol] = result;

	if (m_enableCodec)
	{
		SimpleCodec codec(m_outgoing);
		codec.OnSymbolIndexMsg(symbol, static_cast<unsigned>(result));
	}
	return result;
}
int32 CQuotesSender::DoTimeSynch(const CDateTime now)
{
	int64 delta = now - m_lastTime;

	if ((numeric_limits<int32>::max() > delta) && (delta > numeric_limits<int32>::min()))
	{
		return static_cast<int32>(delta);
	}
	SimpleCodec codec(m_outgoing);
	codec.OnTimeSynchMsg(now);
	m_lastTime = now;
	return 0;
}




