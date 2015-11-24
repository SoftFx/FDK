namespace Mql2Fdk.Attributes
{
    using System;

    /// <summary>
    /// A link to the company website
    /// </summary>
    public class linkAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">can be null</param>
        public linkAttribute(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets a link attribute value.
        /// </summary>
        public string Value { get; private set; }
    }
}
