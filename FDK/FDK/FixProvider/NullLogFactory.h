#ifndef __FixProvider_Null_Log_Factory__
#define __FixProvider_Null_Log_Factory__


class NullLogFactory : public FIX::LogFactory
{
public:
	virtual Log* create();
	virtual FIX::Log* create(const FIX::SessionID& id);
	virtual void destroy(FIX::Log* pLog);
};
#endif
