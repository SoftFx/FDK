namespace SoftFX.AutomaticTrading.Core.Indicators
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines indicator results methods and properties.
    /// </summary>
    /// <typeparam name="TResult">Result type.</typeparam>
    public interface IIndicatorResults<out TResult>
    {
        /// <summary>
        /// Gets last calculated value.
        /// </summary>
        TResult LastValue { get; }
    }

    /// <summary>
    /// Defines indicator results methods and properties.
    /// </summary>
    /// <typeparam name="TInput">Input type.</typeparam>
    /// <typeparam name="TResult">Result type.</typeparam>
    public interface IIndicatorResults<out TInput, out TResult> : IIndicatorResults<TResult>
    {
        /// <summary>
        /// Gets indicator results history.
        /// </summary>
        IEnumerable<IResultContainer<TInput, TResult>> History { get; }

        /// <summary>
        /// Gets indicator result.
        /// </summary>
        /// <param name="index">Result index.</param>
        /// <returns></returns>
        IResultContainer<TInput, TResult> GetValue(int index);
    }
}
