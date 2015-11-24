#include "stdafx.h"
#include "DataTrade.h"
#include "TradeCacheImpl.h"

namespace
{
	FxRef<CDataTrade> DataTradeFromHandle(void* handle)
	{
		if (nullptr == handle)
		{
			throw CArgumentNullException("handle can not be null");
		}
		FxRef<CDataTrade> result = TypeFromHandle<CDataTrade>(handle);
		if (!result)
		{
			throw CInvalidHandleException("invalid handle");
		}
		return result;
	}
}
CFxAccountInfo CTradeCacheImpl::GetAccountInfo(void* handle)
{
	FxRef<CDataTrade> trade = DataTradeFromHandle(handle);
	return trade->Cache().GetAccountInfo();
}

vector<CFxOrder> CTradeCacheImpl::GetRecords(void* handle)
{
	FxRef<CDataTrade> trade = DataTradeFromHandle(handle);
	return trade->Cache().GetOrders();
}
vector<CFxPositionReport> CTradeCacheImpl::GetPositions(void* handle)
{
	FxRef<CDataTrade> trade = DataTradeFromHandle(handle);
	return trade->Cache().GetPositions();
}
