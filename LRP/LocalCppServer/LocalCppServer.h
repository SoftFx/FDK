#ifdef LOCALCPPSERVER_EXPORTS
#define LOCALCPPSERVER_API __declspec(dllexport)
#else
#define LOCALCPPSERVER_API __declspec(dllimport)
#endif




#ifndef LOCALCPPSERVER_EXPORTS
#include "Simple.h"
#endif