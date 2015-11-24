namespace SoftFX.Lrp
{
    /// <summary>
    /// 
    /// </summary>
    public class TimeoutException : ProtocolException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public TimeoutException(string message)
            : base(message)
        {
        }
    }
}
