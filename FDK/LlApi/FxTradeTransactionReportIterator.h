#ifndef __Native_FxTradeTransactionReportIterator__
#define __Native_FxTradeTransactionReportIterator__

#include "DataTrade.h"
#include "Waiter.h"

class CFxTradeTransactionReportIterator : public CFxIterator
{
public:
    CFxTradeTransactionReportIterator(const FxTimeDirection direction, const Nullable<CDateTime>& from, const Nullable<CDateTime>& to, const uint32 preferedBufferSize, const Nullable<bool>& skipCancel, CDataTrade& dataTrade);
    virtual ~CFxTradeTransactionReportIterator();
public:
    virtual int32 VTotalItems();
    virtual HRESULT VEndOfStream();
    virtual HRESULT VNext(const uint32 timeoutInMilliseconds);
    virtual void* VItem();
public:
    HRESULT Construct(const bool subscribe, const uint32 timeoutInMilliseconds);
private:
    CFxTradeTransactionReportIterator(const CFxTradeTransactionReportIterator&);
    CFxTradeTransactionReportIterator& operator = (const CFxTradeTransactionReportIterator&);
private:
    HRESULT RequestReports(const bool subscribe, const string& position, const Nullable<bool>& skipCancel, const uint32 timeoutInMilliseconds);
private:
    FxTimeDirection m_direction;
    uint32 m_preferedBufferSize;
    Nullable<CDateTime> m_from;
    Nullable<CDateTime> m_to;
    FxRef<CDataTrade> m_dataTrade;
    Waiter<CFxTradeTransactionReport> m_waiter;
private:
    bool m_isFinished;
    bool m_endOfStream;
    uint32 m_reportsNumber;
    uint32 m_reportsTotal;
    string m_position;
    Nullable<bool> m_skipCancel;
private:
    void* m_pointer;
    CFxTradeTransactionReport m_report;
};

#endif