namespace SoftFX.Extended.Storage
{
    using System;

	/// <summary>
	/// History information of bars/quotes.
	/// </summary>
	public class HistoryInfo
	{
		/// <summary>
        /// Initializes a new instance of the <see cref="SoftFX.Extended.Storage.HistoryInfo" /> object.
		/// </summary>
		/// <param name="availableFrom"></param>
		/// <param name="availableTo"></param>
		public HistoryInfo(DateTime availableFrom, DateTime availableTo)
		{
			this.AvailableFrom = availableFrom;
			this.AvailableTo = availableTo;
		}

		/// <summary>
		/// The time from which information is available.
		/// </summary>
		public DateTime AvailableFrom { get; private set; }

		/// <summary>
        /// The time to which information is available.
		/// </summary>
		public DateTime AvailableTo { get; private set; }

		/// <summary>
		/// Returns formatted string for the class instance.
		/// </summary>
		/// <returns>Can not be null.</returns>
		public override string ToString()
		{
			var result = string.Format("[{0}, {1}]", this.AvailableFrom, this.AvailableTo);
			return result;
		}
	}
}
