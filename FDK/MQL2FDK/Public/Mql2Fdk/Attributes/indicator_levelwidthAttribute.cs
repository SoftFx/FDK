namespace Mql2Fdk.Attributes
{
    using System;

    /// <summary>
    /// level line width
    /// </summary>
    public class indicator_levelwidthAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">can be null</param>
        public indicator_levelwidthAttribute(int value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets attribute value.
        /// </summary>
        public int Value { get; private set; }
    }
}