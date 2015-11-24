#pragma once
#include "ILrpChannel.h"






class LRPCORE_API CLrpClient : public ILrpChannel
{
public:
	CLrpClient(const char* signature);
	virtual ~CLrpClient();
protected:
	bool Connect(unsigned int timeoutInMillisecond, SOCKET s, const char* address, int port, const char* username, const char* password);
private:
	CLrpClient(const CLrpClient&);
	CLrpClient& operator = (const CLrpClient&);
};
