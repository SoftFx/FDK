#include "stdafx.h"
#include "Text.h"


std::wstring UnicodeFromAscii(const std::string& st)
{
	std::wstring result(st.begin(), st.end());
	return result;
}
std::string AsciiFromUnicode(const std::wstring& st)
{
	std::string result(st.begin(), st.end());
	return result;
}
