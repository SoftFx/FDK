#include "stdafx.h"
#include "HandleImpl.h"

void CHandleImpl::Delete(void* handle)
{
	FxDelete(handle);
}
CNotification CHandleImpl::NotificationFromHandle(void* handle)
{
	CFxMsgData<CNotification>* ptr = reinterpret_cast<CFxMsgData<CNotification>*>(handle);
	if (nullptr == ptr)
	{
		throw CArgumentNullException("handle can not be null");
	}
	return ptr->Data();
}
