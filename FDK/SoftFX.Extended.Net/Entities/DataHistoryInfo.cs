namespace SoftFX.Extended
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class DataHistoryInfo
    {
        internal DataHistoryInfo()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime FromAll { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ToAll { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? From { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? To { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastTickId { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string[] Files { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public Bar[] Bars { get; internal set; }
    }
}
