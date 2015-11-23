namespace SoftFX.Internal
{
    using System;

    public class FixParsingResult
    {
        /// <summary>
        /// true, if parser recognizes an input text and precesses it.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// sending time - receiving time for log line of Feeder; null for FIX message.
        /// </summary>
        public TimeSpan? TimeDiff
        {
            get
            {
                if (!this.IsTimeDeviationCalculated)
                    return null;

                var ticks = TimeDeviation * 10000;
                var result = new TimeSpan(ticks);
                return result;
            }
        }

        internal bool IsTimeDeviationCalculated { get; set; }

        internal long TimeDeviation { get; set; }

        /// <summary>
        /// Session ID of FIX message.
        /// </summary>
        public FixSessionId SessionId { get; set; }

        /// <summary>
        /// List of symbols affected by parsing; It can contain more than one symbol for Logon, Logout messages and Incremental updates.
        /// </summary>
        public string[] Symbols { get; set; }
    }
}
