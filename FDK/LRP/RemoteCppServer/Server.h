#pragma once
#include "Channel.h"



class CServer : LrpStServer<CChannel>
{
public:
	CServer(int port);
public:
	virtual CChannel* CreateNewConnection(const std::string& address);
	virtual bool ValidateCredentials(const std::string& /*username*/, const std::string& /*password*/);
private:
	CChannel m_channel;
};