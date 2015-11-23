#include "stdafx.h"
#include "FixFieldFormatter.h"
using namespace std;

/// <summary>
/// map requires this constructor
/// </summary>
CFixFieldFormatter::CFixFieldFormatter()
{
}
CFixFieldFormatter::CFixFieldFormatter(const string& name) : m_name(name)
{
}
void CFixFieldFormatter::AddEnum(const std::string& key, const std::string& value)
{
	m_enums[key] = value;
}
void CFixFieldFormatter::Format(const std::string& st, ostream& stream) const
{
	stream<<m_name<<"=";
	auto it = m_enums.find(st);
	if (it == m_enums.end())
	{
		stream<<st;
	}
	else
	{
		stream<<it->second;
	}
}
void CFixFieldFormatter::AddEnums(const CFixFieldFormatter& formatter)
{
	m_enums.insert(formatter.m_enums.begin(), formatter.m_enums.end());
}
