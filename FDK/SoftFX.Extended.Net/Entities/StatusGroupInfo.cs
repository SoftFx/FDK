namespace SoftFX.Extended
{
    using System;

    /// <summary>
    /// Represents status group information.
    /// </summary>
    public class StatusGroupInfo
    {
        internal StatusGroupInfo()
        {
        }

        /// <summary>
        /// Status group id.
        /// </summary>
        public string StatusGroupId { get; internal set; }

        /// <summary>
        /// Status group state.
        /// </summary>
        public SessionStatus Status { get; internal set; }

        /// <summary>
        /// Gets start time of the current feed/trade session.
        /// </summary>
        public DateTime StartTime { get; internal set; }

        /// <summary>
        /// Gets the end time of the current feed/trade session.
        /// </summary>
        public DateTime EndTime { get; internal set; }

        /// <summary>
        /// Returns string representation.
        /// </summary>
        public override string ToString()
        {
            return string.Format("StatusGroupId = {0}; Status = {1}; Start = {2}; End = {3};", StatusGroupId, Status, StartTime, EndTime);
        }
    }
}