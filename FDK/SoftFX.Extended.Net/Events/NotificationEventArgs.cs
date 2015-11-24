namespace SoftFX.Extended.Events
{
    using System;
    using SoftFX.Extended.Data;

    /// <summary>
    /// Notification message.
    /// </summary>
    public class NotificationEventArgs : EventArgs
    {
        internal NotificationEventArgs(Notification notification)
        {
            this.Severity = notification.Severity;
            this.Type = notification.Type;
            this.Text = notification.Text;
        }

        /// <summary>
        /// Gets the notification severity.
        /// </summary>
        public Severity Severity { get; private set; }

        /// <summary>
        /// Gets the notification type.
        /// </summary>
        public NotificationType Type { get; private set; }

        /// <summary>
        /// Gets the notification text.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Returns formatted string for the class instance.
        /// </summary>
        /// <returns>can not be null</returns>
        public override string ToString()
        {
            var result = string.Format("Severity = {0}; Type = {1}; Text = {2}", this.Severity, this.Type, this.Text);
            return result;
        }
    }
    /// <summary>
    /// Notification message with argument.
    /// </summary>
    /// <typeparam name="T">any type.</typeparam>
    public class NotificationEventArgs<T> : NotificationEventArgs
    {
        internal unsafe NotificationEventArgs(Notification notification)
            : base(notification)
        {
        }

        /// <summary>
        /// Gets notification argument.
        /// </summary>
        public T Data { get; internal set; }
    }
}
