namespace SoftFX.Extended.Errors
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using SoftFX.Extended.Core;

    /// <summary>
    /// This exception indicates that outgoing request has not been sent.
    /// </summary>
    [Serializable]
    public class SendException : RuntimeException
    {
        internal SendException(string message)
            : base(HResults.FX_CODE_ERROR_SEND, message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        protected SendException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
