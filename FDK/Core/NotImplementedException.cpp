#include "stdafx.h"
#include "NotImplementedException.h"

CNotImplementedException::CNotImplementedException()
    : std::runtime_error("The method / function is not implemented.")
{
}

CNotImplementedException::CNotImplementedException(const char* message)
    : std::runtime_error(message)
{	
}
