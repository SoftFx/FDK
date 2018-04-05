#include "stdafx.h"
#include "FixMessage.h"
#include "FieldNumbers.h"
#include "FixFieldFormatter.h"
using namespace std;

namespace
{
	const string cNumber = "number";
	const string cName = "name";
	const string cType = "type";
	const string cEnum = "enum";
	const string cDescription = "description";
}
namespace
{
	const char* FindSubstring(const string& line, const char* substring, const size_t offset)
	{
		const size_t index = line.find(substring);
		if (string::npos == index)
		{
			return nullptr;
		}
		const char* result = line.c_str() + index + offset;
		return result;
	}
	template<size_t count> const char* FindSubstring(const string& line, const char (&buffer)[count])
	{
		return FindSubstring(line, buffer, count - 1);
	}
	bool ProcessField(const string& line, int& number, string& name)
	{
		const char* stNumber = FindSubstring(line, "number='");
		if (nullptr == stNumber)
		{
			return false;
		}
		number = atoi(stNumber);
		const char* stAccountBegin = FindSubstring(line, "name='");
		if (nullptr == stAccountBegin)
		{
			return false;
		}
		const char* stAccountEnd = strchr(stAccountBegin, '\'');
		if (nullptr == stAccountEnd)
		{
			return false;
		}
		name.assign(stAccountBegin, stAccountEnd);
		return true;
	}
	bool ProcessEnum(const string& line, string& key, string& value)
	{
		const char* stKeyBegin = FindSubstring(line, "enum='");
		if (nullptr == stKeyBegin)
		{
			return false;
		}
		const char* stKeyEnd = strchr(stKeyBegin, '\'');
		if (nullptr == stKeyEnd)
		{
			return false;
		}
		const char* stValueBegin = FindSubstring(line, "description='");
		if (nullptr == stValueBegin)
		{
			return false;
		}
		const char* stValueEnd = strchr(stValueBegin, '\'');
		if (nullptr == stValueEnd)
		{
			return false;
		}
		key.assign(stKeyBegin, stKeyEnd);
		value.assign(stValueBegin, stValueEnd);
		return true;
	}
}




namespace
{
	map<int, CFixFieldFormatter> gFormatters;
}


CFixMessage::CFixMessage(const std::string& text) : m_text(text)
{

}
void CFixMessage::LoadDictionary(const std::string& text)
{
	std::stringstream file;
	file<<text;

	string line;
	for(getline(file, line); !file.eof() && file.good(); getline(file, line))
	{
		const size_t index = line.find("<fields>");
		if (string::npos != index)
		{
			break;
		}
	}

	int number = 0;
	string name;
	string key;
	string value;

	CFixFieldFormatter* pFormatter = nullptr;

	for(getline(file, line); !file.eof() && file.good(); getline(file, line))
	{
		if (ProcessField(line, number, name))
		{
			CFixFieldFormatter& formatter = gFormatters[number];
			formatter = CFixFieldFormatter(name);
			pFormatter = &formatter;
		}
		else if (ProcessEnum(line, key, value))
		{
			if (nullptr != pFormatter)
			{
				pFormatter->AddEnum(key, value);
			}
		}
	}

	CFixFieldFormatter& msgType = gFormatters[FIX::FIELD::MsgType];
	CFixFieldFormatter& refMsgType = gFormatters[FIX::FIELD::RefMsgType];

	refMsgType.AddEnums(msgType);

}
namespace
{
	const char* ReadKey(const char* text, int32& key)
	{
		key = atoi(text);
		for (;; ++text)
		{
			const char ch = *text;
			if ('\0' == ch)
			{
				return text;
			}
			if ('=' == ch)
			{
				return (text + 1);
			}
		}
	}
	const char* ReadValue(const char* text, string& value)
	{
		for (;; ++text)
		{
			const char ch = *text;
			if ('\0' == ch)
			{
				return text;
			}
			if (1 == ch)
			{
				return (text + 1);
			}
			value += ch;
		}
	}
}
bool CFixMessage::Format(std::ostream& stream)const
{
	size_t length = 0;
	const char* text = &*m_text.begin();
	for (; 0 != *text;)
	{
		int32 key = 0;
		text = ReadKey(text, key);
		if (0 == key)
		{
			return false;
		}
		string value;
		if (FIX::FIELD::RawData != key)
		{
			text = ReadValue(text, value);
		}
		else
		{
			value = "<DATA>";
			text += (1 + length);
			length = 0;
		}
		if (FIX::FIELD::RawDataLength == key)
		{
			length = atoi(value.c_str());
		}
		auto it = gFormatters.find(key);
		if (gFormatters.end() == it)
		{
			stream<<key<<'='<<value;
		}
		else
		{
			it->second.Format(value, stream);
		}
		stream<<' ';
	}
	return true;
}
std::ostream& operator<<(std::ostream& stream, const CFixMessage& message)
{
	stringstream temp;
	if (message.Format(temp))
	{
		const string st = temp.str();
		stream<<st;
	}
	else
	{
		stream<<message.Text();
	}
	return stream;
}
std::wostream& operator<<(std::wostream& stream, const CFixMessage& message)
{
	stringstream temp;
	if (message.Format(temp))
	{
		const string st = temp.str();
		stream << st.c_str();
	}
	else
	{
		stream << message.Text().c_str();
	}
	return stream;
}