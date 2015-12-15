// do not include the file directly, instead off use Event.h file.


// base class for all handlers

class IBaseHandler
{
public:
	virtual bool Equal(IBaseHandler* pHandler) = 0;
	virtual ~IBaseHandler() {};
};

// zero parameters
class IHandler
{
public:
	virtual void Invoke() = 0;
};
template<typename H> class Handler : public IHandler
{
public:
	Handler(const H& handler) : m_handler(hanlder)
	{
	}
public:
	virtual bool Equal(IBaseHandler* pHandler)
	{
		Handler<H>* handler = dynamic_cast<Handler<H>*>(pHandler);
		bool result = false;
		if (nullptr != handler)
		{
			result = (m_handler == handler->m_handler);
		}
		return result;
	}
	virtual void Invoke()
	{
		m_handler();
	}
private:
	Handler(const Handler&);
	Handler& operator = (const Handler&);
private:
	H m_handler;
};

