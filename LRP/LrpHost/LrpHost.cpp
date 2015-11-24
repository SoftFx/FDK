#include "stdafx.h"
#include "LrpHost.h"
#include "Proxy.h"


int DoMain(LPTSTR lpCmdLine)
{
	try
	{
		int argc = 0;
		wchar_t** argv = CommandLineToArgvW(lpCmdLine, &argc);
		SetCurrentDirectory(argv[0]);
		CProxy proxy(argv[1], argv[2]);
		proxy.Run();
		return 0;
	}
	catch (const std::exception& e)
	{
		string message = string("[LRP] ") + e.what();
		OutputDebugStringA(message.c_str());
		return 1;
	}
}

void DoException2Log(PEXCEPTION_POINTERS pExceptionInfo)
{
	stringstream stream;
	stream<<"[LRP] Exception has been thrown: code = 0x"<<hex<<uppercase<<pExceptionInfo->ExceptionRecord->ExceptionCode<<endl;
	string st = stream.str();
	OutputDebugStringA(st.c_str());
}

LONG Exception2Log(PEXCEPTION_POINTERS pExceptionInfo)
{
	__try
	{
		DoException2Log(pExceptionInfo);
	}
	__except(EXCEPTION_EXECUTE_HANDLER)
	{
		OutputDebugStringA("[LRP] Exception2Log() - fatal error");
	}
	return EXCEPTION_EXECUTE_HANDLER;
}


int APIENTRY _tWinMain(HINSTANCE /*hInstance*/, HINSTANCE /*hPrevInstance*/, LPTSTR lpCmdLine, int /*nCmdShow*/)
{
	__try
	{
		DoMain(lpCmdLine);
		return 0;
	}
	__except(Exception2Log(GetExceptionInformation()))
	{
		return 2;
	}
}