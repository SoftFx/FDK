#include "stdafx.h"
#include "DllMain.h"

namespace
{
	tstring gRoot;
}

const tstring& GetDllLocation()
{
	return gRoot;
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD  ul_reason_for_call, LPVOID /*lpReserved*/)
{
	if (DLL_PROCESS_ATTACH == ul_reason_for_call)
	{
		try
		{
			TCHAR buffer[MAX_PATH] = TEXT("");
			GetModuleFileName(hModule, buffer, countof(buffer));
			tstring path = buffer;
			size_t index = path.find_last_of('\\');
			gRoot = path.substr(0, 1 + index);
		}
		catch (const std::bad_alloc&)
		{
			return FALSE;
		}
	}
	return TRUE;
}