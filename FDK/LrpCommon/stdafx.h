// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently, but
// are changed infrequently
//

#pragma once

#include "targetver.h"

#define WIN32_LEAN_AND_MEAN             // Exclude rarely-used stuff from Windows headers

#define _WINSOCK_DEPRECATED_NO_WARNINGS

#ifdef _DEBUG
#define _CRTDBG_MAP_ALLOC
#endif

#include <WinSock2.h>
#include <WS2tcpip.h>
#include <Wspiapi.h>
#include <MetaHost.h>

#include <string>
#include <vector>
#include <sstream>
#include <map>
#include <set>
#include <limits>
#include <fstream>
#include <memory>
#include <assert.h>
#include <iomanip>


#define FX_OVERRIDE_WINSOCKS
#include "../Sal/Sal.h"


#ifdef max
#undef max
#endif


#ifdef min
#undef min
#endif


using namespace std;


typedef unsigned __int16 uint16;
typedef unsigned __int32 uint32;
typedef unsigned __int64 uint64;

typedef __int16 int16;
typedef __int32 int32;
typedef __int64 int64;



#define HAVE_CUSTOM_DATE_TIME


#ifdef FX_OVERRIDE_WINSOCKS
#undef FX_OVERRIDE_WINSOCKS
#endif


#include "../Sal/Sal.h"
#include "../Sal/Threading.h"


#pragma warning (push)
#pragma warning (disable : 4251)
#include "../Core/Core.h"
#pragma warning (pop)



#include "../../External/Include/lrp/Lrp.Core.h"
#include "../LrpCommon/LrpCommon.h"



inline void LrpWriteRaw(const char* /*name*/, const MemoryBuffer& /*value*/, ostream& /*stream*/)
{
}



#ifdef _DEBUG
#include <crtdbg.h>
#define new new (_CLIENT_BLOCK, __FILE__, __LINE__)
#endif




typedef unsigned __int16 uint16;