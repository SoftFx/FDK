#ifndef __Sal_Event__
#define __Sal_Event__
#include "Delegate.h"

#ifdef SAL_EXPORTS
#ifndef __Sal_EventImpl__
#include "EventImpl.h"
#endif
#endif


class SAL_API EventInfo
{
protected:
	EventInfo();
	~EventInfo();
protected:
	bool Add(const CDelegateInfo& handler);
	bool Del(const CDelegateInfo& handler);
	void* Next(void* current, CDelegateInfo& handler);
private:
	#ifdef SAL_EXPORTS
	EventInfoImpl* m_impl;
	#else
	void* m_impl;
	#endif
};


template<typename H> class BaseEvent : protected EventInfo
{
public:
	void operator += (const H& handler)
	{
		if (!Add(handler))
		{
			throw runtime_error("Couldn't add a new handler");
		}
	}
	void operator -= (const H& handler)
	{
		if (!Del(handler))
		{
			throw runtime_error("Couldn't remove an existing handler");
		}
	}
};



template<typename T, typename Signature> class Event;

#endif