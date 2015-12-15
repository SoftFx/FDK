
 
template<typename T FX_TYPES> class Event<T, void (FX_PARAMS)> : public BaseEvent<Delegate<void (FX_PARAMS)> >
{
private:
	void operator()(FX_PARAMS)
	{
		CDelegateInfo info;
		for (void* it = Next(nullptr, info); nullptr != it; it = Next(it, info))
		{
			Delegate<void (FX_PARAMS)>& handler = static_cast<Delegate<void (FX_PARAMS)>& >(info);
			handler(FX_ARGS);
		}
	}
private:
	friend T;
};