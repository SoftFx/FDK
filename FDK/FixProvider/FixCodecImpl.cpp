#include "stdafx.h"
#include "FixCodecImpl.h"
#include "FixCodec.h"

void* CFixCodecImpl::Constructor()
{
	CFixCodec* result = new CFixCodec();
	return result;
}
void CFixCodecImpl::Destructor(void* handle)
{
	CFixCodec* pFixCodec = reinterpret_cast<CFixCodec*>(handle);
	delete pFixCodec;
}
int64 CFixCodecImpl::GetSize(void* handle)
{
	CFixCodec* pFixCodec = reinterpret_cast<CFixCodec*>(handle);
	return pFixCodec->GetSize();
}
int64 CFixCodecImpl::GetCount(void* handle)
{
	CFixCodec* pFixCodec = reinterpret_cast<CFixCodec*>(handle);
	return pFixCodec->GetCount();
}
double CFixCodecImpl::GetTime(void* handle)
{
	CFixCodec* pFixCodec = reinterpret_cast<CFixCodec*>(handle);
	return pFixCodec->GetTime();
}
void CFixCodecImpl::EncodeSlow(void* handle, const vector<CFxQuote>& quotes)
{
	CFixCodec* pFixCodec = reinterpret_cast<CFixCodec*>(handle);
	pFixCodec->EncodeSlow(quotes);
}
void CFixCodecImpl::EncodeFast(void* handle, const vector<CFxQuote>& quotes)
{
	CFixCodec* pFixCodec = reinterpret_cast<CFixCodec*>(handle);
	pFixCodec->EncodeFast(quotes);
}
void CFixCodecImpl::Clear(void* handle)
{
	CFixCodec* pFixCodec = reinterpret_cast<CFixCodec*>(handle);
	pFixCodec->Clear();
}
