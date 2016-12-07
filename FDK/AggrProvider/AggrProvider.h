#ifndef __AggrProvider__
#define __AggrProvider__

#ifdef AGGRPROVIDER_EXPORTS
#define AGGRPROVIDER_API __declspec(dllexport)
#else
#define AGGRPROVIDER_API __declspec(dllimport)
#endif

const char* GetProtocolType();
IConnection* CreateConnection(const std::string& name, const std::string& connectionString);
IConnection* CreateAggrConnection(const std::string& name, const std::string& connectionString);
#endif