namespace SoftFX.Extended.Storage.Sequences
{
    using System.Collections;
    using System.Collections.Generic;

	class QuotesSingleEnumerator : IEnumerator<Quote>
	{
		#region Construction

		public QuotesSingleEnumerator(QuotesSingleSequence sequence)
		{
			this.sequence = sequence;
		}

		#endregion

		#region IEnumerator interface

		public Quote Current
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
				this.iterator = new QuotesSingleIterator(this.sequence);
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

		readonly QuotesSingleSequence sequence;
		QuotesSingleIterator iterator;

		#endregion
	}
}
