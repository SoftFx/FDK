namespace Mql2Fdk
{
    /// <summary>
    /// 
    /// </summary>
    public class ConnectionStrings
    {
        /// <summary>
        /// 
        /// </summary>
        protected ConnectionStrings()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="feedConnectionString"></param>
        /// <param name="tradeConnectionString"></param>
        public ConnectionStrings(string feedConnectionString, string tradeConnectionString)
        {
            this.FeedConnectionString = feedConnectionString;
            this.TradeConnectionString = tradeConnectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        public string FeedConnectionString { get; protected set; }

        /// <summary>
        ///
        /// </summary>
        public string TradeConnectionString { get; protected set; }
    }
}
