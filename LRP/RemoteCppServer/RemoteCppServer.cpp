#include "stdafx.h"
#include "Server.h"


int main(int argc, char* argv[])
{
	int port = 0;

	if (2 == argc)
	{
		port = atoi(argv[1]);
	}
	else
	{
		cout<<"Usage:"<<endl;
		cout<<"\tRemoteCppServer <port>"<<endl;
	}
	if (port < 1)
	{
		return 1;
	}
	try
	{
		CServer server(port);
		cout<<"Press enter to stop server"<<endl;
		cin.get();
	}
	catch (const std::exception& e)
	{
		cout<<"e.what() = "<<e.what()<<endl;
	}
	return 0;
}

