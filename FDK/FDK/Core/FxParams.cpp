#include "stdafx.h"
#include "FxParams.h"
#include "RuntimeError.h"

namespace
{	
	const string cBooleanType = "Boolean";
	const string cInt32Type = "Int32";
	const string cRealType = "Real";
	const string cStringType = "String";
}

namespace
{
	void Split(const string& st, vector<string>& items)
	{
		auto it = st.begin();
		auto end = st.end();
		for (;it != end;)
		{
			stringstream stream;
			for (; it != end;)
			{
				char ch = *it;
				++it;
				if (';' != ch)
				{
					stream<<ch;
					continue;
				}
				else if (it == end)
				{
					break;
				}
				ch = *it;
				if (';' == ch)
				{
					stream<<';';
				}
				else
				{
					break;
				}
			}
			const string s = stream.str();
			items.push_back(s);
		}
	}
	template<typename T> T ConvertFromString(const string& st)
	{
		stringstream stream;
		stream<<st;
		T result = T();
		stream>>result;
		if (stream.bad() || !stream.eof())
		{
			throw CRuntimeError("Can not convert from \"") + st +"\" to " + typeid(T).name();
		}
		return result;
	}
	template<> bool ConvertFromString<bool>(const string& st)
	{
		if ("true" == st)
		{
			return true;
		}
		else if ("false" == st)
		{
			return false;
		}
		throw CRuntimeError("Can not convert from \"") + st +"\" to " + typeid(bool).name();
	}
}

CFxParams::CFxParams()
{
}

CFxParams::CFxParams(const string& parametersString)
{
	Parse(parametersString);
}

void CFxParams::Parse(const string& parametersString)
{
	m_key2Int32.clear();
	m_key2Real.clear();
	m_key2String.clear();

	vector<string> items;
	Split(parametersString, items);
	set<string> keys;
	foreach(const auto& item, items)
	{
		Parse(item, keys);
	}
}

namespace
{
	bool ReadInternal(istream& stream, const char separator, string& value)
	{
		for (; !stream.eof(); )
		{
			char ch = 0;
			stream.get(ch);
			if (separator == ch)
			{
				return true;
			}
			if (0 == ch)
			{
				return false;
			}
			value += ch;
		}
		return false;
	}
	bool ParseInternal(const string& st, string& type, string& key, string& value)
	{
		stringstream stream;
		stream<<st;
		char ch = 0;
		stream.get(ch);
		if ('[' != ch)
		{
			return false;
		}
		if (!ReadInternal(stream, ']', type))
		{
			return false;
		}
		if (!ReadInternal(stream, '=', key))
		{
			return false;
		}
		if (!ReadInternal(stream, '\0', value))
		{
			return false;
		}
		return true;
	}
}

void CFxParams::Parse(const string& item, set<string>& keys)
{
	string type;
	string key;
	string value;
	const bool status = ParseInternal(item, type, key, value);
	if (!status)
	{
		throw CRuntimeError("Incorrect parameter = ") + item;
	}
	if (keys.count(key) > 0)
	{
		throw CRuntimeError("Duplicate key = ") + key;
	}
	keys.insert(key);
	if (cInt32Type == type)
	{
		m_key2Int32[key] = ConvertFromString<int32>(value);
	}
	else if (cRealType == type)
	{
		m_key2Real[key] = ConvertFromString<double>(value);
	}
	else if (cStringType == type)
	{
		m_key2String[key] = value;
	}
	else if (cBooleanType == type)
	{
		m_key2Boolean[key] = ConvertFromString<bool>(value);
	}
	else
	{
		throw CRuntimeError("Unsupported parameter type = ") + type;
	}
}

const string& CFxParams::GetString(const string& key) const
{
	auto it = m_key2String.find(key);
	if (m_key2String.end() == it)
	{
		throw CRuntimeError("String key = \"") + key +  "\" not found";
	}
	return it->second;
}

int32 CFxParams::GetInt32(const string& key) const
{
	auto it = m_key2Int32.find(key);
	if (m_key2Int32.end() == it)
	{
		throw CRuntimeError("Integer key = \"") + key +  "\" not found";
	}
	return it->second;
}

double CFxParams::GetReal(const string& key) const
{
	auto it = m_key2Real.find(key);
	if (m_key2Real.end() == it)
	{
		throw CRuntimeError("Real key = \"") + key +  "\" not found";
	}
	return it->second;
}

bool CFxParams::GetBoolean(const string& key) const
{
	auto it = m_key2Boolean.find(key);
	if (m_key2Boolean.end() == it)
	{
		throw CRuntimeError("Boolean key = \"") + key +  "\" not found";
	}
	return it->second;
}

bool CFxParams::TryGetString(const string& key, string& value)const
{
	auto it = m_key2String.find(key);
	if (m_key2String.end() != it)
	{
		value = it->second;
		return true;
	}
	return false;
}

bool CFxParams::TryGetInt32(const string& key, int32& value)const
{
	auto it = m_key2Int32.find(key);
	if (m_key2Int32.end() != it)
	{
		value = it->second;
		return true;
	}
	return false;
}

bool CFxParams::TryGetReal(const string& key, double& value) const
{
	auto it = m_key2Real.find(key);
	if (m_key2Real.end() != it)
	{
		value = it->second;
		return true;
	}
	return false;
}

bool CFxParams::TryGetBoolean(const string& key, bool& value) const
{
	auto it = m_key2Boolean.find(key);
	if (m_key2Boolean.end() != it)
	{
		value = it->second;
		return true;
	}
	return false;
}

void CFxParams::AddInt32(const string& key, const int32 value)
{
	m_key2Int32[key] = value;
}

void CFxParams::AddBoolean(const string& key, const bool value)
{
	m_key2Boolean[key] = value;
}

void CFxParams::AddReal(const string& key, const double value)
{
	m_key2Real[key] = value;
}

void CFxParams::AddString(const string& key, const string& value)
{
	m_key2String[key] = value;
}

string CFxParams::ToString() const
{
	stringstream stream;
	stream.precision(DBL_DIG);
	stream<<boolalpha;

	foreach(const auto& element, m_key2Int32)
	{
		stream<<'['<<cInt32Type<<']'<<element.first<<'='<<element.second<<';';
	}
	foreach(const auto& element, m_key2Real)
	{
		stream<<'['<<cRealType<<']'<<element.first<<'='<<element.second<<';';
	}
	foreach(const auto& element, m_key2Boolean)
	{
		stream<<'['<<cBooleanType<<']'<<element.first<<'='<<element.second<<';';
	}
	foreach(const auto& element, m_key2String)
	{
		stream<<'['<<cStringType<<']'<<element.first<<'=';
		foreach(const auto ch, element.second)
		{
			stream<<ch;
			if (';' == ch)
			{
				stream<<ch;
			}
		}
		stream<<';';
	}
	const string result = stream.str();
	return result;
}
