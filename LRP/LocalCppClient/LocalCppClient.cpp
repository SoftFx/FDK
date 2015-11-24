#include "stdafx.h"

#include "InType.h"
#include "InOutType.h"
#include "OutType.h"
#include "ReturnType.h"
#include "Level2.h"
#include "TypesSerializer.hpp"
#include "Simple.hpp"
#include "Extended.hpp"
#include "Signature.hpp"



const char* dllPath = "c:\\Workspace\\Bank.Bridge\\Aggregator\\src\\AggregatorFeeder\\Debug\\LP.Btc.Net.dll";
const char* typeName = "LP.Btc.Net.Server";

int main()
{
	CLrpLocalClient client(LrpSignature(), dllPath, typeName);
	Simple simple(client);
	int result = 0;

	const bool status = simple.Factorial(10, result);
	cout<<"status = "<<boolalpha<<status<<endl;
	cout<<"result = "<<result<<endl;

	return 0;
}

