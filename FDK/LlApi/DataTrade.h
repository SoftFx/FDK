#ifndef __Native_Data_Trade__
#define __Native_Data_Trade__

#include "Client.h"
#include "DataTradeCache.h"

class CDataTrade : public CClient
{
public:
	CDataTrade(const string& connectionString);

private:
	CDataTrade(const CDataTrade&);
	CDataTrade& operator = (const CDataTrade&);

public:
	CFxAccountInfo GetAccountInfo(const uint32 timeoutInMilliseconds);
	vector<CFxOrder> GetOrders(const size_t timeoutInMilliseconds);

public:
	CFxOrder OpenNewOrder(const string& operationId, const CFxOrder& order, const size_t timeoutInMilliseconds);
	CFxOrder ModifyOrder(const string& operationId, const CFxOrder& order, const size_t timeoutInMilliseconds);
	CFxClosePositionResult CloseOrder(const string& operationId, const string& orderId, Nullable<double> closingVolume, const size_t timeoutInMilliseconds);
	void DeleteOrder(const string& operationId, const string& orderId, const string& clientId, FxTradeRecordSide side, size_t timeoutInMilliseconds);
	bool CloseByOrders(const string& firstOrderId, const string& secondOrderId, const size_t timeoutInMilliseconds);
	size_t CloseAllOrders(const uint32 timeoutInMilliseconds);
	FxIterator GetTradeTransactionReportsAndSubscribeToNotifications(FxTimeDirection direction, bool subscribe, const Nullable<CDateTime>& from, const Nullable<CDateTime>& to, uint32 bufferSize, uint32 timeoutInMilliseconds);
	void UnsubscribeTradeTransactionReports(size_t timeoutInMilliseconds);

public:
	virtual void VLogon(const CFxEventInfo& eventInfo, const string& protocolVersion);
	virtual void VLogout(const CFxEventInfo& eventInfo, const FxLogoutReason reason, const string& description);

	virtual void VExecution(const CFxEventInfo& eventInfo, CFxExecutionReport& executionReport);
	virtual void VAccountInfo(const CFxEventInfo& eventInfo, CFxAccountInfo& accountInfo);
	virtual void VPositionReport(const CFxEventInfo& info, CFxPositionReport& positionReport);
	virtual void VNotify(const CFxEventInfo& eventInfo, const CNotification& notification);

private:
	void UpdateAccountInfo(FxAccountType accountType, const string& account);

public:
	const CDataTradeCache& Cache()const;
	CDataTradeCache& Cache();

private:
	FxAccountType m_accountType;
	CDataTradeCache m_cache;
};

#pragma region inline methods

inline const CDataTradeCache& CDataTrade::Cache() const
{
	return m_cache;
}

inline CDataTradeCache& CDataTrade::Cache()
{
	return m_cache;
}

#pragma endregion

#endif
