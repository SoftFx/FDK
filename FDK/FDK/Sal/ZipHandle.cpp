#include "stdafx.h"
#include "ZipHandle.h"


namespace
{
	const HZIP cZipNull = HZIP();
}
CZipHandle::CZipHandle(size_t size) : m_handle()
{
	m_handle = CreateZip(nullptr, static_cast<unsigned long>(size), nullptr);

	if (cZipNull == m_handle)
	{
		throw runtime_error("Couldn't create zip");
	}
}
CZipHandle::CZipHandle(void* data, size_t size) : m_handle()
{
	m_handle = OpenZip(data, static_cast<unsigned long>(size), nullptr);
	if (cZipNull == m_handle)
	{
		throw runtime_error("Couldn't create zip");
	}
}
CZipHandle::~CZipHandle()
{
	CloseZip(m_handle);
}
CZipHandle::operator HZIP()
{
	return m_handle;
}
