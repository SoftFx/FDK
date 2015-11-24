#include "stdafx.h"
#include "FeederSources.h"

namespace
{
	CFeederSources gSources;
}
CFeederSource* CFeederSources::CreateSource(const CConnectionParams& params)
{
	return gSources.DoCreateSource(params);
}
void CFeederSources::DestroySource(CFeederSource* pReceiver)
{
	gSources.DoDestroySource(pReceiver);
}
CFeederSource* CFeederSources::DoCreateSource(const CConnectionParams& params)
{
	CLock lock(m_synchronizer);
	auto it = m_connectionParamsToQuotesReceiver.find(params);
	if (m_connectionParamsToQuotesReceiver.end() != it)
	{
		it->second->Acquire();
		return it->second;
	}
	auto_ptr<CFeederSource> guard(new CFeederSource(params));
	CFeederSource* result = guard.get();
	result->Construct();
	m_connectionParamsToQuotesReceiver[params] = result;
	result->Acquire();
	guard.release();
	return result;
}
void CFeederSources::DoDestroySource(CFeederSource* pReceiver)
{
	CLock lock(m_synchronizer);
	if (pReceiver->Release())
	{
		m_connectionParamsToQuotesReceiver.erase(pReceiver->Params());
		delete pReceiver;
	}
}