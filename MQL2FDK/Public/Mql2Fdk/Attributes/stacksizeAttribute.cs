namespace Mql2Fdk.Attributes
{
    using System;

    /// <summary>
    /// stack size
    /// </summary>
    public class stacksizeAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">can be null</param>
        public stacksizeAttribute(int value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets attribute value.
        /// </summary>
        public int Value { get; private set; }
    }
}