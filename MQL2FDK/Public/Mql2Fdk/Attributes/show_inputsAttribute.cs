namespace Mql2Fdk.Attributes
{
    using System;

    /// <summary>
    /// MQL's show_input #property
    /// </summary>
    public class show_inputsAttribute : Attribute
    {
         /// <summary>
        /// Fixes signature call for generated C# code
        /// </summary>
        /// <param name="dummy"></param>
        public show_inputsAttribute(int dummy)
        {
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public show_inputsAttribute()
        {
        }
    }
}