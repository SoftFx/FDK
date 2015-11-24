#include "stdafx.h"
#include "LrpDll.h"

namespace
{
	const char* cLrpSignatureName = "LrpSignature";
	const char* cLrpInvokeName = "LrpInvoke";
}



CLrpDll::CLrpDll(const TCHAR* dllPath) : m_dll(), m_signature(), m_invoke(), m_error()
{
	m_dll = LoadLibraryEx(dllPath, nullptr, LOAD_WITH_ALTERED_SEARCH_PATH);
	if (nullptr != m_dll)
	{
		m_signature = reinterpret_cast<LrpSignatureFunc>(GetProcAddress(m_dll, cLrpSignatureName));
		m_invoke = reinterpret_cast<LrpInvokeFunc>(GetProcAddress(m_dll, cLrpInvokeName));
		if ((nullptr == m_signature) || (nullptr == m_invoke))
		{
			m_errorDescription = "Lrp functions are not found";
		}
	}
	else
	{
		m_error = GetLastError();
		m_errorDescription = string("Couldn't load dll from ") + CW2A(dllPath).operator LPSTR();
	}
	if ((nullptr == m_signature) || (nullptr == m_invoke))
	{
		Finalize();
	}
}
CLrpDll::~CLrpDll()
{
	Finalize();
}
void CLrpDll::Finalize()
{
	m_invoke = nullptr;
	m_signature = nullptr;
	if (nullptr != m_dll)
	{
		FreeLibrary(m_dll);
		m_dll = nullptr;
	}
}

const char* CLrpDll::Signature()
{
	return m_signature();
}
int CLrpDll::Invoke(unsigned short componentId, unsigned short methodId, void* heap, unsigned __int32* size, void** ppData, unsigned __int32* pCapacity)
{
	return m_invoke(componentId, methodId, heap, size, ppData, pCapacity);
}
bool CLrpDll::IsValid() const
{
	const bool result = (nullptr != m_dll) && (nullptr != m_signature) && (nullptr != m_invoke);
	return result;
}
DWORD CLrpDll::GetError() const
{
	return m_error;
}
const string& CLrpDll::GetErrorDescription() const
{
	return m_errorDescription;
}
