#include "stdafx.h"
#include "FeederHandler.h"

CFeedHandler::CFeedHandler() : m_isLoggedOn(false)
{
}
void CFeedHandler::Construct(FeedHandlerMode mode, int32 bankCode)
{
	m_mode = mode;
	m_bankCode = bankCode;
}
void CFeedHandler::SetReceiver(IReceiver* pReceiver)
{
	m_receiver = pReceiver;
}
void CFeedHandler::VProtocolVersion(const string& protocolVersion)
{
	Delegate<void (const string&)> func(this, &CFeedHandler::OnProtocolVersion);
	func.DoAsynch(m_thread, protocolVersion);
}
void CFeedHandler::VSessionInfo(const map<int32, CFSessionInfo>& bankToSession)
{
	Delegate<void (const map<int32, CFSessionInfo>&)> func(this, &CFeedHandler::OnSessionInfo);
	func.DoAsynch(m_thread, bankToSession);
}
void CFeedHandler::VSymbolInfo(const map<int32, set<CFSymbolInfo> >& bankToSymbols)
{
	if (FeedHandlerMode_Feed == m_mode)
	{
		Delegate<void (const map<int32, set<CFSymbolInfo> >&)> func(this, &CFeedHandler::OnSymbolInfo);
		func.DoAsynch(m_thread, bankToSymbols);
	}
}
void CFeedHandler::VLogon(const set<int32>& ids)
{
	Delegate<void (const set<int32>&)> func(this, &CFeedHandler::OnLogon);
	func.DoAsynch(m_thread, ids);
}
void CFeedHandler::VAccountInfo(const shared_ptr<map<int32, CFAccountInfo> >& info)
{
	if (FeedHandlerMode_Trade == m_mode)
	{
		Delegate<void (const shared_ptr<map<int32, CFAccountInfo> >&)> func(this, &CFeedHandler::OnAccountInfo);
		func.DoAsynch(m_thread, info);
	}
}
void CFeedHandler::VPositions(const shared_ptr<map<int32, map<string, double> > >& bankToPositions)
{
	if (FeedHandlerMode_Trade == m_mode)
	{
		Delegate<void (const shared_ptr<map<int32, map<string, double> > >&)> func(this, &CFeedHandler::OnPositions);
		func.DoAsynch(m_thread, bankToPositions);
	}
}
void CFeedHandler::VTick(const shared_ptr<CFLevel2>& arg)
{
	if (FeedHandlerMode_Feed == m_mode)
	{
		Delegate<void (const shared_ptr<CFLevel2>&)> func(this, &CFeedHandler::OnTick);
		func.DoAsynch(m_thread, arg);
	}
}
void CFeedHandler::VLogout(const set<int32>& ids)
{
	Delegate<void (const set<int32>&)> func(this, &CFeedHandler::OnLogout);
	func.DoAsynch(m_thread, ids);
}
void CFeedHandler::VLogout()
{
	set<int32> ids;
	ids.insert(m_bankCode);
	VLogout(ids);
}
void CFeedHandler::OnProtocolVersion(const string& protocolVersion)
{
	CLock lock(m_synchronizer);
	m_protocolVersion = protocolVersion;
}
void CFeedHandler::OnSessionInfo(const map<int32, CFSessionInfo>& bankToSession)
{
	auto it = bankToSession.find(m_bankCode);
	if (bankToSession.end() != it)
	{
		CLock lock(m_synchronizer);
		m_sessionInfo = it->second;
	}
}
void CFeedHandler::OnSymbolInfo(const map<int32, set<CFSymbolInfo> >& bankToSymbols)
{
	auto it = bankToSymbols.find(m_bankCode);
	if (bankToSymbols.end() != it)
	{
		CLock lock(m_synchronizer);
		m_symbols = it->second;
	}
}
void CFeedHandler::OnAccountInfo(const shared_ptr<map<int32, CFAccountInfo> >& info)
{
	auto it = info->find(m_bankCode);
	if (info->end() == it)
	{
		return;
	}
	CLock lock(m_synchronizer);
	if (m_accountInfo != it->second)
	{
		m_accountInfo = it->second;
		CFxEventInfo info;
		CFxAccountInfo accountInfo;
		DoFillAccountInfo(accountInfo);
		m_receiver->VAccountInfo(info, accountInfo);
	}
}
void CFeedHandler::OnPositions(const shared_ptr<map<int32, map<string, double> > >& bankToPositions)
{
	auto it = bankToPositions->find(m_bankCode);
	if (bankToPositions->end() == it)
	{
		return;
	}
	CLock lock(m_synchronizer);
}
void CFeedHandler::OnTick(const shared_ptr<CFLevel2>& arg)
{
	CFxQuote tick;
	if (!arg->CopyTo(m_bankCode, tick))
	{
		return;
	}

	CFxQuote& quote = m_quotes[tick.Symbol];
	if (quote == tick)
	{
		return;
	}
	std::swap(quote, tick);

	CLock lock(m_synchronizer);
	if (m_symbolToMarketDepth.count(quote.Symbol) > 0)
	{
		CFxEventInfo info;
		m_receiver->VTick(info, quote);
	}
}
void CFeedHandler::OnLogon(const set<int32>& ids)
{
	if (!m_isLoggedOn)
	{
		if (ids.count(m_bankCode) > 0)
		{
			m_isLoggedOn = true;
			DoLogon();
		}
	}
}
void CFeedHandler::DoLogon()
{
	CFxEventInfo info;
	m_receiver->VLogon(info, m_protocolVersion);

	CFxSessionInfo sessionInfo;
	DoFillSessionInfo(sessionInfo);
	m_receiver->VSessionInfo(info, sessionInfo);

	if (FeedHandlerMode_Feed == m_mode)
	{
		vector<CFxSymbolInfo> symbols;
		DoFillSymbols(symbols);
		m_receiver->VGetSupportedSymbols(info, symbols);
	}
	if (FeedHandlerMode_Trade == m_mode)
	{
		CFxAccountInfo accountInfo;
		DoFillAccountInfo(accountInfo);
		m_receiver->VAccountInfo(info, accountInfo);
	}
}
void CFeedHandler::OnLogout(const set<int32>& ids)
{
	if (m_isLoggedOn)
	{
		if (ids.count(m_bankCode) > 0)
		{
			m_isLoggedOn = false;
			DoLogout();
		}
	}
}
void CFeedHandler::DoLogout()
{
	{
		CLock lock(m_synchronizer);
		m_sessionInfo = CFSessionInfo();
		m_accountInfo = CFAccountInfo();
		m_symbols.clear();
		m_quotes.clear();
	}
	CFxEventInfo info;
	m_receiver->VLogout(info, FxLogoutReason_Unknown, string());
}
void CFeedHandler::Subsribe(const vector<string>& symbols, int marketDepth)
{
	CLock lock(m_synchronizer);
	for each(const auto& element in symbols)
	{
		m_symbolToMarketDepth[element] = marketDepth;
	}
}
void CFeedHandler::Unsubscribe(const vector<string>& symbols)
{
	CLock lock(m_synchronizer);
	for each(const auto& element in symbols)
	{
		m_symbolToMarketDepth.erase(element);
	}
}
void CFeedHandler::FillSymbols(vector<CFxSymbolInfo>& symbols)
{
	CLock lock(m_synchronizer);
	DoFillSymbols(symbols);
}
void CFeedHandler::DoFillSymbols(vector<CFxSymbolInfo>& symbols)
{
	for each(const auto& element in m_symbols)
	{
		CFxSymbolInfo info;
		info.Name = element.Name;
		info.Currency = element.Currency;
		info.SettlementCurrency = element.SettlementCurrency;
		info.Color = element.Color;
		info.RoundLot = element.RoundLot;
		info.Precision = element.Precision;
		info.MinTradeVolume = element.MinTradeVolume;
		info.TradeVolumeStep = element.TradeVolumeStep;
		info.MaxTradeVolume = element.MaxTradeVolume;
		info.ContractMultiplier = pow(10.0, info.Precision);
		info.MarginCalcMode = (MarginCalcMode)0;// MarginCalcMode_FOREX;
		info.ProfitCalcMode = (ProfitCalcMode)0;//ProfitCalcMode_FOREX;
		info.MarginHedge = 0.5;
		info.MarginFactor = 100;
		symbols.push_back(info);
	}
}
void CFeedHandler::FillSessionInfo(CFxSessionInfo& info)
{
	CLock lock(m_synchronizer);
	DoFillSessionInfo(info);
}
void CFeedHandler::DoFillSessionInfo(CFxSessionInfo& info)
{
	info.StartTime = m_sessionInfo.StartTime;
	info.EndTime = m_sessionInfo.EndTime;
	info.OpenTime = m_sessionInfo.OpenTime;
	info.CloseTime = m_sessionInfo.CloseTime;
	info.ServerTimeZoneOffset = m_sessionInfo.PlatformTimezoneOffset;
}
void CFeedHandler::FillAccountInfo(CFxAccountInfo& info)
{
	CLock lock(m_synchronizer);
	DoFillAccountInfo(info);
}
void CFeedHandler::DoFillAccountInfo(CFxAccountInfo& info)
{
	info.Type = FxAccountType_Net;
	info.Leverage = 100;
	info.Balance = 1000000.0;
	info.Margin = m_accountInfo.Equity - m_accountInfo.FreeMargin;
	info.Equity = 1000000.0;
	info.Currency = "USD";
}
