#ifndef __Corestdafx__
#define __Corestdafx__


#ifdef _MSC_VER
#ifdef _DEBUG
#define _CRTDBG_MAP_ALLOC
#endif
#endif


#ifndef _MSC_VER
#pragma GCC diagnostic ignored "-Wdeprecated-declarations"
#endif

#include <string>
#include <locale>
#include <assert.h>
#include <map>
#include <string>
#include <vector>
#include <string>
#include <sstream>
#include <set>
#include <regex>
#include <iostream>
#include <fstream>
#include <float.h>
#include <memory.h>
#include <list>
#include <memory>


#include "../Sal/Sal.h"
#include "../Sal/Threading.h"
#include "../../External/Include/lrp/Nullable.h"



namespace std
{
	using namespace std::tr1;
}


using namespace std;





#ifdef _MSC_VER
#ifdef _DEBUG
#include <crtdbg.h>
#define new new (_CLIENT_BLOCK, __FILE__, __LINE__)
#endif
#endif



#ifdef max
#undef max
#endif


#ifdef min
#undef min
#endif

#include "Types.h"
#ifndef CORE_API
#define CORE_API EXPORT_API
#endif


#ifdef _MSC_VER
#pragma warning (disable : 4251)
#endif
#endif
