#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)
#pragma warning (disable : 4251)

class CORE_API CFxFileChunk
{
public:
    CFxFileChunk();
public:
    string FileId;
    string FileName;
    int32 FileSize;
    int32 ChunkId;
    int32 TotalChunks;
    vector<uint8> Data;
};

#pragma warning (pop)
