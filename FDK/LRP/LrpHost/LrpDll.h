#pragma once


typedef const char* (__stdcall *LrpSignatureFunc)();
typedef int (__stdcall *LrpInvokeFunc)(unsigned short componentId, unsigned short methodId, void* heap, unsigned __int32* size, void** ppData, unsigned __int32* pCapacity);


class CLrpDll
{
public:
	CLrpDll(const TCHAR* dllPath);
	~CLrpDll();
public:
	const char* Signature();
	int Invoke(unsigned short componentId, unsigned short methodId, void* heap, unsigned __int32* size, void** ppData, unsigned __int32* pCapacity);
	bool IsValid() const;
	DWORD GetError() const;
	const string& GetErrorDescription() const;
private:
	void Finalize();
private:
	HMODULE m_dll;
	LrpSignatureFunc m_signature;
	LrpInvokeFunc m_invoke;
private:
	DWORD m_error;
	string m_errorDescription;
};