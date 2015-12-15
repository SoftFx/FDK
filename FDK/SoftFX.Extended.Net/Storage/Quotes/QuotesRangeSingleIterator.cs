namespace SoftFX.Extended.Storage.Sequences
{
    using System;

	/// <summary>
    /// Quotes range iterator.
    /// If startTime is less than endTime this is enumeration from past to future.
    /// If startTime is more than endTime this is enumeration from future to past.
	/// </summary>
	public class QuotesRangeSingleIterator
	{
		#region Construction

		/// <summary>
		/// Creates a new iterator instance.
		/// </summary>
		/// <param name="sequence">An existing sequence instance.</param>
		/// <exception cref="System.ArgumentNullException">if sequence is null.</exception>
		public QuotesRangeSingleIterator(QuotesRangeSingleSequence sequence)
		{
			if (sequence == null)
    			throw new ArgumentNullException(nameof(sequence), "Sequence parameter can not be null.");

			this.Sequence = sequence;

            this.iterator = new QuotesSingleIterator(sequence.Storage, sequence.Symbol, sequence.StartTime, sequence.EndTime, sequence.Depth);
            var range = new Range<Quote>(sequence.LowerBound, sequence.UpperBound);

            var index = sequence.LowerBound;

            for (; (index < sequence.UpperBound) && this.iterator.Continue; ++index)
            {
                range[index] = this.iterator.Current;
                this.iterator.NextTick();
            }

            if (index == sequence.UpperBound)
            {
                this.Current = range;
                this.Continue = true;
            }
            else
            {
                this.Continue = false;
            }
		}

		/// <summary>
		/// Creates a new iterator instance.
		/// </summary>
		/// <param name="storage">specifies storage, which will be used for quotes requesting.</param>
		/// <param name="symbol">specifies symbol of quotes enumeration.</param>
		/// <param name="startTime">specifies start time of quotes enumeration.</param>
		/// <param name="endTime">specifies finish time of quotes enumeration.</param>
		/// <param name="depth">specifies required depth of enumerating quotes.</param>
		/// <param name="lowerBound"></param>
		/// <param name="uppperBound"></param>
		/// <exception cref="System.ArgumentNullException">If storage or symbol are null.</exception>
		/// <exception cref="System.ArgumentOutOfRangeException">If depth is negative or zero.</exception>
		public QuotesRangeSingleIterator(IStorage storage, string symbol, DateTime startTime, DateTime endTime, int depth, int lowerBound, int uppperBound)
            : this(new QuotesRangeSingleSequence(storage, symbol, startTime, endTime, depth, lowerBound, uppperBound))
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets used sequence.
		/// </summary>
		public QuotesRangeSingleSequence Sequence { get; private set; }

		/// <summary>
		/// Returns true, it the iterator points to the location before the first element in sequence.
		/// </summary>
		public bool Finish
		{
			get
			{
				return !this.Continue;
			}
		}

		/// <summary>
		/// Returns true, if the iterator points to the location after the last element in sequence.
		/// </summary>
		public bool Continue { get; private set; }

		/// <summary>
		/// Gets the current quote.
		/// </summary>
		public Range<Quote> Current { get; private set; }

		#endregion

		#region Public Methods

		/// <summary>
		/// The method moves the iterator to the next tick.
		/// </summary>
		public void NextTick()
		{
			this.iterator.NextTick();
			if (this.iterator.Continue)
			{
				this.Current.Push(this.iterator.Current);
			}
			else
			{
				this.Current = null;
				this.Continue = false;
			}
		}

		#endregion

		#region Members

		readonly QuotesSingleIterator iterator;

		#endregion
	}
}
