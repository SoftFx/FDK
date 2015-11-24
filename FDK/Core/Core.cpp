#include "stdafx.h"
#include "Core.h"
#include "FxParams.h"


//
HRESULT FxDelete(FxHandle handle)
{
	if (nullptr == handle)
	{
		return S_FALSE;
	}
	CFxHandle* pHandle = reinterpret_cast<CFxHandle*>(handle);
	pHandle->Release();
	return S_OK;
}