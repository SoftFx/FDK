#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)

class CORE_API CRejectException : public std::runtime_error
{
public:
	int Code;
public:
	CRejectException(const char* message);
	CRejectException(const std::string& message);
	CRejectException(const std::string& message, int code);
};
#pragma warning (pop)