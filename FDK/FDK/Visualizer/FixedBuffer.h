#pragma once

class FixedBuffer : public basic_streambuf<char>
{
public:
	inline FixedBuffer(char* buffer, const size_t size):
		m_current(buffer), m_end(buffer + size)
	{
	}
private:
	FixedBuffer(const FixedBuffer&);
	FixedBuffer& operator = (const FixedBuffer&);
protected:
	virtual int_type overflow(int_type character)
	{
		if (m_current == m_end)
		{
			return 0;
		}
		*m_current = static_cast<char>(character);
		m_current++;
		return 1;
	}
	virtual int sync()
	{
		return 1;
	}
	virtual streamsize xsputn(const char* ptr, streamsize size)
	{
		size_t capacity = static_cast<size_t>(m_end - m_current);
		size_t count = min<size_t>(static_cast<size_t>(size), capacity);
		const char* end = m_current + count;
		for (; m_current < end; m_current++, ptr++)
		{
			*m_current = *ptr;
		}
		return static_cast<streamsize>(count);
	}
private:
	char* m_current;
	char* const m_end;
};



