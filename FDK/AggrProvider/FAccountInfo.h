#pragma once


class CBinaryReader;
class CFAccountInfo
{
public:
	CFAccountInfo();
public:
	double Equity;
	double FreeMargin;
private:
	friend bool operator == (const CFAccountInfo& first, const CFAccountInfo& second);
	friend bool operator != (const CFAccountInfo& first, const CFAccountInfo& second);
private:
	friend CBinaryReader& operator >> (CBinaryReader& stream, CFAccountInfo& info);
	friend ostream& operator << (ostream& stream, const CFAccountInfo& info);
};
