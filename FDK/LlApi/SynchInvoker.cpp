#include "stdafx.h"
#include "Waiter.h"
#include "SynchInvoker.h"

CSynchInvoker::CSynchInvoker()
{
}

void CSynchInvoker::RegisterWaiter(const type_info& info, const string& id, IWaiter* pWaiter)
{
	Key key(id, info.name());
	CLock lock(m_synchronizer);
	assert(m_key2Waiter.end() == m_key2Waiter.find(key));
	m_key2Waiter[key] = pWaiter;
}

void CSynchInvoker::ReleaseWaiter(const type_info& info, const string& id)
{
	Key key(id, info.name());
	CLock lock(m_synchronizer);
	assert(m_key2Waiter.end() != m_key2Waiter.find(key));
	m_key2Waiter.erase(key);
}

void CSynchInvoker::Response(const CFxEventInfo& eventInfo)
{
	const string& id = eventInfo.ID;
	Key eventKey(id, string());
	CLock lock(m_synchronizer);
	auto it = m_key2Waiter.lower_bound(eventKey);
	for (; m_key2Waiter.end() != it; ++it)
	{
		const Key& key = it->first;
		if (key.first == id)
		{
			break;
		}
		IWaiter* pWaiter = it->second;
		pWaiter->VResponse(eventInfo);
	}
}

void CSynchInvoker::Response(const type_info& info, const CFxEventInfo& eventInfo, void* pData)
{
	Key key(eventInfo.ID, info.name());
	CLock lock(m_synchronizer);
	auto it = m_key2Waiter.find(key);
	if (m_key2Waiter.end() != it)
	{
		IWaiter* pWaiter = it->second;
		pWaiter->VResponse(eventInfo, pData);
	}
}

void CSynchInvoker::Disconnect()
{
	CLock lock(m_synchronizer);
	auto it = m_key2Waiter.begin();
	auto end = m_key2Waiter.end();
	for (; it != end; ++it)
	{
		IWaiter* pWaiter = it->second;
		pWaiter->Disconnect();
	}
}
