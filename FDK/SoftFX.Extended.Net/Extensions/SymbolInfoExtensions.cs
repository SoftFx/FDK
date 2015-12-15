namespace SoftFX.Extended.Extensions
{
    using System;
    using SoftFX.Extended.Errors;

    /// <summary>
    /// Extensions for SymbolInfo class.
    /// </summary>
    public static class SymbolInfoExtensions
    {
        /// <summary>
        /// Returns whether swap is enabled for symbol.
        /// </summary>
        /// <param name="symbolInfo"></param>
        /// <returns>True if swap is enabled.</returns>
        public static bool IsSwapEnabled(this SymbolInfo symbolInfo)
        {
            try
            {
                if (symbolInfo == null)
                    throw new ArgumentNullException(nameof(symbolInfo));

                return symbolInfo.SwapSizeShort.HasValue && symbolInfo.SwapSizeLong.HasValue;
            }
            catch (UnsupportedFeatureException ex)
            {
                throw new UnsupportedFeatureException("'IsSwapEnabled()' method is not supported.", ex);
            }
        }

        /// <summary>
        /// Returns margin factor.
        /// </summary>
        /// <param name="symbolInfo"></param>
        /// <returns>Margin factor.</returns>
        public static double GetMarginFactor(this SymbolInfo symbolInfo)
        {
            if (symbolInfo == null)
                throw new ArgumentNullException(nameof(symbolInfo));

            if (symbolInfo.Features.IsMarginFactorFractionalSupported && symbolInfo.MarginFactorFractional.HasValue)
                return symbolInfo.MarginFactorFractional.Value;

            return symbolInfo.MarginFactor / 100D;
        }
    }
}
