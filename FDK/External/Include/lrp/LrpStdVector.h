



public:
	typedef value_type* iterator;
	typedef const value_type* const_iterator;
	typedef const value_type& reference;
	typedef const value_type& const_reference;
public:
	LRP_STD_API size_t size() const;
	LRP_STD_API bool empty() const;
	LRP_STD_API void clear();
	LRP_STD_API size_t capacity() const;
	LRP_STD_API void reserve(const size_t capacity);

	LRP_STD_API const value_type& front() const;
	LRP_STD_API value_type& front();
	LRP_STD_API const value_type& back() const;
	LRP_STD_API value_type& back();
	LRP_STD_API const value_type& operator[] (size_t index) const;
	LRP_STD_API value_type& operator[] (size_t index);
	LRP_STD_API const value_type& at(size_t index) const;
	LRP_STD_API value_type& at(size_t index);
	LRP_STD_API iterator begin();
	LRP_STD_API iterator end();
	LRP_STD_API const_iterator begin() const;
	LRP_STD_API const_iterator end() const;
	LRP_STD_API void push_back(const value_type& entry);
	LRP_STD_API void erase(iterator it);
	LRP_STD_API void pop_back();
private:
	LrpVector(value_type) m_entries;