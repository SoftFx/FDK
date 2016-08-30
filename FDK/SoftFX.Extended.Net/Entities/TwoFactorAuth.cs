using System;

namespace SoftFX.Extended
{
    /// <summary>
    /// Two factor auth details.
    /// </summary>
    public class TwoFactorAuth
    {
        /// <summary>
        /// Two factor auth reason.
        /// </summary>
        public TwoFactorReason Reason { get; set; }

        /// <summary>
        /// Two factor auth details.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Two factor auth session expiration time.
        /// </summary>
        public DateTime Expire { get; set; }
    }
}
