#ifndef __Native_Id_Generator__
#define __Native_Id_Generator__

class CIdGenerator
{
public:
	CIdGenerator();
public:
	uint64 Next();
	string Next(const string& prefix);
	void Reset();
private:
	uint64 m_id;
	string m_suffix;
	CCriticalSection m_synchronizer;
};
#endif
