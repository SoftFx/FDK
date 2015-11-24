


size_t LrpContainer::size() const
{
	return m_entries.size();
}
size_t LrpContainer::capacity() const
{
	return m_entries.capacity();
}
bool LrpContainer::empty() const
{
	return m_entries.empty();
}
void LrpContainer::clear()
{
	m_entries.clear();
}
void LrpContainer::reserve(const size_t capacity)
{
	m_entries.reserve(capacity);
}
const LrpContainer::value_type& LrpContainer::front() const
{
	return m_entries.front();
}
LrpContainer::value_type& LrpContainer::front()
{
	return m_entries.front();
}
const LrpContainer::value_type& LrpContainer::back() const
{
	return m_entries.back();
}
LrpContainer::value_type& LrpContainer::back()
{
	return m_entries.back();
}
const LrpContainer::value_type& LrpContainer::operator[](size_t index) const
{
	return m_entries[index];
}
LrpContainer::value_type& LrpContainer::operator[](size_t index)
{
	return m_entries[index];
}
const LrpContainer::value_type& LrpContainer::at(size_t index) const
{
	return m_entries.at(index);
}
LrpContainer::value_type& LrpContainer::at(size_t index)
{
	return m_entries.at(index);
}
LrpContainer::const_iterator LrpContainer::begin() const
{
	const_iterator result = m_entries.empty() ? nullptr : &m_entries.front();
	return result;
}
LrpContainer::const_iterator LrpContainer::end() const
{
	const_iterator result = begin() + size();
	return result;
}
LrpContainer::iterator LrpContainer::begin()
{
	iterator result = m_entries.empty() ? nullptr : &m_entries.front();
	return result;
}
LrpContainer::iterator LrpContainer::end()
{
	iterator result = begin() + size();
	return result;
}
void LrpContainer::push_back(const LrpContainer::value_type& entry)
{
	m_entries.push_back(entry);
}
void LrpContainer::erase(iterator it)
{
	size_t offset = it - begin();
	auto position = m_entries.begin() + offset;
	m_entries.erase(position);
}
void LrpContainer::pop_back()
{
	m_entries.pop_back();
}