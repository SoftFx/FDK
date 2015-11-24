#include "stdafx.h"
#include "RuntimeError.h"

CRuntimeError::CRuntimeError() : m_stream(new stringstream())
{
	m_stream->precision(DBL_DIG);
}

CRuntimeError::CRuntimeError(const char* message) : m_stream(new stringstream())
{
	m_stream->precision(DBL_DIG);
	*m_stream<<message;
}

CRuntimeError::CRuntimeError(const string& message) : m_stream(new stringstream())
{
	m_stream->precision(DBL_DIG);
	*m_stream<<message;
}

// The destructor required by GCC, because parent destructor has exception specifications.
CRuntimeError::~CRuntimeError() throw()
{
}

const char * CRuntimeError::what() const throw()
{
	const char* result  = nullptr;
	try
	{
		m_message = m_stream->str();
		result = m_message.c_str();
	}
	catch(const exception& e)
	{
		result = e.what();
	}	
	return result;
}
