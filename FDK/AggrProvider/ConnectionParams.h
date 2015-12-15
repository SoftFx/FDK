#pragma once

class CConnectionParams
{
public:
	string Address;
	int Port;
	string Username;
	string Password;
	string LogPath;
public:
	CConnectionParams();
private:
	friend bool operator < (const CConnectionParams& first, const CConnectionParams& second);
};