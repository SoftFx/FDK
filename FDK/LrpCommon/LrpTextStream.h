#pragma once


class CLrpTextStream : public ILrpTextStream
{
public:
	CLrpTextStream(ostream& stream) : m_stream(stream)
	{
	}
private:
	CLrpTextStream(const CLrpTextStream&);
	CLrpTextStream& operator = (const CLrpTextStream&);
public:
	virtual void Write(const std::string& text)
	{
		m_stream<<text;
	}
private:
	ostream& m_stream;
};