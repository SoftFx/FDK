#pragma once
#include "Incomming.h"
#include "Outgoing.h"
#include "HandShakeBehaviour.h"
#include "ConnectedBehaviour.h"
#include "QuotesSender.h"

class CServer;
class CChannelStaticInitializer;




class CChannel : public CReferenceable
{
public:
	CChannel(CServer& server, const CParameters& params, uint64 id, SOCKET socket);
	virtual ~CChannel();
private:
	CChannel(const CChannel&);
	CChannel& operator = (const CChannel&);
public:
	HRESULT Process();
	void Connect(const HRESULT status);
	void Logon(const HRESULT status, const string& message, bool twofactor);
	void Finalize();
	const uint64 GetId() const;
	HRESULT SendMessage(const CMessage& message);
	void SendQuote(const CFxQuote& quote);
public:
	CIncomming& Incomming();
	COutgoing& Outgoing();
public:
	void WriteOutgoingMessage(CMessage& message);
	void ProcessHeartBeatResponse();
private:
	HRESULT DoProcess(const uint64 now);
	HRESULT DoSendMessage(const CMessage& message);
public:
	CServer& GetServer();
	CLrpLogger& GetLogger();
	CChannelState& GetState();
	CTransport& GetTransport();
	const CParameters& GetParameters() const;
private: // input arguments
	CServer& m_server;
	CLrpLogger& m_logger;
	const CParameters& m_params;
	const uint64 m_id;
	string m_address;
	CTransport m_transport;
private:
	bool m_useCodec;
	CChannelState m_state;
private:
	IBehaviour* m_behaviour;
	CHandShakeBehaviour m_handShake;
	CConnectedBehaviour m_connected;
private:
	CIncomming m_incomming;
	COutgoing m_outgoing;
	CCriticalSection m_synchronizer;
	CQuotesSender m_sender;
};