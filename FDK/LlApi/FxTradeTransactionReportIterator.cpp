#include "stdafx.h"
#include "FxTradeTransactionReportIterator.h"

namespace
{
	const string cOutOfRangeError = "Out of range exception";
}

CFxTradeTransactionReportIterator::CFxTradeTransactionReportIterator(const FxTimeDirection direction, const Nullable<CDateTime>& from, const Nullable<CDateTime>& to, const uint32 preferedBufferSize, CDataTrade& dataTrade)
    : m_direction(direction)
    , m_preferedBufferSize(preferedBufferSize)
    , m_dataTrade(&dataTrade)
    , m_from(from)
    , m_to(to)
    , m_waiter(0, cExternalSynchCall, dataTrade)
    , m_isFinished(false)
    , m_endOfStream(false)
    , m_reportsNumber()
    , m_pointer(nullptr)
{
	dataTrade.Acquire();
}

CFxTradeTransactionReportIterator::~CFxTradeTransactionReportIterator()
{
}

HRESULT CFxTradeTransactionReportIterator::VEndOfStream()
{
	const HRESULT result = m_isFinished ? S_OK : S_FALSE;
	return result;
}

void* CFxTradeTransactionReportIterator::VItem()
{
	if (nullptr == m_pointer)
	{
		throw runtime_error("CFxTradeTransactionReportIterator::VItem()");
	}
	return m_pointer;
}

HRESULT CFxTradeTransactionReportIterator::VNext(const uint32 timeoutInMilliseconds)
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
		const HRESULT result = RequestReports(false, m_position, timeoutInMilliseconds);
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

HRESULT CFxTradeTransactionReportIterator::RequestReports(const bool subscribe, const string& position, const uint32 timeoutInMilliseconds)
{
	Waiter<pair<int32, bool> > waiter(timeoutInMilliseconds, m_waiter, *m_dataTrade);
	ISender& sender = m_dataTrade->Sender();
	sender.VSendGetTradeTransactionReportsAndSubscribeToNotifications(waiter.Id(), m_direction, subscribe, m_from, m_to, m_preferedBufferSize, position);
	pair<int32, bool> response = waiter.WaitForResponse();

	m_reportsNumber = response.first;
	m_endOfStream = response.second;
	return S_OK;
}

HRESULT CFxTradeTransactionReportIterator::Construct(const bool subscribe, const uint32 timeoutInMilliseconds)
{
	CTimeout timeout(timeoutInMilliseconds);
	const HRESULT result = RequestReports(subscribe, string(), timeoutInMilliseconds);
	if (SUCCEEDED(result))
	{
		VNext(timeout);
	}
	return result;
}
