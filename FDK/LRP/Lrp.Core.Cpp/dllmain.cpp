// dllmain.cpp : Defines the entry point for the DLL application.
#include "stdafx.h"



BOOL APIENTRY DllMain( HMODULE /*hModule*/, DWORD  ul_reason_for_call, LPVOID /*lpReserved*/)
{
	if (DLL_PROCESS_ATTACH == ul_reason_for_call)
	{
		const WORD version = MAKEWORD(2, 2);
		WSADATA data;
		if (WSAStartup(version, &data))
		{
			return FALSE;
		}
	}
	else if (DLL_PROCESS_DETACH == ul_reason_for_call)
	{
		WSACleanup();
	}
	return TRUE;
}

