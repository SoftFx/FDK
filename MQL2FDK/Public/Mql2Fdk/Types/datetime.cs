namespace Mql2Fdk
{
    using System;

    /// <summary>
	/// Provides date and time functionality.
	/// </summary>
	public struct datetime
	{
		#region Construction

		/// <summary>
		/// Initializes a new instance of datetime.
		/// </summary>
		/// <param name="value">datetime value</param>
		public datetime(int value)
            : this()
		{
			if (value < 0)
				throw new ArgumentOutOfRangeException("value", value, "datetime value can not be negative");

			this.value = value;
		}

		/// <summary>
		/// Initializes a new instance of datetime from .NET DateTime.
		/// </summary>
		/// <param name="value"></param>
		public datetime(DateTime value)
            : this()
		{
			if (value < ReferenceTime)
				throw new ArgumentOutOfRangeException("value", value, "An input date time shoul be 1970/01/01 or later");

			var interval = value - ReferenceTime;
			this.value = (int)Math.Round(interval.TotalSeconds);
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets the datetime as .NET DateTime.
		/// </summary>
		public DateTime DateTime
		{
			get
			{
				var result = ReferenceTime.AddSeconds(this.value);
				return result;
			}
		}

		/// <summary>
		/// Gets wrapped value.
		/// </summary>
		public int Value
		{
			get
			{
				return this.value;
			}
		}

		/// <summary>
		/// 1970.01.01
		/// </summary>
		public static readonly DateTime ReferenceTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);

		#endregion

		#region Operators

		/// <summary>
		/// The operator creates datetime instance from integer value.
		/// </summary>
		/// <param name="arg">datetime value</param>
		/// <returns></returns>
		public static implicit operator datetime(int arg)
		{
			return new datetime(arg);
		}

        /// <summary>
        /// The operator creates datetime instance from integer value.
        /// </summary>
        /// <param name="arg">datetime value</param>
        /// <returns></returns>
        public static implicit operator datetime(double arg)
        {
            return new datetime((int)arg);
        }

		/// <summary>
		/// The operator creates datetime instance from .NET DateTime value.
		/// </summary>
		/// <param name="arg">datetime value</param>
		/// <returns></returns>
		public static implicit operator datetime(DateTime arg)
		{
			return new datetime(arg);
		}

		/// <summary>
		/// The operator converts a datetime instance to a value.
		/// </summary>
		/// <param name="arg"></param>
		/// <returns></returns>
		public static implicit operator int(datetime arg)
		{
			return arg.value;
		}

        /// <summary>
        /// The operator converts a datetime instance to a value.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static implicit operator double(datetime arg)
        {
            return arg.value;
        }

		/// <summary>
		/// The operator summarizes two datetime instances.
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns></returns>
		public static datetime operator + (datetime first, datetime second)
		{
			return first.value + second.value;
		}

		/// <summary>
		/// The operator subtracts two datetime instances.
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns></returns>
		public static datetime operator -(datetime first, datetime second)
		{
			return first.value - second.value;
		}

		#endregion

		/// <summary>
		/// Returns formatted string for the datatime instance.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return this.DateTime.ToString();
		}

		readonly int value;
	}
}
