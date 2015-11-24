#include "stdafx.h"
#include "ParamsImpl.h"


void* CParamsImpl::Create()
{
	CFxParams* result = new CFxParams();
	return result;
}

void CParamsImpl::SetString(void* handle, const string& key, const string& value)
{
	if (nullptr == handle)
	{
		throw CArgumentNullException("handle can not be null");
	}
	FxRef<CFxParams> pParams = TypeFromHandle<CFxParams>(handle);
	if (!pParams)
	{
		throw CInvalidHandleException("invalid handle");
	}
	pParams->AddString(key, value);
}

void CParamsImpl::SetInt32(void* handle, const string& key, const int value)
{
	if (nullptr == handle)
	{
		throw CArgumentNullException("handle can not be null");
	}
	FxRef<CFxParams> pParams = TypeFromHandle<CFxParams>(handle);
	if (!pParams)
	{
		throw CInvalidHandleException("invalid handle");
	}
	pParams->AddInt32(key, value);
}

void CParamsImpl::SetDouble(void* handle, const string& key, const double value)
{
	if (nullptr == handle)
	{
		throw CArgumentNullException("handle can not be null");
	}
	FxRef<CFxParams> pParams = TypeFromHandle<CFxParams>(handle);
	if (!pParams)
	{
		throw CInvalidHandleException("invalid handle");
	}
	pParams->AddReal(key, value);
}

void CParamsImpl::SetBoolean(void* handle, const string& key, const bool value)
{
	if (nullptr == handle)
	{
		throw CArgumentNullException("handle can not be null");
	}
	FxRef<CFxParams> pParams = TypeFromHandle<CFxParams>(handle);
	if (!pParams)
	{
		throw CInvalidHandleException("invalid handle");
	}
	pParams->AddBoolean(key, value);
}

std::string CParamsImpl::ToText(void* handle)
{
	if (nullptr == handle)
	{
		throw CArgumentNullException("handle can not be null");
	}
	FxRef<CFxParams> pParams = TypeFromHandle<CFxParams>(handle);
	if (!pParams)
	{
		throw CInvalidHandleException("invalid handle");
	}
	const string result = pParams->ToString();
	return result;
}
