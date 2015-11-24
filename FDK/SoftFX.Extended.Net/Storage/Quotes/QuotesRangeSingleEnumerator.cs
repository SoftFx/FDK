namespace SoftFX.Extended.Storage.Sequences
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

	class QuotesRangeSingleEnumerator : IEnumerator<Range<Quote>>
	{
		#region Construction

		public QuotesRangeSingleEnumerator(QuotesRangeSingleSequence sequence)
		{
			if (sequence == null)
				throw new ArgumentNullException("sequence", "Sequence parameter can not be null.");

			this.sequence = sequence;
		}

		#endregion
	
		#region IEnumerator interface implementation

		public Range<Quote> Current
		{
			get
			{
				if (this.iterator == null)
				{
					return null;
				}
				return this.iterator.Current;
			}
		}

		public void Dispose()
		{
		}

		object IEnumerator.Current
		{
			get
			{
				if (this.iterator == null)
				{
					return null;
				}
				return this.iterator.Current;
			}
		}

		public bool MoveNext()
		{
			if (this.iterator == null)
			{
				this.iterator = new QuotesRangeSingleIterator(this.sequence);
			}
			else
			{
				this.iterator.NextTick();
			}
			return this.iterator.Continue;
		}

		public void Reset()
		{
			this.iterator = null;
		}

		#endregion

		#region Members

		readonly QuotesRangeSingleSequence sequence;
		QuotesRangeSingleIterator iterator;

		#endregion
	}
}
