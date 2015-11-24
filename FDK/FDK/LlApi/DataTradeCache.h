#ifndef __Native_Data_Trade_Cache__
#define __Native_Data_Trade_Cache__

#include "DataCache.h"
#include "FxQueue.h"


class CDataTradeCache;

typedef void TradeCacheChangedSignature(const CDataTradeCache&);
typedef Delegate<TradeCacheChangedSignature> TradeCacheChangedHandler;

class CDataTradeCache : public CDataCache
{
public:
	CDataTradeCache(CFxQueue& queue);

public:
	CFxAccountInfo GetAccountInfo() const;
	vector<CFxOrder> GetOrders() const;
	vector<CFxPositionReport> GetPositions() const;
	FxAccountType GetAccountType() const;
	bool GetSnapshot(FxAccountType& type, string& accountCurrency, int32& leverage, double& balance, map<string, CFxOrder>& orders, map<string, CFxPositionReport>& positions) const;

public:
	void Clear();
	void UpdateAccountInfo(const CFxAccountInfo& accountInfo);
    void UpdateAssets(const string& currency, const double balance);
    void UpdateAssets(const vector<CAssetInfo>& assets);
	void UpdatePosition(const CFxPositionReport& position);
	void UpdateOrders(const CFxExecutionReport& report);
	void UpdateBalance(const double newBalance);

private:
	void DoClear();
	void DoUpdateAccountInfo(const CFxAccountInfo& accountInfo);
    void DoUpdateAssets(const string& currency, const double balance);
	void DoUpdateOrders(const CFxExecutionReport& report);
	void DoUpdateBalance(const double newBalance);
	void DoUpdatePosition(const CFxPositionReport& position);

public:
	Event<CDataTradeCache, TradeCacheChangedSignature> Changed;

private:
	void Update();
	void RaiseChanged();

private:
	bool m_isAccontInfoInitialized;
	bool m_isOrdersInitialized;
	size_t m_reportsNumber;
	CFxAccountInfo m_accountInfo;
	map<string, CFxOrder> m_orders;
	map<string, CFxPositionReport> m_positions;
};

#endif
