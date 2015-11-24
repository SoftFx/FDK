#include "stdafx.h"
#include "BinaryReadHelpers.h"

#pragma warning (push, 4)

CBinaryReader::CBinaryReader(const vector<char>& buffer) : m_current(), m_end()
{
	const size_t count = buffer.size();
	if (count > 0)
	{
		m_current = &buffer.front();
		m_end = m_current + count;
	}
}
CBinaryReader& CBinaryReader::operator>>(string& argument)
{
	uint32 count = 0;
	(*this)>>count;
	argument.clear();
	argument.insert(argument.end(), count, ' ');
	if (count > 0)
	{
		char* pointer = &*argument.begin();
		Read(static_cast<int>(count), pointer);
	}
	return *this;
}
void CBinaryReader::Read(int count, void* data)
{
	assert(count >= 0);
	const char* next = m_current + count;
	if (next > m_end)
	{
		throw runtime_error("index out of range");
	}
	memcpy(data, m_current, count);
	m_current = next;
}
void CBinaryReader::Skip(size_t numberOfBytes)
{
	const char* next = m_current + numberOfBytes;
	if (next > m_end)
	{
		throw runtime_error("index out of range");
	}
	m_current = next;
}
