#pragma once

#include "targetver.h"

// Windows Header Files:
#include <windows.h>

// C RunTime Header Files
#include <stdlib.h>
#include <malloc.h>
#include <memory.h>
#include <tchar.h>
#include <string>
#include <assert.h>
#include <memory>
#include <atlbase.h>
#include <atlconv.h>
#include <sstream>
#include <iomanip>
using namespace ATL;

#include "../Lrp.Core.Cpp/MemoryBuffer.h"



using namespace std;

#ifdef _UNICODE
typedef wstring tstring;
#else
typedef string tstring;
#endif


#define PIPE_BUFFER_SIZE (1024 * 1024)