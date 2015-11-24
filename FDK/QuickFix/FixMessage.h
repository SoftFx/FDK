#pragma once

class CFixMessage
{
public:
	CFixMessage(const std::string& text);
private:
	CFixMessage& operator = (const CFixMessage&);
public:
	static void LoadDictionary(const std::string& text);
	const std::string& Text()const;
private:
	bool Format(std::ostream& stream)const;
private:
	const std::string& m_text;
private:
	friend std::ostream& operator << (std::ostream& stream, const CFixMessage& message);
};


#pragma region inline methods
inline const std::string& CFixMessage::Text()const
{
	return m_text;
}
#pragma endregion