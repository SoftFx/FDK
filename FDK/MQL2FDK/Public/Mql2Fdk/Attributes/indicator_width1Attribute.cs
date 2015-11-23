namespace Mql2Fdk.Attributes
{
    using System;

    /// <summary>
    /// indicator width 1
    /// </summary>
    public class indicator_width1Attribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">can be null</param>
        public indicator_width1Attribute(int value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets attribute value.
        /// </summary>
        public int Value { get; private set; }
    }
}