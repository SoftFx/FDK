#ifndef __Native_Fx_Queue__
#define __Native_Fx_Queue__

#include "LimitedQueue.h"


class CFxQueue : public CFxHandle
{
public:
	CFxQueue();
	virtual ~CFxQueue();

private:
	CFxQueue(const CFxQueue&);
	CFxQueue& operator = (const CFxQueue&);

public:
	bool GetNextMessage(CFxMessage& message);
	void DispatchMessage(const CFxMessage& message);

public:
	size_t GetThreshold() const;
	void SetThreshold(size_t newSize);

protected:
	HRESULT Construct();
	void ReleaseQueue();
	void Dispose();

public:
	void ProcessMessage(CFxMessage& message);
	void ProcessMessage(const string& type, CFxMessage& message);

private:
	CSemaphore* m_semaphore;
	mutable CCriticalSection m_synchronizer;
	LimitedQueue<CFxMessage> m_messages;
};

#endif
