#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)
#pragma warning (disable : 4251)

#include "FxHandle.h"

class CORE_API CFxParams : public CFxHandle
{
public:
	CFxParams();
	CFxParams(const string& parametersString);
public:
	void AddInt32(const string& key, const int32 value);
	void AddReal(const string& key, const double value);
	void AddString(const string& key, const string& value);
	void AddBoolean(const string& key, const bool value);
public:
	const string& GetString(const string& key)const;
	int32 GetInt32(const string& key)const;
	double GetReal(const string& key)const;
	bool GetBoolean(const string& key)const;
public:
	bool TryGetString(const string& key, string& value)const;
	bool TryGetInt32(const string& key, int32& value)const;
	bool TryGetReal(const string& key, double& value)const;
	bool TryGetBoolean(const string& key, bool& value)const;
public:
	string ToString()const;
private:
	void Parse(const string& parametersString);
	void Parse(const string& item, set<string>& keys);
private:
	map<string, bool> m_key2Boolean;
	map<string, int32> m_key2Int32;
	map<string, double> m_key2Real;
	map<string, string> m_key2String;
};

#pragma warning (pop)
