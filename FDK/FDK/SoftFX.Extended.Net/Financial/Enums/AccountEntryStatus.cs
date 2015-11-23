namespace SoftFX.Extended.Financial
{
    /// <summary>
    /// Possible states of account entry properties.
    /// </summary>
    public enum AccountEntryStatus
    {
        /// <summary>
        /// Property of account entry is not calculated.
        /// </summary>
        NotCalculated,

        /// <summary>
        /// Property of account entry is calculated successfully.
        /// </summary>
        Calculated,

        /// <summary>
        /// Property of account entry is calculated with errors.
        /// </summary>
        CalculatedWithErrors,

        /// <summary>
        /// Property of account entry is not calculated due to unknown account currency.
        /// </summary>
        UnknownAccountCurrency
    }
}
