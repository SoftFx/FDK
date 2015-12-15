#pragma once

namespace FDK
{
	/// <summary>
	/// Represents list of the most quoted currencies
	/// </summary>
	class CCurrencyEntries
	{
	public:
		LLCOMMON_API CCurrencyEntries();
		LLCOMMON_API CCurrencyEntries(const CCurrencyEntries& entries);
		LLCOMMON_API CCurrencyEntries& operator = (const CCurrencyEntries& entries);
		LLCOMMON_API ~CCurrencyEntries();
	public:
		LLCOMMON_API size_t size() const;
		LLCOMMON_API bool empty() const;
		LLCOMMON_API void clear();
		LLCOMMON_API size_t capacity() const;
		LLCOMMON_API void reserve(const size_t capacity);
	public:
		inline const std::string front() const { return do_front(); }
		inline const std::string back() const { return do_back(); }
		inline const std::string operator[] (size_t index) const { return do_operator(index); }
		inline const std::string at(size_t index) const { return do_at(index); }
		inline bool exist(const std::string& currency) const { return exist(currency.c_str()); }
		LLCOMMON_API bool exist(const char* currency) const;
	public:
		inline void push_back(const std::string& currency) { push_back(currency.c_str()); }
		LLCOMMON_API void push_back(const char* currency);
		inline bool erase(const std::string& currency) { return erase(currency.c_str()); }
		LLCOMMON_API bool erase(const char* currency);
		LLCOMMON_API void exchange(const size_t first, const size_t second);
	internal:
		inline const LrpVector(std::string)& GetEntries() const { return m_entries; }
	private:
		LLCOMMON_API const char* do_front() const;
		LLCOMMON_API const char* do_back() const;
		LLCOMMON_API const char* do_operator(size_t index) const;
		LLCOMMON_API const char* do_at(size_t index) const;
	private:
		LrpVector(std::string) m_entries;
	};
}