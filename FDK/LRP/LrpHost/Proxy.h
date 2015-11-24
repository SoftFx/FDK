#pragma once
#include "LrpDll.h"
#include "LrpPipe.h"
#include "LrpHeap.h"




class CProxy
{
public:
	CProxy(const TCHAR* pipeId, const TCHAR* dllPath);
public:
	int Run();
private:
	bool Construct();
	bool RunProlog();
	void DoRun();
private:
	CLrpDll m_dll;
	CLrpPipe m_pipe;
	CLrpHeap m_heap;
};