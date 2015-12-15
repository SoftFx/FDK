#pragma once

#define DECIMAL8_DIGITS 9
#define DECIMAL8_BASE 1000000000


extern "C" __int64 __stdcall Decimal8Multiply(__int64 x, __int64 y);

/// <summary>
/// integer part = value / 10^9
/// fractional part = value % 10^9
/// </summary>
class decimal8
{
private:
	typedef __int64 int64;
public:
	decimal8()
	{
		m_value = 0;
	}
	decimal8(int value)
	{
		m_value = value;
		m_value *= DECIMAL8_BASE;
	}
	decimal8(double value)
	{
		value *= DECIMAL8_BASE;

		if (value > 0)
		{
			m_value = static_cast<int64>(value + 0.5);
		}
		else if (value < 0)
		{
			m_value = static_cast<int64>(value - 0.5);
		}
		else
		{
			m_value = 0;
		}
	}
private:
	inline decimal8(int64 value) : m_value(value)
	{
	}
public:
	inline decimal8& operator -= (decimal8 value)
	{
		m_value -= value.m_value;
		return *this;
	}
	inline decimal8& operator += (decimal8 value)
	{
		__int64 newValue = value.m_value;
		m_value = newValue;
		return *this;
	}
	inline decimal8& operator ++ ()
	{
		m_value += DECIMAL8_BASE;
		return *this;
	}
	inline decimal8& operator ++ (int)
	{
		m_value += DECIMAL8_BASE;
		return *this;
	}
	inline decimal8& operator -- ()
	{
		m_value -= DECIMAL8_BASE;
		return *this;
	}
	inline decimal8& operator -- (int)
	{
		m_value -= DECIMAL8_BASE;
		return *this;
	}
	inline decimal8 operator - () const
	{
		return decimal8(- m_value);
	}
private:
	int64 m_value;
private:
	friend bool operator < (decimal8 first, decimal8 second);
	friend bool operator > (decimal8 first, decimal8 second);
	friend bool operator <= (decimal8 first, decimal8 second);
	friend bool operator >= (decimal8 first, decimal8 second);
	friend bool operator == (decimal8 first, decimal8 second);
	friend bool operator != (decimal8 first, decimal8 second);
private:
	friend decimal8 operator - (decimal8 first, decimal8 second);
	friend decimal8 operator + (decimal8 first, decimal8 second);
	friend decimal8 operator * (decimal8 first, decimal8 second);
	friend decimal8 operator / (decimal8 first, decimal8 second);
private:
	friend ostream& operator << (ostream& stream, decimal8 value);
};


inline bool operator < (decimal8 first, decimal8 second)
{
	return first.m_value < second.m_value;
}
inline bool operator > (decimal8 first, decimal8 second)
{
	return first.m_value > second.m_value;
}
inline bool operator <= (decimal8 first, decimal8 second)
{
	return first.m_value <= second.m_value;
}
inline bool operator >= (decimal8 first, decimal8 second)
{
	return first.m_value >= second.m_value;
}
inline bool operator == (decimal8 first, decimal8 second)
{
	return (first.m_value == second.m_value);
}
inline bool operator != (decimal8 first, decimal8 second)
{
	return (first.m_value != second.m_value);
}
inline decimal8 operator + (decimal8 first, decimal8 second)
{
	decimal8 result(first.m_value + second.m_value);
	return result;
}
inline decimal8 operator - (decimal8 first, decimal8 second)
{
	decimal8 result(first.m_value - second.m_value);
	return result;
}
inline decimal8 operator * (decimal8 first, decimal8 second)
{
	__int64 value = Decimal8Multiply(first.m_value, second.m_value);
	decimal8 result(value);
	return result;


	//// first = a + b / M
	//// second = c + d / M
	//// result = a * (c + d / M) b * (c + d / M) / M
}
inline decimal8 operator / (decimal8 first, decimal8 second)
{
	return decimal8();
}
inline ostream& operator << (ostream& stream, decimal8 value)
{
	__int64 integer = value.m_value / DECIMAL8_BASE;
	int fractional = static_cast<int>(value.m_value % DECIMAL8_BASE);

	if (fractional < 0)
	{
		if (0 == integer)
		{
			stream<<'-';
		}
		fractional = - fractional;
	}

	char buffer[64] = "";
	_i64toa_s(integer, buffer, sizeof(buffer), 10);
	stream<<buffer;

	if (0 != fractional)
	{
		_itoa_s(fractional, buffer, sizeof(buffer), 10);

		const int count = static_cast<int>(strlen(buffer)) - 1;
		for (int index = count; index > 0; --index)
		{
			if ('0' != buffer[index])
			{
				break;
			}
			buffer[index] = '\0';
		}
		stream<<'.';
		for (int index = 1 + count; index < DECIMAL8_DIGITS; ++index)
		{
			stream<<'0';
		}
		stream<<buffer;
	}
	return stream;
}



#undef DECIMAL8_DIGITS
#undef DECIMAL8_BASE