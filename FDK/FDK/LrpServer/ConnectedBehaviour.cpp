#include "stdafx.h"
#include "ConnectedBehaviour.h"
#include "Channel.h"
#include "Server.h"

namespace
{
	const size_t cStackBufferSize = 16 * 1024;
}
CConnectedBehaviour::CConnectedBehaviour(CChannel& channel) : CBaseBehaviour(channel), m_heartBeatInProcess(false), m_incomming(channel.Incomming()), m_outgoing(channel.Outgoing())
{
}
HRESULT CConnectedBehaviour::VProcess(const uint64 now)
{
	const size_t heartBeatTimeout = m_params.HeartbeatTimeout;
	if ((m_state.LastIncommingEvent + heartBeatTimeout < now) || (m_state.LastOutgoingEvent + heartBeatTimeout < now))
	{
		if ((m_state.LastIncommingEvent + 2 * heartBeatTimeout < now) || (m_state.LastOutgoingEvent + 2 * heartBeatTimeout < now))
		{
			return E_FAIL;
		}
		if (!m_heartBeatInProcess)
		{
			m_outgoing.SendHeartBeatRequest();
			m_heartBeatInProcess = true;
		}
	}
	return DoProcess(now);
}
HRESULT CConnectedBehaviour::DoProcess(const uint64 now)
{
	CSocketState state = m_transport.CanReadWrite();
	HRESULT result = S_FALSE;

	if (state.CanRead)
	{
		const HRESULT status = DoReadConnected(now);
		if (FAILED(status))
		{
			return status;
		}
	}
	if (state.CanWrite)
	{
		const HRESULT status = DoWriteConnected(now);
		if (FAILED(status))
		{
			return status;
		}
	}
	return result;
}
HRESULT CConnectedBehaviour::DoReadConnected(const uint64 now)
{
	HRESULT result = m_transport.Read(m_state.Buffer);
	if (FAILED(result))
	{
		return result;
	}

	uint8* pData = reinterpret_cast<uint8*>(m_state.Buffer.GetData());
	size_t position = 0;
	size_t available = m_state.Buffer.GetPosition();

	for (; available >= sizeof(uint16);)
	{
		MemoryBuffer header(nullptr, pData + position, sizeof(uint16), sizeof(uint16));
		const size_t dataSize = ReadUInt16(header);

		const size_t required = sizeof(uint16) + dataSize;

		if (available < required)
		{
			break;
		}

		const size_t size = position + required;
		MemoryBuffer data(nullptr, pData + position + sizeof(uint16), size, size);

		{
			// logging

			const size_t position = data.GetPosition();

			const uint16 componentId = ReadUInt8(data);
			const uint16 methodId = ReadUInt8(data);

			m_logger.WriteIncommingMessage(m_id, componentId, methodId, data);
			data.SetPosition(position);
		}

		result = m_incomming.Process(data);
		if (FAILED(result))
		{
			return result;
		}

		position += required;
		available -= required;

		result = S_OK;
	}

	if (S_OK != result)
	{
		return result;
	}


	MemoryBuffer remaining(nullptr, pData + position, available, available);
	m_state.Buffer.Construct(CChannelState::DefaultBufferSizeInBytes);

	WriteRaw(remaining, m_state.Buffer);

	m_state.Buffer.SetPosition(available);

	UpdateLastIncommingEvent(now);

	return result;
}
HRESULT CConnectedBehaviour::DoWriteConnected(const uint64 now)
{
	if (m_state.Messages.IsEmpty())
	{
		return S_FALSE;
	}
	char stack[cStackBufferSize];
	MemoryBuffer buffer(nullptr, stack, 0, sizeof(stack));
	m_state.Messages.Begin(buffer);

	HRESULT result = m_transport.Write(buffer);
	if (FAILED(result))
	{
		return result;
	}

	const size_t numberOfSentMessages = m_state.Messages.End(buffer);
	if (numberOfSentMessages > 0)
	{
		UpdateLastOutgoingEvent(now);
		return S_OK;
	}
	return S_FALSE;
}
void CConnectedBehaviour::UpdateLastIncommingEvent(const uint64 now)
{
	m_state.LastIncommingEvent = now;
}
void CConnectedBehaviour::UpdateLastOutgoingEvent(const uint64 now)
{
	m_state.LastOutgoingEvent = now;
}
void CConnectedBehaviour::ProcessHeartBeatResponse()
{
	m_heartBeatInProcess = false;
}
