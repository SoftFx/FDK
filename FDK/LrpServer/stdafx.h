#pragma once

#include "targetver.h"

#define _WINSOCK_DEPRECATED_NO_WARNINGS

#ifdef _DEBUG
#define _CRTDBG_MAP_ALLOC
#endif


//#define WIN32_LEAN_AND_MEAN             // Exclude rarely-used stuff from Windows headers
// Windows Header Files:
//#include <windows.h>
#include <WinSock2.h>
#include <WS2tcpip.h>
#include <Wspiapi.h>
#include <MetaHost.h>


#include <atlbase.h>
#include <atlcore.h>

#include <string>
#include <exception>
#include <set>
#include <map>
#include <unordered_map>
#include <MSTcpIP.h>
#include <deque>
#include <vector>
#include <assert.h>
#include <limits>
#include <iostream>
#include <sstream>
#include <fstream>
#include <iomanip>
#include <regex>
#include <memory>
#include <list>

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


const uint32 cInitialProtocolVersion = 0;
const uint32 cMaximumStringLength = 1024;


#define HAVE_CUSTOM_DATE_TIME


#define FX_OVERRIDE_WINSOCKS
#include "../Sal/Sal.h"
#include "../Sal/Threading.h"
#include "../Core/Core.h"

#include "../../External/Include/lrp/Lrp.Core.h"
#include "../LrpCommon/LrpCommon.h"





#ifdef _DEBUG
#include <crtdbg.h>
#define new new (_CLIENT_BLOCK, __FILE__, __LINE__)
#endif




