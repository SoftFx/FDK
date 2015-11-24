#ifndef __Core_FxDataHistoryInfo__
#define __Core_FxDataHistoryInfo__

class CDataHistoryInfo
{
public:
	CDateTime FromAll;
	CDateTime ToAll;
	Nullable<CDateTime> From;
	Nullable<CDateTime> To;
	string LastTickId;
	vector<string> Files;
	vector<CFxBar> Bars;
};


#endif