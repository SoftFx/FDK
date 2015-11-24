#include "stdafx.h"
#include "SendException.h"


CSendException::CSendException()
    : std::runtime_error("Server request has not been send.")
{
}

CSendException::CSendException(const char* message)
    : std::runtime_error(message)
{
}

CSendException::CSendException(const std::string& message)
    : std::runtime_error(message.c_str())
{
}
