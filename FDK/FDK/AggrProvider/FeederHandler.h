#pragma once
#include "IFeederHandler.h"

enum FeedHandlerMode
{
	FeedHandlerMode_None = 0,
	FeedHandlerMode_Feed = 1,
	FeedHandlerMode_Trade = 2
};


class CFeedHandler : public IFeederHandler
{
public:
	CFeedHandler();
	//virtual ~CFeedHandler();
public:
	void Construct(FeedHandlerMode mode, int32 bankCode);
	//void Finalize();
public:
	void SetReceiver(IReceiver* pReceiver);
	void Subsribe(const vector<string>& symbols, int marketDepth);
	void Unsubscribe(const vector<string>& symbols);
public:
	virtual void VProtocolVersion(const string& protocolVersion);
	virtual void VSessionInfo(const map<int32, CFSessionInfo>& bankToSession);
	virtual void VSymbolInfo(const map<int32, set<CFSymbolInfo> >& bankToSymbols);
	virtual void VAccountInfo(const shared_ptr<map<int32, CFAccountInfo> >& info);
	virtual void VPositions(const shared_ptr<map<int32, map<string, double> > >& bankToPositions);
	virtual void VTick(const shared_ptr<CFLevel2>& arg);
	virtual void VLogon(const set<int32>& ids);
	virtual void VLogout();
	virtual void VLogout(const set<int32>& ids);
private:
	void OnProtocolVersion(const string& protocolVersion);
	void OnSessionInfo(const map<int32, CFSessionInfo>& bankToSession);
	void OnSymbolInfo(const map<int32, set<CFSymbolInfo> >& bankToSymbols);
	void OnLogon(const set<int32>& ids);
	void OnAccountInfo(const shared_ptr<map<int32, CFAccountInfo> >& info);
	void OnPositions(const shared_ptr<map<int32, map<string, double> > >& bankToPositions);
	void OnTick(const shared_ptr<CFLevel2>& arg);
	void OnLogout(const set<int32>& ids);
private:
	void DoLogon();
	void DoLogout();
public:
	void FillSymbols(vector<CFxSymbolInfo>& symbols);
	void FillSessionInfo(CFxSessionInfo& info);
	void FillAccountInfo(CFxAccountInfo& info);
private:
	void DoFillSymbols(vector<CFxSymbolInfo>& symbols);
	void DoFillSessionInfo(CFxSessionInfo& info);
	void DoFillAccountInfo(CFxAccountInfo& info);
private:
	void RaiseTick(const CFLevel2& level2, const CFxQuote& quote);
private:
	FeedHandlerMode m_mode;
	IReceiver* m_receiver;
	int32 m_bankCode;
private:
	CThreadPool m_thread;
	CCriticalSection m_synchronizer;
private:
	bool m_isLoggedOn;
	string m_protocolVersion;
	CFSessionInfo m_sessionInfo;
	CFAccountInfo m_accountInfo;
	set<CFSymbolInfo> m_symbols;
	map<string, CFxQuote> m_quotes;
private:
	map<string, int32> m_symbolToMarketDepth;
};


