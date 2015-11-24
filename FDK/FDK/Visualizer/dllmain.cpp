#include "StdAfx.h"
#include "Visualizer.h"

namespace
{
	DllMainFunc gHandlers[] = {DisableFreeLibrary, LoadFixFields, ProcessConfig};
}




BOOL APIENTRY DllMain(HANDLE hModule, DWORD ul_reason_for_call, LPVOID lpReserved)
{	
	UNREFERENCED_PARAMETER(lpReserved);
	if (DLL_PROCESS_ATTACH != ul_reason_for_call)
	{
		return TRUE;
	}
	for each(auto func in gHandlers)
	{
		const bool status = func(hModule);
		if (!status)
		{
			return FALSE;
		}
	}	
	return TRUE;
}