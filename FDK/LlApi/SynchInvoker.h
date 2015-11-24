#ifndef __Native_Synch_Invoker__
#define __Native_Synch_Invoker__

#include "IWaiter.h"

class CSynchInvoker
{
private:
	typedef pair<string, string> Key;
public:
	CSynchInvoker();
public:
	void RegisterWaiter(const type_info& info, const string& id, IWaiter* pWaiter);
	void ReleaseWaiter( const type_info& info, const string& id);
	void Response(const type_info& info, const CFxEventInfo& eventInfo, void* pData);
	void Response(const CFxEventInfo& eventInfo);
	void Disconnect();
public:
	template<typename T> void Response(const CFxEventInfo& eventInfo, T& response)
	{
		Response(typeid(T), eventInfo, &response);
	}
private:
	CSynchInvoker(const CSynchInvoker&);
	CSynchInvoker& operator = (const CSynchInvoker&);
private:
	CCriticalSection m_synchronizer;
	map<Key, IWaiter*> m_key2Waiter;
};

#endif
