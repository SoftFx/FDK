#include "stdafx.h"
#include "Request.h"

CRequest::CRequest()
{
}
CRequest::CRequest(const string& id, const CFxOrder& order) : ID(id), Order(order)
{
}
