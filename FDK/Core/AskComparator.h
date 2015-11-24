#ifndef __Core_Ask_Comparator__
#define __Core_Ask_Comparator__

class CAskComparator
{
public:
	bool operator ()(const CFxQuoteEntry& first, const CFxQuoteEntry& second);
};

#pragma region inline methods

inline bool CAskComparator::operator ()(const CFxQuoteEntry& first, const CFxQuoteEntry& second)
{
	return (first.Price < second.Price);
}


#pragma endregion
#endif
