#pragma once

class CSimpleCodec
{
public:
	CSimpleCodec();
public:
	void SetReceiver(IReceiver* pReceiver);
public:
	void OnSymbolIndexMsg(const string& symbol, const uint32 index);
	void OnTimeSynchMsg(const CDateTime time);
	void OnSymbol8Time32Level2Msg(const uint8 symbolIndex, const uint32 timeOffset, MemoryBuffer& buffer);
	void OnQuoteZipRawMsg(MemoryBuffer& buffer);
private:
	IReceiver* m_receiver;
	CDateTime m_time;
	vector<string> m_symbols;
private:
	CSimpleDecoder m_decoder;
};