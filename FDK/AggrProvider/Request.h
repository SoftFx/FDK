#pragma once


class CRequest
{
public:
	string ID;
	CFxOrder Order;
public:
	CRequest();
	CRequest(const string& id, const CFxOrder& order);
};