#include "stdafx.h"
#include "LrpClientLogger.h"

namespace Logging
{
#include "Client_Logging.hpp"
}


typedef CLrpClientLogger LrpChannel;

#include "Client_TypesSerializer.hpp"
#define LrpInvoke LrpClientFormat
#define LrpSignature LrpClientLoggerSignature
#include "Client_Server.hpp"
#undef LrpSignature
#undef LrpInvoke




CLrpClientLogger::CLrpClientLogger(ostream& stream) : Logging::Client(&m_stream), Logging::SimpleCodec(&m_stream), m_stream(stream)
{
}
CLrpClientLogger& CLrpClientLogger::GetClient()
{
    return *this;
}
CLrpClientLogger& CLrpClientLogger::GetSimpleCodec()
{
    return *this;
}
void CLrpClientLogger::Format(const uint16 componentId, const uint16 methodId, MemoryBuffer& buffer)
{
    LrpInvokeEx(0, componentId, methodId, buffer, this);
}

void CLrpClientLogger::OnFileChunkMsg(const std::string& requestId, const CFxFileChunk& chunk)
{
    CFxFileChunk _chunk;
    _chunk.FileId = chunk.FileId;
    _chunk.ChunkId = chunk.ChunkId;
    _chunk.TotalChunks = chunk.TotalChunks;
    _chunk.FileSize = chunk.FileSize;
    _chunk.FileName = chunk.FileName;
    __super::OnFileChunkMsg(requestId, _chunk);
}
void CLrpClientLogger::OnQuoteRawMsg(const MemoryBuffer& data)
{
    __super::OnQuoteRawMsg(data);

    MemoryBuffer buffer(nullptr, data.GetData(), data.GetSize(), data.GetCapacity());
    buffer.SetPosition(data.GetPosition());


    CDateTime sendingTime = ReadTime(buffer);
    CFxQuote quote = ReadQuote(buffer);

    std::stringstream stream;

    stream<<" ";
    LrpWriteTime("sendingTime", sendingTime, stream);

    stream<<" ";
    Logging::LrpWriteQuote("quote", quote, stream);

    m_stream.Write(stream.str());
}

void FormatQuote(const CFxQuote& quote, ostream& stream)
{
    Logging::LrpWriteQuote("quote", quote, stream);
}
