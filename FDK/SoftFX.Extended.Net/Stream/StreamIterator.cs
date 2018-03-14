namespace SoftFX.Extended
{
    using System;
    using System.Collections.Generic;
    using SoftFX.Extended.Core;
    using SoftFX.Lrp;

    /// <summary>
    /// Contains common part of all streams.
    /// </summary>
    public abstract class StreamIterator<T> : IDisposable
    {
        internal StreamIterator(DataClient dataClient, LPtr handleIterator)
        {
            if (dataClient == null)
                throw new ArgumentNullException(nameof(dataClient), "Data client can not be null.");

            this.dataClient = dataClient;
            this.handleIterator = handleIterator;
        }

        #region Public Methods

        /// <summary>
        /// Returns total items in the iterator (0 if information is not available).
        /// </summary>
        public int TotalItems
        {
            get
            {
                return Native.TradeHistoryIterator.TotalItems(this.handleIterator);
            }
        }

        /// <summary>
        /// Returns true, if the end of associated stream has been reached.
        /// </summary>
        public bool EndOfStream
        {
            get
            {
                return Native.TradeHistoryIterator.EndOfStream(this.handleIterator);
            }
        }

        /// <summary>
        /// Moves the iterator to the next stream element.
        /// </summary>
        public void Next()
        {
            var timeoutInMilliseconds = this.dataClient.SynchOperationTimeout;
            this.NextEx(timeoutInMilliseconds);
        }

        /// <summary>
        /// Moves the iterator to the next stream element.
        /// </summary>
        /// <param name="timeoutInMilliseconds">Timeout of the operation in milliseconds.</param>
        public void NextEx(int timeoutInMilliseconds)
        {
            Native.TradeHistoryIterator.Next(this.handleIterator, (uint)timeoutInMilliseconds);
        }

        /// <summary>
        /// Gets the current stream element.
        /// </summary>
        public T Item
        {
            get
            {
                return this.ItemFromPointer(this.handleIterator);
            }
        }

        /// <summary>
        /// Reads an associated stream to the end and returns all elements as array.
        /// </summary>
        /// <returns>Can not be null.</returns>
        public T[] ToArray()
        {
            var list = new List<T>();
            for (; !this.EndOfStream; this.Next())
            {
                var item = this.Item;
                list.Add(item);
            }

            return list.ToArray();
        }

        /// <summary>
        /// Release all unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Native.Handle.Delete(this.handleIterator);
            this.handleIterator = LPtr.Zero;
        }

        /// <summary>
        /// Release all unmanaged resources.
        /// </summary>
        ~StreamIterator()
        {
            if (!Environment.HasShutdownStarted)
            {
                this.Dispose();
            }
        }

        #endregion

        #region Internal Methods

        internal unsafe abstract T ItemFromPointer(LPtr handle);

        #endregion

        #region Members

        readonly DataClient dataClient;
        LPtr handleIterator;

        #endregion
    }
}
