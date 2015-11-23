namespace SoftFX.AutomaticTrading.Core.Indicators
{
    using System.Collections.Generic;


    /// <summary>
    /// Defines methods and properties for compound indicators.
    /// </summary>
    /// <typeparam name="TResult">Result type.</typeparam>
    public interface ICompoundIndicator<out TResult>
    {
        /// <summary>
        /// Gets child indicators.
        /// </summary>
        IEnumerable<IIndicatorResults<TResult>> ChildIndicators { get; }
    }
}
