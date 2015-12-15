#pragma once


class CLrpCodecImpl
{
public:
	static void* Constructor();
	static void Destructor(void* handle);
	static int64 GetSize(void* handle);
	static int64 GetCount(void* handle);
	static double GetTime(void* handle);
	static void EncodeRaw(void* handle, const vector<CFxQuote>& quotes);
	static void EncodeSlow(void* handle, const vector<CFxQuote>& quotes);
	static void EncodeFast(void* handle, size_t precision, double volumeStep, const vector<CFxQuote>& quotes);
	static void Clear(void* handle);
};