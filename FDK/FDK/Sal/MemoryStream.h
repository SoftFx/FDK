#pragma once

#pragma warning (push)
#pragma warning (disable : 4251)

class SAL_API CMemoryStream : public std::ostream, private std::basic_streambuf<char>
{
public:
	CMemoryStream();
	CMemoryStream(CMemoryStream&) = delete;
	CMemoryStream(CMemoryStream&&) = delete;
public:
	CMemoryStream& operator << (const char* arg);
	CMemoryStream& operator << (const std::string& arg);
public:
	template<typename T> inline CMemoryStream& operator << (const T& arg)
	{
		ostream& _stream = *this;
		_stream<<arg;
		return *this;
	}
public:
	std::string str() const;
protected:
	virtual std::basic_streambuf<char>::int_type overflow(std::basic_streambuf<char>::int_type character);
	virtual int sync();
	virtual std::streamsize xsputn(const char* ptr, std::streamsize size);
private:
	void Copy();
private:
	bool m_stack;
	size_t m_position;
	char m_buffer[1024];
	std::vector<char> m_data;
};

#pragma warning (pop)