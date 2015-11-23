#pragma once


class MemoryBuffer;

SAL_API void ZipCompress(const TCHAR* name, void* data, size_t size, MemoryBuffer& buffer);
SAL_API void ZipDecompress(const TCHAR* name, void* data, size_t size, MemoryBuffer& buffer);