#pragma once
#include "FLevel2.h"
#include "FSessionInfo.h"
#include "FSymbolInfo.h"
#include "FAccountInfo.h"


class IFeederHandler
{

public:
	virtual void VLogon(const set<int32>& ids) = 0;
	virtual void VProtocolVersion(const string& protocolVersion) = 0;
	virtual void VSessionInfo(const map<int32, CFSessionInfo>& bankToSession) = 0;
	virtual void VSymbolInfo(const map<int32, set<CFSymbolInfo> >& bankToSymbols) = 0;
	virtual void VAccountInfo(const shared_ptr<map<int32, CFAccountInfo> >& info) = 0;
	virtual void VPositions(const shared_ptr<map<int32, map<string, double> > >& bankToPositions) = 0;
	virtual void VTick(const shared_ptr<CFLevel2>& arg) = 0;
	virtual void VLogout(const set<int32>& ids) = 0;
	virtual void VLogout() = 0;
	virtual ~IFeederHandler(){}
};