#pragma once

namespace FDK
{
	/// <summary>
	/// List of possible margin calculation modes.
	/// </summary>
	__declspec(align(4)) enum MarginCalcMode
    {
        FxMarginCalcMode_Forex = 0,
        FxMarginCalcMode_Cfd = 1,
        FxMarginCalcMode_Futures = 2,
        FxMarginCalcMode_CfdIndex = 3,
        FxMarginCalcMode_CfdLeverage = 4
    };
}