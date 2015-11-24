namespace Mql2Fdk.Attributes
{
    using System;

    /// <summary>
    /// indicator color 1
    /// </summary>
    public class indicator_color1Attribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">can be null</param>
        public indicator_color1Attribute(color value)
        {
            this.Value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">can be null</param>
        public indicator_color1Attribute(string value)
        {
            var knownToArgb = value.ColorFromString();
            this.Value = knownToArgb;
        }

        /// <summary>
        /// Gets attribute value.
        /// </summary>
        public color Value { get; private set; }
    }
}