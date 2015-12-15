#pragma once


class CParamsImpl
{
public:
	void* Create();
	void SetString(void* handle, const string& key, const string& value);
	void SetInt32(void* handle, const string& key, const int value);
	void SetDouble(void* handle, const string& key, const double value);
	void SetBoolean(void* handle, const string& key, const bool value);
	string ToText(void* handle);
};

