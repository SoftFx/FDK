#include "stdafx.h"
#include "FixProvider.h"
#include "FixConnection.h"


IConnection* CreateConnection(const std::string& connectionString)
{
	IConnection* result = CreateFixConnection(connectionString);
	return result;
}
IConnection* CreateFixConnection(const std::string& connectionString)
{
	IConnection* result = new CFixConnection(connectionString);
	return result;
}
const char* GetProtocolType()
{
	return "Fix";
}
