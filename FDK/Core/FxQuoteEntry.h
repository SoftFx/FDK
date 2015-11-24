#ifndef __Core_FxQuoteEntry__
#define __Core_FxQuoteEntry__


class CFxQuoteEntry
{
public:
	CFxQuoteEntry();
	CFxQuoteEntry(const double price, const double volume);
public:
	double Price;
	double Volume;
};


#pragma region inline methods
inline CFxQuoteEntry::CFxQuoteEntry() : Price(), Volume()
{
}
inline CFxQuoteEntry::CFxQuoteEntry(const double price, const double volume) : Price(price), Volume(volume)
{
}
#pragma endregion
#endif
