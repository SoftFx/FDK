#ifndef __Core_Api_Core__
#define __Core_Api_Core__


#ifndef CORE_API
#ifdef CORE_EXPORTS
#define CORE_API EXPORT_API
#else
#define CORE_API IMPORT_API
#endif
#endif


#include "../../External/Include/lrp/Nullable.h"

#include "Types.h"
#include "StartupInitializer.h"
#include "IConnection.h"
#include "FxHandle.h"
#include "FxParams.h"
#include "FxMessage.h"
#include "FxMsgLogout.h"
#include "FxTwoFactorAuth.h"
#include "FxSessionInfo.h"
#include "FxAccountInfo.h"
#include "FxSymbolInfo.h"
#include "FxFileChunk.h"
#include "FxDataHistoryRequest.h"
#include "FxDataHistoryResponse.h"
#include "FxClosePositionResult.h"
#include "RuntimeError.h"
#include "FxMsgData.h"
#include "FxIterator.h"
#include "HandlesPool.h"
#include "FxRef.h"
#include "DataHistoryInfo.h"
#include "Notification.h"
#include "FxCurrencyInfo.h"


#include "ArgumentNullException.h"
#include "ArgumentException.h"
#include "InvalidHandleException.h"
#include "NotImplementedException.h"
#include "SendException.h"
#include "TimeoutException.h"
#include "RejectException.h"
#include "LogoutException.h"
#include "UnsupportedFeatureException.h"

template<typename T> FxRef<T> TypeFromHandle(FxHandle handle)
{
    CFxHandle* pInstance = reinterpret_cast<CFxHandle*>(handle);
    T* result = CHandlesPool::TypeFromHandle<T>(pInstance);
    return result;
}

template<typename T> void* HandleFromType(T* handle)
{
    CFxHandle* pHandle = static_cast<CFxHandle*>(handle);
    void* result = pHandle;
    return result;
}

#endif
