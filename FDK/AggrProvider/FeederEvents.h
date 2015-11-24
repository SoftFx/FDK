#pragma once
#include "IFeederHandler.h"

class CFeederEvents
{
public:
	void operator += (IFeederHandler* pHandler);
	void operator -= (IFeederHandler* pHandler);
public:
	void RaiseLogon(const set<int32>& ids);
	void RaiseProtocolVersion(const string& protocolVersion);
	void RaiseSessionInfo(const map<int32, CFSessionInfo>& bankToSession);
	void RaiseSymbolInfo(const map<int32, set<CFSymbolInfo> >& bankToSymbols);
	void RaiseAccountInfo(const shared_ptr<map<int32, CFAccountInfo> >& accounts);
	void RaisePositions(const shared_ptr<map<int32, map<string, double> > >& bankToPositions);
	void RaiseTick(const shared_ptr<CFLevel2>& arg);
	void RaiseLogout(const set<int32>& ids);
private:
	void RaiseAdd(IFeederHandler* pHandler);
private:
	CCriticalSection m_synchronizer;
	set<IFeederHandler*> m_handlers;
private:
	string m_protocolVersion;
	set<int32> m_banks;
	shared_ptr<map<int32, CFAccountInfo> > m_accounts;
	shared_ptr<map<int32, map<string, double> > > m_positions;
	map<int32, CFSessionInfo> m_sessionsInfo;
	map<int32, set<CFSymbolInfo> > m_symbolsInfo;
	map<string, shared_ptr<CFLevel2> > m_quotes;
};