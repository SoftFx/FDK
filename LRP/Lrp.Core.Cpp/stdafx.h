// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently, but
// are changed infrequently
//

#pragma once

#define _WINSOCK_DEPRECATED_NO_WARNINGS

#include "targetver.h"

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






#ifdef max
#undef max
#endif


#ifdef min
#undef min
#endif



using namespace std;


typedef ATL::CComAutoCriticalSection CriticalSection;

typedef ATL::CComCritSecLock<CriticalSection> CLock;



#include "Lrp.Core.h"

typedef __int8 int8;
typedef __int16 int16;
typedef __int32 int32;
typedef __int64 int64;

typedef unsigned __int8 uint8;
typedef unsigned __int16 uint16;
typedef unsigned __int32 uint32;
typedef unsigned __int64 uint64;


const uint32 cInitialProtocolVersion = 0;
const uint32 cMaximumStringLength = 1024;






