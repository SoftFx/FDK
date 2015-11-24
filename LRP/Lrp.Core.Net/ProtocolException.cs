namespace SoftFX.Lrp
{
    using System;

    /// <summary>
    /// All Local/Remote Protocol exceptions inherit the class.
    /// </summary>
    public class ProtocolException : Exception
    {
        /// <summary>
        /// Creates a new instance of ProtocolException.
        /// </summary>
        public ProtocolException()
        {
        }

        /// <summary>
        /// Creates a new instance of ProtocolException.
        /// </summary>
        /// <param name="message">the message that describes the error.</param>
        public ProtocolException(string message)
            : base(message)
        {
        }
    }
}
