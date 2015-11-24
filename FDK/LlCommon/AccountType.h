#pragma once

namespace FDK
{

	/// <summary>
	/// Represents possible accounting types.
	/// </summary>
	__declspec(align(4)) enum AccountType
	{
		AccountType_None = -1,
		/// <summary>
		/// Net accounting is similar to bank accounting.
		/// </summary>
		AccountType_Net = 0,
		/// <summary>
		/// Gross accounting.
		/// </summary>
		AccountType_Gross = 1
	};

	inline std::ostream& operator << (std::ostream& stream, AccountType argument)
	{
		if (AccountType_None == argument)
		{
			stream << "None";
		}
		else if (AccountType_Net == argument)
		{
			stream << "Net";
		}
		else if (AccountType_Gross == argument)
		{
			stream << "Gross";
		}
		else
		{
			stream << static_cast<int>(argument);
		}
		return stream;
	}
}