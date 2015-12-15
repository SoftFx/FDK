#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)

class CORE_API CArgumentException : public std::runtime_error
{
public:
	CArgumentException(const char* message);
	CArgumentException(const std::string& message);
};
#pragma warning (pop)