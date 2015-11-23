namespace Mql2Fdk.Attributes
{
    using System;

    /// <summary>
    /// show the indicator in the chart window
    /// </summary>
    public class indicator_chart_windowAttribute : Attribute
    {
        /// <summary>
        /// Fixes signature call for generated C# code
        /// </summary>
        /// <param name="dummy"></param>
        public indicator_chart_windowAttribute(int dummy)
        {
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public indicator_chart_windowAttribute()
        {
        }
    }
}