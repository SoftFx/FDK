#include "stdafx.h"
#include "ChannelsPool.h"
#include "Server.h"
#include "Channel.h"
#include "ChannelExclusiveAccessor.h"
#include "ChannelSharedAccessor.h"


namespace
{
	const string cEventLogFileName = "events.log";
	const string cMessagesLogFileName = "messages.log";
}



CChannelsPool::CChannelsPool(const CParameters& params) :
	m_params(params), m_continue(true), m_semaphore(0, params.ThreadsNumber), m_nextId(), m_logger(params.LogPath, cEventLogFileName, cMessagesLogFileName)
{
	m_position = m_channels.end();
	const size_t threadsNumber = static_cast<size_t>(params.ThreadsNumber);
	m_threads.reserve(threadsNumber);

	for (size_t index = 0; index < threadsNumber; ++index)
	{
		HANDLE thread = reinterpret_cast<HANDLE>(_beginthreadex(nullptr, 0, &CChannelsPool::ThreadFunction, this, 0, nullptr));
		if (nullptr == thread)
		{
			Finalize();
			throw runtime_error("Can not start a new thread");
		}
		m_threads.push_back(thread);
	}
}
CChannelsPool::~CChannelsPool()
{
	Finalize();
}
void CChannelsPool::Finalize()
{
	if (m_continue)
	{
		m_continue = false;
		m_semaphore.Release(m_threads.size());

		for each(const auto element in m_threads)
		{
			WaitForSingleObject(element, INFINITE);
		}
		m_threads.clear();

		DoFinalize();
	}
}
void CChannelsPool::DoFinalize()
{
	for each(auto element in m_channels)
	{
		CChannel* pChannel = element.second.Channel;
		pChannel->Finalize();
		delete pChannel;
	}
	m_channels.clear();
}
void CChannelsPool::AddConnection(SOCKET socket, CServer& owner)
{
	const uint64 id = static_cast<uint64>(InterlockedIncrement64(&m_nextId));
	auto_ptr<CChannel> channel(new CChannel(owner, m_params, id, socket));

	bool wakeUp = false;

	{
		CExclusiveLocker lock(m_synchronizer);
		m_channels.insert(m_channels.end(), make_pair(id, CChannelEntry(channel.get())));
		wakeUp = (m_channels.size() <= m_threads.size());
	}

	channel.release();

	if (wakeUp)
	{
		m_semaphore.Release();
	}
}
CLrpLogger& CChannelsPool::GetLogger()
{
	return m_logger;
}
unsigned __stdcall CChannelsPool::ThreadFunction(void* arg)
{
	__try
	{
		CChannelsPool* pPool = reinterpret_cast<CChannelsPool*>(arg);
		pPool->Loop();
		return 0;
	}
	__except(EXCEPTION_EXECUTE_HANDLER)
	{
		return 1;
	}
}
void CChannelsPool::Loop()
{
	for (m_semaphore.WaitFor(INFINITE); m_continue; m_semaphore.WaitFor(INFINITE))
	{
		Step();
	}
}
void CChannelsPool::Step()
{
	for (; m_continue; )
	{
		if (!DoStep())
		{
			break;
		}
	}
}
bool CChannelsPool::DoStep()
{
	CChannelAccessor accessor(*this);
	CChannel* pChannel = accessor.GetChannel();

	if (nullptr == pChannel)
	{
		return false;
	}

	const HRESULT result = pChannel->Process();
	if (SUCCEEDED(result))
	{
		return true;
	}

	accessor.Reset();
	const uint64 id = pChannel->GetId();
	{
		CExclusiveLocker lock(m_synchronizer);
		m_channels.erase(id);
	}
	pChannel->Finalize();
	pChannel->Release();
	pChannel->Release();

	return true;
}
CChannel* CChannelsPool::Acquire(uint64 id)
{
	CSharedLocker lock(m_synchronizer);
	auto it = m_channels.find(id);
	if (m_channels.end() == it)
	{
		return nullptr;
	}
	CChannel* result = it->second.Channel;
	result->Acquire();
	return result;
}
CChannelEntry* CChannelsPool::Acquire()
{
	CSharedLocker lock(m_synchronizer);
	CLock lock2(m_synchronizer2);

	auto end = m_channels.end();
	for (auto it = m_position; it != end; ++it)
	{
		CChannelEntry* result = &(it->second);
		if (!result->IsProcessed)
		{
			m_position = ++it;
			result->IsProcessed = true;
			result->Channel->Acquire();
			return result;
		}
	}

	for (auto it = m_channels.begin(); it != m_position; ++it)
	{
		CChannelEntry* result = &(it->second);
		if (!result->IsProcessed)
		{
			m_position = ++it;
			result->IsProcessed = true;
			result->Channel->Acquire();
			return result;
		}
	}

	return nullptr;
}
void CChannelsPool::Release(CChannelEntry* pEntry)
{
	if (nullptr != pEntry)
	{
		{
			CLock lock(m_synchronizer2);
			pEntry->IsProcessed = false;
		}
		pEntry->Channel->Release();
	}
}
