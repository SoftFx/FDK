#include "CriticalSection.h"

string FxGenerateGuid()
{
	GUID guid;
	ZeroMemory(&guid, sizeof(guid));
	HRESULT hr = CoCreateGuid(&guid);
	if (FAILED(hr))
	{
		throw runtime_error(__FUNCTION__"(): couldn't create a new guid");
	}
	CComBSTR st = guid;
	string result = CW2A(st);
	return result;
}
SAL_API std::string FxStartupDirectory()
{
	char path[MAX_PATH] = "";
	GetModuleFileNameA(nullptr, path, _countof(path));
	string result = path;
	size_t index = result.find_last_of('\\');
	result = result.substr(0, index);
	return result;
}

std::string FxStringFromResource(void* module, const std::string& id, const std::string& type)
{
	HMODULE hModule = reinterpret_cast<HMODULE>(module);
	const int idValue = atoi(id.c_str());
	const char* idResource = MAKEINTRESOURCEA(idValue);
	HRSRC info = FindResourceA(hModule, idResource, type.c_str());
	if (NULL == info)
	{
		return string();
	}
	HGLOBAL handle = LoadResource(hModule, info);
	if (NULL == handle)
	{
		return string();
	}
	char* text = reinterpret_cast<char*>(LockResource(handle));
	DWORD size = SizeofResource(hModule, info);
	string result;
	try
	{
		result = string(text, text + size);
	}
	catch (const exception&)
	{
	}
	UnlockResource(handle);
	return result;
}




namespace
{
	volatile uint64 gCurrentTickInMilliseconds = 0;
	volatile DWORD gLastUpdatedTime = 0;
	const DWORD cGetTickCountMaximum =  numeric_limits<DWORD>::max();
	CCriticalSection gSection;
}

uint64 FxGetTickCount()
{
	gSection.Acquire();
	const DWORD currentTime = GetTickCount();
	const DWORD lastUpdateTime = gLastUpdatedTime;
	if (currentTime >= lastUpdateTime)
	{
		gCurrentTickInMilliseconds += (currentTime - lastUpdateTime);
	}
	else
	{
		gCurrentTickInMilliseconds += currentTime + (cGetTickCountMaximum - lastUpdateTime) + 1;
	}
	gLastUpdatedTime = currentTime;
	const uint64 result = gCurrentTickInMilliseconds;
	gSection.Release();
	return result;
}
SAL_API bool IsFinite(const double value)
{
	const bool result = (0 != _finite(value));
	return result;
}
SAL_API size_t FxInterlockedIncrement(size_t* pValue)
{
	return static_cast<size_t>(InterlockedIncrement(pValue));
}

SAL_API size_t FxInterlockedDecrement(size_t* pValue)
{
	return static_cast<size_t>(InterlockedDecrement(pValue));
}


void GetFiles(const tstring& path, vector<tstring>& files, const tstring& mask /* = string() */)
{
	tstring root = path;
	const TCHAR ch = path.at(path.length() - 1);
	if (TEXT('\\') != ch)
	{
		root += TEXT('\\');
	}
	tstring pattern;
	if (mask.empty())
	{
		pattern = root + TEXT('*');
	}
	else
	{
		pattern = root + mask;
	}

	WIN32_FIND_DATA data;
	ZeroMemory(&data, sizeof(data));
	HANDLE handle = FindFirstFile(pattern.c_str(), &data);
	if (INVALID_HANDLE_VALUE == handle)
	{
		return;
	}
	try
	{
		for (BOOL status = TRUE; status; status = FindNextFile(handle, &data))
		{
			tstring st = data.cFileName;
			if ((TEXT(".") == st) || (TEXT("..") == st))
			{
				continue;
			}
			if (!(data.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY))
			{
				files.push_back(root + st);
			}
		}
		FindClose(handle);
	}
	catch (const bad_alloc&)
	{
		FindClose(handle);
		throw;
	}
}
string FxCombinePath(const string& directory, const string& fileName)
{
	if (directory.empty())
	{
		return fileName;
	}
	if ('\\' == directory.back())
	{
		return directory + fileName;
	}
	else
	{
		return directory + '\\' + fileName;
	}
}