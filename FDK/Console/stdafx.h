// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently, but
// are changed infrequently
//

#pragma once

#include "targetver.h"
#ifdef _MSC_VER
#ifdef _DEBUG
#define _CRTDBG_MAP_ALLOC
#endif
#endif
//#include <Windows.h>


#include <stdio.h>
#include <tchar.h>
#include <assert.h>
#include <iostream>
#include <WinSock2.h>
#include <WS2tcpip.h>
#include <Wspiapi.h>

#include <malloc.h>
#include <stdio.h>
#include <crtdbg.h>
#include <vector>
#include <set>
#include <map>
#include <string>
#include <sstream>
#include <fstream>
#include <regex>
#include <memory>
#include <atomic>

//#include <DbgHelp.h>
//#pragma comment (lib, "DbgHelp")
#include <list>

namespace std
{
	using namespace std::tr1;
}
using namespace std;




//#include <MSWSock.h>

//#define LRP_STD_DEBUG_MODE
//#include "../../FRE/include/LlCommon.h"
//using namespace FDK;


#ifdef _MSC_VER
#ifdef _DEBUG
#include <crtdbg.h>
#define new new (_CLIENT_BLOCK, __FILE__, __LINE__)
#endif
#endif


#define HAVE_CUSTOM_DATE_TIME
#include "../Sal/Sal.h"

#include "../../External/Include/lrp/Lrp.Core.h"