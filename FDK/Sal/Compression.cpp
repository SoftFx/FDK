#include "stdafx.h"
#include "Compression.h"
#include "ZipHandle.h"
#include "DateTime.h"
#define HAVE_CUSTOM_DATE_TIME
#include "../../External/Include/lrp/MemoryBuffer.h"


namespace
{
	size_t CompressedSizeFromUncompressedSize(size_t size)
	{
		size_t result = 2 * size + 4096;
		return result;
	}
}

void ZipCompress(const TCHAR* name, void* data, size_t size, MemoryBuffer& buffer)
{
	const size_t compressedSize = CompressedSizeFromUncompressedSize(size);

	CZipHandle handle(compressedSize);

	ZipAdd(handle, name, data, static_cast<unsigned long>(size));

	void* _data = nullptr;
	unsigned long _size = 0;
	ZipGetMemory(handle, &_data, &_size);

	buffer.WriteRaw(_data, _size);
}
void ZipDecompress(const TCHAR* name, void* data, size_t size, MemoryBuffer& buffer)
{
	CZipHandle handle(data, size);

	int index = -1;
	ZIPENTRY zipEntry;
	ZeroMemory(&zipEntry, sizeof(zipEntry));

	ZRESULT status = FindZipItem(handle, name, false, &index, &zipEntry);
	if (ZR_OK != status)
	{
		throw runtime_error("ZipDecompress(): couldn't find zip item");
	}

	buffer.Construct(zipEntry.unc_size);
	UnzipItem(handle, index, buffer.GetData(), zipEntry.unc_size);
}