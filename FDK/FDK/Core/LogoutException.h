#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)

class CORE_API CLogoutException : public std::runtime_error
{
public:
	CLogoutException();
	CLogoutException(const char* message);
	CLogoutException(const std::string& message);
};


#pragma warning (pop)