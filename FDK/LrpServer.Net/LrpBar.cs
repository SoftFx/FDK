namespace LrpServer.Net
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class LrpBar
    {
        /// <summary>
        /// Start date and time of the bar.
        /// </summary>
        public DateTime From;

        /// <summary>
        /// End date and time of the bar.
        /// </summary>
        public DateTime To;

        /// <summary>
        /// Gets bar open price.
        /// </summary>
        public double Open;

        /// <summary>
        /// Gets bar close price.
        /// </summary>
        public double Close;

        /// <summary>
        /// Gets bar highest price.
        /// </summary>
        public double High;

        /// <summary>
        /// Gets bar lowest price.
        /// </summary>
        public double Low;

        /// <summary>
        /// Gets volume of the bar period.
        /// </summary>
        public double Volume;
    }
}
