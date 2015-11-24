#pragma once
//#ifdef LRPCORE_EXPORTS
//#include "MtServerImpl.h"
//#endif
//
//
//
//class LRPCORE_API CLrpServer
//{
//public:
//	CLrpServer(const int port, const size_t numberOfThreadsInPool, const size_t maxJobsNumber, LrpInvokeHandler handler);
//	virtual ~CLrpServer();
//protected:
//	CLrpServer();
//protected:
//	void Construct(const int port, const size_t numberOfThreadsInPool, const size_t maxJobsNumber, LrpInvokeHandler handler);
//private:
//	CLrpServer(const CLrpServer&);
//	CLrpServer& operator = (const CLrpServer&);
//private:
//	virtual void* CreateNewConnectionInternal(const char* address) = 0;
//	virtual bool ValidateCredentialsInternal(const char* username, const char* password);
//	virtual void ShutdownConnectionInternal(void* handle);
//private:
//	#ifdef LRPCORE_EXPORTS
//	CMtServerImpl* m_impl;
//	friend class CMtServerImpl;
//	#else
//	void* m_impl;
//	#endif
//};
//
//template<typename T> class LrpServer : public CLrpServer
//{
//public:
//	LrpServer(const int port, const size_t numberOfThreadsInPool, const size_t maxJobsNumber, LrpInvokeHandler handler) :
//		CLrpServer(port, numberOfThreadsInPool, maxJobsNumber, handler)
//	{
//	}
//protected:
//	LrpServer()
//	{
//	}
//public:
//	virtual T* CreateNewConnection(const std::string& address) = 0;
//	virtual bool ValidateCredentials(const std::string& /*username*/, const std::string& /*password*/)
//	{
//		return true;
//	}
//	virtual void ShutdownConnection(T* /*handle*/)
//	{
//	}
//private:
//	virtual void* CreateNewConnectionInternal(const char* address)
//	{
//		return CreateNewConnection(std::string(address));
//	}
//	virtual bool ValidateCredentialsInternal(const char* username, const char* password)
//	{
//		return ValidateCredentials(std::string(username), std::string(password));
//	}
//	virtual void ShutdownConnectionInternal(void* handle)
//	{
//		ShutdownConnection(reinterpret_cast<T*>(handle));
//	}
//};