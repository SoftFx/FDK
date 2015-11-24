#include "stdafx.h"
#include "LogoutException.h"


CLogoutException::CLogoutException()
    : std::runtime_error("Client has been logged out.")
{
}

CLogoutException::CLogoutException(const char* message)
    : std::runtime_error(message)
{
}

CLogoutException::CLogoutException(const std::string& message)
    : std::runtime_error(message.c_str())
{
}
