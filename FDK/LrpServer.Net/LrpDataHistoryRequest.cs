namespace LrpServer.Net
{
    using System;

    public class LrpDataHistoryRequest
    {
        #region Magic Numbers

        public const int FxPriceType_Bid = 1;
        public const int FxPriceType_Ask = 2;

        public const int FX_REPORT_TYPE_GROUPS = 0;
        public const int FX_REPORT_TYPE_BINARY = 1;
        public const int FX_REPORT_TYPE_FILE = 2;

        public const int FX_GRAPH_TYPE_BARS = 0;
        public const int FX_GRAPH_TYPE_TICKS = 1;
        public const int FX_GRAPH_TYPE_LEVEL2 = 2;

        #endregion

        #region Members

        public string Symbol;
        public DateTime Time;
        public int BarsNumber;
        public int PriceType;
        public string GraphPeriod;
        public int ReportType;
        public int GraphType;

        #endregion
    }
}
