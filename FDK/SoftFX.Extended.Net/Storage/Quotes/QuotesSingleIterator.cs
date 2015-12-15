namespace SoftFX.Extended.Storage.Sequences
{
    using System;
    using TickTrader.Common;

	/// <summary>
    /// Quotes iterator.
	/// If startTime is less than endTime this is enumeration from past to future.
	/// If startTime is more than endTime this is enumeration from future to past.
	/// </summary>
	public class QuotesSingleIterator
	{
		#region Construction

		/// <summary>
		/// Creates a new iterator instance.
		/// </summary>
		/// <param name="sequence">An existing sequence instance.</param>
		/// <exception cref="System.ArgumentNullException">if sequence is null.</exception>
		public QuotesSingleIterator(QuotesSingleSequence sequence)
		{
			if (sequence == null)
				throw new ArgumentNullException(nameof(sequence), "Sequence parameter can not be null.");

			this.Sequence = sequence;

            if (sequence.StartTime <= sequence.EndTime)
            {
                this.from = sequence.StartTime.Normalize();
                this.to = sequence.EndTime.Normalize();
                this.direction = TimeDirection.Forward;
            }
            else
            {
                this.from = sequence.EndTime.Normalize();
                this.to = sequence.StartTime.Normalize();
                this.direction = TimeDirection.Backward;
            }

            this.fromEx = this.from.RoundDownMilliseconds();
            this.toEx = this.to.RoundUpMilliseconds();

            if (this.direction == TimeDirection.Forward)
                this.Current = this.ConstructForward();
            else
                this.Current = this.ConstructBackward();

            this.Continue = this.Current != null;
		}

		/// <summary>
		/// Creates a new iterator instance.
		/// </summary>
		/// <param name="storage">specifies storage, which will be used for quotes requesting.</param>
		/// <param name="symbol">specifies symbol of quotes enumeration.</param>
		/// <param name="startTime">specifies start time of quotes enumeration.</param>
		/// <param name="endTime">specifies finish time of quotes enumeration.</param>
		/// <param name="depth">specifies required depth of enumerating quotes.</param>
		/// <exception cref="System.ArgumentNullException">If storage or symbol are null.</exception>
		/// <exception cref="System.ArgumentOutOfRangeException">If depth is negative or zero.</exception>
		public QuotesSingleIterator(IStorage storage, string symbol, DateTime startTime, DateTime endTime, int depth)
            : this(new QuotesSingleSequence(storage, symbol, startTime, endTime, depth))
		{
		}

		Quote ConstructForward()
		{
			var sequence = this.Sequence;
			this.current = this.fromEx;
			var next = this.current + Interval;

			for (; this.current <= this.toEx;)
			{
				var next2 = next.AddMilliseconds(-1);
				this.buffer = sequence.GetQuotes(this.current, next2);
				for (this.position = 0; this.position < this.buffer.Length; ++this.position)
				{
					var result = this.buffer[this.position];
					if ((result.CreatingTime >= this.from) && (result.CreatingTime <= this.to))
					{
						return result;
					}
				}
				this.current = next;
				next = next + Interval;
			}
			return null;
		}

		Quote ConstructBackward()
		{
			var sequence = this.Sequence;
			this.current = this.toEx - Interval;
			var next = this.toEx;

			for (; next >= this.fromEx; )
			{
				var current2 = this.current.AddMilliseconds(1);
				this.buffer = sequence.GetQuotes(current2, next);
				for (this.position = this.buffer.Length - 1; this.position >= 0; --this.position)
				{
					var result = this.buffer[this.position];
					if ((result.CreatingTime >= this.from) && (result.CreatingTime <= this.to))
					{
						return result;
					}
				}
				next = this.current;
				this.current = this.current - Interval;
			}
			return null;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets used sequence.
		/// </summary>
		public QuotesSingleSequence Sequence { get; private set; }

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
		public Quote Current { get; private set; }

		#endregion

		#region Public Methods

		/// <summary>
		/// The method moves the iterator to the next tick.
		/// </summary>
		public void NextTick()
		{
			if (this.direction  == TimeDirection.Forward)
				this.Increment();
			else
				this.Decrement();
		}

		#endregion

		#region Increment Method

		void Increment()
		{
			if (this.Finish)
				throw new InvalidOperationException("Couldn't increment the iterator, because end of enumeration has been reached.");

			this.Current = this.DoIncrement();
			this.Continue = this.Current != null;
		}

		Quote DoIncrement()
		{
			++this.position;
			Quote result = null;

			if (this.position < this.buffer.Length)
			{
				result = this.buffer[this.position];
				if (result.CreatingTime <= this.to)
				{
					return result;
				}
			}

			this.buffer = EmptyArray;
			this.position = 0;

			var sequence = this.Sequence;
			this.current = this.current + Interval;
			var next = this.current + Interval;

			for (; this.current <= this.toEx;)
			{
				var next2 = next.AddMilliseconds(-1);
				this.buffer = sequence.GetQuotes(this.current, next2);
				if (this.buffer.Length > 0)
				{
					break;
				}
				this.current = next;
				next = next + Interval;
			}
			if (this.buffer.Length == 0)
			{
				return null;
			}

			result = this.buffer[this.position];
			if (result.CreatingTime > this.to)
			{
				result = null;
			}

			return result;
		}

		#endregion

		#region Decrement Method

		void Decrement()
		{
			if (this.Finish)
				throw new InvalidOperationException("Couldn't decrement the iterator, because end of enumeration has been reached.");

			this.Current = this.DoDecrement();
			this.Continue = this.Current != null;
		}

		Quote DoDecrement()
		{
			Quote result = null;
			--this.position;
			if (this.position >= 0)
			{
				result = this.buffer[this.position];
				if (result.CreatingTime >= this.from)
				{
					return result;
				}
			}
			this.buffer = EmptyArray;
			this.position = 0;

			var sequence = this.Sequence;
			var next = this.current;
			this.current = this.current - Interval;

			for (; next >= this.fromEx; )
			{
				var current2 = this.current.AddMilliseconds(1);
				this.buffer = sequence.GetQuotes(current2, next);
				if (this.buffer.Length > 0)
				{
					break;
				}
				next = this.current;
				this.current = this.current - Interval;
			}
            if (this.buffer.Length == 0)
			{
				return null;
			}

			this.position = this.buffer.Length - 1;
			result = this.buffer[this.position];
			if (result.CreatingTime < this.from)
			{
				result = null;
			}
			return result;
		}

		#endregion

		#region Constants

		static readonly TimeSpan Interval = new TimeSpan(0, 1, 0);
		static readonly Quote[] EmptyArray = new Quote[0];

		#endregion

		#region Input Members

		readonly DateTime from;
		readonly DateTime fromEx;    // fromEx <= from, but fromEx.Millisecond = 0
		readonly DateTime to;
		readonly DateTime toEx;      // toEx >= to, but toEx.Millisecond = 0;
		readonly TimeDirection direction;

		#endregion

		#region Members

		Quote[] buffer;
		int position;
		DateTime current;

		#endregion
	}
}
