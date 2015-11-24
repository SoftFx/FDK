#pragma once


class CLrpCodec
{
public:
	CLrpCodec();
public:
	int64 GetSize();
	int64 GetCount();
	double GetTime();
	void EncodeRaw(const vector<CFxQuote>& quotes);
	void EncodeSlow(const vector<CFxQuote>& quotes);
	void EncodeFast(size_t precision, double volumeStep, const vector<CFxQuote>& quotes);
	void Clear();
private:
	int64 m_size;
	int64 m_count;
	double m_time;
private:
	CSimpleEncoder m_encoder;
};