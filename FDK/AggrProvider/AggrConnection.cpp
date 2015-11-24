#include "stdafx.h"
#include "AggrConnection.h"
#include "FeederSources.h"

namespace
{
	const string cType = "Type";

	const string cFeederAddress = "FeederAddress";
	const string cFeederPort = "FeederPort";
	const string cFeederUsername = "FeederUsername";
	const string cFeederPassword = "FeederPassword";
	const string cFeederLogPath = "FeederLogPath";

	const string cBridgeAddress = "BridgeAddress";
	const string cBridgePort = "BridgePort";
	const string cBridgeUsername = "BridgeUsername";
	const string cBridgePassword = "BridgePassword";
	const string cBridgeLogPath = "BridgeLogPath";

	const string cBankCode = "BankCode";
	const string cMetaAccount = "MetaAccount";
	const string cMetaPasword = "MetaPassword";

}

namespace
{
	const string cFeedType = "Feed";
	const string cTradeType = "Trade";
}

CAggrConnection::CAggrConnection(const string& connectionString) : m_receiver(nullptr), m_feeder(nullptr), m_bridge(nullptr)
{
	CFxParams params(connectionString);

	const string type = params.GetString(cType);

	CConnectionParams feederParams;
	feederParams.Address = params.GetString(cFeederAddress);
	feederParams.Port = params.GetInt32(cFeederPort);
	feederParams.Username = params.GetString(cFeederUsername);
	feederParams.Password = params.GetString(cFeederPassword);
	params.TryGetString(cFeederLogPath, feederParams.LogPath);

	int32 bankCode = params.GetInt32(cBankCode);
	if (cFeedType == type)
	{
		m_handler.Construct(FeedHandlerMode_Feed, bankCode);
	}
	else if (cTradeType == type)
	{
		m_handler.Construct(FeedHandlerMode_Trade, bankCode);
		const string bridgeAddress = params.GetString(cBridgeAddress);
		const int bridgePort = params.GetInt32(cBridgePort);
		const string bridgeUsername = params.GetString(cBridgeUsername);
		const string bridgePassword = params.GetString(cBridgePassword);
		const int metaAccount = params.GetInt32(cMetaAccount);
		const string metaPassword = params.GetString(cMetaPasword);
		string bridgeLogPath;
		params.TryGetString(cBridgeLogPath, bridgeLogPath);

		m_bridge = new CBridgeClient(bankCode, metaAccount, metaPassword, bridgeAddress, bridgePort, bridgeUsername, bridgePassword, bridgeLogPath);
		m_sender.SetTrader(m_bridge);
	}
	else
	{
		throw CRuntimeError("Invalid connection type = ") + type;
	}

	m_sender.SetFeederHandler(&m_handler);
	m_feeder = CFeederSources::CreateSource(feederParams);
}
CAggrConnection::~CAggrConnection()
{
	VShutdown();
	CFeederSources::DestroySource(m_feeder);
	m_feeder = nullptr;
	if (nullptr != m_bridge)
	{
		m_bridge->Finalize();
		delete m_bridge;
		m_bridge = nullptr;
	}
}
void CAggrConnection::VReceiver(IReceiver* pReceiver)
{
	m_receiver = pReceiver;
	m_sender.SetReceiver(pReceiver);
	m_handler.SetReceiver(pReceiver);
	if (nullptr != m_bridge)
	{
		m_bridge->SetReceiver(pReceiver);
	}
}
ISender* CAggrConnection::VSender()
{
	return &m_sender;
}
void CAggrConnection::VStart()
{
	(*m_feeder) += &m_handler;
}
void CAggrConnection::VShutdown()
{
	(*m_feeder) -= &m_handler;
}
void CAggrConnection::VStop()
{
	//throw std::exception("The method or operation is not implemented.");
}
void CAggrConnection::VGetActivity(uint64* pLogicalBytesSent, uint64* pPhysicalBytesSent, uint64* pLogicalBytesReceived, uint64* pPhysicalBytesReceived)
{
	if (nullptr != pLogicalBytesSent)
	{
		*pLogicalBytesSent = 0;
	}
	if (nullptr != pPhysicalBytesSent)
	{
		*pPhysicalBytesSent = 0;
	}
	if (nullptr != pLogicalBytesReceived)
	{
		*pLogicalBytesReceived = 0;
	}
	if (nullptr != pPhysicalBytesReceived)
	{
		*pPhysicalBytesReceived = 0;
	}
}
