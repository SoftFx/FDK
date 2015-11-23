#pragma once

class CIteratorImpl
{
public:
	bool EndOfStream(void* handle);
	void Next(void* handle, size_t timeoutInMilliseconds);
	CFxTradeTransactionReport GetTradeTransactionReport(void* handle);
};

