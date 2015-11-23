#pragma once



class CFixFieldFormatter
{
public:
	CFixFieldFormatter();
	CFixFieldFormatter(const std::string& name);
public:
	void AddEnum(const std::string& key, const std::string& value);
	void AddEnums(const CFixFieldFormatter& formatter);
	void Format(const std::string& st, std::ostream& stream)const;
private:
	std::string m_name;
	std::map<std::string, std::string> m_enums;
};