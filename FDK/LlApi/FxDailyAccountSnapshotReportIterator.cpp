#include "stdafx.h"
#include "FxDailyAccountSnapshotReportIterator.h"

namespace
{
    const string cOutOfRangeError = "Out of range exception";
}

CFxDailyAccountSnapshotReportIterator::CFxDailyAccountSnapshotReportIterator(const FxTimeDirection direction, const Nullable<CDateTime>& from, const Nullable<CDateTime>& to, const uint32 preferedBufferSize, CDataTrade& dataTrade)
    : m_direction(direction)
    , m_preferedBufferSize(preferedBufferSize)
    , m_dataTrade(&dataTrade)
    , m_from(from)
    , m_to(to)
    , m_waiter(0, cExternalSynchCall, dataTrade)
    , m_isFinished(false)
    , m_endOfStream(false)
    , m_reportsNumber()
    , m_reportsTotal()
    , m_pointer(nullptr)
{
    dataTrade.Acquire();
}

CFxDailyAccountSnapshotReportIterator::~CFxDailyAccountSnapshotReportIterator()
{
}

int32 CFxDailyAccountSnapshotReportIterator::VTotalItems()
{
    return m_reportsTotal;
}

HRESULT CFxDailyAccountSnapshotReportIterator::VEndOfStream()
{
    const HRESULT result = m_isFinished ? S_OK : S_FALSE;
    return result;
}

void* CFxDailyAccountSnapshotReportIterator::VItem()
{
    if (nullptr == m_pointer)
    {
        throw runtime_error("CFxDailyAccountSnapshotReportIterator::VItem()");
    }
    return m_pointer;
}

HRESULT CFxDailyAccountSnapshotReportIterator::VNext(const uint32 timeoutInMilliseconds)
{
    if (m_isFinished)
    {
        return S_OK;
    }
    m_waiter.ResetTimeout(timeoutInMilliseconds);
    m_pointer = nullptr;

    if (0 == m_reportsNumber)
    {
        if (m_endOfStream)
        {
            m_isFinished = true;
            return S_OK;
        }
        const HRESULT result = RequestReports(m_position, timeoutInMilliseconds);
        if (FAILED(result))
        {
            return result;
        }
        if (0 == m_reportsNumber)
        {
            return S_FALSE;
        }
    }
    m_report = m_waiter.WaitForResponse();
    m_reportsNumber--;
    if (0 == m_reportsNumber)
    {
        m_position = m_report.NextStreamPositionId;
    }
    m_pointer = &m_report;
    return S_OK;
}

HRESULT CFxDailyAccountSnapshotReportIterator::RequestReports(const string& position, const uint32 timeoutInMilliseconds)
{
    Waiter<tuple<int32, int32, bool> > waiter(timeoutInMilliseconds, m_waiter, *m_dataTrade);
    ISender& sender = *m_dataTrade->m_sender;
    sender.VSendGetDailyAccountSnapshotReports(waiter.Id(), m_direction, m_from, m_to, m_preferedBufferSize, position);
    tuple<int32, int32, bool> response = waiter.WaitForResponse();

    m_reportsNumber = get<0>(response);
    if (m_reportsTotal == 0)
        m_reportsTotal = get<1>(response);
    m_endOfStream = get<2>(response);

    return S_OK;
}

HRESULT CFxDailyAccountSnapshotReportIterator::Construct(const uint32 timeoutInMilliseconds)
{
    CTimeout timeout(timeoutInMilliseconds);
    const HRESULT result = RequestReports(string(), timeoutInMilliseconds);
    if (SUCCEEDED(result))
    {
        VNext(timeout);
    }
    return result;
}
