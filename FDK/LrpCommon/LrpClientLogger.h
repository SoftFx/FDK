#pragma once
#include "LrpTextStream.h"

namespace Logging
{
#include "Client_Logging.h"
}


class CLrpClientLogger : public Logging::Client, public Logging::SimpleCodec
{
public:
	CLrpClientLogger(ostream& stream);
public:
	CLrpClientLogger& GetClient();
	CLrpClientLogger& GetSimpleCodec();
public:
	void OnFileChunkMsg(const std::string& requestId, const CFxFileChunk& chunk);
	void OnQuoteRawMsg(const MemoryBuffer& data);
public:
	void Format(const uint16 componentId, const uint16 methodId, MemoryBuffer& buffer);
private:
	CLrpTextStream m_stream;
};

void FormatQuote(const CFxQuote& quote, ostream& stream);


