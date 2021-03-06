#include "stdafx.h"
#include "LrpProvider.h"
#include "LrpConnection.h"


IConnection* CreateConnection(const std::string& name, const std::string& connectionString)
{
	return CreateLrpConnection(name, connectionString);
}
IConnection* CreateLrpConnection(const std::string& name, const std::string& connectionString)
{
	CFxParams params(connectionString);
	IConnection* result = new CLrpConnection(params);
	return result;
}
const char* GetProtocolType()
{
	return "Lrp";
}
