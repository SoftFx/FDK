#include "stdafx.h"
#include "Client.h"

CClient::CClient() : m_receiver()
{
}
CClient::CClient(IReceiver* pReceiver) : m_receiver(pReceiver)
{
}
void CClient::OnTick(MemoryBuffer& buffer)
{

}
