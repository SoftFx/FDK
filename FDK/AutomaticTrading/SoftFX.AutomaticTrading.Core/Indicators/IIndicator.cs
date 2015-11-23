namespace SoftFX.AutomaticTrading.Core.Indicators
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines indicator methods and properties.
    /// </summary>
    public interface IIndicator
    {
        /// <summary>
        /// Gets whether indicator is ready.
        /// </summary>
        bool IsReady { get; }

        /// <summary>
        /// Resets indicator.
        /// </summary>
        void Reset();

        /// <summary>
        /// Calculates new value.
        /// </summary>
        /// <param name="value">Source value.</param>
        /// <returns>Result value.</returns>
        object Calculate(object value);

        /// <summary>
        /// Calculates new value.
        /// </summary>
        /// <param name="value">Source value.</param>
        /// <returns>Result value.</returns>
        object CalculateCurrent(object value);
    }

    /// <summary>
    /// Defines indicator methods and properties.
    /// </summary>
    /// <typeparam name="TInput">Input type.</typeparam>
    /// <typeparam name="TResult">Result type.</typeparam>
    public interface IIndicator<TInput, out TResult> : IIndicator, IIndicatorResults<TInput, TResult>
    {
        /// <summary>
        /// Calculates new value.
        /// </summary>
        /// <param name="value">Source value.</param>
        /// <returns>Result value.</returns>
        TResult Calculate(TInput value);

        /// <summary>
        /// Calculates new value.
        /// </summary>
        /// <param name="value">Source value.</param>
        /// <returns>Result value.</returns>
        TResult CalculateCurrent(TInput value);
    }
}
