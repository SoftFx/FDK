#include "stdafx.h"
#include "Config.h"

Config gConfig;

Config::Config() : IsDestructorVirtual(false)
{
}


namespace
{
	string ConfigPath(HMODULE module)
	{
		char buffer[MAX_PATH] = "";
		GetModuleFileNameA(module, buffer, sizeof(buffer));
		string result = buffer;
		result += ".xml";
		return result;
	}
	bool ExistFile(const string& path)
	{
		WIN32_FIND_DATAA data;
		ZeroMemory(&data, sizeof(data));
		HANDLE handle = FindFirstFileA(path.c_str(), &data);
		if (INVALID_HANDLE_VALUE == handle)
		{
			return false;
		}
		FindClose(handle);
		return true;
	}
	bool LoadConfig(const string& path)
	{
		ifstream stream(path.c_str());
		string text;
		string st;
		for (getline(stream, st); !stream.eof(); getline(stream, st))
		{
			text += st;
		}
		text += st;


		Config config;
		const HRESULT status = XmlDeserialize(text, config);
		const bool result = SUCCEEDED(status);
		if (result)
		{
			swap(gConfig, config);
		}
		return result;
	}
	void SaveConfig(const string& path)
	{
		string text;
		XmlSerialize(gConfig, text);
		ofstream stream(path.c_str());
		stream<<text;
	}
}

bool ProcessConfig(HANDLE module)
{
	string path = ConfigPath(reinterpret_cast<HMODULE>(module));
	if (!ExistFile(path) || !LoadConfig(path))
	{
		SaveConfig(path);
	}
	return true;
}