#pragma once

class CChannel;
class COutgoing;

class CQuotesSender
{
public:
	CQuotesSender(CChannel& owner);
private:
	CQuotesSender(const CQuotesSender&);
	CQuotesSender& operator = (const CQuotesSender&);
public:
	void Initialize();
	void SendQuote(const CFxQuote& quote);
private:
	ptrdiff_t KeyFromSymbol(const string& symbol);
	ptrdiff_t DoKeyFromSymbol(const string& symbol);
	int32 DoTimeSynch(const CDateTime now);
	bool TrySendThroughSimpleCodec(const CFxQuote& quote);
	bool TrySendAsSmall(const CFxQuote& quote);
	bool TrySendAsLarge(const CFxQuote& quote);
	bool ValidateCodec(const CFxQuote& quote, MemoryBuffer& buffer);
private:
	CChannel& m_owner;
	COutgoing& m_outgoing;
	bool m_enableCodec;
	bool m_smallCodecSupported;
	bool m_largeCodecSupported;
	bool m_validateCodec;
private:
	CCriticalSection m_synchronizer;
	CDateTime m_lastTime;
    unordered_map<string, size_t> m_symbols;
	CSimpleEncoder m_encoder;
};