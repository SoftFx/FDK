#include "stdafx.h"
#include "Formating.h"
#include "Constants.h"

namespace FDK
{

	void Process(const char* name, const std::string& value, std::ostream& stream)
	{
		std::stringstream _stream;
		_stream<<value;
		LrpWriteAString(name, _stream.str(), stream);
		stream<<"; ";
	}

	void Process(const char* name, const double value, std::ostream& stream)
	{
		LrpWriteDouble(name, value, stream);
		stream<<"; ";
	}

	void Process(const char* name, const Nullable<double> value, std::ostream& stream)
	{
		if (value.HasValue())
		{
			Process(name, value.Value(), stream);
		}
		else
		{
			if (nullptr != name)
			{
				stream<<name<<" = ";
			}
			stream<<"null; ";
		}
	}

	void Process(const char* name, const CTagEntry value, std::ostream& stream)
	{
		stringstream _stream;
		_stream<<value;
		string st = _stream.str();

		Process(name, st, stream);
	}

	void ValidateVerbatimText(const char* text, std::istream& stream)
	{
		for (char ch = *text; 0 != ch; ch = *(++text))
		{
			char _ch = 0;
			stream.get(_ch);
			if (ch != _ch)
			{
				stringstream strstream;
				strstream<<"ValidateVerbatim(text = "<<text<<"): read ch = "<<_ch<<", but expected ch = "<<ch;
				throw runtime_error(strstream.str());
			}
		}
	}

	void ValidateVerbatimText(const std::string& text, std::istream& stream)
	{
		ValidateVerbatimText(text.c_str(), stream);
	}

	void ReadForCharacter(char value, std::istream& stream)
	{
		for (;;)
		{
			char ch = 0;
			stream.get(ch);
			if (value == ch)
			{
				break;
			}
			if (0 == ch)
			{
				throw runtime_error("ReadForCharacter() end of stream has been reached");
			}
		}
	}

	int ReadEnumValue(const char* name, std::istream& stream)
	{
		if (nullptr != name)
		{
			ValidateVerbatimText(name, stream);
			ValidateVerbatimText(" = ", stream);
		}
		ReadForCharacter('(', stream);
		int result = -1;
		stream>>result;
		if (stream.bad())
		{
			throw runtime_error("ReadEnumValue(): bad format");
		}
		ValidateVerbatimText("); ", stream);
		return result;
	}

	void ValidateEndOfStream(std::istream& stream)
	{
		char ch = 0;
		stream.get(ch);
		if (!stream.eof())
		{
			throw runtime_error("ValidateEndOfStream(): line was not read to end");
		}
	}

	void Process(const char* name, std::string& value, std::istream& stream)
	{
		if (nullptr != name)
		{
			ValidateVerbatimText(name, stream);
			ValidateVerbatimText(" = ", stream);
		}

		ValidateVerbatimText("\"", stream);

		stringstream builder;

		for (; ; )
		{
			char ch = 0;
			stream.get(ch);
			if ('\\' == ch)
			{
				ch = 0;
				stream.get(ch);
				if ('n' == ch)
				{
					builder<<'\n';
				}
				else if ('t' == ch)
				{
					builder<<'\t';
				}
				else if ('r' == ch)
				{
					builder<<'\r';
				}
				else if ('"' == ch)
				{
					builder<<'\"';
				}
				else if ('0' == ch)
				{
					builder<<'\0';
				}
				else if ('0' == ch)
				{
					builder<<'\0';
				}
				else if ('\\' == ch)
				{
					builder<<'\\';
				}
				else
				{
					throw runtime_error("Incorrect escape character");
				}
			}
			else if ('"' == ch)
			{
				break;
			}
			else
			{
				builder<<ch;
			}
		}

		value = builder.str();

		ValidateVerbatimText("; ", stream);
	}


	namespace
	{
		int Int32FromHexChar(char ch)
		{
			int result = (ch - '0');
			if (result < 0)
			{
				throw runtime_error(string("Binary data contains invalid character = ") + ch);
			}
			if (result > 9)
			{
				result -= 7;
				if ((result < 10) || (result > 15))
				{
					throw runtime_error(string("Binary data contains invalid character = ") + ch);
				}
			}
			return result;
		}

		void ReadBinaryData(std::istream& stream, void* data, size_t count)
		{
			unsigned char* begin = reinterpret_cast<unsigned char*>(data);
			unsigned char* end = begin + count;

			for (unsigned char* current = begin; current < end; ++current)
			{
				char ch = 0;
				stream.get(ch);
				if (0 == ch)
				{
					throw runtime_error("ReadBinaryData(): end of stream has been reached");
				}
				int first = Int32FromHexChar(ch);

				ch = 0;
				stream.get(ch);
				if (0 == ch)
				{
					throw runtime_error("ReadBinaryData(): end of stream has been reached");
				}
				int second = Int32FromHexChar(ch);
				*current = (unsigned char)(16 * first + second);
			}
		}
	}

	void Process(const char* name, double& value, std::istream& stream)
	{
		if (nullptr != name)
		{
			ValidateVerbatimText(name, stream);
			ValidateVerbatimText(" = ", stream);
		}
		ReadForCharacter('(', stream);
		ValidateVerbatimText("0x", stream);
		ReadBinaryData(stream, &value, sizeof(value));
		ValidateVerbatimText("); ", stream);
	}

	void Process(const char* name, Nullable<double>& value, std::istream& stream)
	{
		if (nullptr != name)
		{
			ValidateVerbatimText(name, stream);
			ValidateVerbatimText(" = ", stream);
		}

		string st;
		getline(stream, st, ';');
		if (cNull == st)
		{
			value = Nullable<double>();
		}
		else
		{
			stringstream _stream;
			_stream<<st;
		
			double _value = 0;
			ReadForCharacter('(', _stream);
			ValidateVerbatimText("0x", _stream);
			ReadBinaryData(_stream, &_value, sizeof(_value));
			ValidateVerbatimText(")", _stream);
			value = _value;
		}

		ValidateVerbatimText(" ", stream);
	}

	void Process(const char* name, CTagEntry& value, std::istream& stream)
	{
		string st;
		Process(name, st, stream);
		value = st.c_str();
	}
}