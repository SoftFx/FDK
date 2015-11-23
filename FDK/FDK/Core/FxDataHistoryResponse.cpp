#include "stdafx.h"
#include "FxDataHistoryResponse.h"
#include "Core.h"

namespace
{
	class CFxBarForwardComparator
	{
	public:
		inline bool operator () (const CFxBar& first, const CFxBar& second)const
		{
			return (first.From < second.From);
		}
	};
	class CFxBarBackwardComparator
	{
	public:
		inline bool operator () (const CFxBar& first, const CFxBar& second)const
		{
			return (first.From > second.From);
		}
	};
}

void CFxDataHistoryResponse::SortForward()
{
	sort(Bars.begin(), Bars.end(), CFxBarForwardComparator());
}

void CFxDataHistoryResponse::SortBackward()
{
	sort(Bars.begin(), Bars.end(), CFxBarBackwardComparator());
}