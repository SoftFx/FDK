// This is always generated file. Do not change anything.

class Simple
{
private:
	ILrpChannel* m_channel;
public:
	inline Simple(ILrpChannel& channel) : m_channel(&channel)
	{
	}
	const static unsigned short LrpComponentId = 0;
	bool IsSupported() const
	{
		return m_channel->IsSupported(0);
	}
	const static unsigned short LrpMethod_Inverse_Id = 0;
	bool Is_Inverse_Supported() const
	{
		return m_channel->IsSupported(0, 0);
	}
	std::string Inverse(const std::string& text)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteAString(text, buffer);

		const HRESULT _status = m_channel->Invoke(0, 0, buffer);
		Throw(_status, buffer);

		auto _result = ReadAString(buffer);
		return _result;
	}
	const static unsigned short LrpMethod_Factorial_Id = 1;
	bool Is_Factorial_Supported() const
	{
		return m_channel->IsSupported(0, 1);
	}
	bool Factorial(const __int32& value, __int32& result)
	{
		MemoryBuffer buffer;
		m_channel->Initialize(buffer);

		WriteInt32(value, buffer);

		const HRESULT _status = m_channel->Invoke(0, 1, buffer);
		Throw(_status, buffer);

		result = ReadInt32(buffer);
		auto _result = ReadBoolean(buffer);
		return _result;
	}
};
