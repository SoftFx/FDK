namespace SoftFX.Extended
{
    /// <summary>
    /// 
    /// </summary>
    public class DataTradeCache : DataCache<DataTrade>
    {
        internal DataTradeCache(DataTrade dataTrade)
            : base(dataTrade)
        {
        }

        #region Properties

        /// <summary>
        /// Gets trade records.
        /// </summary>
        public TradeRecord[] TradeRecords
        {
            get
            {
                return this.Client.DataTradeHandle.GetCacheOrders(this.Client);
            }
        }

        /// <summary>
        /// Gets postions; available for Net account only.
        /// </summary>
        public Position[] Positions
        {
            get
            {
                return this.Client.DataTradeHandle.GetCachePositions();
            }
        }

        /// <summary>
        /// Gets account information.
        /// </summary>
        public AccountInfo AccountInfo
        {
            get
            {
                return this.Client.DataTradeHandle.GetCacheAccountInfo();
            }
        }

        #endregion
    }
}
