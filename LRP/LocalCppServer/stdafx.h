// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently, but
// are changed infrequently
//

#pragma once

#include "targetver.h"

#define WIN32_LEAN_AND_MEAN
#include <windows.h>
#include <string>
#include <assert.h>
#include <vector>
#include <iostream>

using namespace std;

#include "LocalCppServer.h"
#define LRPCORE_API __declspec(dllimport)
#include "../Include/MemoryBuffer.h"