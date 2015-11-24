#pragma once

#include "BaseBehaviour.h"



class CIncomming;
class COutgoing;

class CConnectedBehaviour : public CBaseBehaviour
{
public:
	CConnectedBehaviour(CChannel& channel);
	virtual HRESULT VProcess(const uint64 now);
public:
	void ProcessHeartBeatResponse();
	void SendQuote(const CFxQuote& quote);
private:
	HRESULT DoProcess(const uint64 now);
private:
	HRESULT DoReadConnected(const uint64 now);
	HRESULT DoWriteConnected(const uint64 now);
private:
	void UpdateLastIncommingEvent(const uint64 now);
	void UpdateLastOutgoingEvent(const uint64 now);
private:
	bool m_heartBeatInProcess;
	CIncomming& m_incomming;
	COutgoing& m_outgoing;
};