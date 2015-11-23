#include "stdafx.h"

#include "Local_Channel.h"
typedef CLocalChannel LrpChannel;

#include "Local_TypesSerializer.hpp"
#include "Local_Server.hpp"




void WriteQuoteToMemoryBuffer(const CFxQuote& quote, MemoryBuffer& buffer)
{
	WriteQuote(quote, buffer);
}