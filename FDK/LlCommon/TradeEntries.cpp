#include "stdafx.h"

#include "TradeEntries.h"

namespace FDK
{
	CTradeEntries::CTradeEntries()
	{
	}

	CTradeEntries::CTradeEntries(const CTradeEntries& entries)
        : m_entries(entries.m_entries)
	{
	}

	CTradeEntries& CTradeEntries::operator = (const CTradeEntries& entries)
	{
		if (this != &entries)
		{
			m_entries = entries.m_entries;
		}
		return *this;
	}

	CTradeEntries::~CTradeEntries()
	{
	}

	typedef CTradeEntries LrpContainer;

	#include "../../../External/Include/lrp/LrpStdVector.hpp"
}