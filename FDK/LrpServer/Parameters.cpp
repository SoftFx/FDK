#include "stdafx.h"
#include "Parameters.h"


CParameters::CParameters()
{
    EnableCodec = false;
    ValidateCodec = false;
    MessagesNumberLimit = 256;
    MessagesSizeLimit = 64 * 1024;
    ThreadsNumber = 1;
    HandshakeTimeout = 5000;
    HeartbeatTimeout = 30000;
}
