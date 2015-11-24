namespace SoftFX.AutomaticTrading.Core.Indicators
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides ability to supply converted input value to any underlying indicator.
    /// </summary>
    /// <typeparam name="TInput">Input type.</typeparam>
    /// <typeparam name="TUnderlyingInput">Underyling indicator input type.</typeparam>
    /// <typeparam name="TResult">Result type.</typeparam>
    public class ConverterIndicator<TInput, TUnderlyingInput, TResult> : IndicatorBase<TInput, TResult>, ICompoundIndicator<TResult>
    {
        readonly IIndicator<TUnderlyingInput, TResult> underlyingIndicator;
        readonly Converter<TInput, TUnderlyingInput> valueFactory;

        /// <summary>
        /// Creates new indicator converter.
        /// </summary>
        /// <param name="underlyingIndicator">Underlying indicator.</param>
        /// <param name="valueFactory">Input value converter.</param>
        public ConverterIndicator(IIndicator<TUnderlyingInput, TResult> underlyingIndicator, Converter<TInput, TUnderlyingInput> valueFactory)
        {
            if (underlyingIndicator == null)
                throw new ArgumentNullException("underlyingIndicator");

            if (valueFactory == null)
                throw new ArgumentNullException("valueFactory");


            this.underlyingIndicator = underlyingIndicator;
            this.valueFactory = valueFactory;
        }

        /// <summary>
        /// Gets whether indicator is ready.
        /// </summary>
        public override bool IsReady
        {
            get { return this.underlyingIndicator.IsReady; }
        }

        /// <summary>
        /// Called when indicator is reset.
        /// </summary>
        protected override void OnReset()
        {
            base.OnReset();
            this.underlyingIndicator.Reset();
        }

        /// <summary>
        /// Called to calculate indicator value.
        /// </summary>
        /// <param name="value">Input value.</param>
        /// <param name="current">Whether latest value should be updated.</param>
        /// <returns></returns>
        protected override TResult OnCalculate(TInput value, bool current)
        {
            if (!current)
                return this.underlyingIndicator.Calculate(this.valueFactory(value));
            else
                return this.underlyingIndicator.CalculateCurrent(this.valueFactory(value));
        }

        /// <summary>
        /// Gets underlying indicator.
        /// </summary>
        IEnumerable<IIndicatorResults<TResult>> ICompoundIndicator<TResult>.ChildIndicators
        {
            get { yield return this.underlyingIndicator; }
        }
    }

    /// <summary>
    /// Provides ability to supply converted input value to any underlying indicator.
    /// </summary>
    /// <typeparam name="TInput">Input type.</typeparam>
    /// <typeparam name="T">Underlying indicator input and result type.</typeparam>
    public class ConverterIndicator<TInput, T> : ConverterIndicator<TInput, T, T>
    {
        /// <summary>
        /// Creates new indicator converter.
        /// </summary>
        /// <param name="underlyingIndicator">Underlying indicator.</param>
        /// <param name="valueFactory">Input value converter.</param>
        public ConverterIndicator(IIndicator<T, T> underlyingIndicator, Converter<TInput, T> valueFactory)
            : base(underlyingIndicator, valueFactory)
        {
        }
    }
}
