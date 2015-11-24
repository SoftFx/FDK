#include "stdafx.h"
#include "CurrencyEntries.h"
#include "Constants.h"

namespace FDK
{
	CCurrencyEntries::CCurrencyEntries()
	{
	}

	CCurrencyEntries::CCurrencyEntries(const CCurrencyEntries& entries)
        : m_entries(entries.m_entries)
	{
	}

	CCurrencyEntries& CCurrencyEntries::operator = (const CCurrencyEntries& entries)
	{
		if (this != &entries)
		{
			m_entries = entries.m_entries;
		}
		return *this;
	}

	CCurrencyEntries::~CCurrencyEntries()
	{
	}

	size_t CCurrencyEntries::size() const
	{
		return m_entries.size();
	}

	bool CCurrencyEntries::empty() const
	{
		return m_entries.empty();
	}

	void CCurrencyEntries::clear()
	{
		m_entries.clear();
	}

	size_t CCurrencyEntries::capacity() const
	{
		return m_entries.capacity();
	}

	void CCurrencyEntries::reserve(const size_t capacity)
	{
		m_entries.reserve(capacity);
	}

	const char* CCurrencyEntries::do_front() const
	{
		const std::string& st = m_entries.front();
		return st.c_str();
	}

	const char* CCurrencyEntries::do_back() const
	{
		const std::string& st = m_entries.back();
		return st.c_str();
	}

	const char* CCurrencyEntries::do_operator(size_t index) const
	{
		const std::string& st = m_entries[index];
		return st.c_str();
	}

	const char* CCurrencyEntries::do_at(size_t index) const
	{
		const std::string& st = m_entries.at(index);
		return st.c_str();
	}

	void CCurrencyEntries::push_back(const char* currency)
	{
		m_entries.push_back(currency);
	}

	bool CCurrencyEntries::erase(const char* currency)
	{
		string st = currency;
		auto it = m_entries.begin();
		auto end = m_entries.end();

		for (; it != end; ++it)
		{
			if (st == *it)
			{
				m_entries.erase(it);
				return true;
			}
		}
		return false;
	}

	void CCurrencyEntries::exchange(const size_t first, const size_t second)
	{
		std::swap(m_entries[first], m_entries[second]);
	}

	bool CCurrencyEntries::exist(const char* currency) const
	{
		string st = currency;
		for each(const auto& element in m_entries)
		{
			if (currency == element)
			{
				return true;
			}
		}
		return false;
	}
}