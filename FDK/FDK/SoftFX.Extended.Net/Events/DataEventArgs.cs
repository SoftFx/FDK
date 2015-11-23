namespace SoftFX.Extended.Events
{
    using System;
    using SoftFX.Extended.Core;

    /// <summary>
    /// The class contains common part of all SoftFX event arguments.
    /// </summary>
    public abstract class DataEventArgs : EventArgs
    {
        /// <summary>
        /// Gets UTC server date and time, when the event has been sent by server (if available).
        /// </summary>
        public DateTime? SendingTime { get; private set; }

        /// <summary>
        /// Gets UTC client date and time, when the event has been received by server.
        /// </summary>
        public DateTime ReceivingTime { get; private set; }

        internal DataEventArgs(FxMessage message)
        {
            if (message.SendingTime.HasValue)
            {
                this.SendingTime = message.SendingTime;
            }
            this.ReceivingTime = message.ReceivingTime.Value;
        }

        /// <summary>
        /// Returns formated string for the instance.
        /// </summary>								
        /// <returns>can not be null</returns>
        public override string ToString()
        {
            var stSendingTime = string.Empty;
            if (this.SendingTime != null)
            {
                var sendingTime = (DateTime)this.SendingTime;
                stSendingTime = string.Format("SendingTime = {0};", sendingTime);
            }
            var result = string.Format("{0}ReceivingTime = {1}", stSendingTime, this.ReceivingTime);
            return result;
        }
    }
}
