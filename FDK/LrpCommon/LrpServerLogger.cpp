#include "stdafx.h"
#include "LrpServerLogger.h"

namespace Logging
{
	#include "Server_Logging.hpp"
}


typedef CLrpServerLogger LrpChannel;

#include "Server_TypesSerializer.hpp"
#define LrpInvoke LrpServerFormat
#define LrpSignature LrpServerLoggerSignature
#include "Server_Server.hpp"
#undef LrpSignature
#undef LrpInvoke




CLrpServerLogger::CLrpServerLogger(ostream& stream) : Logging::Server(&m_stream), m_stream(stream)
{
}
CLrpServerLogger& CLrpServerLogger::GetServer()
{
	return *this;
}
void CLrpServerLogger::Format(const uint16 componentId, const uint16 methodId, MemoryBuffer& buffer)
{
	LrpInvokeEx(0, componentId, methodId, buffer, this);
}
