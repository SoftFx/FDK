#include "stdafx.h"
#include "FxQueue.h"

CFxQueue::CFxQueue()
    : m_semaphore(nullptr)
{
}

CFxQueue::~CFxQueue()
{
	if (nullptr != m_semaphore)
	{
		delete m_semaphore;
		m_semaphore = nullptr;
	}
	foreach(const auto& element, m_messages)
	{
		DispatchMessage(element.second);
	}
	m_messages.clear();
}

HRESULT CFxQueue::Construct()
{
	m_semaphore = new CSemaphore();
	return S_OK;
}

void CFxQueue::ReleaseQueue()
{
	m_semaphore->Release(numeric_limits<int16>::max());
}

void CFxQueue::Dispose()
{
	if(nullptr != m_semaphore)
	{
		delete m_semaphore;
		m_semaphore = nullptr;
	}
}

bool CFxQueue::GetNextMessage(CFxMessage& message)
{
	m_semaphore->WaitFor();
	CLock lock(m_synchronizer);
	if (m_messages.empty())
	{
		return false;
	}
	message = m_messages.front();
	m_messages.pop_front();
	return true;
}

void CFxQueue::DispatchMessage(const CFxMessage& message)
{
	CFxHandle* handle = reinterpret_cast<CFxHandle*>(message.Data);
	if (nullptr != handle)
	{
		delete handle;
	}
}

void CFxQueue::ProcessMessage(CFxMessage& message)
{
	ProcessMessage(string(), message);
}

void CFxQueue::ProcessMessage(const string& type, CFxMessage& message)
{
	CLock lock(m_synchronizer);
	try
	{
		CFxMessage removing;
		if (m_messages.push_back(type, message, removing))
		{
			m_semaphore->Release();
		}
		else
		{
			DispatchMessage(removing);
		}
	}
	catch (const bad_alloc&)
	{
		CFxHandle* handle = reinterpret_cast<CFxHandle*>(message.Data);
		if (nullptr != handle)
		{
			delete handle;
		}
		message.Data = nullptr;
	}
}

size_t CFxQueue::GetThreshold() const
{
	return m_messages.get_threshold();
}

void CFxQueue::SetThreshold(size_t newSize)
{
	m_messages.set_threshold(newSize);
}
