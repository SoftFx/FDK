namespace SoftFX.Extended.Financial
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Represents financial entry.
    /// </summary>
    public abstract class FinancialEntry
    {
        #region Construction

        /// <summary>
        /// Creates new instance of financial entry.
        /// </summary>
        /// <param name="owner">valid instance of owner</param>
        protected FinancialEntry(object owner)
        {
            if (owner == null)
                throw new ArgumentNullException("owner");

            this.owner = owner;
            this.ArrayIndex = -1;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or owner of the financial entry instance.
        /// </summary>
        [Browsable(false)]
        public object Owner
        {
            get
            {
                return this.owner;
            }
        }

        /// <summary>
        /// Gets or sets user defined data.
        /// </summary>
        [Category("Parameters")]
        public object Tag { get; set; }

        internal int ArrayIndex { get; set; }

        #endregion

        #region Overrides

        /// <summary>
        /// Returns user defined information or internal index.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var tag = this.Tag;
            if (tag == null)
            {
                var result = string.Format("Index = {0}", this.ArrayIndex);
                return result;
            }
            return tag.ToString();
        }

        #endregion

        #region Fields

        readonly object owner;

        #endregion
    }

    /// <summary>
    /// Represents financial entry.
    /// </summary>
    /// <typeparam name="TOwner">Owner type.</typeparam>
    public abstract class FinancialEntry<TOwner> : FinancialEntry
    {
        /// <summary>
        /// Creates new instance of financial entry.
        /// </summary>
        /// <param name="owner">valid instance of owner</param>
        protected FinancialEntry(TOwner owner)
            : base(owner)
        {
        }

        /// <summary>
        /// Gets or owner of the financial entry instance.
        /// </summary>
        [Browsable(false)]
        public new TOwner Owner
        {
            get
            {
                return (TOwner)base.Owner;
            }
        }
    }
}
