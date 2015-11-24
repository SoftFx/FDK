namespace SoftFX.Extended
{
    using System;

    /// <summary>
    /// Represents available price types.
    /// </summary>
    [Flags]
    public enum PriceType
    {
        /// <summary>
        /// Price type undefined, unknown or ignored.
        /// </summary>
        None = 0,

        /// <summary>
        /// Bid price type.
        /// </summary>
        Bid = 0x1,

        /// <summary>
        /// Ask price type.
        /// </summary>
        Ask = 0x2
    }
}
