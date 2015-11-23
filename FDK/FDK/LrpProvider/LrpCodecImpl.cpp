#include "stdafx.h"
#include "LrpCodecImpl.h"
#include "LrpCodec.h"

void* CLrpCodecImpl::Constructor()
{
	CLrpCodec* result = new CLrpCodec();
	return result;
}
void CLrpCodecImpl::Destructor(void* handle)
{
	CLrpCodec* pFixCodec = reinterpret_cast<CLrpCodec*>(handle);
	delete pFixCodec;
}
int64 CLrpCodecImpl::GetSize(void* handle)
{
	CLrpCodec* pFixCodec = reinterpret_cast<CLrpCodec*>(handle);
	return pFixCodec->GetSize();
}
int64 CLrpCodecImpl::GetCount(void* handle)
{
	CLrpCodec* pFixCodec = reinterpret_cast<CLrpCodec*>(handle);
	return pFixCodec->GetCount();
}
double CLrpCodecImpl::GetTime(void* handle)
{
	CLrpCodec* pFixCodec = reinterpret_cast<CLrpCodec*>(handle);
	return pFixCodec->GetTime();
}
void CLrpCodecImpl::EncodeRaw(void* handle, const vector<CFxQuote>& quotes)
{
	CLrpCodec* pFixCodec = reinterpret_cast<CLrpCodec*>(handle);
	pFixCodec->EncodeRaw(quotes);
}
void CLrpCodecImpl::EncodeSlow(void* handle, const vector<CFxQuote>& quotes)
{
	CLrpCodec* pFixCodec = reinterpret_cast<CLrpCodec*>(handle);
	pFixCodec->EncodeSlow(quotes);
}
void CLrpCodecImpl::EncodeFast(void* handle, size_t precision, double volumeStep, const vector<CFxQuote>& quotes)
{
	CLrpCodec* pFixCodec = reinterpret_cast<CLrpCodec*>(handle);
	pFixCodec->EncodeFast(precision, volumeStep, quotes);
}
void CLrpCodecImpl::Clear(void* handle)
{
	CLrpCodec* pFixCodec = reinterpret_cast<CLrpCodec*>(handle);
	pFixCodec->Clear();
}
