#include "stdafx.h"
#include "FeederEvents.h"

void CFeederEvents::operator += (IFeederHandler* pHandler)
{
	CLock lock(m_synchronizer);
	if (0 == m_handlers.count(pHandler))
	{
		m_handlers.insert(pHandler);
		RaiseAdd(pHandler);
	}
}
void CFeederEvents::RaiseAdd(IFeederHandler* pHandler)
{
	pHandler->VProtocolVersion(m_protocolVersion);
	pHandler->VSessionInfo(m_sessionsInfo);
	pHandler->VSymbolInfo(m_symbolsInfo);
	pHandler->VLogon(m_banks);
	for each(const auto& element in m_quotes)
	{
		pHandler->VTick(element.second);
	}
}
void CFeederEvents::operator -= (IFeederHandler* pHandler)
{
	CLock lock(m_synchronizer);
	auto it = m_handlers.find(pHandler);
	if (m_handlers.end() != it)
	{
		m_handlers.erase(it);
		pHandler->VLogout();
	}
}
void CFeederEvents::RaiseLogon(const set<int32>& ids)
{
	CLock lock(m_synchronizer);
	for each(const auto& element in ids)
	{
		m_banks.insert(element);
	}
	for each(auto element in m_handlers)
	{
		element->VLogon(ids);
	}
}
void CFeederEvents::RaiseProtocolVersion(const string& protocolVersion)
{
	CLock lock(m_synchronizer);
	m_protocolVersion = protocolVersion;
	for each(auto element in m_handlers)
	{
		element->VProtocolVersion(protocolVersion);
	}
}
void CFeederEvents::RaiseSessionInfo(const map<int32, CFSessionInfo>& bankToSession)
{
	CLock lock(m_synchronizer);
	m_sessionsInfo = bankToSession;
	for each(auto element in m_handlers)
	{
		element->VSessionInfo(bankToSession);
	}
}
void CFeederEvents::RaiseSymbolInfo(const map<int32, set<CFSymbolInfo> >& bankToSymbols)
{
	CLock lock(m_synchronizer);
	m_symbolsInfo = bankToSymbols;
	for each(auto element in m_handlers)
	{
		element->VSymbolInfo(bankToSymbols);
	}
}
void CFeederEvents::RaiseAccountInfo(const shared_ptr<map<int32, CFAccountInfo> >& accounts)
{
	CLock lock(m_synchronizer);
	m_accounts = accounts;
	for each(auto element in m_handlers)
	{
		element->VAccountInfo(accounts);
	}
}
void CFeederEvents::RaisePositions(const shared_ptr<map<int32, map<string, double> > >& bankToPositions)
{
	CLock lock(m_synchronizer);
	m_positions = bankToPositions;
	for each(auto element in m_handlers)
	{
		element->VPositions(bankToPositions);
	}
}
void CFeederEvents::RaiseTick(const shared_ptr<CFLevel2>& arg)
{
	CLock lock(m_synchronizer);
	m_quotes[arg->Symbol] = arg;
	for each(auto element in m_handlers)
	{
		element->VTick(arg);
	}
}
void CFeederEvents::RaiseLogout(const set<int32>& ids)
{
	CLock lock(m_synchronizer);
	for each(const auto& element in ids)
	{
		m_banks.erase(element);
	}
	for each(auto element in m_handlers)
	{
		element->VLogout(ids);
	}
}
