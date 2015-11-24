namespace SoftFX.Extended.Errors
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using SoftFX.Extended.Core;

    /// <summary>
    /// This exception indicates that invalid pointer (not null) has been passed to FDK native library.
    /// It can be, if your code destroying data feed/trade object and use it at the same time.
    /// </summary>
    [Serializable]
    public class InvalidHandleException : RuntimeException
    {
        internal InvalidHandleException(string message)
            : base(HResults.FX_CODE_ERROR_INVALID_HANDLE, message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        protected InvalidHandleException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
