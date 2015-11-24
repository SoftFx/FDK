#include "stdafx.h"
#include "InvalidHandleException.h"

namespace
{
	const char* cMessage = "Invalid handle.";
}

CInvalidHandleException::CInvalidHandleException()
    : std::runtime_error(cMessage)
{
}

CInvalidHandleException::CInvalidHandleException(const char* message)
    : std::runtime_error(message)
{
}

CInvalidHandleException::CInvalidHandleException(void* /*handle*/)
    : std::runtime_error(cMessage)
{
}

CInvalidHandleException::CInvalidHandleException(const string& message)
    : std::runtime_error(message)
{
}
