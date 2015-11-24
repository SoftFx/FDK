#pragma once


template<typename T> class Nullable
{
public:
	inline Nullable() : m_value(), m_hasValue(false)
	{
	}
	inline Nullable(void* ) : m_value(), m_hasValue(false)
	{
	}
	inline Nullable(const T& value) : m_value(value), m_hasValue(true)
	{
	}
public:
	inline const T& operator * () const
	{
		assert(m_hasValue);
		return m_value;
	}
	inline T& operator * ()
	{
		assert(m_hasValue);
		return m_value;
	}
	inline void operator = (const T& value)
	{
		m_hasValue = true;
		m_value = value;
	}
public:
	void Reset()
	{
		m_hasValue = false;
		m_value = T();
	}
	bool HasValue() const
	{
		return m_hasValue;
	}
	T& Value()
	{
		assert(m_hasValue);
		return m_value;
	}
	const T& Value() const
	{
		assert(m_hasValue);
		return m_value;
	}
private:
	T m_value;
	bool m_hasValue;
};


template<typename T> std::ostream& operator << (std::ostream& stream,  const Nullable<T>& argument)
{
	if (argument.HasValue())
	{
		stream<<argument.Value();
	}
	else
	{
		stream<<"null";
	}
	return stream;
}