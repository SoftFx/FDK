#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)

class CORE_API CSendException : public std::runtime_error
{
public:
	CSendException();
	CSendException(const char* message);
	CSendException(const std::string& message);
};


#pragma warning (pop)