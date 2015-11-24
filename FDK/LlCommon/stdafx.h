#pragma once

#include "targetver.h"

#ifdef _DEBUG
#define _CRTDBG_MAP_ALLOC
#endif

#define WIN32_LEAN_AND_MEAN

#include <windows.h>

#include <unordered_map>
#include <map>
#include <string>
#include <vector>
#include <assert.h>
#include <sstream>
#include <algorithm>



using namespace std;



#ifdef max
#undef max
#endif


#ifdef min
#undef min
#endif


#include "../../External/Include/lrp/Lrp.Core.h"
#define LRP_STD
#include "../../External/Include/lrp/LrpStd.h"

#include "LlCommon.h"

#ifdef _DEBUG
#include <crtdbg.h>
#define new new (_CLIENT_BLOCK, __FILE__, __LINE__)
#endif

