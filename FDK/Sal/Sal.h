#ifndef __Sal_Sal__
#define __Sal_Sal__

#ifdef __INTEL_COMPILER
#define nullptr 0
#endif



#include "Types.h"

#ifdef _MSC_VER
#include "Sal.Windows.h"
#else
#include "Sal.Linux.h"
#endif


#ifdef SAL_EXPORTS
#define SAL_API EXPORT_API
#else
#define SAL_API IMPORT_API
#endif


SAL_API std::string FxGenerateGuid();
SAL_API std::string FxStartupDirectory();
SAL_API uint64 FxGetTickCount();
SAL_API std::string FxStringFromResource(void* module, const std::string& id, const std::string& type);
SAL_API bool IsFinite(const double value);
SAL_API size_t FxInterlockedIncrement(size_t* pValue);
SAL_API size_t FxInterlockedDecrement(size_t* pValue);
SAL_API void GetFiles(const tstring& directory, std::vector<tstring>& files, const tstring& mask = tstring());
SAL_API std::string FxCombinePath(const std::string& directory, const std::string& fileName);



#ifndef SAL_EXPORTS
#include "CriticalSection.h"
#include "Semaphore.h"
#include "SharedExclusiveLock.h"
#include "Lock.h"
#include "Text.h"
#include "DateTime.h"
#include "TlsValue.h"
#include "Networking.h"
#include "SocketActivity.h"
#include "Compression.h"
#pragma warning (push)
#pragma warning (disable : 4251) // 'identifier' : class 'type' needs to have dll-interface to be used by clients of class 'type2'
#include "Logger.h"
#include "LogStream.h"
#pragma warning (pop)
#endif

#define countof(array) (sizeof(array)/sizeof(array[0]))

#endif