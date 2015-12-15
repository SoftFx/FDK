// This is always generated file. Do not change anything.

class Simple
{
private:
	CLrpLocalClient* m_channel;
public:
	inline Simple() : m_channel()
	{
	}
	inline Simple(CLrpLocalClient& client) : m_channel(&client)
	{
	}
	bool IsSupported() const
	{
		return m_channel->IsSupported(0);
	}
	const static unsigned short LrpMethod_Constructor_Id = 0;
	bool Is_Constructor_Supported() const
	{
		return m_channel->IsSupported(0, 0);
	}
	void* Constructor()
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);


		const HRESULT _status = m_channel->Invoke(0, 0, buffer);
		Throw(_status, buffer);

		auto _result = ReadLocalPointer(buffer);
		return _result;
	}
	const static unsigned short LrpMethod_Inverse_Id = 1;
	bool Is_Inverse_Supported() const
	{
		return m_channel->IsSupported(0, 1);
	}
	std::string Inverse(const std::string& text)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(text, buffer);

		const HRESULT _status = m_channel->Invoke(0, 1, buffer);
		Throw(_status, buffer);

		auto _result = ReadAString(buffer);
		return _result;
	}
	const static unsigned short LrpMethod_Factorial_Id = 2;
	bool Is_Factorial_Supported() const
	{
		return m_channel->IsSupported(0, 2);
	}
	bool Factorial(const __int32& value, __int32& result)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteInt32(value, buffer);

		const HRESULT _status = m_channel->Invoke(0, 2, buffer);
		Throw(_status, buffer);

		result = ReadInt32(buffer);
		auto _result = ReadBoolean(buffer);
		return _result;
	}
};
