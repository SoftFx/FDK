#include "stdafx.h"
#include "EventImpl.h"
#include "Lock.h"



EventInfoEntry::EventInfoEntry() : Previous(), Next(), m_counter()
{
}
EventInfoEntry::EventInfoEntry(const CDelegateInfo& handler) : Previous(), Next(), m_counter(1), Handler(handler)
{
}
void EventInfoEntry::Acquire()
{
	m_counter++;
}
void EventInfoEntry::Relase()
{
	m_counter--;
	if (0 == m_counter)
	{
		delete this;
	}
}
EventInfoImpl::EventInfoImpl()
{
	m_head.Next = &m_tail;
	m_tail.Previous = &m_head;
}
EventInfoImpl::~EventInfoImpl()
{
	assert(m_head.Next == &m_tail);
	assert(m_tail.Previous == &m_head);
	for (EventInfoEntry* entry = m_head.Next; entry != &m_tail;)
	{
		EventInfoEntry* temp = entry;
		entry = entry->Next;
		delete temp;
	}
}
void EventInfoImpl::Add(const CDelegateInfo& handler)
{
	CLock lock(m_synchronizer);
	m_delegateToCounter[handler]++;
	for (EventInfoEntry* entry = m_head.Next; entry != &m_tail; entry = entry->Next)
	{
		if (handler == entry->Handler)
		{
			return;
		}
	}
	EventInfoEntry* entry = new EventInfoEntry(handler);
	EventInfoEntry* previous = m_tail.Previous;
	
	previous->Next = entry;
	entry->Previous = previous;

	entry->Next = &m_tail;
	m_tail.Previous = entry;
}
void EventInfoImpl::Del(const CDelegateInfo& handler)
{
	CLock lock(m_synchronizer);
	auto it = m_delegateToCounter.find(handler);
	if (m_delegateToCounter.end() != it)
	{
		if (0 == --(it->second))
		{
			m_delegateToCounter.erase(it);
		}
	}
	for (EventInfoEntry* entry = m_head.Next; entry != &m_tail; entry = entry->Next)
	{
		if (handler == entry->Handler)
		{
			EventInfoEntry* previous = entry->Previous;
			EventInfoEntry* next = entry->Next;
			previous->Next = next;
			next->Previous = previous;
			entry->Relase();
			break;
		}
	}
}
void* EventInfoImpl::Next(void* ptr, CDelegateInfo& handler)
{
	CLock lock(m_synchronizer);
	EventInfoEntry* current = reinterpret_cast<EventInfoEntry*>(ptr);
	EventInfoEntry* result = nullptr;

	if (nullptr == current)
	{
		if (m_head.Next != &m_tail)
		{
			result = m_head.Next;
		}
	}
	else
	{
		if (current->Next != &m_tail)
		{
			result = current->Next;
		}
	}
	if (nullptr != current)
	{
		current->Relase();
	}
	if (nullptr != result)
	{
		result->Acquire();
		handler = result->Handler;
	}
	return result;
}