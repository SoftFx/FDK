#pragma once

#ifdef LLCOMMON_EXPORTS
#define LLCOMMON_API __declspec(dllexport)
#else
#define LLCOMMON_API __declspec(dllimport)
#endif


#ifndef LLCOMMON_EXPORTS
#define internal private
#include "LrpStd.h"
#include "Nullable.h"
#include "FinancialCalculator.h"
#else
#define internal public
#endif

