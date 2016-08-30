#ifndef __FixProvider_Fix_Connection__
#define __FixProvider_Fix_Connection__

#include "FixSender.h"
#include "FixExecutionReport.h"
#include "FixVersion.h"

class CFixConnection;

typedef void (CFixConnection::*MessageHandler)(const FIX::Message& message);

class CFixConnection : public IConnection, public FIX::NullApplication
{
public:
	virtual void VReceiver(IReceiver* pReceiver);
	virtual ISender* VSender();
	virtual void VStart();
	virtual void VShutdown();
	virtual void VStop();
	virtual void VGetActivity(uint64* pLogicalBytesSent, uint64* pPhysicalBytesSent, uint64* pLogicalBytesReceived, uint64* pPhysicalBytesReceived);

public:
	CFixConnection(const string& connectionString);
	virtual ~CFixConnection();
	static void InitializeMessageHandlers();

public:
	virtual void toAdmin(FIX::Message& message, const FIX::SessionID& sessionID);
	virtual void fromApp(const Message& message, const FIX::SessionID& sessionID)throw (FIX::FieldNotFound, FIX::IncorrectDataFormat, FIX::IncorrectTagValue, FIX::UnsupportedMessageType);
	virtual void onLogon(const Message& message, const SessionID& sessionID);
	virtual void onLogout(const Message& message, const SessionID& sessionID);

private:
	void OnEmpty(const FIX::Message& message);
	void OnTwoFactorAuth(const FIX44::TwoFactorLogon& message);
	void OnSessionInfo(const FIX44::TradingSessionStatus& message);
    void OnCurrenciesInfo(const FIX44::CurrencyList& message);
	void OnSymbolsInfo(const FIX44::SecurityList& message);
	void OnSubscribeToQuotesReject(const FIX44::MarketDataRequestReject& message);
	void OnSubscribeToQuotesAck(const FIX44::MarketDataRequestAck& message);
	void OnTick(const FIX44::MarketDataSnapshotFullRefresh& message);
	void OnClose(const FIX44::ClosePositionRequestAck& message);
	void OnExecution(const CFixExecutionReport& message);
	void OnCancel(const FIX44::OrderCancelReject& message);
	void OnAccountInfo(const FIX44::AccountInfo& message);
	void OnMarketDataHistory(const FIX44::MarketDataHistory& message);
	void OnMarketDataHistoryReject(const FIX44::MarketDataHistoryRequestReject& message);
	void OnMarketTradeHistoryAck(const FIX44::TradeCaptureReportRequestAck& message);
	void OnMarketTradeHistory(const FIX44::TradeCaptureReport& message);
	void OnFileChunk(const FIX44::FileChunk& message);
	void OnFileChunkReject(const FIX44::FileChunkReqReject& message);
	void OnMarketDataHistoryMetadataReport(const FIX44::MarketDataHistoryMetadataReport& message);
	void OnBusinessReject(const FIX44::BusinessMessageReject& message);
	void OnSuscribeToTradeTransactionReportsAck(const FIX44::TradeTransactionReportRequestAck& message);
	void OnTradeTransactionReport(const FIX44::TradeTransactionReport& message);
	void OnPositionReport(const FIX44::PositionReport& message);
	void OnNotification(const FIX44::Notification& message);
	void OnComponentsInfoReport(const FIX44::ComponentsInfoReport& message);

private:
	CFixSender m_sender;
	IReceiver* m_receiver;
	FIX::LogFactory* m_logFactory;
	FIX::SocketInitiator* m_initiator;

private:
	CFixVersion m_version;

private:
	FIX::SessionSettings m_settings;
	FIX::MemoryStoreFactory m_messageStorefactory;
	FIX::SessionID m_sessionID;
	string m_deviceId;
	string m_username;
	string m_password;
	string m_protocolVersion;
};



#endif
