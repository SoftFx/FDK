#ifndef __Native_FxDailyAccountSnapshotReportIterator__
#define __Native_FxDailyAccountSnapshotReportIterator__

#include "DataTrade.h"
#include "Waiter.h"

class CFxDailyAccountSnapshotReportIterator : public CFxIterator
{
public:
    CFxDailyAccountSnapshotReportIterator(const FxTimeDirection direction, const Nullable<CDateTime>& from, const Nullable<CDateTime>& to, const uint32 preferedBufferSize, CDataTrade& dataTrade);
    virtual ~CFxDailyAccountSnapshotReportIterator();
public:
    virtual int32 VTotalItems();
    virtual HRESULT VEndOfStream();
    virtual HRESULT VNext(const uint32 timeoutInMilliseconds);
    virtual void* VItem();
public:
    HRESULT Construct(const uint32 timeoutInMilliseconds);
private:
    CFxDailyAccountSnapshotReportIterator(const CFxDailyAccountSnapshotReportIterator&);
    CFxDailyAccountSnapshotReportIterator& operator = (const CFxDailyAccountSnapshotReportIterator&);
private:
    HRESULT RequestReports(const string& position, const uint32 timeoutInMilliseconds);
private:
    FxTimeDirection m_direction;
    uint32 m_preferedBufferSize;
    Nullable<CDateTime> m_from;
    Nullable<CDateTime> m_to;
    FxRef<CDataTrade> m_dataTrade;
    Waiter<CFxDailyAccountSnapshotReport> m_waiter;
private:
    bool m_isFinished;
    bool m_endOfStream;
    uint32 m_reportsNumber;
    uint32 m_reportsTotal;
    string m_position;
private:
    void* m_pointer;
    CFxDailyAccountSnapshotReport m_report;
};

#endif