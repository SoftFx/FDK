#include "stdafx.h"
#include "SimpleCodec.h"
#include "Client_TypesSerializer.hpp"

CSimpleCodec::CSimpleCodec() : m_receiver()
{
}
void CSimpleCodec::SetReceiver(IReceiver* pReceiver)
{
	m_receiver = pReceiver;
}
void CSimpleCodec::OnSymbolIndexMsg(const string& symbol, const uint32 index)
{
	for (; index >= m_symbols.size();)
	{
		m_symbols.push_back(string());
	}
	m_symbols[index] = symbol;
}
void CSimpleCodec::OnTimeSynchMsg(const CDateTime time)
{
	m_time = time;
}
void CSimpleCodec::OnSymbol8Time32Level2Msg(const uint8 symbolIndex, const uint32 timeOffset, MemoryBuffer& buffer)
{
	CFxEventInfo info;
	info.SendingTime = m_time + timeOffset;
	CFxQuote quote;
	quote.Symbol = m_symbols.at(symbolIndex);
	quote.CreatingTime = info.SendingTime;

	m_decoder.Decode(buffer, quote);

	m_receiver->VTick(info, quote);
}
void CSimpleCodec::OnQuoteZipRawMsg(MemoryBuffer& buffer)
{
	MemoryBuffer decompressed(CHeap::Instance());
	ZipDecompress(L"0", buffer.GetData(), buffer.GetSize(), decompressed);
	decompressed.SetPosition(0);

	CFxEventInfo info;
	CFxQuote quote = ReadQuote(decompressed);
	info.SendingTime = quote.CreatingTime;
	m_receiver->VTick(info, quote);
}
