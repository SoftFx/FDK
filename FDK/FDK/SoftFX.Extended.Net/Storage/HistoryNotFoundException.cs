namespace SoftFX.Extended.Errors
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using SoftFX.Extended.Core;

    /// <summary>
    /// This exception wraps related exception from TickTrader libraries.
    /// </summary>
    [Serializable]
    public class HistoryNotFoundException : RuntimeException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public HistoryNotFoundException(string message, Exception innerException)
            : base(HResults.E_FAIL, message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        protected HistoryNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
