#pragma once
#include "zip.h"
#include "unzip.h"

class CZipHandle
{
public:
	CZipHandle(size_t size);
	CZipHandle(void* data, size_t size);
	~CZipHandle();
private:
	CZipHandle(const CZipHandle&);
	CZipHandle& operator = (const CZipHandle&);
public:
	operator HZIP ();
private:
	HZIP m_handle;
};