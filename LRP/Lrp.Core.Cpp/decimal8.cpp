#include "stdafx.h"
#include "decimal8.h"

//
extern "C" __int64 __stdcall Decimal8Multiply(__int64 x, __int64 y)
{
	return 0;
}
//{
//	__int64 a = x / DECIMAL8_BASE;
//	int b = y % DECIMAL8_BASE;
//
//	//// rounding
//	__int64 value = b * second.m_value;
//	if (value > 0)
//	{
//		value += DECIMAL8_BASE / 2;
//	}
//	else if (value < 0)
//	{
//		value -= DECIMAL8_BASE / 2;
//	}
//	value /= DECIMAL8_BASE;
//
//	value += a * second.m_value;
//
//	return decimal8(value);
//
//}