#include "stdafx.h"
#include "Logger.h"

CLogger::CLogger(LrpLogHandler pLogHandler, void* pUserParam) : m_logHandler(pLogHandler), m_userParam(pUserParam)
{
}

