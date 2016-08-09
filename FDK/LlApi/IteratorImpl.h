#pragma once

class CIteratorImpl
{
public:
    int TotalItems(void* handle);
    bool EndOfStream(void* handle);
    void Next(void* handle, size_t timeoutInMilliseconds);
    CFxTradeTransactionReport GetTradeTransactionReport(void* handle);
};
