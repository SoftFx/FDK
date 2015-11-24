#include "stdafx.h"
#include "AggrSender.h"


void CAggrSender::VSendDeleteOrder(const string& /*id*/, const string& /*orderID*/, const string& /*clientID*/, int32 /*side*/)
{
	throw std::runtime_error("Delete order operation is not supported by aggregator provider.");
}
void CAggrSender::VSendCloseOrder(const string& /*id*/, const string& /*orderId*/, Nullable<double> /*closingVolume*/)
{
	throw std::runtime_error("Close order operation is not supported by aggregator provider.");
}
void CAggrSender::VSendCloseByOrders(const string& /*id*/, const string& /*firstOrderId*/, const string& /*secondOrderId*/)
{
	throw std::runtime_error("Close by operation is not supported by aggregator provider.");
}
void CAggrSender::VSendCloseAllOrders(const string& id)
{
	throw runtime_error("Close all orders operation is not supported by aggregator provider.");
}
void CAggrSender::VSendModifyOrder(const string& id, const CFxOrder& request)
{
	throw runtime_error("Modify order operation is not supported by aggregator provider.");
}
void CAggrSender::VSendGetFileChunk(const string& id, const string& fileId, const uint32 chunkId)
{
	throw runtime_error("Get file chunk operation is not supported by aggregator provider.");
}
void CAggrSender::VSendGetBarsHistoryMetaInfoFile(const string& id, const string& symbol, int32 priceType, const string& period)
{
	throw runtime_error("Get bars history meta info file operation is not supported by aggregator provider.");
}
void CAggrSender::VSendGetTicksHistoryMetaInfoFile(const string& id, const string& symbol, bool includeLevel2)
{
	throw runtime_error("Get ticks history meta info file operation is not supported by aggregator provider.");
}