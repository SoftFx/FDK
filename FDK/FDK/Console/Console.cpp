#include "stdafx.h"





int main()
{
	_CrtSetDbgFlag(_CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF);

	void* heap = HeapCreate(0, 0, 0);
	MemoryBuffer compressed(heap);
	MemoryBuffer plain(heap);

	string st;
	for (size_t index = 0; index < 1000; ++index)
	{
		st = st + "test";
	}

	ZipCompress(L"0", const_cast<char*>(st.c_str()), st.length() + 1, compressed);


	ZipDecompress(L"0", compressed.GetData(), compressed.GetSize(), plain);

	const char* output = reinterpret_cast<const char*>(plain.GetData());



	/*
	std::ifstream input("E:\\ticks.zip", std::ios::binary);
	char buffer[256 * 1024] = "";
	input.read(buffer, sizeof(buffer));
	const size_t count = input.gcount();


	HZIP handle = OpenZip(buffer, count, nullptr);

	int index = -1;
	ZIPENTRY zipEntry;
	ZeroMemory(&zipEntry, sizeof(zipEntry));
	ZRESULT status = FindZipItem(handle, L"ticks.txt", false, &index, &zipEntry);

	char* uncompressed = new char[zipEntry.unc_size];
	ZeroMemory(uncompressed, zipEntry.unc_size);


	UnzipItem(handle, index, uncompressed, zipEntry.unc_size);

	std::ofstream output("E:\\ticks2.txt", std::ios::binary);
	output.write(uncompressed, zipEntry.unc_size);


	CloseZip(handle);*/

	return 0;
}
