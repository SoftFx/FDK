#pragma once

class CSocketState
{
public:
	bool CanRead;
	bool CanWrite;
public:
	inline CSocketState() : CanRead(), CanWrite()
	{
	}
};
