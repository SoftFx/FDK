#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)

class CORE_API CArgumentNullException : public std::runtime_error
{
public:
	CArgumentNullException();
	CArgumentNullException(const char* message);
	CArgumentNullException(const string& message);
};


#pragma warning (pop)