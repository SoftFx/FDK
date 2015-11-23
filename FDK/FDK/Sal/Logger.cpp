#include "stdafx.h"
#pragma warning (disable : 4251) // 'identifier' : class 'type' needs to have dll-interface to be used by clients of class 'type2'
#include "Logger.h"
#include "Delegate.h"
#include "Lock.h"
namespace
{
	CCriticalSection gSynchronizer;
	shared_ptr<CThreadPool> gThreadPool;
}
namespace
{
	ostream& operator << (ostream& stream, SYSTEMTIME& time)
	{
		stream<<setw(4)<<setfill('0')<<time.wYear<<'.';
		stream<<setw(2)<<setfill('0')<<time.wMonth<<'.';
		stream<<setw(2)<<setfill('0')<<time.wDay;

		stream<<" ";

		stream<<setw(2)<<setfill('0')<<time.wHour<<':';
		stream<<setw(2)<<setfill('0')<<time.wMinute<<':';
		stream<<setw(2)<<setfill('0')<<time.wSecond<<".";
		stream<<setw(3)<<setfill('0')<<time.wMilliseconds;
		return stream;
	}

}
namespace
{
	shared_ptr<CThreadPool> CreateThreadPool()
	{
		CLock locker(gSynchronizer);
		if (!gThreadPool)
		{
			gThreadPool.reset(new CThreadPool());
		}
		return gThreadPool;
	}
	void DeleteThreadPool()
	{
		shared_ptr<CThreadPool> threadPool;
		{
			CLock locker(gSynchronizer);
			if (gThreadPool.unique())
			{
				std::swap(threadPool, gThreadPool);
			}
		}
		threadPool.reset();
	}
}
CLogger::CLogger(const char* path) : m_threadPool(CreateThreadPool()), m_stream(path, std::ios::app)
{
}
CLogger::CLogger(const string& path) : m_threadPool(CreateThreadPool()), m_stream(path.c_str(), std::ios::app)
{
}
CLogger::~CLogger()
{
	m_waiter.WaitForFinish();
	m_threadPool.reset();
	DeleteThreadPool();
}
void CLogger::Output(const string& message)
{
	try
	{
		CLock lock(m_synchronizer);
		if (m_messages.empty())
		{
			Delegate<void ()> action(this, &CLogger::DoOutput);
			action.DoAsynch(*m_threadPool, &m_waiter);
		}
		m_messages.push_back(Entry(FxUtcNow(), message));
	}
	catch (...)
	{
	}
}
void CLogger::DoOutput()
{
	vector<Entry> messages;
	{
		CLock locker(m_synchronizer);
		swap(messages, m_messages);
	}
	for each(const auto& element in messages)
	{
		m_stream<<element.first<<">: "<<element.second<<endl;
	}
	m_stream<<flush;
}
void CLogger::OutputHandler(void* pUserParam, const char* message)
{
	CLogger* pLogger = reinterpret_cast<CLogger*>(pUserParam);
	pLogger->Output(message);
}
//void CLogger::Construct(const char* path)
//{
//
//}
//void CLogger::Construct(const std::string& path)
//{
//
//}
