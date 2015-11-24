#pragma once
#include "ChannelEntry.h"
#include "Parameters.h"

class CServer;
class CChannel;
class CChannelAccessor;

class CChannelsPool
{
public:
	CChannelsPool(const CParameters& params);
	~CChannelsPool();
private:
	CChannelsPool(const CChannelsPool&);
	CChannelsPool& operator = (const CChannelsPool&);
public:
	void Finalize();
	void AddConnection(SOCKET socket, CServer& owner);
	CLrpLogger& GetLogger();
public:
	CChannelEntry* Acquire();
	CChannel* Acquire(uint64 id);
	void Release(CChannelEntry* pEntry);
private:
	static unsigned __stdcall ThreadFunction(void* arg);
	void Loop();
	void Step();
	bool DoStep();
	void DoFinalize();
private:
	const CParameters m_params;
private:
	volatile bool m_continue;
	CSemaphore m_semaphore;
	vector<HANDLE> m_threads;
private:
	volatile LONGLONG m_nextId;
	CSharedExclusiveLock m_synchronizer;
	CCriticalSection m_synchronizer2;
private:
	CLrpLogger m_logger;
private:
	map<uint64, CChannelEntry> m_channels;
	map<uint64, CChannelEntry>::iterator m_position;
private:
	friend class CChannelAccessor;
};