#pragma once

struct ICLRRuntimeHost;
class CDotNetBridge
{
public:
	CDotNetBridge();
public:
	int Execute(const std::string& assemblyPath, const std::string& typeName, const std::string& methodName, const std::string& argument);
private:
	ICLRRuntimeHost* m_host;
};