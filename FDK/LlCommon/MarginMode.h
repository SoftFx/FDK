#pragma once

namespace FDK
{
	/// <summary>
	/// List of possible margin modes.
	/// </summary>
	__declspec(align(4)) enum MarginMode
	{
		/// <summary>
		/// Calculate margin in dynamic mode
		/// </summary>
		MarginMode_Dynamic = 0,
		/// <summary>
		/// Calculate margin in static mode
		/// </summary>
		MarginMode_Static = 1,
		/// <summary>
		/// Margin rate will be calculated dynamically, if it is not specified directly.
		/// </summary>
		MarginMode_StaticIfPossible = 2
	};

	inline std::ostream& operator << (std::ostream& stream, MarginMode argument)
	{
		if (MarginMode_Dynamic == argument)
		{
			stream << "Dynamic";
		}
		else if (MarginMode_Static == argument)
		{
			stream << "Static";
		}
		else if (MarginMode_StaticIfPossible == argument)
		{
			stream << "StaticIfPossible";
		}
		else
		{
			stream << static_cast<int>(argument);
		}
		return stream;
	}
}