#pragma once



class CFixVersion
{
public:
	int Major;
	int Minor;
public:
	CFixVersion();
	CFixVersion(int major, int minor);
	CFixVersion(const std::string& st);
private:
	friend bool operator < (const CFixVersion& first, const CFixVersion& second);
	friend bool operator > (const CFixVersion& first, const CFixVersion& second);
	friend bool operator <= (const CFixVersion& first, const CFixVersion& second);
	friend bool operator >= (const CFixVersion& first, const CFixVersion& second);
};