#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)
#pragma warning (disable : 4251)

class CORE_API CRuntimeError : public exception
{
public:
	CRuntimeError();
	CRuntimeError(const char* message);
	CRuntimeError(const string& message);
	virtual ~CRuntimeError() throw();
public:
	template<typename T> CRuntimeError& operator + (const T& arg)
	{
		*m_stream<<arg;
		return *this;
	}
public:
	virtual const char* what() const throw();
private:
	shared_ptr<stringstream> m_stream;
	mutable string m_message;
};

#pragma warning (pop)
