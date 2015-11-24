#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)
#pragma warning (disable : 4251)

class CORE_API CTimeoutException : public std::runtime_error
{
public:
	int32 WaitingInterval;
    string OperationId;
public:
	CTimeoutException();
    CTimeoutException(const char* message);
    CTimeoutException(const string& message);
	CTimeoutException(const char* operationId, const int32 waitingInterval);
	CTimeoutException(const string& operationId, const int32 waitingInterval);
};


#pragma warning (pop)