#pragma once


class CFixCodecImpl
{
public:
	static void* Constructor();
	static void Destructor(void* handle);
	static int64 GetSize(void* handle);
	static int64 GetCount(void* handle);
	static double GetTime(void* handle);
	static void EncodeSlow(void* handle, const vector<CFxQuote>& quotes);
	static void EncodeFast(void* handle, const vector<CFxQuote>& quotes);
	static void Clear(void* handle);
};