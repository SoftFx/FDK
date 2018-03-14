#include "stdafx.h"
#include "TradeHistoryIteratorImpl.h"
#include "FxTradeTransactionReportIterator.h"

namespace
{
    FxRef<CFxTradeTransactionReportIterator> IteratorFromHandle(void* handle)
    {
        if (nullptr == handle)
        {
            throw CArgumentNullException("handle can not be null");
        }
        FxRef<CFxTradeTransactionReportIterator> result = TypeFromHandle<CFxTradeTransactionReportIterator>(handle);
        if (!result)
        {
            throw CInvalidHandleException("invalid handle");
        }
        return result;
    }
}
int CTradeHistoryIteratorImpl::TotalItems(void* handle)
{
    FxRef<CFxTradeTransactionReportIterator> iterator = IteratorFromHandle(handle);
    return iterator->VTotalItems();
}
bool CTradeHistoryIteratorImpl::EndOfStream(void* handle)
{
    FxRef<CFxTradeTransactionReportIterator> iterator = IteratorFromHandle(handle);
    const HRESULT status = iterator->VEndOfStream();
    const bool result = (S_OK == status);
    return result;
}
void CTradeHistoryIteratorImpl::Next(void* handle, size_t timeoutInMilliseconds)
{
    FxRef<CFxTradeTransactionReportIterator> iterator = IteratorFromHandle(handle);
    const HRESULT status = iterator->VNext(static_cast<uint32>(timeoutInMilliseconds));
    if (FAILED(status))
    {
        throw runtime_error("Couldn't mote the iterator to the next element");
    }
}
CFxTradeTransactionReport CTradeHistoryIteratorImpl::GetTradeTransactionReport(void* handle)
{
    FxRef<CFxTradeTransactionReportIterator> iterator = IteratorFromHandle(handle);
    void* item = iterator->VItem();
    if (nullptr == item)
    {
        throw runtime_error("Couldn't get the current item");
    }
    CFxTradeTransactionReport* pReport = reinterpret_cast<CFxTradeTransactionReport*>(item);
    return *pReport;
}
