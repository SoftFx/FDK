#include "stdafx.h"
#include "DataTradeCache.h"

#ifndef _MSC_VER
typedef CDataCache __super;
#endif

CDataTradeCache::CDataTradeCache(CFxQueue& queue)
    : CDataCache(queue)
    , m_isAccontInfoInitialized()
    , m_isOrdersInitialized()
{
}

CFxAccountInfo CDataTradeCache::GetAccountInfo()const
{
    CSharedLocker lock(m_synchronizer);
    return m_accountInfo;
}

vector<CFxOrder> CDataTradeCache::GetOrders() const
{
    vector<CFxOrder> result;
    CSharedLocker lock(m_synchronizer);
    foreach(const auto& entry, m_orders)
    {
        result.push_back(entry.second);
    }
    return result;
}

vector<CFxPositionReport> CDataTradeCache::GetPositions() const
{
    vector<CFxPositionReport> result;
    CSharedLocker lock(m_synchronizer);
    foreach(const auto& entry, m_positions)
    {
        result.push_back(entry.second);
    }
    return result;
}

void CDataTradeCache::Clear()
{
    DoClear();
    RaiseChanged();
    __super::Clear();
}

void CDataTradeCache::DoClear()
{
    CExclusiveLocker lock(m_synchronizer);
    m_accountInfo = CFxAccountInfo();
    m_isAccontInfoInitialized = false;
    m_isOrdersInitialized = false;
    m_reportsNumber = 0;
    m_orders.clear();
    m_positions.clear();
}

void CDataTradeCache::UpdateAccountInfo(const CFxAccountInfo& accountInfo)
{
    DoUpdateAccountInfo(accountInfo);
    RaiseChanged();
}

void CDataTradeCache::DoUpdateAccountInfo(const CFxAccountInfo& accountInfo)
{
    CExclusiveLocker lock(m_synchronizer);
    m_accountInfo = accountInfo;
    m_isAccontInfoInitialized = true;
    /* FDKPRO-511: DataTrade.Cache.TradeRecords.Length = 0 after EVENT Trade.CacheInitialized
    if (FxAccountType_Cash == m_accountInfo.Type || FxAccountType_Net ==  m_accountInfo.Type)
    {
        m_isOrdersInitialized = true;
    }
    */
    Update();
}

void CDataTradeCache::UpdateAssets(const string& currency, const double balance)
{
    DoUpdateAssets(currency, balance);
    RaiseChanged();
}

void CDataTradeCache::UpdateAssets(const vector<CAssetInfo>& assets)
{
    foreach(const auto& entry, assets)
    {
        DoUpdateAssets(entry.Currency, entry.Balance);
    }
    RaiseChanged();
}

void CDataTradeCache::DoUpdateAssets(const string& currency, const double balance)
{
    CExclusiveLocker lock(m_synchronizer);

    bool updated = false;

    for (size_t n = 0; n < m_accountInfo.Assets.size(); n++)
    {
        auto& entry = m_accountInfo.Assets[n];

        if (entry.Currency != currency)
            continue;

        entry.Balance = balance;
        updated = true;

        if (abs(entry.Balance) < DBL_EPSILON)
        {
            m_accountInfo.Assets.erase(m_accountInfo.Assets.begin() + n);
        }

        break;
    }

    if (!updated)
    {
        CAssetInfo assetInfo;
        assetInfo.Currency = currency;
        assetInfo.Balance = balance;
        m_accountInfo.Assets.push_back(assetInfo);
    }

    Update();
}

void CDataTradeCache::UpdateOrders(const CFxExecutionReport& report)
{
    DoUpdateOrders(report);
    RaiseChanged();
}

void CDataTradeCache::DoUpdateOrders(const CFxExecutionReport& report)
{
    CExclusiveLocker lock(m_synchronizer);
    if (IsFinite(report.Balance))
    {
        m_accountInfo.Balance = report.Balance;
    }
    CFxOrder order;
    string orderId;
    double leavesVolume = 0;
    double commission = 0;
    double agentCommission = 0;
    double swap = 0;
    if (report.TryGetTradeRecord(order))
    {
        const string& id = order.OrderId;
        m_orders[id] = order;
    }
    else if (report.TryGetClosedPosition(orderId, leavesVolume, commission, agentCommission, swap))
    {
        auto it = m_orders.find(orderId);
        if (m_orders.end() != it)
        {
            if (leavesVolume > 0)
            {
                it->second.Volume = leavesVolume;
                it->second.Commission = commission;
                it->second.AgentCommission = agentCommission;
                it->second.Swap = swap;
            }
            else
            {
                m_orders.erase(it);
            }
        }
    }
    else if (report.TryGetDeletedOrder(orderId))
    {
        m_orders.erase(orderId);
    }
    else if (report.TryGetActivatedOrder(orderId))
    {
        m_orders.erase(orderId);
    }

    if (!m_isOrdersInitialized)
    {
        if (report.ReportsNumber == 0 || (int32)(++m_reportsNumber) == report.ReportsNumber)
        {
            m_isOrdersInitialized = true;
            Update();
        }
    }
}

void CDataTradeCache::Update()
{
    const bool isInitialized = m_isAccontInfoInitialized && m_isOrdersInitialized;
    __super::Update(isInitialized);
}

void CDataTradeCache::UpdateBalance(const double newBalance)
{
    DoUpdateBalance(newBalance);
    RaiseChanged();
}

void CDataTradeCache::DoUpdateBalance(const double newBalance)
{
    CExclusiveLocker lock(m_synchronizer);
    m_accountInfo.Balance = newBalance;
}

FxAccountType CDataTradeCache::GetAccountType() const
{
    CSharedLocker lock(m_synchronizer);
    return m_accountInfo.Type;
}

bool CDataTradeCache::GetSnapshot(FxAccountType& type, string& accountCurrency, int32& leverage, double& balance, map<string, CFxOrder>& orders, map<string, CFxPositionReport>& positions) const
{
    CSharedLocker lock(m_synchronizer);
    if (!m_isAccontInfoInitialized)
    {
        return false;
    }
    type = m_accountInfo.Type;
    accountCurrency = m_accountInfo.Currency;
    leverage = m_accountInfo.Leverage;
    balance = m_accountInfo.Balance;
    orders = m_orders;
    positions = m_positions;
    return true;
}

void CDataTradeCache::UpdatePosition(const CFxPositionReport& position)
{
    DoUpdatePosition(position);
    RaiseChanged();
}

void CDataTradeCache::DoUpdatePosition(const CFxPositionReport& position)
{
    CExclusiveLocker lock(m_synchronizer);
    m_positions[position.Symbol] = position;
    if (position.Balance.HasValue())
    {
        m_accountInfo.Balance = position.Balance.Value();
    }
}

void CDataTradeCache::RaiseChanged()
{
    Changed(*this);
}