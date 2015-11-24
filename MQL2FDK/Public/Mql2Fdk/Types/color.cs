namespace Mql2Fdk
{
    /// <summary>
    /// Provides color functionality.
    /// </summary>
    public struct color
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of color.
        /// </summary>
        /// <param name="value">datetime value</param>
        public color(int value)
            : this()
        {
            this.value = value;
        }

        /// <summary>
        /// Initializes a new instance of color.
        /// </summary>
        /// <param name="value">datetime value</param>
        public color(double value)
            : this()
        {
            this.value = (int)value;
        }

        #endregion

        #region Operators

        /// <summary>
        /// The operator creates color instance from integer value.
        /// </summary>
        /// <param name="arg">datetime value</param>
        /// <returns></returns>
        public static implicit operator color(int arg)
        {
            return new color(arg);
        }

        /// <summary>
        /// The operator converts color instance to int type.
        /// </summary>
        /// <param name="arg">datetime value</param>
        /// <returns></returns>
        public static implicit operator int(color arg)
        {
            return arg.value;
        }

        /// <summary>
        /// The operator converts color instance to int type.
        /// </summary>
        /// <param name="arg">datetime value</param>
        /// <returns></returns>
        public static implicit operator double(color arg)
        {
            return arg.value;
        }

        #endregion

        /// <summary>
        /// Returns formatted string for the color instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.value.ToString();
        }

        readonly int value;
    }
}
