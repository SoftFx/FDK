#pragma once


class CLocalCppLibrary
{
public:
	static void SetDotNetDllPath(const string& path);
};


HRESULT InitializeLocalCSharpClient(CLrpLocalClient& client);