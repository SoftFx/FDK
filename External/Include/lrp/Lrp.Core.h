#ifdef LRPCORE_EXPORTS
#define LRPCORE_API __declspec(dllexport)
#else
#define LRPCORE_API __declspec(dllimport)
#endif




class MemoryBuffer;
typedef HRESULT (__stdcall *LrpInvokeHandler)(size_t componentId, size_t methodId, MemoryBuffer& buffer, void* handle);


class CLrpStClientImpl;
typedef void (*LrpLogHandler)(void* pUserParam, const char* message);

#ifndef LRPCORE_EXPORTS
#include "MemoryBuffer.h"
#include "BitReader.h"
#include "BitWriter.h"
#include "LrpMtServer.h"
#include "LrpStServer.h"
#include "LrpTextStream.h"
#include "LrpStClient.h"
#include "LrpLocalClient.h"
extern "C" void LrpSetExceptionHandler(LPTOP_LEVEL_EXCEPTION_FILTER handler);
#else
LONG LrpExceptionHandler(PEXCEPTION_POINTERS execeptionInfo);
#endif



