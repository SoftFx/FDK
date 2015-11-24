#pragma once
#include "LrpClientImpl.h"


typedef HRESULT (__stdcall *LrpInvokeFunc)(unsigned __int16 componentId, unsigned __int16 methodId, void* heap, size_t* pSize, void** ppData, size_t* pCapacity);


class CLrpLocalClientImpl : public CLrpClientImpl
{
public:
	CLrpLocalClientImpl(const char* localSignature, const char* dllPath, const char* typeName);
	~CLrpLocalClientImpl();
public:
	HRESULT Invoke(unsigned __int16 componentId, unsigned __int16 methodId, MemoryBuffer& buffer);
private:
	CLrpLocalClientImpl(const CLrpLocalClientImpl&);
	CLrpLocalClientImpl& operator = (const CLrpLocalClientImpl&);
private:
	static void __stdcall CallBack(const char* signature, void* invoke, void* pParam);
	void DoCallBack(const char* signature, void* invoke);
private:
	LrpInvokeFunc m_invoke;
};