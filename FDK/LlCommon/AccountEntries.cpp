#include "stdafx.h"
#include "AccountEntries.h"
#include "Constants.h"

namespace FDK
{
	CAccountEntries::CAccountEntries()
	{
	}

	CAccountEntries::CAccountEntries(const CAccountEntries& entries)
        : m_entries(entries.m_entries)
	{
	}

	CAccountEntries& CAccountEntries::operator = (const CAccountEntries& entries)
	{
		if (this != &entries)
		{
			m_entries = entries.m_entries;
		}
		return *this;
	}

	CAccountEntries::~CAccountEntries()
	{
	}

	typedef CAccountEntries LrpContainer;

	#include "../../../External/Include/lrp/LrpStdVector.hpp"

}