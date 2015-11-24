#pragma once
#include "LrpTextStream.h"

namespace Logging
{
	#include "Server_Logging.h"
}


class CLrpServerLogger : public Logging::Server
{
public:
	CLrpServerLogger(ostream& stream);
public:
	CLrpServerLogger& GetServer();
public:
	void Format(const uint16 componentId, const uint16 methodId, MemoryBuffer& buffer);
private:
	CLrpTextStream m_stream;
};


