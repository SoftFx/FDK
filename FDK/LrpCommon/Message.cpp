#include "stdafx.h"
#include "Message.h"
#include "MessageData.h"

CMessage::CMessage()
{
	m_data = new CMessageData(-1);
}
CMessage::CMessage(const CMessage& msg)
{
	m_data = msg.m_data;
	m_data->Acquire();
}
CMessage::CMessage(const ptrdiff_t key, const uint16 componentId, const uint16 methodId, MemoryBuffer& buffer)
{
	m_data = new CMessageData(key, componentId, methodId, buffer);
}
CMessage::~CMessage()
{
	m_data->Release();
}
CMessage& CMessage::operator=(const CMessage& msg)
{
	if (this != &msg)
	{
		m_data->Release();
		m_data = msg.m_data;
		m_data->Acquire();
	}
	return *this;
}
MemoryBuffer& CMessage::GetBuffer()
{
	return m_data->Buffer;
}
const MemoryBuffer& CMessage::GetBuffer() const
{
	return m_data->Buffer;
}
const ptrdiff_t CMessage::GetKey() const
{
	return m_data->Key;
}
const size_t CMessage::GetSize() const
{
	return m_data->Buffer.GetSize();
}
const uint16 CMessage::GetComponentId() const
{
	return m_data->ComponentId;
}
const uint16 CMessage::GetMethodId() const
{
	return m_data->MethodId;
}
