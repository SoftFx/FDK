namespace SoftFX.Extended
{
    using System;
    using SoftFX.Extended.Data;

    /// <summary>
    /// Represents market, position or pending order.
    /// </summary>
    public class TradeRecord
    {
        internal TradeRecord()
        {
        }

        internal TradeRecord(DataTrade dataTrade, FxOrder fxOrder)
        {
            this.DataTrade = dataTrade;
            this.OrderId = fxOrder.OrderId;
            this.ClientOrderId = fxOrder.ClientOrderId;
            this.Symbol = fxOrder.Symbol;
            this.Price = fxOrder.Price;
            this.StopLoss = fxOrder.StopLoss;
            this.TakeProfit = fxOrder.TakeProfit;
            this.InitialVolume = fxOrder.InitialVolume;
            this.Volume = fxOrder.Volume ?? 0;
            this.StopPrice = fxOrder.StopPrice;
            this.MaxVisibleVolume = fxOrder.MaxVisibleVolume;
            this.Commission = fxOrder.Commission;
            this.Swap = fxOrder.Swap;
            this.Profit = fxOrder.Profit;
            this.Type = (TradeRecordType)fxOrder.Type;
            this.Side = fxOrder.Side;
            this.Comment = fxOrder.Comment;
            this.Tag = fxOrder.Tag;
            this.Magic = fxOrder.Magic;
            this.IsReducedOpenCommission = fxOrder.IsReducedOpenCommission;
            this.IsReducedCloseCommission = fxOrder.IsReducedCloseCommission;
            this.ImmediateOrCancel = fxOrder.ImmediateOrCancel;
            this.MarketWithSlippage = fxOrder.MarketWithSlippage;
            this.Expiration = fxOrder.Expiration;
            this.Created = fxOrder.Created;
            this.Modified = fxOrder.Modified;
        }

        internal TradeRecord(DataTrade dataTrade)
        {
            this.DataTrade = dataTrade;
        }

        #region Properties

        /// <summary>
        /// Gets related data trade instance.
        /// </summary>
        public DataTrade DataTrade { get; internal set; }

        /// <summary>
        /// Gets unique identifier of the order. Can not be null.
        /// </summary>
        public string OrderId { get; internal set; }

        /// <summary>
        /// Gets unique client identifier of the order. Can not be null.
        /// </summary>
        public string ClientOrderId { get; internal set; }

        /// <summary>
        /// Gets currency pair of the order. Can not be null.
        /// </summary>
        public string Symbol { get; internal set; }

        /// <summary>
        /// Initially requested order size.
        /// </summary>
        public double InitialVolume { get; internal set; }

        /// <summary>
        /// Gets volume of the order.
        /// </summary>
        public double Volume { get; internal set; }

        /// <summary>
        /// Gets max visible volume of the order.
        /// </summary>
        public double? MaxVisibleVolume { get; internal set; }

        /// <summary>
        /// Gets price of the order.
        /// </summary>
        public double? Price { get; internal set; }

        /// <summary>
        /// Gets stop price of the order.
        /// </summary>
        public double? StopPrice { get; internal set; }

        /// <summary>
        /// Gets take profit of the order.
        /// </summary>
        public double? TakeProfit { get; internal set; }

        /// <summary>
        /// Gets stop loss of the order.
        /// </summary>
        public double? StopLoss { get; internal set; }

        /// <summary>
        /// Gets commission of the trade record.
        /// </summary>
        public double Commission { get; internal set; }

        /// <summary>
        /// Gets agents' commission of the trade record.
        /// </summary>
        public double AgentCommission { get; internal set;}

        /// <summary>
        ///
        /// </summary>
        public double Swap { get; internal set; }

        /// <summary>
        /// It's used by FinancialCalculator.
        /// </summary>
        public double? Profit { get; internal set; }

        /// <summary>
        /// Gets type of the order.
        /// </summary>
        public TradeRecordType Type { get; internal set; }

        /// <summary>
        /// Gets side of the order.
        /// </summary>
        public TradeRecordSide Side { get; internal set; }

        /// <summary>
        /// Gets ReducedOpenCommission flag.
        /// </summary>
        public bool IsReducedOpenCommission { get; internal set; }

        /// <summary>
        /// Gets ReducedCloseCommission flag.
        /// </summary>
        public bool IsReducedCloseCommission { get; internal set; }

        /// <summary>
        /// Gets ImmediateOrCancel flag.
        /// </summary>
        public bool ImmediateOrCancel { get; internal set; }

        /// <summary>
        /// Gets MarketWithSlippage flag.
        /// </summary>
        public bool MarketWithSlippage { get; internal set; }

        /// <summary>
        /// Gets IsHidden trade.
        /// </summary>
        public bool IsHidden
        { get { return MaxVisibleVolume.HasValue && MaxVisibleVolume.Value == 0; } }

        /// <summary>
        /// Gets IsIceberg trade.
        /// </summary>
        public bool IsIceberg
        { get { return MaxVisibleVolume.HasValue && MaxVisibleVolume.Value > 0; } }

        /// <summary>
        /// Gets IsHiddenOrIceberg trade.
        /// </summary>
        public bool IsHiddenOrIceberg
        { get { return IsHidden || IsIceberg; } }

        /// <summary>
        /// Gets expiration time of the trade record (if specified by user).
        /// </summary>
        public DateTime? Expiration { get; internal set; }

        /// <summary>
        /// Gets the trade record created time.
        /// </summary>
        public DateTime? Created { get; internal set; }

        /// <summary>
        /// Gets the trade record modified time.
        /// </summary>
        public DateTime? Modified { get; internal set; }

        /// <summary>
        /// Gets comment of the order. Can not be null.
        /// </summary>
        public string Comment { get; internal set; }

        /// <summary>
        /// Gets tag of the order. Can not be null.
        /// </summary>
        public string Tag { get; internal set; }

        /// <summary>
        /// Gets magic number of the order.
        /// </summary>
        public int? Magic { get; internal set; }

        #endregion

        #region Computing properties

        /// <summary>
        /// Returns true, if the trade record is position.
        /// </summary>
        public bool IsPosition
        {
            get
            {
                return this.Type == TradeRecordType.Position;
            }
        }

        /// <summary>
        /// Returns true, if the trade record is stop order.
        /// </summary>
        public bool IsStopOrder
        {
            get
            {
                return this.Type == TradeRecordType.Stop;
            }
        }

        /// <summary>
        /// Returns true, if the trade record is limit order.
        /// </summary>
        public bool IsLimitOrder
        {
            get
            {
                return this.Type == TradeRecordType.Limit;
            }
        }

        /// <summary>
        /// Returns true, if the trade record is stop limit order.
        /// </summary>
        public bool IsStopLimitOrder
        {
            get
            {
                return this.Type == TradeRecordType.StopLimit;
            }
        }

        /// <summary>
        /// Returns true, if the trade record is limit or stop order.
        /// </summary>
        public bool IsPendingOrder
        {
            get
            {
                return this.IsLimitOrder || this.IsStopOrder || this.IsStopLimitOrder;
            }
        }

        #endregion

        #region Methods

        #region Close Position Methods

        /// <summary>
        /// The method closes an existing position.
        /// </summary>
        /// <returns>Can not be null.</returns>
        public ClosePositionResult Close()
        {
            return this.DataTrade.Server.ClosePosition(this.OrderId);
        }

        /// <summary>
        /// The method closes an existing position.
        /// </summary>
        /// <param name="timeoutInMilliseconds">Timeout of the operation ins milliseconds.</param>
        /// <returns>Can not be null.</returns>
        public ClosePositionResult CloseEx(int timeoutInMilliseconds)
        {
            return this.DataTrade.Server.ClosePositionEx(this.OrderId, timeoutInMilliseconds);
        }

        /// <summary>
        /// The method closes an existing position.
        /// </summary>
        /// <param name="operationId">
        /// Can be null, in this case FDK generates a new unique operation ID automatically.
        /// Otherwise, please use GenerateOperationId method of DataClient object.
        /// </param>
        /// <returns>Can not be null.</returns>
        public ClosePositionResult CloseEx(string operationId)
        {
            return this.DataTrade.Server.ClosePositionEx(this.OrderId, operationId);
        }

        /// <summary>
        /// The method closes an existing position.
        /// </summary>
        /// <param name="operationId">
        /// Can be null, in this case FDK generates a new unique operation ID automatically.
        /// Otherwise, please use GenerateOperationId method of DataClient object.
        /// </param>
        /// <param name="timeoutInMilliseconds">Timeout of the operation ins milliseconds.</param>
        /// <returns>Can not be null.</returns>
        public ClosePositionResult CloseEx(string operationId, int timeoutInMilliseconds)
        {
            return this.DataTrade.Server.ClosePositionEx(this.OrderId, operationId, timeoutInMilliseconds);
        }

        #endregion

        #region Close Partially Methods

        /// <summary>
        /// Closes an existing position partially; not valid for pending orders.
        /// </summary>
        /// <param name="volume">Closing volume.</param>
        /// <returns>Can not be null.</returns>
        public ClosePositionResult ClosePartially(double volume)
        {
            return this.DataTrade.Server.ClosePositionPartially(this.OrderId, volume);
        }

        /// <summary>
        /// Closes an existing position partially; not valid for pending orders.
        /// </summary>
        /// <param name="volume">Closing volume.</param>
        /// <param name="timeoutInMilliseconds">Timeout of the operation in milliseconds.</param>
        /// <returns>Can not be null.</returns>
        public ClosePositionResult ClosePartiallyEx(double volume, int timeoutInMilliseconds)
        {
            return this.DataTrade.Server.ClosePositionPartiallyEx(this.OrderId, volume, timeoutInMilliseconds);
        }

        /// <summary>
        /// Closes an existing position partially; not valid for pending orders.
        /// </summary>
        /// <param name="volume">Closing volume.</param>
        /// <param name="operationId">
        /// Can be null, in this case FDK generates a new unique operation ID automatically.
        /// Otherwise, please use GenerateOperationId method of DataClient object.
        /// </param>
        public ClosePositionResult ClosePartiallyEx(double volume, string operationId)
        {
            return this.DataTrade.Server.ClosePositionPartiallyEx(this.OrderId, volume, operationId);
        }

        /// <summary>
        /// Closes an existing position partially; not valid for pending orders.
        /// </summary>
        /// <param name="volume">Closing volume.</param>
        /// <param name="operationId">
        /// Can be null, in this case FDK generates a new unique operation ID automatically.
        /// Otherwise, please use GenerateOperationId method of DataClient object.
        /// </param>
        /// <param name="timeoutInMilliseconds">Timeout of the operation in milliseconds.</param>
        public ClosePositionResult ClosePartiallyEx(double volume, string operationId, int timeoutInMilliseconds)
        {
            return this.DataTrade.Server.ClosePositionPartiallyEx(this.OrderId, volume, operationId, timeoutInMilliseconds);
        }

        #endregion

        #region Close by Methods

        /// <summary>
        /// Closes by two orders.
        /// </summary>
        /// <param name="other">Another order; can not be null.</param>
        /// <returns>True, if the operation has been succeeded; otherwise false.</returns>
        /// <returns>Can not be null.</returns>
        public bool CloseBy(TradeRecord other)
        {
            return this.DataTrade.Server.CloseByPositions(this.OrderId, other.OrderId);
        }

        /// <summary>
        /// Closes by two orders.
        /// </summary>
        /// <param name="other">Another order; can not be null.</param>
        /// <param name="timeoutInMilliseconds">Timeout of the operation in milliseconds.</param>
        /// <returns>True, if the operation has been succeeded; otherwise false.</returns>
        /// <returns>Can not be null.</returns>
        public bool CloseByEx(TradeRecord other, int timeoutInMilliseconds)
        {
            return this.DataTrade.Server.CloseByPositionsEx(this.OrderId, other.OrderId, timeoutInMilliseconds);
        }

        #endregion

        #region Delete Methods

        /// <summary>
        /// Deletes pending order; not valid for market orders.
        /// </summary>
        public void Delete()
        {
            this.DeleteEx(this.DataTrade.SynchOperationTimeout);
        }

        /// <summary>
        /// Deletes pending order; not valid for market orders.
        /// </summary>
        /// <param name="timeoutInMilliseconds">timeout of the operation in milliseconds</param>
        public void DeleteEx(int timeoutInMilliseconds)
        {
            this.DataTrade.Server.DeletePendingOrderEx(this.OrderId, this.Side, timeoutInMilliseconds);
        }

        #endregion

        #region Modify Methods

        /// <summary>
        /// Modifies an existing order.
        /// </summary>
        /// <param name="newPrice">A new pending order price.</param>
        /// <param name="newStopPrice">A new pending order stop price.</param>
        /// <param name="newStopLoss">A new pending order stop loss.</param>
        /// <param name="newTakeProfit">A new pending order take profit.</param>
        /// <param name="newExpirationTime">A new pending order expiration time.</param>
        /// <param name="newComment">A new comment</param>
        /// <param name="newTag">A new comment</param>
        /// <param name="newMagic">A new comment</param>
        /// <returns>A modified trade record.</returns>
        public TradeRecord Modify(double? newPrice, double? newStopPrice, double? newStopLoss, double? newTakeProfit, DateTime? newExpirationTime, string newComment, string newTag, int? newMagic)
        {
            var result = this.ModifyEx(newPrice, newStopPrice, newStopLoss, newTakeProfit, newExpirationTime, newComment, newTag, newMagic, this.DataTrade.SynchOperationTimeout);
            return result;
        }

        /// <summary>
        /// Modifies an existing order.
        /// </summary>
        /// <param name="newPrice">A new pending order price.</param>
        /// <param name="newStopPrice">A new pending order stop price.</param>
        /// <param name="newStopLoss">A new pending order stop loss.</param>
        /// <param name="newTakeProfit">A new pending order take profit.</param>
        /// <param name="newExpirationTime">A new pending order expiration time.</param>
        /// <param name="newComment">A new comment</param>
        /// <param name="newTag">A new comment</param>
        /// <param name="newMagic">A new comment</param>
        /// <param name="timeoutInMilliseconds">Timeout of the operation in milliseconds.</param>
        /// <returns>A modified trade record.</returns>
        public TradeRecord ModifyEx(double? newPrice, double? newStopPrice, double? newStopLoss, double? newTakeProfit, DateTime? newExpirationTime, string newComment, string newTag, int? newMagic, int timeoutInMilliseconds)
        {
            var result = this.DataTrade.Server.ModifyTradeRecordEx(this.OrderId, this.Symbol, this.Type, this.Side, this.Volume, null, newPrice, newStopPrice, newStopLoss, newTakeProfit, newExpirationTime, newComment, newTag, newMagic, timeoutInMilliseconds);
            return result;
        }

        #endregion

        #endregion

        /// <summary>
        /// Returns formatted string for the class instance.
        /// </summary>
        /// <returns>can not be null</returns>
        public override string ToString()
        {
            return string.Format("Id = {0}; {1} {2} {3}; Price = {4}; Volume = {5}; SP = {8}; SL = {6}; TP = {7}", this.OrderId, this.Symbol, this.Type, this.Side, this.Price, this.Volume, this.StopLoss, this.TakeProfit, this.StopPrice);
        }
    }
}
