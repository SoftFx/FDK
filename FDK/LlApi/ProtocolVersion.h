#pragma once

class CProtocolVersion
{
public:
	int Major;
	int Minor;
public:
	CProtocolVersion();
	CProtocolVersion(int major, int minor);
	CProtocolVersion(const std::string& st);
private:
	friend bool operator < (const CProtocolVersion& first, const CProtocolVersion& second);
	friend bool operator > (const CProtocolVersion& first, const CProtocolVersion& second);
	friend bool operator <= (const CProtocolVersion& first, const CProtocolVersion& second);
	friend bool operator >= (const CProtocolVersion& first, const CProtocolVersion& second);
};