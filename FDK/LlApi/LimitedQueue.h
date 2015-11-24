#pragma once

template<typename T> class LimitedQueue
{
private:
	typedef std::pair<std::string, T> Entry;
	typedef std::deque<list<pair<string, CFxMessage> >::iterator> Iterators;
	typedef typename std::list<Entry>::iterator iterator;
	typedef typename std::list<Entry>::const_iterator const_iterator;
public:
	inline LimitedQueue()
        : m_threshold(1)
	{
	}
public:
	inline const size_t get_threshold() const
	{
		return m_threshold;
	}
	inline void set_threshold(size_t newThreshold)
	{
		if (0 == newThreshold)
		{
			throw runtime_error("Threshold should more than zero");
		}
		m_threshold = newThreshold;
	}
public:
	inline bool push_back(const T& value)
	{
		return push_back(string(), value);
	}
	bool push_back(const std::string& key, const T& value)
	{
		m_entries.push_back(Entry(key, value));
		if (key.empty())
		{
			return true;
		}

		const size_t threshold = m_threshold;
		Iterators& iterators = m_keyToIterators[key];

		try
		{
			iterators.push_back(--m_entries.end());
			if (iterators.size() > threshold)
			{
				m_entries.erase(iterators.front());
				iterators.pop_front();
				return false;
			}
			return true;
		}
		catch (const std::bad_alloc&)
		{
			m_entries.pop_back();
			if (iterators.empty())
			{
				m_keyToIterators.erase(key);
			}
			throw;
		}
	}
	bool push_back(const std::string& key, const T& value, T& removing)
	{
		m_entries.push_back(Entry(key, value));
		if (key.empty())
		{
			return true;
		}

		const size_t threshold = m_threshold;
		Iterators& iterators = m_keyToIterators[key];

		try
		{
			iterators.push_back(--m_entries.end());
			if (iterators.size() > threshold)
			{
				auto it = iterators.front();
				std::swap(it->second, removing);
				m_entries.erase(it);
				iterators.pop_front();
				return false;
			}
			return true;
		}
		catch (const std::bad_alloc&)
		{
			m_entries.pop_back();
			if (iterators.empty())
			{
				m_keyToIterators.erase(key);
			}
			throw;
		}
	}
	inline const T& front() const
	{
		return m_entries.front().second;
	}
	inline const bool empty() const
	{
		return m_entries.empty();
	}
	inline const size_t size() const
	{
		return m_entries.size();
	}
	inline void clear()
	{
		m_entries.clear();
		m_keyToIterators.clear();
	}
	const_iterator begin() const
	{
		return m_entries.begin();
	}
	const_iterator end() const
	{
		return m_entries.end();
	}
	iterator begin()
	{
		return m_entries.begin();
	}
	iterator end()
	{
		return m_entries.end();
	}
	void pop_front()
	{
		const Entry& entry = m_entries.front();
		const string& key = entry.first;
		if (!key.empty())
		{
			auto it = m_keyToIterators.find(key);
			assert(m_keyToIterators.end() != it);
			Iterators& iterators = it->second;
			iterators.pop_front();
			m_entries.pop_front();
			if (iterators.empty())
			{
				m_keyToIterators.erase(it);
			}
		}
		else
		{
			m_entries.pop_front();
		}
	}

private:
	size_t m_threshold;
	std::list<Entry> m_entries;
	std::map<string, Iterators> m_keyToIterators;
};