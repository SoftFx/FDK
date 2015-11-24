#include "stdafx.h"
#include "Lrp.Core.h"


namespace
{
	LPTOP_LEVEL_EXCEPTION_FILTER gExceptionHandler;
}

 

LONG LrpExceptionHandler(PEXCEPTION_POINTERS execeptionInfo)
{
	LPTOP_LEVEL_EXCEPTION_FILTER handler = gExceptionHandler;
	if(nullptr != handler)
	{
		return handler(execeptionInfo);
	}
	return EXCEPTION_EXECUTE_HANDLER;
}

extern "C"
{
	void LrpSetExceptionHandler(LPTOP_LEVEL_EXCEPTION_FILTER handler)
	{
		gExceptionHandler = handler;
	}
}
