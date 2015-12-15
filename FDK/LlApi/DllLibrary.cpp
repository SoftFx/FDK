#include "stdafx.h"
#include "DllLibrary.h"

namespace
{
	const char* cGetProtocolTypeName = "GetProtocolType";
	const char* cCreateConnectionName = "CreateConnection";
}

CDllLibrary::CDllLibrary(const tstring& path)
    : m_module()
    , m_getProtocolType()
    , m_createConnection()
{
	SetErrorMode(SEM_FAILCRITICALERRORS);
	m_module = LoadLibraryEx(path.c_str(), nullptr, LOAD_WITH_ALTERED_SEARCH_PATH);
	if (nullptr != m_module)
	{
		m_getProtocolType = reinterpret_cast<GetProtocolTypeFunc>(GetProcAddress(m_module, cGetProtocolTypeName));
		m_createConnection = reinterpret_cast<CreateConnectionFunc>(GetProcAddress(m_module, cCreateConnectionName));
	}
}

CDllLibrary::~CDllLibrary()
{
	if (nullptr != m_module)
	{
		m_createConnection = nullptr;
		m_getProtocolType = nullptr;
		FreeLibrary(m_module);
		m_module = nullptr;
	}
}

bool CDllLibrary::IsAdapter() const
{
	const bool result = (nullptr != m_getProtocolType) && (nullptr != m_createConnection);
	return result;
}

std::string CDllLibrary::GetProtocolType() const
{
	return m_getProtocolType();
}

IConnection* CDllLibrary::CreateConnection(const string& connectionString) const
{
	return m_createConnection(connectionString);
}
