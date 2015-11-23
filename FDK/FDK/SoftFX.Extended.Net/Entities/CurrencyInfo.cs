namespace SoftFX.Extended
{
    /// <summary>
    /// Currency information.
    /// </summary>
    public class CurrencyInfo
    {
        internal CurrencyInfo()
        {
        }

        /// <summary>
        /// Gets currency name.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets currency description.
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// Gets currency priority.
        /// </summary>
        public int SortOrder { get; internal set; }

        /// <summary>
        /// Gets currency precision.
        /// </summary>
        public int Precision { get; internal set; }
    }
}
