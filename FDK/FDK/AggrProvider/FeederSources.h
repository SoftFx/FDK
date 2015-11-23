#pragma once
#include "FeederSource.h"

class CFeederSources
{
public:
	static CFeederSource* CreateSource(const CConnectionParams& params);
	static void DestroySource(CFeederSource* pReceiver);
private:
	CFeederSource* DoCreateSource(const CConnectionParams& params);
	void DoDestroySource(CFeederSource* pReceiver);	
private:
	CCriticalSection m_synchronizer;
	map<CConnectionParams, CFeederSource*> m_connectionParamsToQuotesReceiver;
};