#pragma once

namespace FDK
{
	/// <summary>
	/// Possible states of account entry properties.
	/// </summary>
	__declspec(align(4)) enum AccountEntryStatus
	{
		/// <summary>
		/// Property of account entry are not calculated.
		/// </summary>
		AccountEntryStatus_NotCalculated = 0,
		/// <summary>
		/// Property of account entry are calculated successfully.
		/// </summary>
		AccountEntryStatus_Calculated = 1,
		/// <summary>
		/// Property of account entry are calculated with errors.
		/// </summary>
		AccountEntryStatus_CalculatedWithErrors = 2,
		/// <summary>
		/// Property of account entry are not calculated due to unknown account currency.
		/// </summary>
		AccountEntryStatus_UnknownAccountCurrency = 3
	};


	inline std::ostream& operator << (std::ostream& stream, AccountEntryStatus argument)
	{
		if (AccountEntryStatus_NotCalculated == argument)
		{
			stream << "NotCalculated";
		}
		else if (AccountEntryStatus_Calculated == argument)
		{
			stream << "Calculated";
		}
		else if (AccountEntryStatus_CalculatedWithErrors == argument)
		{
			stream << "CalculatedWithErrors";
		}
		else if (AccountEntryStatus_UnknownAccountCurrency == argument)
		{
			stream << "UnknownAccountCurrency";
		}
		else
		{
			stream << static_cast<int>(argument);
		}
		return stream;
	}
}