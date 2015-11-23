#include "stdafx.h"
#include "AggrProvider.h"
#include "AggrConnection.h"



const char* GetProtocolType()
{
	return "Aggr";
}
IConnection* CreateConnection(const std::string& connectionString)
{
	IConnection* result = CreateAggrConnection(connectionString);
	return result;
}
IConnection* CreateAggrConnection(const std::string& connectionString)
{
	IConnection* result = new CAggrConnection(connectionString);
	return result;
}
