#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)
#pragma warning (disable : 4251)

class CORE_API CUnsupportedFeatureException : public std::runtime_error
{
public:
    string Feature;
public:
	CUnsupportedFeatureException();
    CUnsupportedFeatureException(const char* message);
    CUnsupportedFeatureException(const string& message);
	CUnsupportedFeatureException(const char* message, const char* feature);
	CUnsupportedFeatureException(const string& message, const string& feature);
};


#pragma warning (pop)