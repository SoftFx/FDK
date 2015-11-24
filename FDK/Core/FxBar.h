#ifndef __Core_Bar__
#define __Core_Bar__

#pragma warning (push)
#pragma warning (disable : 4275)
#pragma warning (disable : 4251)

class CORE_API CFxBar
{
public:
	CFxBar();
public:
	double Open;
	double Close;
	double High;
	double Low;
	double Volume;
	CDateTime From;
};

#endif
