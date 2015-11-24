#include "stdafx.h"
#include "UnsupportedFeatureException.h"


CUnsupportedFeatureException::CUnsupportedFeatureException()
    : std::runtime_error("This feature is not supported.")
    , Feature()
{
}

CUnsupportedFeatureException::CUnsupportedFeatureException(const char* message)
    : std::runtime_error(message)
    , Feature()
{
}

CUnsupportedFeatureException::CUnsupportedFeatureException(const string& message)
    : std::runtime_error(message)
    , Feature()
{
}

CUnsupportedFeatureException::CUnsupportedFeatureException(const char* message, const char* feature)
    : std::runtime_error(message)
    , Feature(feature)
{
}

CUnsupportedFeatureException::CUnsupportedFeatureException(const string& message, const string& feature)
    : std::runtime_error(message)
    , Feature(feature)
{
}
