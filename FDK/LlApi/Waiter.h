#ifndef __Native_Waiter__
#define __Native_Waiter__

#include "IWaiter.h"
#include "Client.h"
#include "Timeout.h"

class CWaiter : public IWaiter
{
public:
	CWaiter(const uint32 timeoutInMilliseconds);
	virtual ~CWaiter();

public:
	const string& Id()const;
	void ResetTimeout(const uint32 timeoutInMilliseconds);

protected:
	uint32 m_waitingInterval;
	CTimeout m_timeout;
	CSemaphore m_semaphore;
	string m_id;
	CCriticalSection m_synchronizer;
};


template<typename T> class Waiter : public CWaiter
{
private:
	typedef std::pair<CFxEventInfo, T> Entry;

public:
	Waiter(const uint32 timeoutInMilliseconds, const string& prefix, CClient& receiver) :
		CWaiter(timeoutInMilliseconds), m_receiver(receiver)
	{
		m_id = receiver.NextId(prefix);
		receiver.RegisterWaiter(typeid(T), m_id, this);
	}
	Waiter(const uint32 timeoutInMilliseconds, const CWaiter& waiter, CClient& receiver) : CWaiter(timeoutInMilliseconds), m_receiver(receiver)
	{
		m_id = waiter.Id();
		receiver.RegisterWaiter(typeid(T), m_id, this);
	}
	Waiter(const uint32 timeoutInMilliseconds, const string& prefix, const string& id, CClient& receiver) :
	CWaiter(timeoutInMilliseconds), m_receiver(receiver)
	{
		m_id = prefix + id;
		receiver.RegisterWaiter(typeid(T), m_id, this);
	}
	virtual ~Waiter()
	{
		m_receiver.ReleaseWaiter(typeid(T), m_id);
	}

public:
	virtual bool VResponse(const CFxEventInfo& info)
	{
		{
			CLock lock(m_synchronizer);
			m_items.push_back(Entry());
			Entry& entry = m_items.back();
			entry.first = info;
		}
		m_semaphore.Release();
		return true;
	}
	virtual bool VResponse(const CFxEventInfo& info, void* pData)
	{
		T* pTypedData = reinterpret_cast<T*>(pData);
		T& data = *pTypedData;
		{
			CLock lock(m_synchronizer);
			m_items.push_back(Entry());
			Entry& entry = m_items.back();
			entry.first = info;
			swap(data, entry.second);
		}
		m_semaphore.Release();
		return true;
	}
	virtual void Disconnect()
	{
		CFxEventInfo info;
		info.Status = FX_CODE_ERROR_LOGOUT;
		info.Message = "Disconnected";
		VResponse(info);
	}
	T WaitForResponse()
	{
		CFxEventInfo info;
		return WaitForResponseEx(info);
	}
	T WaitForResponse(CFxEventInfo& info)
	{
		return WaitForResponseEx(info);
	}

private:
	T WaitForResponseEx(CFxEventInfo& info)
	{
		T result = T();
		const uint32 timeoutInMilliseconds = m_timeout;
		m_semaphore.WaitFor(timeoutInMilliseconds);
		{
			CLock lock(m_synchronizer);
			if (m_items.empty())
			{
				throw CTimeoutException(m_id, (int32)m_timeout.ToMilliseconds());
			}
			Entry& entry = m_items.front();
			swap(entry.first, info);
			swap(entry.second, result);
			m_items.pop_front();
		}
		if (FAILED(info.Status))
		{
			const string& text = !info.Message.empty() ? info.Message : info.Description;
            if (info.Status == FX_CODE_ERROR_LOGOUT)
                throw CLogoutException(text);
            else
			    throw runtime_error(text);
		}
		return result;
	}

private:
    CClient& m_receiver;
	deque<Entry> m_items;
};

#endif
