#pragma once
#include "AggrSender.h"
#include "ConnectionParams.h"
#include "FeederSource.h"
#include "FeederHandler.h"
#include "BridgeClient.h"




class CAggrConnection : public IConnection
{
public:
	virtual void VReceiver(IReceiver* pReceiver);
	virtual ISender* VSender();
	virtual void VStart();
	virtual void VShutdown();
	virtual void VStop();
	virtual void VGetActivity(uint64* pLogicalBytesSent, uint64* pPhysicalBytesSent, uint64* pLogicalBytesReceived, uint64* pPhysicalBytesReceived);
public:
	CAggrConnection(const string& connectionString);
	~CAggrConnection();
private:
	IReceiver* m_receiver;
	CAggrSender m_sender;
	CFeedHandler m_handler;
public:
	CFeederSource* m_feeder;
	CBridgeClient* m_bridge;
};