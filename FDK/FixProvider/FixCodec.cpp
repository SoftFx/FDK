#include "stdafx.h"
#include "FixCodec.h"
#include "FixLevel2.h"

CFixCodec::CFixCodec() : m_size(), m_count(), m_time()
{
}
int64 CFixCodec::GetSize()
{
	return m_size;
}
int64 CFixCodec::GetCount()
{
	return m_count;
}
double CFixCodec::GetTime()
{
	return m_time;
}
void CFixCodec::EncodeSlow(const vector<CFxQuote>& quotes)
{
	ptrdiff_t sequenceNumber = 0;
	uint64 start = FxGetTickCount();
	for each(const auto& element in quotes)
	{
		m_size += CalcSlowFixCodec(sequenceNumber++, element);
		++m_count;
	}
	uint64 finish = FxGetTickCount();
	m_time += (finish - start) / 1000.0;
}
void CFixCodec::EncodeFast(const vector<CFxQuote>& quotes)
{
	ptrdiff_t sequenceNumber = 0;
	uint64 start = FxGetTickCount();
	for each(const auto& element in quotes)
	{
		m_size += CalcFastFixCodec(sequenceNumber++, element);
		++m_count;
	}
	uint64 finish = FxGetTickCount();
	m_time += (finish - start) / 1000.0;
}
void CFixCodec::Clear()
{
	m_size = 0;
	m_count = 0;
	m_time = 0;
}
