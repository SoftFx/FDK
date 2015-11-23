#ifndef __Core_IConnection__
#define __Core_IConnection__
#include "ISender.h"
#include "IReceiver.h"

class IConnection
{
public:
	virtual void VReceiver(IReceiver* pReceiver) = 0;
	virtual ISender* VSender() = 0;
	virtual void VStart() = 0;
	virtual void VShutdown() = 0;
	virtual void VStop() = 0;
	virtual void VGetActivity(uint64* pLogicalBytesSent, uint64* pPhysicalBytesSent, uint64* pLogicalBytesReceived, uint64* pPhysicalBytesReceived) = 0;
	virtual ~IConnection(){};
};
#endif
