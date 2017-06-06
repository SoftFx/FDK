namespace SoftFX.Extended
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public class ExecutionReport
    {
        internal ExecutionReport()
        {
        }

        /// <summary>
        /// Gets OrderID = 37 field.
        /// </summary>
        public string OrderId { get; internal set; }

        /// <summary>
        /// Gets ClOrdID = 11 field.
        /// </summary>
        public string ClientOrderId { get; internal set; }

        /// <summary>
        /// Gets TradeRequestID = 568 field.
        /// </summary>
        public string TradeRequestId { get; internal set; }

        /// <summary>
        /// Gets OrdStatus = 39 field.
        /// </summary>
        public OrderStatus OrderStatus { get; internal set; }

        /// <summary>
        /// Gets ExecType = 150 field.
        /// </summary>
        public ExecutionType ExecutionType { get; internal set; }

        /// <summary>
        /// Gets Symbol = 55 field.
        /// </summary>
        public string Symbol { get; internal set; }

        /// <summary>
        /// Gets CumQty = 14 field.
        /// </summary>
        public double ExecutedVolume { get; internal set; }

        /// <summary>
        /// Gets OrderQty = 38 field.
        /// </summary>
        public double? InitialVolume { get; internal set; }

        /// <summary>
        /// Gets LeavesQty = 151 field.
        /// </summary>
        public double LeavesVolume { get; internal set; }

        /// <summary>
        /// Gets MaxVisibleVolume.
        /// </summary>
        public double? MaxVisibleVolume { get; internal set; }

        /// <summary>
        /// Get LastQty = 32 field.
        /// </summary>
        public double? TradeAmount { get; internal set; }

        /// <summary>
        /// Gets Commission = 12 field.
        /// </summary>
        public double Commission { get; internal set; }

        /// <summary>
        /// Gets AgentCommission = 10113 field.
        /// </summary>
        public double AgentCommission { get;  internal set;}

        /// <summary>
        /// Gets Swap = 10096 field.
        /// </summary>
        public double Swap { get; internal set; }

        /// <summary>
        /// Gets OrdType = 40 field.
        /// </summary>
        public TradeRecordType OrderType { get; internal set; }

        /// <summary>
        /// Gets Side = 54 field.
        /// </summary>
        public TradeRecordSide OrderSide { get; internal set; }

        /// <summary>
        /// Gets AvgPx = 6 field.
        /// </summary>
        public double? AveragePrice { get; internal set; }

        /// <summary>
        /// Gets Price = 44 field.
        /// </summary>
        public double? Price { get; internal set; }

        /// <summary>
        /// Gets StopPx = 99 field.
        /// </summary>
        public double? StopPrice { get; internal set; }

        /// <summary>
        /// Gets LastPx = 31 field.
        /// </summary>
        public double TradePrice { get; internal set; }

        /// <summary>
        /// Gets ExpireTime = 126 field.
        /// </summary>
        public DateTime? Expiration { get; internal set; }

        /// <summary>
        /// Gets OrdCreated = 10083
        /// </summary>
        public DateTime? Created { get; internal set; }

        /// <summary>
        /// Gets OrdModified = 10084
        /// </summary>
        public DateTime? Modified { get; internal set; }

        /// <summary>
        /// Gets OrdRejReason = 103 field.
        /// </summary>
        public RejectReason RejectReason { get; internal set; }

        /// <summary>
        /// Gets TakeProfit = 10037 field.
        /// </summary>
        public double? TakeProfit { get; internal set; }

        /// <summary>
        /// Gets StopLoss = 10038 field.
        /// </summary>
        public double? StopLoss { get; internal set; }

        /// <summary>
        /// Gets Text = 58 field.
        /// </summary>
        public string Text { get; internal set; }

        /// <summary>
        /// Gets user comment, if it is available
        /// </summary>
        public string Comment { get; internal set; }

        /// <summary>
        /// Gets user tag, if it is available
        /// </summary>
        public string Tag { get; internal set; }

        /// <summary>
        /// Gets magic number
        /// </summary>
        public int? Magic { get; internal set; }

        /// <summary>
        /// Gets ReducedOpenCommission flag.
        /// </summary>
        public bool IsReducedOpenCommission { get; set; }

        /// <summary>
        /// Gets ReducedCloseCommission flag.
        /// </summary>
        public bool IsReducedCloseCommission { get; set; }

        /// <summary>
        /// Gets ImmediateOrCancel flag.
        /// </summary>
        public bool ImmediateOrCancel { get; internal set; }

        /// <summary>
        /// Gets MarketWithSlippage flag.
        /// </summary>
        public bool MarketWithSlippage { get; internal set; }

        /// <summary>
        /// Gets ClosePositionRequestId = 10045 field.
        /// </summary>
        public string ClosePositionRequestId { get; internal set; }

        /// <summary>
        /// Gets assets; it is available for cash accounts only.
        /// </summary>
        public AssetInfo[] Assets { get; internal set; }

        /// <summary>
        /// Account balance or Double.Nan.
        /// </summary>
        public double Balance { get; internal set; }
    }
}
