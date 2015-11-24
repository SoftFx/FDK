#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)
#pragma warning (disable : 4251)

class CORE_API CFxFileChunk
{
public:
	CFxFileChunk();
public:
	int32 ChunksNumber;
	int32 FileSize;
	string FileName;
	vector<uint8> Data;
};

#pragma warning (pop)
