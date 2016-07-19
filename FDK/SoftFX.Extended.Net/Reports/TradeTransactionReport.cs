namespace SoftFX.Extended.Reports
{
    using System;

    /// <summary>
    /// Trade transaction report
    /// </summary>
    public class TradeTransactionReport
    {
        internal TradeTransactionReport()
        {
        }

        /// <summary>
        ///
        /// </summary>
        public TradeTransactionReportType TradeTransactionReportType { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        public TradeTransactionReason TradeTransactionReason { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        public double AccountBalance { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        public double TransactionAmount { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        public string TransactionCurrency { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        public string ClientId { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        public double Quantity { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        public double LeavesQuantity { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        public double Price { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        public double StopPrice { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        public TradeRecordType TradeRecordType { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        public TradeRecordSide TradeRecordSide { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        public string Symbol { get; internal set; }

        /// <summary>
        /// Gets user-defined comment.
        /// </summary>
        public string Comment { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime OrderCreated { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime OrderModified { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        public string PositionId { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        public string PositionById { get; internal set; }

        /// <summary>
        /// Time of position opening (always indicated in UTC).
        /// </summary>
        public DateTime PositionOpened { get; internal set; }

        /// <summary>
        /// Requested (by client) price at which the position is to be opened
        /// </summary>
        public double PosOpenReqPrice { get; internal set; }

        /// <summary>
        /// Real price at which the position has be opened.
        /// </summary>
        public double PosOpenPrice { get; internal set; }

        /// <summary>
        /// Quantity of a position. Quantity closed on this (last) fill.
        /// </summary>
        public double PositionQuantity { get; internal set; }

        /// <summary>
        /// Quantity of the last fill transaction.
        /// </summary>
        public double PositionLastQuantity { get; internal set; }

        /// <summary>
        /// Quantity of position is still opened for further execution after a transaction.
        /// </summary>
        public double PositionLeavesQuantity { get; internal set; }

        /// <summary>
        /// Requested (by client) price at which the position is to be closed.
        /// </summary>
        public double PositionCloseRequestedPrice { get; internal set; }

        /// <summary>
        /// Real price at which the position has be closed.
        /// </summary>
        public double PositionClosePrice { get; internal set; }

        /// <summary>
        /// Time of position closing (always indicated in UTC).
        /// </summary>
        public DateTime PositionClosed { get; internal set; }

        /// <summary>
        /// Time of position modification (always indicated in UTC).
        /// </summary>
        public DateTime PositionModified { get; internal set; }

        /// <summary>
        /// Position remaining amount side.
        /// </summary>
        public TradeRecordSide PosRemainingSide { get; internal set;}

        /// <summary>
        /// Position remaining amount price.
        /// </summary>
        public double? PosRemainingPrice { get; internal set; }

        /// <summary>
        /// Commission.
        /// </summary>
        public double Commission { get; internal set; }

        /// <summary>
        /// Agent Commission.
        /// </summary>
        public double AgentCommission { get; internal set; }

        /// <summary>
        /// Swap.
        /// </summary>
        public double Swap { get; internal set; }

        /// <summary>
        /// Specifies currency to be used for Commission.
        /// </summary>
        public string CommCurrency { get; internal set; }

        /// <summary>
        /// Price at which the order is to be closed.
        /// </summary>
        public double StopLoss { get; internal set; }

        /// <summary>
        /// Price at which the order is to be closed.
        /// </summary>
        public double TakeProfit { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        public string NextStreamPositionId { get; internal set; }

        /// <summary>
        /// Transaction time.
        /// </summary>
        public DateTime TransactionTime { get; internal set; }

        /// <summary>
        /// Last fill price.
        /// </summary>
        public double? OrderFillPrice { get; internal set; }

        /// <summary>
        /// Last fill amount.
        /// </summary>
        public double? OrderLastFillAmount { get; internal set; }

        /// <summary>
        /// Open conversion rate.
        /// </summary>
        public double? OpenConversionRate { get; internal set; }

        /// <summary>
        /// Close conversion rate.
        /// </summary>
        public double? CloseConversionRate { get; internal set; }

        /// <summary>
        /// Order action number.
        /// </summary>
        public int ActionId { get; internal set; }

        /// <summary>
        /// Gets ExpireTime = 126 field.
        /// </summary>
        public DateTime? Expiration { get; internal set; }
    }
}
