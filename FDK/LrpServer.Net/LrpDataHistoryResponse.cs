namespace LrpServer.Net
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class LrpDataHistoryResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime FromAll;

        /// <summary>
        /// 
        /// </summary>
        public DateTime ToAll;

        /// <summary>
        /// 
        /// </summary>
        public DateTime From;

        /// <summary>
        /// 
        /// </summary>
        public DateTime To;

        /// <summary>
        /// 
        /// </summary>
        public string LastTickId;

        /// <summary>
        /// 
        /// </summary>
        public LrpBar[] Bars;

        /// <summary>
        /// 
        /// </summary>
        public string[] Files;
    }
}
