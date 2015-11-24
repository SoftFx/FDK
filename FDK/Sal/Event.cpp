#include "stdafx.h"
#include "Event.h"



EventInfo::EventInfo()
{
	m_impl = new EventInfoImpl();
}
EventInfo::~EventInfo()
{
	delete m_impl;
}
bool EventInfo::Add(const CDelegateInfo& handler)
{
	if (handler.IsNull())
	{
		return false;
	}
	try
	{
		m_impl->Add(handler);
		return true;
	}
	catch (const std::bad_alloc&)
	{
		return false;
	}
}
bool EventInfo::Del(const CDelegateInfo& handler)
{
	if (handler.IsNull())
	{
		return false;
	}
	m_impl->Del(handler);
	return true;
}
void* EventInfo::Next(void* current, CDelegateInfo& handler)
{
	return m_impl->Next(current, handler);
}
