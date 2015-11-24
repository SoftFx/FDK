#include "stdafx.h"
#include "LocalCppLibrary.h"



#define LrpSignature LocalCSharp_Signature
#include "LocalCSharp_Signature.hpp"
#undef  LrpSignature 


namespace
{
	string gDotNetDllPath;
}

HRESULT InitializeLocalCSharpClient(CLrpLocalClient& client)
{
	const HRESULT result = client.Construct(LocalCSharp_Signature(), gDotNetDllPath.c_str(), "LrpServer.Net.LocalCSharp.Server");
	return result;
}

void CLocalCppLibrary::SetDotNetDllPath(const string& path)
{
	gDotNetDllPath = path;
}
