namespace Mql2Fdk.Attributes
{
    using System;

    /// <summary>
    /// level line style
    /// </summary>
    public class indicator_levelstyleAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">can be null</param>
        public indicator_levelstyleAttribute(int value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets attribute value.
        /// </summary>
        public int Value { get; private set; }
    }
}