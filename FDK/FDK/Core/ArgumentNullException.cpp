#include "stdafx.h"
#include "ArgumentNullException.h"

CArgumentNullException::CArgumentNullException()
    : std::runtime_error("Argument can not be null.")
{
}

CArgumentNullException::CArgumentNullException(const char* message)
    : std::runtime_error(message)
{
}

CArgumentNullException::CArgumentNullException(const string& message)
    : std::runtime_error(message)
{
}
