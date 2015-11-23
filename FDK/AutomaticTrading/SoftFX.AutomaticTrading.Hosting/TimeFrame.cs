namespace SoftFX.AutomaticTrading.Hosting
{
    using System;

    public sealed class TimeFrame
    {
        public TimeFrame(DateTime start, DateTime end)
        {
            this.Start = start;
            this.End = end;
        }

        public DateTime Start { get; private set; }

        public DateTime End { get; private set; }
    }
}
