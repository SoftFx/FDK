namespace SoftFX.Extended.Storage.Sequences
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Range<T>
    {
        internal Range(int lowerBound, int upperBound)
        {
            this.LowerBound = lowerBound;
            this.UpperBound = upperBound;
            this.offset = 0;
            this.data = new T[this.Count];
        }

        #region Properties

        /// <summary>
        /// Gets index, which points to the first element.
        /// </summary>
        public int LowerBound { get; private set; }

        /// <summary>
        /// Gets index, which points the position after last element.
        /// </summary>
        public int UpperBound { get; private set; }

        /// <summary>
        /// Gets total elements number.
        /// </summary>
        public int Count
        {
            get
            {
                return this.UpperBound - this.LowerBound;
            }
        }

        /// <summary>
        /// Gets the first element.
        /// </summary>
        public T Front
        {
            get
            {
                return this[this.LowerBound];
            }
        }

        /// <summary>
        /// Gets the last element.
        /// </summary>
        public T Back
        {
            get
            {
                return this[this.UpperBound - 1];
            }
        }

        #endregion

        #region Indexer

        /// <summary>
        /// Gets element by index.
        /// </summary>
        /// <param name="index">an index of required element.</param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                if (index < this.LowerBound || index >= this.UpperBound)
                {
                    var message = string.Format("Expected valid range = [{0}, {1}).", this.LowerBound, this.UpperBound);
                    throw new ArgumentOutOfRangeException("index", index, message);
                }

                var position = this.PositionFromIndex(index);
                return this.data[position];
            }
            internal set
            {
                var position = this.PositionFromIndex(index);
                this.data[position] = value;
            }
        }

        int PositionFromIndex(int index)
        {
            var result = index + this.offset - this.LowerBound;
            result %= this.data.Length;
            return result;
        }

        #endregion

        #region Internal Methods

        internal void Push(T value)
        {
            this[this.LowerBound] = value;
            this.offset = (1 + this.offset) % this.data.Length;
        }

        #endregion

        #region Members

        int offset;
        readonly T[] data;

        #endregion
    }
}
