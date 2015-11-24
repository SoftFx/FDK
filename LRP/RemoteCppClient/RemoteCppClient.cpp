#include "stdafx.h"
#include "InType.h"
#include "InOutType.h"
#include "OutType.h"
#include "ReturnType.h"
#include "TypesSerializer.hpp"
#include "Simple.hpp"
#include "Extended.hpp"
#include "Signature.hpp"



int main(int argc, char* argv[])
{
	Sleep(5000);
	int port = 0;

	if (2 == argc)
	{
		port = atoi(argv[1]);
	}
	else
	{
		cout<<"Usage:"<<endl;
		cout<<"\tRemoteCppClient <port>"<<endl;
	}
	if (port < 1)
	{
		return 1;
	}
	try
	{
		for (;; Sleep(1))
		{
			CLrpStClient client(LrpSignature(), "localhost", port, "username", "password", "c:\\Temporary3\\cpp.log", 30000);
			client.Connect(60000);
		}
	}
	catch (const std::exception& e)
	{
		cout<<"e.what() = "<<e.what()<<endl;
	}
	return 0;
}

