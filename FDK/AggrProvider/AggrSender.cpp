#include "stdafx.h"
#include "AggrSender.h"

CAggrSender::CAggrSender()
    : m_receiver(nullptr)
    , m_feederHandler(nullptr)
    , m_bridgeClient(nullptr)
{
}

void CAggrSender::SetReceiver(IReceiver* pReceiver)
{
	m_receiver = pReceiver;
}

void CAggrSender::VSendGetCurrencies(const string& /*id*/)
{

}

void CAggrSender::VSendGetSupportedSymbols(const string& id)
{
	CFxEventInfo info;
	info.ID = id;
	vector<CFxSymbolInfo> symbols;
	m_feederHandler->FillSymbols(symbols);
	m_receiver->VGetSupportedSymbols(info, symbols);
}

void CAggrSender::VSendGetSessionInfo(const string& id)
{
	CFxEventInfo info;
	info.ID = id;
	CFxSessionInfo session;
	m_feederHandler->FillSessionInfo(session);
	m_receiver->VSessionInfo(info, session);
}

void CAggrSender::VSendGetTradeServerInfo(const string& id)
{
}

void CAggrSender::VSendGetAccountInfo(const string& id)
{
	CFxEventInfo info;
	info.ID = id;
	CFxAccountInfo account;
	account.Currency = "USD";
	account.AccountId = "0";
	account.Type = FxAccountType_Net;
	account.Leverage = 100;
	account.Balance = 1000000;
	m_receiver->VAccountInfo(info, account);
}

void CAggrSender::VSendSubscribeToQuotes(const string& id, const vector<string>& symbols, int32 depth)
{
	m_feederHandler->Subsribe(symbols, depth);

	CFxEventInfo info;
	info.ID = id;
	m_receiver->VSubscribeToQuotes(info, S_OK);
}

void CAggrSender::VSendUnsubscribeQuotes(const string& id, const vector<string>& symbols)
{
	m_feederHandler->Unsubscribe(symbols);

	CFxEventInfo info;
	info.ID = id;
	m_receiver->VSubscribeToQuotes(info, S_OK);
}

void CAggrSender::VSendOpenNewOrder(const string& id, const CFxOrder& request)
{
	m_bridgeClient->SendOrder(id, request);
}

void CAggrSender::VSendPositionReportRequest(const string& id, const string& account)
{
}

void CAggrSender::SetFeederHandler(CFeedHandler* pFeederHandler)
{
	m_feederHandler = pFeederHandler;
}

void CAggrSender::SetTrader(CBridgeClient* pBridgeClient)
{
	m_bridgeClient = pBridgeClient;
}

void CAggrSender::VSendQuotesHistoryRequest(const string& /*id*/)
{

}
