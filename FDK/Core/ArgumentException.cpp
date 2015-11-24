#include "stdafx.h"
#include "ArgumentException.h"

CArgumentException::CArgumentException(const char* message)
    : std::runtime_error(message)
{
}

CArgumentException::CArgumentException(const std::string& message)
    : std::runtime_error(message.c_str())
{
}
