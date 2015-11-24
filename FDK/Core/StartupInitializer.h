#ifndef __Core_Startup_Initializer__
#define __Core_Startup_Initializer__
#pragma warning (push, 4)

class StartupInitializer
{
public:
	template<typename Predicate> StartupInitializer(Predicate handler)
	{
		handler();
	}
};



#pragma warning (pop)
#endif
