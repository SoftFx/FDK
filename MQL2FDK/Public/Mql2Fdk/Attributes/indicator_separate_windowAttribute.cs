namespace Mql2Fdk.Attributes
{
    using System;

    /// <summary>
    /// show the indicator in a separate window
    /// </summary>
    public class indicator_separate_windowAttribute : Attribute
    {
        /// <summary>
        /// Fixes signature call for generated C# code
        /// </summary>
        /// <param name="dummy"></param>
        public indicator_separate_windowAttribute(int dummy)
        {
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public indicator_separate_windowAttribute()
        {
        }
    }
}