namespace SoftFX.AutomaticTrading.Core.Indicators
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Base class for building custom indicators.
    /// </summary>
    /// <typeparam name="TValue">Input type.</typeparam>
    /// <typeparam name="TResult">Result type.</typeparam>
    [Serializable]
    public abstract class IndicatorBase<TValue, TResult> : IIndicator<TValue, TResult>, IDeserializationCallback
    {
        readonly IList<IResultContainer<TValue, TResult>> history;

        /// <summary>
        /// Creates new IndicatorBase.
        /// </summary>
        protected IndicatorBase()
        {
            this.history = new List<IResultContainer<TValue, TResult>>();
        }

        /// <summary>
        /// Gets last calculated value.
        /// </summary>
        public TResult LastValue { get; protected set; }

        /// <summary>
        /// Calculates new value.
        /// </summary>
        /// <param name="value">Source value.</param>
        /// <returns>Result value.</returns>
        public TResult Calculate(TValue value)
        {
            var result = this.OnCalculate(value, current: false);
            this.LastValue = result;
            this.AddResult(value, result);
            return result;
        }

        /// <summary>
        /// Calculates new value.
        /// </summary>
        /// <param name="value">Source value.</param>
        /// <returns>Result value.</returns>
        public TResult CalculateCurrent(TValue value)
        {
            if (this.history.Count == 0)
                throw new InvalidOperationException("Cannot update current value, history is empty.");

            var result = this.OnCalculate(value, current: true);
            this.LastValue = result;
            this.UpdateLastResult(value, result);
            return result;
        }

        /// <summary>
        /// Calculates new value.
        /// </summary>
        /// <param name="value">Source value.</param>
        /// <returns>Result value.</returns>
        object IIndicator.Calculate(object value)
        {
            return this.Calculate((TValue)Convert.ChangeType(value, typeof(TValue)));
        }

        /// <summary>
        /// Calculates new value.
        /// </summary>
        /// <param name="value">Source value.</param>
        /// <returns>Result value.</returns>
        object IIndicator.CalculateCurrent(object value)
        {
            return this.CalculateCurrent((TValue)Convert.ChangeType(value, typeof(TValue)));
        }

        /// <summary>
        /// Called to calculate indicator value.
        /// </summary>
        /// <param name="value">Input value.</param>
        /// <param name="current">Whether latest value should be updated.</param>
        /// <returns></returns>
        protected abstract TResult OnCalculate(TValue value, bool current);

        /// <summary>
        /// Gets indicator results history.
        /// </summary>
        public IEnumerable<IResultContainer<TValue, TResult>> History
        {
            get { return this.history; }
        }

        /// <summary>
        /// Gets indicator result.
        /// </summary>
        /// <param name="index">Result index.</param>
        /// <returns></returns>
        public IResultContainer<TValue, TResult> GetValue(int index)
        {
            if (index >= this.history.Count)
                throw new ArgumentOutOfRangeException("index");

            return this.history[this.history.Count - index - 1];
        }

        /// <summary>
        /// Resets indicator.
        /// </summary>
        public void Reset()
        {
            this.LastValue = default(TResult);
            this.history.Clear();
            this.OnReset();
        }

        /// <summary>
        /// Called when indicator is reset.
        /// </summary>
        protected virtual void OnReset()
        {
        }

        void AddResult(TValue source, TResult result)
        {
            var container = CreateContainer(source, result);
            this.history.Add(container);
        }

        void UpdateLastResult(TValue source, TResult result)
        {
            var container = CreateContainer(source, result);
            this.history[this.history.Count - 1] = container;
        }

        static IResultContainer<TValue, TResult> CreateContainer(TValue source, TResult result)
        {
            return new ResultContainer<TValue, TResult>(source, result);
        }

        /// <summary>
        /// Gets whether indicator is ready.
        /// </summary>
        public virtual bool IsReady
        {
            get { return true; }
        }

        void IDeserializationCallback.OnDeserialization(object sender)
        {
            this.OnLoaded();
        }

        /// <summary>
        /// Called after object has been desirialized.
        /// </summary>
        protected virtual void OnLoaded()
        {
        }
    }

    /// <summary>
    /// Base class for building custom indicators.
    /// </summary>
    /// <typeparam name="T">Type of indicator input and result</typeparam>
    [Serializable]
    public abstract class IndicatorBase<T> : IndicatorBase<T, T>
    {
    }
}
