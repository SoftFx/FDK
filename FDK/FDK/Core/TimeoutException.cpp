#include "stdafx.h"
#include "TimeoutException.h"


namespace
{
	const string cTimeoutExceptionTextProlog = "Timeout of synchronous operation has been reached";
}

namespace
{
	string MakeTimeoutExceptionText(const string& id)
	{
		string result = cTimeoutExceptionTextProlog + "; id = " + id;
		return result;
	}
}

CTimeoutException::CTimeoutException()
    : std::runtime_error(cTimeoutExceptionTextProlog)
    , WaitingInterval()
    , OperationId("")
{
}

CTimeoutException::CTimeoutException(const char* message)
    : std::runtime_error(message)
    , WaitingInterval()
    , OperationId()
{
}

CTimeoutException::CTimeoutException(const string& message)
    : std::runtime_error(message)
    , WaitingInterval()
    , OperationId()
{
}

CTimeoutException::CTimeoutException(const char* operationId, const int32 waitingInterval)
    : std::runtime_error(MakeTimeoutExceptionText(operationId))
    , WaitingInterval(waitingInterval)
    , OperationId(operationId)
{
}

CTimeoutException::CTimeoutException(const string& operationId, const int32 waitingInterval)
    : std::runtime_error(MakeTimeoutExceptionText(operationId))
    , WaitingInterval(waitingInterval)
    , OperationId(operationId)
{
}
