#include "stdafx.h"
#include "Visualizer.h"


#define DECIMAL8_DIGITS 9
#define DECIMAL8_BASE 1000000000

namespace
{
	void FormatDecimal8(ostream& stream, __int64 value)
	{
		__int64 integer = value / DECIMAL8_BASE;
		int fractional = static_cast<int>(value % DECIMAL8_BASE);

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
	}
}





VISUALIZER_API HRESULT WINAPI Decimal8_Format(DWORD address, DEBUGHELPER* helper, int /*base*/, BOOL /*bUniStrings*/, char* buffer, size_t max, DWORD /*reserved*/)
{
	prolog;
	__int64 value = 0;
	HRESULT result = debugger.Read(value);
	return_if_failed(result);
	FormatDecimal8(stream, value);
	return S_OK;
}