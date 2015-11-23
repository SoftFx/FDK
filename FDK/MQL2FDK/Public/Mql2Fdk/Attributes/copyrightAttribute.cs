namespace Mql2Fdk.Attributes
{
    using System;

    /// <summary>
    /// The company name
    /// </summary>
    public class copyrightAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">can be null</param>
        public copyrightAttribute(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets a copyright attribute value.
        /// </summary>
        public string Value { get; private set; }
    }
}
