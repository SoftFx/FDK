#include "stdafx.h"
#include "MemoryStream.h"

CMemoryStream::CMemoryStream() : ostream(this), m_stack(true), m_position()
{
}
basic_streambuf<char>::int_type CMemoryStream::overflow(basic_streambuf<char>::int_type character)
{
	const char ch = static_cast<char>(character);
	if (m_stack)
	{
		if (m_position < sizeof(m_buffer))
		{
			m_buffer[m_position++] = ch;
			return 1;
		}
		Copy();
	}
	m_data.push_back(ch);
	return 1;
}
int CMemoryStream::sync()
{
	return 1;
}
streamsize CMemoryStream::xsputn(const char* ptr, streamsize size)
{
	if (m_stack)
	{
		const size_t newPosition = static_cast<size_t>(m_position + size);
		if (newPosition <= sizeof(m_buffer))
		{
			memcpy(m_buffer + m_position, ptr, static_cast<size_t>(size));
			m_position = newPosition;
			return size;
		}
		Copy();
	}
	m_data.insert(m_data.end(), ptr, ptr + size);
	return size;
}
CMemoryStream& CMemoryStream::operator << (const char* arg)
{
	size_t size = strlen(arg);
	xsputn(arg, size);
	return *this;
}
CMemoryStream& CMemoryStream::operator << (const string& arg)
{
	xsputn(arg.c_str(), arg.size());
	return *this;
}
string CMemoryStream::str() const
{
	if (m_stack)
	{
		return string(m_buffer, m_buffer + m_position);
	}
	const char* ptr = &m_data.front();
	return string(ptr, ptr + m_data.size());
}
void CMemoryStream::Copy()
{
	m_data.insert(m_data.end(), m_buffer, m_buffer + m_position);
	m_stack = false;
}

