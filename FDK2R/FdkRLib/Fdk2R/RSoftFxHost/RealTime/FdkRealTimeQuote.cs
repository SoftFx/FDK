using System;

namespace RHost
{
    public class FdkRealTimeQuote
    {
        public double BidPrice { get; set; }

        public double BidVolume { get; set; }

        public double AskPrice { get; set; }

        public double AskVolume { get; set; }

        public DateTime ReceivingTime { get; set; }

        public DateTime? SendingTime { get; set; }
    }
}