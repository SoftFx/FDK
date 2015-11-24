#include "stdafx.h"
#include "LibraryImpl.h"
#include "DllMain.h"

namespace
{
	CCriticalSection gSynchronizer;
	bool gCreateNormalDump = false;
	bool gCreateFullDump = false;
	bool gCrashHandlerEnabled = false;
	wchar_t gCreateNormalDumpCmd[1024] = L"";
	wchar_t gCreateFullDumpCmd[1024] = L"";
	const wchar_t* cNormalDumpType = L"";
	const wchar_t* cFullDumpType = L"-ma";
}


namespace
{
	void RunProcess(wchar_t* cmd)
	{
		STARTUPINFOW inInfo;
		ZeroMemory(&inInfo, sizeof(inInfo));
		inInfo.cb = sizeof(inInfo);

		PROCESS_INFORMATION outInfo;
		ZeroMemory(&outInfo, sizeof(outInfo));
		const BOOL status = CreateProcessW(NULL, cmd, NULL, NULL, FALSE, 0, NULL, NULL, &inInfo, &outInfo);
		if (status)
		{
			WaitForSingleObject(outInfo.hProcess, INFINITE);
			CloseHandle(outInfo.hProcess);
			CloseHandle(outInfo.hThread);
		}
	}
}


namespace
{
	/// <summary>
	/// Victor Marmysh - 23:01:2009 - 12:34:21
	/// The function tries to create two mini dumps (normal and full).
	/// </summary>
	/// <param name="ExceptionInfo">pointer to exception info; can't be null</param>
	/// <returns>always EXCEPTION_EXECUTE_HANDLER</returns>
	LONG WINAPI UnhandledExceptionHandler(EXCEPTION_POINTERS* pExceptionInfo)
	{
		pExceptionInfo;
		if (gCreateNormalDump)
		{
			RunProcess(gCreateNormalDumpCmd);
		}
		if (gCreateFullDump)
		{
			RunProcess(gCreateFullDumpCmd);
		}
		return EXCEPTION_EXECUTE_HANDLER;
	}
}

namespace
{
	/// <summary>
	/// Victor Marmysh - 23:01:2009 - 12:31:50
	/// The function replaces default CRT handler for invalid parameters.
	/// You should replace default invalid parameters handler, if you want to use SetUnhandledExceptionFilter
	/// The function does nothing.
	/// </summary>
	void InvalidParameterHandler(const wchar_t* /*expression*/, const wchar_t* /*function*/, const wchar_t* /*file*/, unsigned int /*line*/, uintptr_t /*pReserved*/)
	{
		__try
		{
			throw nullptr;
		}
		__except(UnhandledExceptionHandler(GetExceptionInformation()))
		{
			TerminateProcess(GetCurrentProcess(), 1);
		}
	}
	void AbortHandler(int /*signal_number*/)
	{
		__try
		{
			throw nullptr;
		}
		__except(UnhandledExceptionHandler(GetExceptionInformation()))
		{
			TerminateProcess(GetCurrentProcess(), 1);
		}
	}
}


/// <summary>
/// [2/26/2010 marmysh]
/// The function change a crash handler default behavior.
/// </summary>
void InitCrashHandler()
{
	// set CRT settings
	_set_abort_behavior(_CALL_REPORTFAULT, 1);
	_set_invalid_parameter_handler(InvalidParameterHandler);
	signal(SIGABRT, AbortHandler);

	// set custom unhandled exception filter
	SetUnhandledExceptionFilter(UnhandledExceptionHandler);

	gCrashHandlerEnabled = true;
}


namespace
{
    void FormatCmd(const wstring& path, const wchar_t* type, wchar_t* cmd, size_t _SizeInWords)
	{
		const tstring& dllLocation = GetDllLocation();
        swprintf_s(cmd, _SizeInWords, L"\"%sprocdump.exe\" -accepteula -o %s %d \"%s\"", dllLocation.c_str(), type, GetCurrentProcessId(), path.c_str());
	}
}

void CLibraryImpl::WriteNormalDumpOnError(const wstring& path)
{
	CLock lock(gSynchronizer);
	gCreateNormalDump = true;
    FormatCmd(path, cNormalDumpType, gCreateNormalDumpCmd, sizeof(gCreateNormalDumpCmd) / sizeof(gCreateNormalDumpCmd[0]));

	if (!gCrashHandlerEnabled)
	{
		InitCrashHandler();
	}
}

void CLibraryImpl::WriteFullDumpOnError(const wstring& path)
{
	CLock lock(gSynchronizer);
	gCreateFullDump = true;
    FormatCmd(path, cFullDumpType, gCreateFullDumpCmd, sizeof(gCreateNormalDumpCmd) / sizeof(gCreateNormalDumpCmd[0]));

	if (!gCrashHandlerEnabled)
	{
		InitCrashHandler();
	}
}

void CLibraryImpl::WriteNormalDump(const wstring& path)
{
	wchar_t cmd[1024] = L"";
    FormatCmd(path.c_str(), cNormalDumpType, cmd, sizeof(cmd) / sizeof(cmd[0]));
	RunProcess(cmd);
}
void CLibraryImpl::WriteFullDump(const wstring& path)
{
	wchar_t cmd[1024] = L"";
    FormatCmd(path.c_str(), cFullDumpType, cmd, sizeof(cmd) / sizeof(cmd[0]));
	RunProcess(cmd);
}
