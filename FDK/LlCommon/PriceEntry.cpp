#include "stdafx.h"
#include "PriceEntry.h"
#include "Formating.h"

namespace FDK
{
	std::ostream& operator << (std::ostream& stream, const CPriceEntry& entry)
	{
		Process("Bid", entry.Bid, stream);
		Process("Ask", entry.Ask, stream);
		return stream;
	}

	std::istream& operator >> (std::istream& stream, CPriceEntry& entry)
	{
		Process("Bid", entry.Bid, stream);
		Process("Ask", entry.Ask, stream);
		return stream;
	}
}
