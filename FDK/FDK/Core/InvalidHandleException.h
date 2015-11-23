#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)

class CORE_API CInvalidHandleException : public std::runtime_error
{
public:
	CInvalidHandleException();
	CInvalidHandleException(void* handle);
	CInvalidHandleException(const char* message);
	CInvalidHandleException(const string& message);
};


#pragma warning (pop)