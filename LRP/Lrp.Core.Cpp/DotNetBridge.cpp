#include "stdafx.h"
#include "DotNetBridge.h"



typedef CComCritSecLock<CComAutoCriticalSection> CLocker;

namespace
{
	volatile ICLRRuntimeHost* gHost;
	CComAutoCriticalSection gSynchronizer;
	wregex gDotNetVersionPattern(L"v(\\d+)\\.(\\d+).(\\d+)");

}
namespace
{
	runtime_error CreateException(const string& message, HRESULT status)
	{
		char buffer[sizeof("0x_1_2_3_4")] = "";
		sprintf_s(buffer, "0x%x", status);
		string text = message;
		text += buffer;
		return runtime_error(text);
	}
}
namespace
{
	class CDotNetVersion
	{
	public:
		int Major;
		int Minor;
		int Build;
	public:
		CDotNetVersion() : Major(), Minor(), Build()
		{
		}
		CDotNetVersion(const CComPtr<ICLRRuntimeInfo>& runtimeInfo) : Major(), Minor(), Build()
		{
			wchar_t buffer[3 *32 + 4] = L"";
			DWORD size = _countof(buffer);
			HRESULT status = runtimeInfo->GetVersionString(buffer, &size);
			if (FAILED(status))
			{
				throw CreateException("Couldn't get version of CLR runtime info", status);
			}
			string version = CW2A(buffer);
			wcmatch what;
			if(!regex_match(buffer, what, gDotNetVersionPattern))
			{
				throw runtime_error("Invalid CLR version format");
			}
			Major = _wtoi(what[1].first);
			Minor = _wtoi(what[2].first);
			Build = _wtoi(what[3].first);
		}
	private:
		friend bool operator < (const CDotNetVersion& first, const CDotNetVersion& second);
	};
	bool operator < (const CDotNetVersion& first, const CDotNetVersion& second)
	{
		if (first.Major < second.Major)
		{
			return true;
		}
		else if (first.Major > second.Major)
		{
			return false;
		}

		if (first.Minor < second.Minor)
		{
			return true;
		}
		else if (first.Minor > second.Minor)
		{
			return false;
		}

		return (first.Build < second.Build);
	}
}


namespace
{

	ICLRRuntimeHost* CreateRuntimeHost()
	{
		CComPtr<ICLRMetaHost> metaHost;
		HRESULT status = CLRCreateInstance(CLSID_CLRMetaHost, IID_ICLRMetaHost, reinterpret_cast<void**>(&metaHost));
		if (FAILED(status))
		{
			throw CreateException("Couldn't create CLR meta host; status = ", status);
		}

		CComPtr<IEnumUnknown> enumerator;
		status = metaHost->EnumerateInstalledRuntimes(&enumerator);
		if (FAILED(status))
		{
			throw CreateException("Couldn't get runtime enumerator; status = ", status);
		}

		CDotNetVersion theLastVersion;
		CComPtr<ICLRRuntimeInfo> runtimeInfo;

		for (;;)
		{
			CComPtr<IUnknown> unknown;
			status = enumerator->Next(1, &unknown, nullptr);
			if (S_OK != status)
			{
				break;
			}
			CComPtr<ICLRRuntimeInfo> info;
			status = unknown->QueryInterface(IID_ICLRRuntimeInfo, reinterpret_cast<void**>(&info));
			if (FAILED(status))
			{
				throw CreateException("Couldn't query ICLRRuntimeInfo interface; status = ", status);
			}
			CDotNetVersion version(info);
			if (theLastVersion < version)
			{
				runtimeInfo = info;
				theLastVersion = version;
			}
		}
		if (FAILED(status))
		{
			throw CreateException("Enumerateion error; status = ", status);
		}
		ICLRRuntimeHost* host = nullptr;
		status = runtimeInfo->GetInterface(CLSID_CLRRuntimeHost, IID_ICLRRuntimeHost, reinterpret_cast<void**>(&host));
		if (FAILED(status))
		{
			throw CreateException("Couldn't get CLR runtime host; status = ", status);
		}
		return host;
	}
	ICLRRuntimeHost* RuntimeHost()
	{
		if (nullptr != gHost)
		{
			return const_cast<ICLRRuntimeHost*>(gHost);
		}

		CLocker lock(gSynchronizer);

		if (nullptr != gHost)
		{
			return const_cast<ICLRRuntimeHost*>(gHost);
		}

		ICLRRuntimeHost* host = CreateRuntimeHost();
		HRESULT status = host->Start();
		if (FAILED(status))
		{
			throw CreateException("Couldn't start CLR runtime host; status = ", status);
		}
		gHost = host;
		return const_cast<ICLRRuntimeHost*>(gHost);
	}
}


CDotNetBridge::CDotNetBridge() : m_host(RuntimeHost())
{
}
int CDotNetBridge::Execute(const std::string& assemblyPath, const std::string& typeName, const std::string& methodName, const std::string& argument)
{
	CComBSTR path = assemblyPath.c_str();
	CComBSTR type = typeName.c_str();
	CComBSTR method = methodName.c_str();
	CComBSTR arg = argument.c_str();
	DWORD result = 0;

	HRESULT status = m_host->ExecuteInDefaultAppDomain(path, type, method, arg, &result);
	if (FAILED(status))
	{
		throw CreateException("Couldn't execute .Net code; status = ", status);
	}
	return static_cast<int>(result);
}

