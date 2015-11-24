namespace SoftFX.Extended.Errors
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using SoftFX.Extended.Core;

    /// <summary>
    /// This exception indicates that user's request has been rejected by server.
    /// </summary>
    [Serializable]
    public class RejectException : RuntimeException
    {
        internal RejectException(string message)
            : base(HResults.FX_CODE_ERROR_REJECT, message)
        {
        }

        internal RejectException(int code, string message)
            : base(HResults.FX_CODE_ERROR_REJECT, message)
        {
            this.Code = code;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        protected RejectException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.Code = info.GetInt32("Code");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Code", this.Code);
        }

        /// <summary>
        /// Gets business logic code error (if available), otherwise -1.
        /// </summary>
        public int Code { get; internal set; }

        /// <summary>
        /// Returns of reject reason.
        /// </summary>
        public RejectReason Reason
        {
            get
            {
                var result = (RejectReason)this.Code;
                return result;
            }
        }
    }
}
