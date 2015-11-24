namespace Mql2Fdk.Attributes
{
    using System;

    /// <summary>
    /// the top scaling limit for a separate indicator window
    /// </summary>
    public class indicator_maximumAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">can be null</param>
        public indicator_maximumAttribute(double value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets attribute value.
        /// </summary>
        public double Value { get; private set; }
    }
}