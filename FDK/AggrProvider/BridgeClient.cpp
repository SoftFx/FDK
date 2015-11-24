#include "stdafx.h"
#include "TradeCommand.h"
#include "TradeSide.h"
#include "BridgeClient.h"
#include "TypesSerializer.hpp"
#include "Signature.hpp"
#include "FTrade.hpp"

namespace
{
	const uint32 cOperationTimeoutInMilliseconds = 2 * 60 * 1000;
	const uint32 cConnectionTimeoutInMilliseconds = 60 * 1000;
}


CBridgeClient::CBridgeClient(int bankCode, int metaAccount, const string& metaPassword, const string& address, int port, const string& username, const string& password, const string& logPath) :
	m_continue(true), m_bankCode(bankCode), m_metaAccount(metaAccount), m_metaPassword(metaPassword), m_receiver(nullptr)
{
	InitializeLog(logPath, address, port, bankCode);
	InitializeLrpClient(address, port, username, password);
	Delegate<void ()> func(this, &CBridgeClient::Loop);
	func.DoAsynch(m_thread);
}
CBridgeClient::~CBridgeClient()
{
	Finalize();
	CLogger* pLogger = m_logger.get();
	if (nullptr != pLogger)
	{
		pLogger->Output("[FINISH]");
	}
}
void CBridgeClient::Finalize()
{
	m_continue = false;
	m_semaphore.Release();
	m_thread.JoinAndFinalize();
}
void CBridgeClient::Loop()
{
	Connect();
	for (m_semaphore.WaitFor(); m_continue; m_semaphore.WaitFor())
	{
		SafeStep();
	}
}
void CBridgeClient::SafeStep()
{
	try
	{
		Step();
	}
	catch(...)
	{
	}
}
void CBridgeClient::Step()
{
	Connect();
	CRequest request;
	{
		CLock lock(m_synchronizer);
		if (m_requests.empty())
		{
			return;
		}
		request = m_requests.front();
		m_requests.pop_front();
	}
	Execute(request.ID, request.Order);
}
void CBridgeClient::Connect()
{
	if (!m_client->IsConnected())
	{
		if (!m_client->Connect(cConnectionTimeoutInMilliseconds))
		{
			return;
		}
		FTrade trade(*m_client);
		HRESULT status = trade.Initialize(m_bankCode, m_metaAccount, m_metaPassword);
		CLogger* pLogger = m_logger.get();
		if (nullptr != pLogger)
		{
			stringstream stream;
			stream<<"Trader::Initialize(): status = "<<status;
			string st = stream.str();
			pLogger->Output(st);
		}
	}
}
void CBridgeClient::SendOrder(const string& id, const CFxOrder& order)
{
	if (FxOrderType_Market != order.Type)
	{
		throw runtime_error("Aggregator provider supports only market orders.");
	}
	if ((FxTradeRecordSide_Buy != order.Side) && (FxTradeRecordSide_Sell != order.Side))
	{
		throw runtime_error("Incorrect order side");
	}
	{
		CLock lock(m_synchronizer);
		CRequest request(id, order);
		request.Order.OrderId = "1";
		m_requests.push_back(request);
	}
	m_semaphore.Release();
}

void CBridgeClient::Execute(const string& id, const CFxOrder& order)
{
	FTrade trade(*m_client);
	TradeSide side = static_cast<TradeSide>(order.Side);
	double executedVolume = 0;
	double executedPrice = 0;

	CDateTime creationTime = FxUtcNow();
	SendNewOrder(id, creationTime, order);

	HRESULT status = E_FAIL;
	CLogger* pLogger = m_logger.get();

	try
	{

		if (nullptr != pLogger)
		{
			stringstream stream;
			stream<<"CBridgeClient::Execute(): id = "<<id<<"; side = "<<side<<"; symbol = "<<order.Symbol<<"; price threshold = "<<order.Price<<"; requested volume = "<<order.Volume;
			string st = stream.str();
			pLogger->Output(st);
		}
		status = trade.ExecuteIOC(side, order.Symbol, order.Price, order.Volume, executedPrice, executedVolume);
		if (nullptr != pLogger)
		{
			stringstream stream;
			stream<<"CBridgeClient::Execute(): id = "<<id<<"; executed price = "<<executedPrice<<"; executed volume = "<<executedVolume;
			string st = stream.str();
			pLogger->Output(st);
		}
	}
	catch(const std::exception& ex)
	{
		if (nullptr != pLogger)
		{
			stringstream stream;
			stream<<"CBridgeClient::Execute(): exception = "<<ex.what();
			string st = stream.str();
			pLogger->Output(st);
		}
	}
	catch (...)
	{
		if (nullptr != pLogger)
		{
			pLogger->Output("unknown exception");
		}
	}

	if (SUCCEEDED(status))
	{
		SendFilled(id, creationTime, order, executedPrice, executedVolume);
	}
	else
	{
		SendReject(id, creationTime, order);
	}
}


