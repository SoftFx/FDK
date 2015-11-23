#pragma once


class CParameters
{
public:
	bool EnableCodec;
	bool ValidateCodec;
	int MessagesNumberLimit;
	int MessagesSizeLimit;
	int ThreadsNumber;
	int HandshakeTimeout;
	int HeartbeatTimeout;
	string LogPath;
public:
	CParameters();
};