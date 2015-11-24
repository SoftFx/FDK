namespace SoftFX.Extended.Financial
{
    /// <summary>
    /// List of possible margin modes.
    /// </summary>
    public enum MarginMode
    {
        /// <summary>
        /// Calculate margin in dynamic mode
        /// </summary>
        Dynamic = 0,

        /// <summary>
        /// Calculate margin in static mode
        /// </summary>
        Static = 1,

        /// <summary>
        /// Margin rate will be calculated dynamically, if it is not specified directly.
        /// </summary>
        StaticIfPossible = 2
    }
}
