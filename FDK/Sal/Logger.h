#ifndef __Sal_Logger__
#define __Sal_Logger__
#include "CriticalSection.h"
#include "CallsWaiter.h"
#include "DateTime.h"

class CThreadPool;
class SAL_API CLogger
{
public:
	CLogger();
	CLogger(const char* path);
	CLogger(const std::string& path);
	~CLogger();
public:
	/*void Construct(const char* path);
	void Construct(const std::string& path);*/
public:
	void Output(const std::string& message);
public:
	static void OutputHandler(void* pUserParam, const char* message);
private:
	void DoOutput();
private:
	typedef std::pair<CDateTime, std::string> Entry;
private:
	CCriticalSection m_synchronizer;
	CCallsWaiter m_waiter;
	std::shared_ptr<CThreadPool> m_threadPool;
	std::ofstream m_stream;
	std::vector<Entry> m_messages;
};

#endif