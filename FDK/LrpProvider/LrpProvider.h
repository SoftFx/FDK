#ifndef __LrpProvider_Api__
#define __LrpProvider_Api__

#ifdef LRPPROVIDER_EXPORTS
#define LRPPROVIDER_API EXPORT_API
#else
#define LRPPROVIDER_API IMPORT_API
#endif

const char* GetProtocolType();
IConnection* CreateConnection(const std::string& connectionString);
IConnection* CreateLrpConnection(const std::string& connectionString);
#endif
