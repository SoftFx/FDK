namespace LrpServer.Net
{
    using System;

    public class LrpStatusGroupInfo
    {
        public string StatusGroupId { get; set;  }

        public LrpSessionStatus Status { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime OpenTime { get; set; }

        public DateTime CloseTime { get; set; }
    }
}
