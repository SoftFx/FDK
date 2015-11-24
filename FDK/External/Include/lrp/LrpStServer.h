#pragma once


class CStServerImpl;
class LRPCORE_API CLrpStServer
{
protected:
	CLrpStServer();
	CLrpStServer(const int port, const char* signature, LrpInvokeHandler handler);
	CLrpStServer(const int port, const char* signature, LrpInvokeHandler handler, LrpLogHandler pLogHandler, void* pUserParam);
	CLrpStServer(const int port, const char* signature, LrpInvokeHandler handler, const char* logPath);
	virtual ~CLrpStServer();
protected:
	HRESULT Construct(const int port, const char* signature, LrpInvokeHandler handler);
	HRESULT Construct(const int port, const char* signature, LrpInvokeHandler handler, LrpLogHandler pLogHandler, void* pUserParam);
	HRESULT Construct(const int port, const char* signature, LrpInvokeHandler handler, const char* logPath);
private:
	CLrpStServer(const CLrpStServer&);
	CLrpStServer& operator = (const CLrpStServer&);
private:
	virtual void* CreateNewConnectionInternal(const char* address) = 0;
	virtual bool ValidateCredentialsInternal(const char* username, const char* password, void* handle);
	virtual void ShutdownConnectionInternal(void* handle);
private:
	CStServerImpl* m_impl;
	friend class CStServerImpl;
};

template<typename T> class LrpStServer : public CLrpStServer
{
protected:
	LrpStServer()
	{
	}
	LrpStServer(const int port, const char* signature, LrpInvokeHandler handler) :
		CLrpStServer(port, signature, handler)
	{
	}
	LrpStServer(const int port, const char* signature, LrpInvokeHandler handler, LrpLogHandler pLogHandler, void* pUserParam) :
		CLrpStServer(port, signature, handler, pLogHandler, pUserParam)
	{
	}
	LrpStServer(const int port, const char* signature, LrpInvokeHandler handler, const char* logPath) :
		CLrpStServer(port, signature, handler, logPath)
	{
	}
protected:
	virtual T* CreateNewConnection(const std::string& address) = 0;
	virtual bool ValidateCredentials(const std::string& username, const std::string& password, T* /*handle*/)
	{
		return ValidateCredentials(username, password);
	}
	virtual bool ValidateCredentials(const std::string& /*username*/, const std::string& /*password*/)
	{
		return true;
	}
	virtual void ShutdownConnection(T* /*handle*/)
	{
	}
private:
	virtual void* CreateNewConnectionInternal(const char* address)
	{
		return CreateNewConnection(std::string(address));
	}
	virtual bool ValidateCredentialsInternal(const char* username, const char* password, void* handle)
	{
		return ValidateCredentials(std::string(username), std::string(password), reinterpret_cast<T*>(handle));
	}
	virtual void ShutdownConnectionInternal(void* handle)
	{
		ShutdownConnection(reinterpret_cast<T*>(handle));
	}
};