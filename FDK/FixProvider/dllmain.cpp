#include "stdafx.h"
#include "resource.h"
#include "FixConnection.h"


#ifdef _MSC_VER

BOOL APIENTRY DllMain(HMODULE hModule, DWORD ul_reason_for_call, LPVOID /*lpReserved*/)
{
	if (DLL_PROCESS_ATTACH == ul_reason_for_call)
	{
		try
		{
			CDictionariesManager::SetModuleHandle(hModule);
			CFixConnection::InitializeMessageHandlers();
			std::stringstream stream;
			stream<<IDR_FIX44_XML;
			std::string st = stream.str();
			LoadDictionaryFromResourceID(st);
		}
		catch (...)
		{
			return FALSE;
		}
	}
	return TRUE;
}

#else

namespace
{
	void Initialize()
	{
		void* module = reinterpret_cast<void*>(&Initialize);
		CDictionariesManager::SetModuleHandle(module);
		CFixConnection::InitializeMessageHandlers();
		LoadDictionaryFromResourceID("FIX44");
	}
}
namespace
{
	class Initializer
	{
	public:
		Initializer();
	};

	Initializer::Initializer()
	{
		try
		{
			Initialize();
		}
		catch (...)
		{
		}
	}
}

namespace
{
	Initializer gInitializer;
}
#endif