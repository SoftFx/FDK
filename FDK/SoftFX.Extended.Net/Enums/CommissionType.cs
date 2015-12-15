namespace SoftFX.Extended
{
    /// <summary>
    /// Commission type.
    /// </summary>
    public enum CommissionType
    {
        /// <summary>
        /// Per unit (implying shares, par, currency, etc)
        /// </summary>
        PerUnit = 0,

        /// <summary>
        /// Percentage
        /// </summary>
        Percent = 1,

        /// <summary>
        /// Absolute (total monetary amount)
        /// </summary>
        Absolute = 2,

        /// <summary>
        /// (For CIV buy orders) percentage waived - cash discount
        /// </summary>
        PercentageWaivedCash = 3,

        /// <summary>
        /// (For CIV buy orders) percentage waived - enhanced units
        /// </summary>
        PercentageWaivedEnhanced = 4,

        /// <summary>
        /// Points per bond or or contract 
        /// </summary>
        PerBond = 5
    }
}
