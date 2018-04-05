#pragma once

inline std::wstring& Utf8ToStd(std::wstring& dest, const std::string& src)
{
	if (!src.length())
	{
		dest = L"";
		return dest;
	}

	int result = MultiByteToWideChar(CP_UTF8, 0, src.data(), src.length(), 0, 0);

	if (!result)
		throw std::logic_error("Invalid string to convert from UTF-8");

	dest.resize(result);

	result = MultiByteToWideChar(CP_UTF8, 0, src.data(), src.length(), const_cast<wchar_t*>(dest.data()), dest.length());

	if (!result)
		throw std::logic_error("Invalid string to convert from UTF-8");

	return dest;
}

inline std::wstring StringToWString(const std::string& s)
{
	std::wstring utf8String;
	std::wstring result = Utf8ToStd(utf8String, s);

	return result;
}
