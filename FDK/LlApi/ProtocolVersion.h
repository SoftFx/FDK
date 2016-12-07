#pragma once

class CProtocolVersion
{
public:

	CProtocolVersion();
	CProtocolVersion(int major, int minor);
	CProtocolVersion(const std::string& st);

    int getMajor() const;
    int getMinor() const;

    std::string toString() const;

private:

    int Major;
    int Minor;

	friend bool operator < (const CProtocolVersion& first, const CProtocolVersion& second);
	friend bool operator > (const CProtocolVersion& first, const CProtocolVersion& second);
	friend bool operator <= (const CProtocolVersion& first, const CProtocolVersion& second);
	friend bool operator >= (const CProtocolVersion& first, const CProtocolVersion& second);
};

bool operator < (const CProtocolVersion& first, const CProtocolVersion& second);
bool operator > (const CProtocolVersion& first, const CProtocolVersion& second);
bool operator <= (const CProtocolVersion& first, const CProtocolVersion& second);
bool operator >= (const CProtocolVersion& first, const CProtocolVersion& second);