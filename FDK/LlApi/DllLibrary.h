#pragma once

class CDllLibrary
{
public:
	CDllLibrary(const tstring& path);
	~CDllLibrary();
public:
	bool IsAdapter() const;
	string GetProtocolType() const;
	IConnection* CreateConnection(const string& connectionString) const;
private:
	typedef const char* (*GetProtocolTypeFunc)();
	typedef IConnection* (*CreateConnectionFunc)(const string& connectionString);
private:
	HMODULE m_module;
	GetProtocolTypeFunc m_getProtocolType;
	CreateConnectionFunc m_createConnection;
};