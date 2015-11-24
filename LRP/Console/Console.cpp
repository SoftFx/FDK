#include "stdafx.h"
#include "../Lrp.Core.Cpp/decimal8.h"




int main()
{
	/*MemoryBuffer buffer(160);

	CBitWriter stream(buffer);
	stream.WriteInt32(12, 4);*/


	SetLastError(WSAEWOULDBLOCK);

	return 0;

	/*
	{
	CBitWriter stream(buffer);



	for (int index = 0; index < count; ++index)
	{
	stream.WriteInt32(-index, size);
	}

	stream.Flush();
	}

	buffer.SetPosition(0);

	{
	CBitReader reader(buffer);

	for (int index = 0; index < count; ++index)
	{
	int value = reader.ReadInt32(size);
	cout<<"value = "<<value<<endl;
	}

	}



	*/

	return 0;
}

