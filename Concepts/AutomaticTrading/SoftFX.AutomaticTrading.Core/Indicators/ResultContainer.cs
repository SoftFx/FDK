namespace SoftFX.AutomaticTrading.Core.Indicators
{
    using System;

    /// <summary>
    /// Simple result container.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    [Serializable]
    sealed class ResultContainer<TInput, TResult> : IResultContainer<TInput, TResult>
    {
        /// <summary>
        /// Create new result container.
        /// </summary>
        /// <param name="input">Input value.</param>
        /// <param name="result">Result value.</param>
        public ResultContainer(TInput input, TResult result)
        {
            this.Input = input;
            this.Result = result;
        }

        /// <summary>
        /// Gets input value.
        /// </summary>
        public TInput Input { get; private set; }

        /// <summary>
        /// Get result value.
        /// </summary>
        public TResult Result { get; private set; }
    }
}
