namespace SoftFX.AutomaticTrading.Core.Indicators
{
    /// <summary>
    /// Defines properties for result contrainer.
    /// </summary>
    /// <typeparam name="TInput">Input value type.</typeparam>
    /// <typeparam name="TResult">Result value type.</typeparam>
    public interface IResultContainer<out TInput, out TResult>
    {
        /// <summary>
        /// Gets input value.
        /// </summary>
        TInput Input { get; }

        /// <summary>
        /// Get result value.
        /// </summary>
        TResult Result { get; }
    }
}
