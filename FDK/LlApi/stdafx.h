#ifndef __Nativestdafx__
#define __Nativestdafx__
// or project specific include files that are used frequently, but
// are changed infrequently
//


#ifdef _MSC_VER
#ifdef _DEBUG
#define _CRTDBG_MAP_ALLOC
#endif
#endif

#ifndef _MSC_VER
#pragma GCC diagnostic ignored "-Wdeprecated-declarations"
#endif


#include <assert.h>
#include <memory>
#include <map>
#include <set>
#include <queue>
#include <list>
#include <stack>
#include <numeric>
#include <iostream>
#include <fstream>
#include <typeinfo>
#include <numeric>
#include <vector>
#include <sstream>
#include <regex>
#include <memory.h>
#include <utility>
#include <process.h>
#include <signal.h>

#include "../Sal/Sal.h"
#include "../Sal/Threading.h"

//#ifdef _MSC_VER
//
//namespace std
//{
//	using namespace std::tr1;
//}
//
//#endif

using namespace std;

#pragma warning (push)
#pragma warning (disable : 4251)
#define FX_OVERRIDE_WINSOCKS
#include "../Core/Core.h"
#pragma warning (pop)


#include "LlApi.h"


#ifdef _MSC_VER
#ifdef _DEBUG
#include <crtdbg.h>
#define new new (_CLIENT_BLOCK, __FILE__, __LINE__)
#endif
#endif

#define LRPCORE_API __declspec(dllimport)

#endif
