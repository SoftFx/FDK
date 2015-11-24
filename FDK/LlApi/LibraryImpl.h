#pragma once


class CLibraryImpl
{
public:
	static void WriteNormalDumpOnError(const wstring& path);
	static void WriteFullDumpOnError(const wstring& path);
	static void WriteNormalDump(const wstring& path);
	static void WriteFullDump(const wstring& path);
};