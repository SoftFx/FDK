#ifndef __Core_Bid_Comparator__
#define __Core_Bid_Comparator__

class CBidComparator
{
public:
	bool operator ()(const CFxQuoteEntry& first, const CFxQuoteEntry& second);
};

#pragma region inline methods

inline bool CBidComparator::operator ()(const CFxQuoteEntry& first, const CFxQuoteEntry& second)
{
	return (first.Price > second.Price);
}


#pragma endregion
#endif
