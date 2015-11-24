#include "stdafx.h"
#include "Referenceable.h"

CReferenceable::CReferenceable() : m_counter(1)
{
}
CReferenceable::~CReferenceable()
{
	if (0 != m_counter)
	{
		assert(!"fail");
	}
	assert(0 == m_counter);
}
void CReferenceable::Acquire()
{
	InterlockedIncrement(&m_counter);
}
void CReferenceable::Release()
{
	LONG newCounter = InterlockedDecrement(&m_counter);
	if (0 == newCounter)
	{
		delete this;
	}
}
