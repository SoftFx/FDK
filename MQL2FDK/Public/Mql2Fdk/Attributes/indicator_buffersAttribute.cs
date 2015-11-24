namespace Mql2Fdk.Attributes
{
    using System;

    /// <summary>
    /// the number of buffers for calculation, up to 8
    /// </summary>
    public class indicator_buffersAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">can be null</param>
        public indicator_buffersAttribute(int value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets attribute value.
        /// </summary>
        public int Value { get; private set; }
    }
}