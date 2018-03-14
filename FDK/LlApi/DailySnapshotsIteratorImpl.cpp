#include "stdafx.h"
#include "DailySnapshotsIteratorImpl.h"
#include "FxDailyAccountSnapshotReportIterator.h"

namespace
{
    FxRef<CFxDailyAccountSnapshotReportIterator> IteratorFromHandle(void* handle)
    {
        if (nullptr == handle)
        {
            throw CArgumentNullException("handle can not be null");
        }
        FxRef<CFxDailyAccountSnapshotReportIterator> result = TypeFromHandle<CFxDailyAccountSnapshotReportIterator>(handle);
        if (!result)
        {
            throw CInvalidHandleException("invalid handle");
        }
        return result;
    }
}
int CDailySnapshotsIteratorImpl::TotalItems(void* handle)
{
    FxRef<CFxDailyAccountSnapshotReportIterator> iterator = IteratorFromHandle(handle);
    return iterator->VTotalItems();
}
bool CDailySnapshotsIteratorImpl::EndOfStream(void* handle)
{
    FxRef<CFxDailyAccountSnapshotReportIterator> iterator = IteratorFromHandle(handle);
    const HRESULT status = iterator->VEndOfStream();
    const bool result = (S_OK == status);
    return result;
}
void CDailySnapshotsIteratorImpl::Next(void* handle, size_t timeoutInMilliseconds)
{
    FxRef<CFxDailyAccountSnapshotReportIterator> iterator = IteratorFromHandle(handle);
    const HRESULT status = iterator->VNext(static_cast<uint32>(timeoutInMilliseconds));
    if (FAILED(status))
    {
        throw runtime_error("Couldn't mote the iterator to the next element");
    }
}
CFxDailyAccountSnapshotReport CDailySnapshotsIteratorImpl::GetDailyAccountSnapshotReport(void* handle)
{
    FxRef<CFxDailyAccountSnapshotReportIterator> iterator = IteratorFromHandle(handle);
    void* item = iterator->VItem();
    if (nullptr == item)
    {
        throw runtime_error("Couldn't get the current item");
    }
    CFxDailyAccountSnapshotReport* pReport = reinterpret_cast<CFxDailyAccountSnapshotReport*>(item);
    return *pReport;
}
