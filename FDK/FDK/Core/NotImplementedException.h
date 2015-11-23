#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)

class CORE_API CNotImplementedException : public std::runtime_error
{
public:
	CNotImplementedException();
	CNotImplementedException(const char* message);
};


#pragma warning (pop)