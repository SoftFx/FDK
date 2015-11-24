#pragma once



class CLrpLocalClientImpl;
class LRPCORE_API CLrpLocalClient
{
public:
	CLrpLocalClient();
	CLrpLocalClient(const char* localSignature, const char* dllPath, const char* typeName);
	~CLrpLocalClient();
public:
	HRESULT Construct(const char* localSignature, const char* dllPath, const char* typeName);
public:
	void Initialize(MemoryBuffer& buffer);
	HRESULT Invoke(const unsigned __int16 componentId, const unsigned __int16 methodId, MemoryBuffer& buffer);
	bool IsSupported(const unsigned __int16 componentId) const;
	bool IsSupported(const unsigned __int16 componentId, const unsigned __int16 methodId) const;
private:
	CLrpLocalClientImpl* m_impl;
};