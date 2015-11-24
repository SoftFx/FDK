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
#ifndef _MSC_VER
#pragma GCC diagnostic ignored "-Wdeprecated-declarations"
#endif

#include <atlbase.h>
#include <sys/timeb.h>
#include <algorithm>
#include <cctype>
#include <cstdio>
#include <cstdio>
#include <cstring>
#include <ctime>
#include <errno.h>
#include <exception>
#include <fcntl.h>
#include <fstream>

#include <functional>
#include <iomanip>

#include <iostream>
#include <iterator>
#include <limits>
#include <map>
#include <math.h>
#include <memory>
#include <numeric>

#include <queue>
#include <set>
#include <signal.h>
#include <sstream>
#include <stack>
#include <stdarg.h>
#include <stdexcept>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <string>
#include <time.h>
#include <typeinfo>
#include <utility>
#include <vector>
#include <regex>
#include <assert.h>


//namespace std
//{
//	using namespace std::tr1;
//}

#define FX_OVERRIDE_WINSOCKS
#include "../Sal/Sal.h"


#ifdef _MSC_VER
#import <msxml3.dll> raw_interfaces_only named_guids rename("send", "msxmlsend")
using namespace MSXML2;
#pragma warning (disable : 4251)
#endif




#include "../Core/Types.h"
#define CORE_API __declspec(dllimport)


#ifdef _MSC_VER
#ifdef _DEBUG
#include <crtdbg.h>
#define new new (_CLIENT_BLOCK, __FILE__, __LINE__)
#endif
#endif




