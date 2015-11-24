
#ifdef _MSC_VER
#ifdef _DEBUG
#define _CRTDBG_MAP_ALLOC
#endif
#endif

#define _WIN32_WINNT 0x0501

#include <stdio.h>
#include <sys/timeb.h>
#include <time.h>
#include <numeric>
#include <vector>
#include <string>
#include <assert.h>
#include <map>
#include <stdexcept>
#include <fstream>
#include <iomanip>
#include <memory>
#include <atomic>
#include <algorithm>


using namespace std;

#ifndef SAL_EXPORTS
#define SAL_EXPORTS
#endif

#include "Sal.h"
#include "openssl/ssl.h"

#ifdef _MSC_VER
#include <atlbase.h>
#ifdef _DEBUG
#include <crtdbg.h>
#define new new (_CLIENT_BLOCK, __FILE__, __LINE__)
#endif
#else
#include <uuid/uuid.h>
#include <unistd.h>
#endif


#ifdef AddJob
#undef AddJob
#endif