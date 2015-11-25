namespace SoftFX.Extended.Storage.Sequences
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

	/// <summary>
	/// The sequence enumerates quotes for a specified time interval.
	/// If startTime is less than endTime this is enumeration from past to future.
	/// If startTime is more than endTime this is enumeration from future to past.
	/// </summary>
	public class QuotesSingleSequence : IEnumerable<Quote>
	{
		#region Construction

		/// <summary>
		/// Creates a new single quotes sequence.
		/// </summary>
		/// <param name="storage">specifies storage, which will be used for quotes requesting.</param>
		/// <param name="symbol">specifies symbol of quotes enumeration.</param>
		/// <param name="startTime">specifies start time of quotes enumeration.</param>
		/// <param name="endTime">specifies finish time of quotes enumeration.</param>
		/// <param name="depth">specifies required depth of enumerating quotes.</param>
		/// <exception cref="System.ArgumentNullException">If storage or symbol are null.</exception>
		/// <exception cref="System.ArgumentOutOfRangeException">If depth is negative or zero.</exception>
		public QuotesSingleSequence(IStorage storage, string symbol, DateTime startTime, DateTime endTime, int depth)
		{
			if (storage == null)
				throw new ArgumentNullException(nameof(storage), "Storage can not be null.");

			if (symbol == null)
				throw new ArgumentNullException(nameof(symbol), "Symbol can not be null.");

			if (depth <= 0)
				throw new ArgumentOutOfRangeException(nameof(depth), depth, "Expected positive depth value.");

			this.Storage = storage;
			this.Symbol = symbol;
			this.StartTime = startTime;
			this.EndTime = endTime;
			this.Depth = depth;
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

		#endregion

		#region Internal Methods

		internal Quote[] GetQuotes(DateTime from, DateTime to)
		{
			return this.Storage.GetQuotes(this.Symbol, from, to, this.Depth);
		}

		#endregion

		#region IEnumerable Interface Implementation

		/// <summary>
		/// Creates enumerator for the sequence.
		/// </summary>
		/// <returns>a new enumerator instance.</returns>
		public IEnumerator<Quote> GetEnumerator()
		{
			return this.CreateEnumerator();
		}

		/// <summary>
		/// Creates enumerator for the sequence.
		/// </summary>
		/// <returns>a new enumerator instance.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.CreateEnumerator();
		}

		#endregion

		#region Private Methods

		IEnumerator<Quote> CreateEnumerator()
		{
			var result = new QuotesSingleEnumerator(this);
			return result;
		}

		#endregion
	}
}
