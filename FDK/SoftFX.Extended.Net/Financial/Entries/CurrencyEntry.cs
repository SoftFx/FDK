namespace SoftFX.Extended.Financial
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// Represents currency
    /// </summary>
    [DebuggerDisplay("{Name}")]
    public class CurrencyEntry : FinancialEntry
    {
        #region Construction

        public CurrencyEntry(FinancialCalculator owner, string name, int precision, int priority)
            : base(owner)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (precision == 0)
                throw new ArgumentNullException(nameof(precision));

            Name = name;
            Precision = precision;
            SortOrder = priority;
        }

        #endregion

        #region Symbols Properties

        /// <summary>
        /// Gets currency name.
        /// </summary>
        [Category("Parameters")]
        public string Name { get; private set; }

        /// <summary>
        /// Gets currency precision.
        /// </summary>
        [Category("Parameters")]
        [DisplayName("Currency precision")]
        public int Precision { get; private set; }

        /// <summary>
        /// Gets currency precision.
        /// </summary>
        [Category("Parameters")]
        [DisplayName("Currency sort order")]
        public int SortOrder { get; private set; }

        #endregion

        #region Overrides

        /// <summary>
        /// Returns a formatted string to simplify debugging.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var result = string.Format("{0}", this.Name);
            return result;
        }

        #endregion
    }
}
