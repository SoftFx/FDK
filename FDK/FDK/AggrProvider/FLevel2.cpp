#include "stdafx.h"
#include "FLevel2.h"

CFLevel2::CFLevel2(uint16 version /* = 0 */) : Version(version), Filtered(false)
{
}
void CFLevel2::Clear()
{
	Filtered = false;
	Symbol.clear();
	Banks.clear();
}
bool CFLevel2::CopyTo(int32 bankCode, CFxQuote& quote) const
{
	auto it = Banks.find(bankCode);
	if (Banks.end() != it)
	{
		quote.Symbol = this->Symbol;
		it->second.UpdateQuote(quote);
		return true;
	}
	else
	{
		return false;
	}
}

CBinaryReader& operator >> (CBinaryReader& stream, CFLevel2& arg)
{
	arg.Clear();
	if (0 == arg.Version)
	{
		stream >> arg.Symbol >> arg.Filtered;

		uint32 count = 0;
		stream>>count;

		for (; count > 0; --count)
		{
			CFBank bank;
			stream>>bank;
			arg.Banks[bank.Code] = bank;
		}
	}
	else if (1 == arg.Version)
	{
		int32 sequenceNumber = 0;
		stream >> arg.Symbol >> arg.Filtered >> sequenceNumber;

		uint32 count = 0;
		stream>>count;

		for (; count > 0; --count)
		{
			CFBank bank;
			stream>>bank;
			arg.Banks[bank.Code] = bank;
		}
	}
	else if (2 == arg.Version)
	{
		int32 sequenceNumber = 0;
		stream >> arg.Symbol >> arg.Filtered >> sequenceNumber;

		uint32 count = 0;
		stream>>count;

		for (; count > 0; --count)
		{
			CFBank bank;
			stream>>bank;
			arg.Banks[bank.Code] = bank;
		}

	}
	return stream;
}
