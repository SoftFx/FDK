#include "stdafx.h"
#include "Visualizer.h"

bool DisableFreeLibrary(HANDLE module)
{
	TCHAR buffer[MAX_PATH] = TEXT("");
	GetModuleFileName(reinterpret_cast<HMODULE>(module), buffer, _countof(buffer));
	HMODULE temp = LoadLibrary(buffer);
	if (temp == module)
	{
		return true;
	}
	FreeLibrary(temp);
	return false;
}

