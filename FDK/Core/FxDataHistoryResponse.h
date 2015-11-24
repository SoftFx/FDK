#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)
#pragma warning (disable : 4251)

#ifndef __Core_Bar__
#include "FxBar.h"
#endif


class CORE_API CFxDataHistoryResponse 
{
public:
	CDateTime FromAll;
	CDateTime ToAll;
	CDateTime From;
	CDateTime To;
	string LastTickId;
	vector<CFxBar> Bars;
	vector<string> Files;
public:
	void SortForward();
	void SortBackward();
};

#pragma warning (pop)
