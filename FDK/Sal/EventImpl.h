#ifndef __Sal_EventImpl__
#define __Sal_EventImpl__
#ifndef __Sal_Delegate__
#include "Delegate.h"
#endif
#ifndef __Sal_Critical_Section__
#include "CriticalSection.h"
#endif



struct EventInfoEntry
{
public:
	EventInfoEntry* Previous;
	EventInfoEntry* Next;
private:
	size_t m_counter;
public:
	CDelegateInfo Handler;
public:
	EventInfoEntry();
	EventInfoEntry(const CDelegateInfo& handler);
public:
	void Acquire();
	void Relase();
};


class EventInfoImpl
{
public:
	EventInfoImpl();
	~EventInfoImpl();
public:
	void Add(const CDelegateInfo& handler);
	void Del(const CDelegateInfo& handler);
public:
	void* Next(void* ptr, CDelegateInfo& handler);
private:
	CCriticalSection m_synchronizer;
	map<CDelegateInfo, size_t> m_delegateToCounter;
	EventInfoEntry m_head;
	EventInfoEntry m_tail;
};
#endif