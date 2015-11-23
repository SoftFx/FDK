#pragma once

#include "IBehaviour.h"


class CServer;
class CChannel;
class CChannelState;
class CTransport;


class CBaseBehaviour : public IBehaviour
{
public:
	CBaseBehaviour(CChannel& channel);
private:
	CBaseBehaviour(const CBaseBehaviour&);
	CBaseBehaviour& operator = (const CBaseBehaviour&);
protected:
	const uint64 m_id;
	CServer& m_server;
	CLrpLogger& m_logger;
	const CParameters& m_params;
	CChannelState& m_state;
	CTransport& m_transport;
};