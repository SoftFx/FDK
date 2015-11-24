// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently, but
// are changed infrequently
//

#pragma once


#include <SDKDDKVer.h>

#ifdef VISUALIZER_EXPORTS
#define VISUALIZER_API __declspec(dllexport)
#else
#define VISUALIZER_API __declspec(dllimport)
#endif

#pragma warning (push)
#pragma warning (disable : 4100)
#pragma warning (disable : 4702)

#include <iostream>
#include <iomanip>
#include <map>
#include <vector>
#include <xmllite.h>
#include <Windows.h>
#include <atlbase.h>
#include <assert.h>
#include <string>
#include <sstream>
#include <fstream>
#include <algorithm>
#include <regex>


namespace std
{
//	using namespace std::tr1;
}

using namespace std;
using namespace std::tr1;

#define return_if_failed(result)			return_if_true(FAILED(result), result)
#define return_if_true(condition, result)	if (condition)\
											{\
												return result;\
											}
#define break_if_true(condition)			if (condition)\
											{\
												break;\
											}
#define continue_if_true(condition)			if (condition)\
											{\
												continue;\
											}



#include "Debugger.h"
#include "FixedBuffer.h"
#ifdef max
#undef max
#endif

#ifdef min
#undef min
#endif


#pragma warning (pop)
#define stdstream FixedBuffer _buffer(buffer, max); ostream stream(&_buffer);
#define prolog stdstream Debugger debugger(helper, address);