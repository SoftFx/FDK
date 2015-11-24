#include "stdafx.h"
#include "Server.h"

typedef CChannel LrpChannel;

#include "TypesSerializer.hpp"
#include "Server.hpp"



CServer::CServer(int port)
{
	const char* signature = LrpSignature();
	Construct(port, signature, LrpInvoke, "D:\\output.txt");
}

CChannel* CServer::CreateNewConnection(const std::string& /*address*/)
{
	return &m_channel;
}

bool CServer::ValidateCredentials(const std::string& /*username*/, const std::string& /*password*/)
{
	return true;
}
