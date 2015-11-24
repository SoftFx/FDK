#pragma once

class CTradeCacheImpl
{
public:
	static CFxAccountInfo GetAccountInfo(void* handle);
	static vector<CFxOrder> GetRecords(void* handle);
	static vector<CFxPositionReport> GetPositions(void* handle);
};