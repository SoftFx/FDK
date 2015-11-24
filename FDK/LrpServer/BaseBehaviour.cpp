#include "stdafx.h"
#include "BaseBehaviour.h"
#include "Channel.h"




CBaseBehaviour::CBaseBehaviour(CChannel& channel) :
	m_id(channel.GetId()), m_server(channel.GetServer()), m_logger(channel.GetLogger()),
	m_params(channel.GetParameters()), m_state(channel.GetState()), m_transport(channel.GetTransport())
{
}
