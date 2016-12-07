#include "stdafx.h"
#include "ConnectionBuilder.h"
#include "DllLibrary.h"
#include "DllMain.h"

namespace
{
	const string cProtocolType = "ProtocolType";
	#ifdef _WIN64
	const tstring cDllMask = TEXT("*.x64.dll");
	#else
	const tstring cDllMask = TEXT("*.x86.dll");
	#endif
	CSharedExclusiveLock gSynchronizer;
	map<string, shared_ptr<CDllLibrary> > gProtocolTypeToDllLibrary;
}

namespace
{
	CDllLibrary* DllLibraryFromProtocolType(const string& protocolType)
	{
		{
			CSharedLocker lock(gSynchronizer);
			auto it = gProtocolTypeToDllLibrary.find(protocolType);
			if (gProtocolTypeToDllLibrary.end() != it)
			{
				return it->second.get();
			}
		}
		CExclusiveLocker lock(gSynchronizer);
		auto it = gProtocolTypeToDllLibrary.find(protocolType);
		if (gProtocolTypeToDllLibrary.end() != it)
		{
			return it->second.get();
		}
		vector<tstring> files;
		tstring root = GetDllLocation();
		GetFiles(root, files, cDllMask);
		for each(const auto& element in files)
		{
			shared_ptr<CDllLibrary> library(new CDllLibrary(element));
			if (!library->IsAdapter())
			{
				continue;
			}
			const string libraryProtocolType = library->GetProtocolType();
			if (protocolType != libraryProtocolType)
			{
				continue;
			}
			CDllLibrary* result = library.get();
			gProtocolTypeToDllLibrary[protocolType] = library;
			return result;
		}
		return nullptr;
	}
}

IConnection* CreateConnection(const string& name, const string& connectionString)
{
	CFxParams params(connectionString);
	const string protocolType = params.GetString(cProtocolType);

	CDllLibrary* pDll = DllLibraryFromProtocolType(protocolType);
	if (nullptr == pDll)
	{
		throw CRuntimeError("Unsupported protocol type = ") + protocolType;
	}
	IConnection* result = pDll->CreateConnection(name, connectionString);
	return result;
}



