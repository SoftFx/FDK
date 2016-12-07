#include "stdafx.h"
#include "FixProvider.h"
#include "FixConnection.h"


IConnection* CreateConnection(const std::string& name, const std::string& connectionString)
{
	IConnection* result = CreateFixConnection(name, connectionString);
	return result;
}
IConnection* CreateFixConnection(const std::string& name, const std::string& connectionString)
{
	IConnection* result = new CFixConnection(name, connectionString);
	return result;
}
const char* GetProtocolType()
{
	return "Fix";
}
