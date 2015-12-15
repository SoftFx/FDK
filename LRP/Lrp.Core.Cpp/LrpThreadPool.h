#pragma once

//#include "MtChannelImpl.h"
//
//
//
//class CLrpThreadPool
//{
//public:
//	CLrpThreadPool(size_t threadsNumber);
//	~CLrpThreadPool();
//public:
//	void Add(const Channel& channel);
//	void Finalize();
//private:
//	static unsigned __stdcall ThreadFunction(void* arg);
//	void Loop();
//	void Invoke();
//private:
//	bool volatile m_continue;
//	HANDLE m_semaphore;
//	CriticalSection m_synchronizer;
//	deque<Channel> m_jobs;
//	vector<HANDLE> m_threads;
//};
