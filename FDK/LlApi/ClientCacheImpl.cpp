#include "stdafx.h"
#include "Client.h"
#include "ClientCacheImpl.h"

namespace
{
	FxRef<CClient> ClientFromHandle(void* handle)
	{
		if (nullptr == handle)
		{
			throw CArgumentNullException("handle can not be null");
		}
		FxRef<CClient> result = TypeFromHandle<CClient>(handle);
		if (!result)
		{
			throw CInvalidHandleException("invalid handle");
		}
		return result;
	}
}



CFxSessionInfo CClientCacheImpl::GetSessionInfo(void* handle)
{
	FxRef<CClient> client = ClientFromHandle(handle);
	return client->Cache().GetSessionInfo();
}

CFxAccountInfo CClientCacheImpl::GetAccountInfo(void* /*handle*/)
{
	return CFxAccountInfo();
}
