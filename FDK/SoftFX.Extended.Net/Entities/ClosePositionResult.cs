namespace SoftFX.Extended
{
    /// <summary>
    /// Close position info.
    /// </summary>
    public class ClosePositionResult
    {
        internal ClosePositionResult()
        {
        }

        #region Properties

        /// <summary>
        /// Gets executed volume, if Success is true, otherwise zero.
        /// </summary>
        public double ExecutedVolume { get; internal set; }

        /// <summary>
        /// Gets executed price, if Success is true, otherwise zero.
        /// </summary>
        public double ExecutedPrice { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the position closing is successful.
        /// </summary>
        public bool Sucess { get; internal set; }

        #endregion

        #region Methods

        /// <summary>
        /// Returns formatted string for the class instance.
        /// </summary>
        /// <returns>Can not be null.</returns>
        public override string ToString()
        {
            return string.Format("Success = {0}; Executed volume = {1}; Executed price = {2}", this.Sucess, this.ExecutedVolume, this.ExecutedPrice);
        }

        #endregion
    }
}
