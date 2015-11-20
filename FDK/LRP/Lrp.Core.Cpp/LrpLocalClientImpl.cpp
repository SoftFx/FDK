#include "stdafx.h"
#include "LrpLocalClientImpl.h"
#include "DotNetBridge.h"
#include "MemoryBuffer.h"


namespace
{
	const string cMethodName = "LrpInitialize";
}



CLrpLocalClientImpl::CLrpLocalClientImpl(const char* localSignature, const char* dllPath, const char* typeName) : CLrpClientImpl(localSignature) , m_invoke()
{
	CDotNetBridge bridge;

	stringstream stream;
	stream<<"callback="<<reinterpret_cast<__int64>(&CLrpLocalClientImpl::CallBack)<<';';
	stream<<"param="<<reinterpret_cast<__int64>(this)<<';';

	string argument = stream.str();
	bridge.Execute(dllPath, typeName, cMethodName, argument);
	if (nullptr == m_invoke)
	{
		throw runtime_error("LRP initialization error: invoke handler is null");
	}
}
CLrpLocalClientImpl::~CLrpLocalClientImpl()
{
}
HRESULT CLrpLocalClientImpl::Invoke(unsigned __int16 componentId, unsigned __int16 methodId, MemoryBuffer& buffer)
{
	Translate(componentId, methodId);

	void* heap = buffer.GetHeap();
	size_t size = buffer.GetSize();
	void* pData = buffer.GetData();
	size_t capacity = buffer.GetSize();
	const HRESULT result = m_invoke(componentId, methodId, heap, &size, &pData, &capacity);

	buffer.ReInitialize(capacity, size, pData);

	return result;
}
void CLrpLocalClientImpl::CallBack(const char* signature, void* invoke, void* pParam)
{
	CLrpLocalClientImpl* pThis = reinterpret_cast<CLrpLocalClientImpl*>(pParam);
	pThis->DoCallBack(signature, invoke);
}
void CLrpLocalClientImpl::DoCallBack(const char* signature, void* invoke)
{
	try
	{
		this->Initialize(signature);
		m_invoke = reinterpret_cast<LrpInvokeFunc>(invoke);
	}
	catch (const std::exception&)
	{
	}
}
