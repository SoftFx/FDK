#pragma once


class CFixCodec
{
public:
	CFixCodec();
public:
	int64 GetSize();
	int64 GetCount();
	double GetTime();
	void EncodeSlow(const vector<CFxQuote>& quotes);
	void EncodeFast(const vector<CFxQuote>& quotes);
	void Clear();
private:
	int64 m_size;
	int64 m_count;
	double m_time;
};