void CBridgeClient::SendNewOrder(const string& id, CDateTime cretionTime, const CFxOrder& order)
{
	CFxExecutionReport report;
	report.ExecutionType = FxExecutionType_New;
	report.OrderStatus = FxOrderStatus_New;
	report.OrderType = FxOrderType_Market;
	report.OrderSide = order.Side;
	report.Symbol = order.Symbol;
	report.OrderId = order.OrderId;
	report.ClientOrderId = id;
	report.InitialVolume = order.Volume;
	report.LeavesVolume = order.Volume;
	report.Created = cretionTime;

	report.ExecutedVolume = 0;
	report.AveragePrice = 0;

	report.TradePrice = 0;
	report.TradeAmount.Reset();

	report.Price = order.Price;
	report.StopPrice.Reset();
	report.TakeProfit.Reset();
	report.StopLoss.Reset();
	report.Swap = 0;

	CFxEventInfo info;
	info.ID = id;
	m_receiver->VExecution(info, report);
}
void CBridgeClient::SendReject(const string& id, CDateTime cretionTime, const CFxOrder& order)
{
	CFxExecutionReport report;
	report.ExecutionType = FxExecutionType_Rejected;
	report.OrderStatus = FxOrderStatus_Rejected;
	report.OrderType = FxOrderType_Market;
	report.OrderSide = order.Side;
	report.Symbol = order.Symbol;
	report.OrderId = order.OrderId;
	report.ClientOrderId = id;
	report.InitialVolume = order.Volume;
	report.LeavesVolume = order.Volume;
	report.Created = cretionTime;

	report.ExecutedVolume = 0;
	report.AveragePrice = 0;

	report.TradePrice = 0;
	report.TradeAmount.Reset();

	report.Price = order.Price;
	report.StopPrice.Reset();
	report.TakeProfit.Reset();
	report.StopLoss.Reset();
	report.Swap = 0;
	report.RejectReason = FxRejectReason_DealerReject;


	CFxEventInfo info;
	info.ID = id;
	m_receiver->VExecution(info, report);
}
void CBridgeClient::SendFilled(const string& id, CDateTime cretionTime, const CFxOrder& order, double executedPrice, double executedVolume)
{
	CDateTime modificationTime = FxUtcNow();
	SendFirstFilled(id, cretionTime, modificationTime, order, executedPrice, executedVolume);
	SendSecondFilled(id, cretionTime, modificationTime, order, executedPrice, executedVolume);
}

void CBridgeClient::SendFirstFilled(const string& id, CDateTime creationTime, CDateTime modificcationTime, const CFxOrder& order, double executedPrice, double executedVolume)
{
	CFxExecutionReport report;
	report.ExecutionType = FxExecutionType_Trade;
	if (executedVolume < order.Volume)
	{
		report.OrderStatus = FxOrderStatus_PartiallyFilled;
	}
	else
	{
		report.OrderStatus = FxOrderStatus_Filled;
	}
	report.Created = creationTime;
	report.Modified = modificcationTime;
	report.OrderType = FxOrderType_Market;
	report.OrderSide = order.Side;
	report.Symbol = order.Symbol;
	report.OrderId = order.OrderId;
	report.AveragePrice = executedPrice;
	report.TradeAmount = executedVolume;
	report.TradePrice = executedPrice;
	report.ClientOrderId = id;
	report.InitialVolume = order.Volume;
	report.LeavesVolume = order.Volume - executedVolume;


	report.ExecutedVolume = executedVolume;

	report.Price = 0;
	report.Swap = 0;
	report.StopPrice.Reset();
	report.TakeProfit.Reset();
	report.StopLoss.Reset();

	CFxEventInfo info;
	info.ID = id;
	m_receiver->VExecution(info, report);
}
void CBridgeClient::SendSecondFilled(const string& id, CDateTime creationTime, CDateTime modificcationTime, const CFxOrder& order, double executedPrice, double executedVolume)
{
	CFxExecutionReport report;
	report.ExecutionType = FxExecutionType_Calculated;
	report.OrderStatus = FxOrderStatus_Calculated;

	report.Created = creationTime;
	report.Modified = modificcationTime;
	report.OrderType = FxOrderType_Position;
	report.OrderSide = order.Side;
	report.Symbol = order.Symbol;
	report.OrderId = order.OrderId;
	report.AveragePrice = executedPrice;
	report.TradeAmount = 0;
	report.TradePrice = 0;
	report.ClientOrderId = id;
	report.InitialVolume = executedVolume;
	report.LeavesVolume = executedVolume;


	report.ExecutedVolume = 0;

	report.Price = executedPrice;

	report.StopPrice.Reset();
	report.TakeProfit.Reset();
	report.StopLoss.Reset();
	report.TradeAmount.Reset();

	report.Swap = 0;
	CFxEventInfo info;
	info.ID = id;
	m_receiver->VExecution(info, report);
}
void CBridgeClient::SetReceiver(IReceiver* pReceiver)
{
	m_receiver = pReceiver;
}
void CBridgeClient::InitializeLog(const string& logPath, const string& address, int port, int bankCode)
{
	if (logPath.empty())
	{
		return;
	}
	stringstream stream;
	stream<<"bridge_"<<address<<"_"<<port<<"_"<<bankCode<<".log";
	string filename = stream.str();
	string path = FxCombinePath(logPath, filename);
	m_logger.reset(new CLogger(path));
	m_logger->Output("[START]");
}
void CBridgeClient::InitializeLrpClient(const string& address, int port, const string& username, const string& password)
{
	void* pUserParam = m_logger.get();
	LrpLogHandler handler = (nullptr != pUserParam) ? &CLogger::OutputHandler : nullptr;
	m_client.reset(new CLrpStClient(LrpSignature(), address.c_str(), port, username.c_str(), password.c_str(),  handler, pUserParam, cOperationTimeoutInMilliseconds));
}
