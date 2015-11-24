#include "stdafx.h"
#include "RejectException.h"

CRejectException::CRejectException(const char* message)
    : std::runtime_error(message)
    , Code()
{
}

CRejectException::CRejectException(const std::string& message)
    : std::runtime_error(message)
    , Code()
{
}

CRejectException::CRejectException(const std::string& message, int code)
    : std::runtime_error(message)
    , Code(code)
{
}
