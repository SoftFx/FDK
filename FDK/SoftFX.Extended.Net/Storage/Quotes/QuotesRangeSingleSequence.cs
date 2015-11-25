namespace SoftFX.Extended.Storage.Sequences
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

	/// <summary>
	/// Sequence of quotes ranges.
	/// </summary>
	public class QuotesRangeSingleSequence : IEnumerable<Range<Quote>>
	{
		#region Construction

		/// <summary>
		/// Creates a new quotes ranges sequence.
		/// </summary>
		/// <param name="storage">specifies storage, which will be used for quotes requesting.</param>
		/// <param name="symbol">specifies symbol of quotes enumeration.</param>
		/// <param name="startTime">specifies start time of quotes enumeration.</param>
		/// <param name="endTime">specifies finish time of quotes enumeration.</param>
		/// <param name="depth">specifies required depth of enumerating quotes.</param>
		/// <param name="lowerBound"></param>
		/// <param name="upperBound"></param>
		/// <exception cref="System.ArgumentNullException">If storage or symbol are null.</exception>
		/// <exception cref="System.ArgumentOutOfRangeException">If depth is negative or zero.</exception>
		public QuotesRangeSingleSequence(IStorage storage, string symbol, DateTime startTime, DateTime endTime, int depth, int lowerBound, int upperBound)
		{
			if (storage == null)
				throw new ArgumentNullException(nameof(storage), "Storage can not be null.");

			if (symbol == null)
				throw new ArgumentNullException(nameof(symbol), "Symbol can not be null.");

			if (depth <= 0)
				throw new ArgumentOutOfRangeException(nameof(depth), depth, "Expected positive depth value.");

			if (lowerBound >= upperBound)
			{
				var message = string.Format("Incorrect range bounds: lower bound = {0}, upper bound = {1}", lowerBound, upperBound);
                throw new ArgumentOutOfRangeException(message);
			}

			this.Storage = storage;
			this.Symbol = symbol;
			this.StartTime = startTime;
			this.EndTime = endTime;
			this.Depth = depth;
			this.LowerBound = lowerBound;
			this.UpperBound = upperBound;
		}

		/// <summary>
        /// Creates a new quotes ranges sequence.
		/// </summary>
		/// <param name="storage">specifies storage, which will be used for quotes requesting.</param>
		/// <param name="symbol">specifies symbol of quotes enumeration.</param>
		/// <param name="startTime">specifies start time of quotes enumeration.</param>
		/// <param name="endTime">specifies finish time of quotes enumeration.</param>
		/// <param name="depth">specifies required depth of enumerating quotes.</param>
		/// <param name="size"></param>
		/// <exception cref="System.ArgumentNullException">If storage or symbol are null.</exception>
		/// <exception cref="System.ArgumentOutOfRangeException">If depth is negative or zero.</exception>
		public QuotesRangeSingleSequence(IStorage storage, string symbol, DateTime startTime, DateTime endTime, int depth, int size)
            : this(storage, symbol, startTime, endTime, depth, 0, size)
		{
		}

		#endregion

		#region Parameters Properties

		/// <summary>
		/// Gets used storage.
		/// </summary>
		public IStorage Storage { get; private set; }

		/// <summary>
		/// Gets used symbol.
		/// </summary>
		public string Symbol { get; private set; }

		/// <summary>
		/// Gets used start time.
		/// </summary>
		public DateTime StartTime { get; private set; }

		/// <summary>
		/// Gets used end time.
		/// </summary>
		public DateTime EndTime { get; private set; }

		/// <summary>
		/// Gets used level2 depth.
		/// </summary>
		public int Depth { get; private set; }

		/// <summary>
		/// Gets lower bound of quotes range, which should be enumerated.
		/// </summary>
		public int LowerBound { get; private set; }

		/// <summary>
		/// Gets upper bound of quotes range, which should be enumerated.
		/// </summary>
		public int UpperBound { get; private set; }

		#endregion

		#region IEnumerable Interface Implementation

		/// <summary>
        ///  Retrieves an object that can iterate through the ranges.
		/// </summary>
		/// <returns></returns>
		public IEnumerator<Range<Quote>> GetEnumerator()
		{
			return CreateEnumerator();
		}

		/// <summary>
        /// Retrieves an object that can iterate through the ranges.
		/// </summary>
		/// <returns></returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.CreateEnumerator();
		}

		#endregion

		#region Private methods

		IEnumerator<Range<Quote>> CreateEnumerator()
		{
			var result = new QuotesRangeSingleEnumerator(this);
			return result;
		}

		#endregion
	}
}
