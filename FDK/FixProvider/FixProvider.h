#ifndef __FixProvider_Api__
#define __FixProvider_Api__

#ifdef FIXPROVIDER_EXPORTS
#define FIXPROVIDER_API EXPORT_API
#else
#define FIXPROVIDER_API IMPORT_API
#endif

const char* GetProtocolType();
IConnection* CreateConnection(const std::string& name, const std::string& connectionString);
IConnection* CreateFixConnection(const std::string& name, const std::string& connectionString);
#endif
