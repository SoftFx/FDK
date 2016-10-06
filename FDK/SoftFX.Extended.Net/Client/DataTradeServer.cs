namespace SoftFX.Extended
{
    using System;
    using SoftFX.Extended.Reports;

    /// <summary>
    /// The class contains methods, which are executed in server side.
    /// </summary>
    public class DataTradeServer : DataServer<DataTrade>
    {
        internal DataTradeServer(DataTrade dataTrade)
            : base(dataTrade)
        {
        }

        /// <summary>
        /// The method returns the current account information.
        /// </summary>
        /// <returns>Can not be null.</returns>
        public AccountInfo GetAccountInfo()
        {
            return this.GetAccountInfoEx(this.Client.SynchOperationTimeout);
        }

        /// <summary>
        /// The method returns the current account information.
        /// </summary>
        /// <param name="timeoutInMilliseconds">timeout of the synchrnous operation.</param>
        /// <returns>Can not be null.</returns>
        public AccountInfo GetAccountInfoEx(int timeoutInMilliseconds)
        {
            var handle = this.Client.Handle;
            return handle.GetAccountInfo(timeoutInMilliseconds);
        }

        #region SendOrder

        /// <summary>
        /// The method opens a new order.
        /// </summary>
        /// <param name="symbol">Trading currency pair symbol; can not be null.</param>
        /// <param name="command">Market, limit or stop.</param>
        /// <param name="side">Order side: buy or sell.</param>
        /// <param name="price">Activating price for pending orders; price threshold for market orders.</param>
        /// <param name="volume">Requsted volume.</param>
        /// <param name="stopLoss">Stop loss price.</param>
        /// <param name="takeProfit">Take profit price.</param>
        /// <param name="expiration">Expiration time, should be specified for pending orders.</param>
        /// <param name="comment">User defined comment for a new opening order. Null is interpreded as empty string.</param>
        /// <returns>A new order; can not be null.</returns>
        public TradeRecord SendOrder(string symbol, TradeCommand command, TradeRecordSide side, double price, double volume, double? stopLoss, double? takeProfit, DateTime? expiration, string comment)
        {
            return this.SendOrderEx(null, symbol, command, side, price, volume, stopLoss, takeProfit, expiration, comment, this.Client.SynchOperationTimeout);
        }

        /// <summary>
        /// The method opens a new order.
        /// </summary>
        /// <param name="symbol">Trading currency pair symbol; can not be null.</param>
        /// <param name="command">Market, limit or stop.</param>
        /// <param name="side">Trade record side: buy or sell.</param>
        /// <param name="price">Activating price for pending orders; price threshold for market orders.</param>
        /// <param name="volume">Requsted volume.</param>
        /// <param name="stopLoss">Stop loss price.</param>
        /// <param name="takeProfit">Take profit price.</param>
        /// <param name="expiration">Expiration time, should be specified for pending orders.</param>
        /// <param name="comment">User defined comment for a new opening order. Null is interpreded as empty string.</param>
        /// <param name="timeoutInMilliseconds">Timeout of the synchronous operation.</param>
        /// <returns>A new trade record; can not be null.</returns>
        public TradeRecord SendOrderEx(string symbol, TradeCommand command, TradeRecordSide side, double price, double volume, double? stopLoss, double? takeProfit, DateTime? expiration, string comment, int timeoutInMilliseconds)
        {
            return this.SendOrderEx(null, symbol, command, side, price, volume, stopLoss, takeProfit, expiration, comment, timeoutInMilliseconds);
        }

        /// <summary>
        /// The method opens a new order.
        /// </summary>
        /// <param name="operationId">
        /// Can be null, in this case FDK generates a new unique operation ID automatically.
        /// Otherwise, please use GenerateOperationId method of DataClient object.
        /// </param>
        /// <param name="symbol">Trading currency pair symbol; can not be null.</param>
        /// <param name="command">Market, limit or stop.</param>
        /// <param name="side">Trade record side: buy or sell.</param>
        /// <param name="price">Activating price for pending orders; price threshold for market orders.</param>
        /// <param name="volume">Requsted volume.</param>
        /// <param name="stopLoss">Stop loss price.</param>
        /// <param name="takeProfit">Take profit price.</param>
        /// <param name="expiration">Expiration time, should be specified for pending orders.</param>
        /// <param name="comment">User defined comment for a new opening order. Null is interpreded as empty string.</param>
        /// <returns>A new trade record; can not be null.</returns>
        public TradeRecord SendOrderEx(string operationId, string symbol, TradeCommand command, TradeRecordSide side, double price, double volume, double? stopLoss, double? takeProfit, DateTime? expiration, string comment)
        {
            return this.SendOrderEx(operationId, symbol, command, side, price, volume, stopLoss, takeProfit, expiration, comment, this.Client.SynchOperationTimeout);
        }

        /// <summary>
        /// The method opens a new order.
        /// </summary>
        /// <param name="operationId">
        /// Can be null, in this case FDK generates a new unique operation ID automatically.
        /// Otherwise, please use GenerateOperationId method of DataClient object.
        /// </param>
        /// <param name="symbol">Trading currency pair symbol; can not be null.</param>
        /// <param name="command">Market, limit or stop.</param>
        /// <param name="side">Trade record side: buy or sell.</param>
        /// <param name="price">Activating price for pending orders; price threshold for market orders.</param>
        /// <param name="volume">Requsted volume.</param>
        /// <param name="stopLoss">Stop loss price.</param>
        /// <param name="takeProfit">Take profit price.</param>
        /// <param name="expiration">Expiration time, should be specified for pending orders.</param>
        /// <param name="comment">User defined comment for a new opening order. Null is interpreded as empty string.</param>
        /// <param name="timeoutInMilliseconds">Timeout of the synchronous operation.</param>
        /// <returns>A new trade record; can not be null.</returns>
        TradeRecord SendOrderEx(string operationId, string symbol, TradeCommand command, TradeRecordSide side, double price, double volume, double? stopLoss, double? takeProfit, DateTime? expiration, string comment, int timeoutInMilliseconds)
        {
            var order = this.Client.DataTradeHandle.OpenNewOrder(operationId, symbol, command, side, price, volume, stopLoss, takeProfit, expiration, comment ?? string.Empty, timeoutInMilliseconds);
            return new TradeRecord(this.Client, order);
        }

        #endregion

        #region DeletePendingOrder

        /// <summary>
        /// The method deletes an existing pending order.
        /// </summary>
        /// <param name="orderId">An existing pending order ID.</param>
        /// <param name="side">Order side: buy or sell.</param>
        public void DeletePendingOrder(string orderId, TradeRecordSide side)
        {
            this.DeletePendingOrderEx(null, orderId, "<OrderID>", side, this.Client.SynchOperationTimeout);
        }

        /// <summary>
        /// The method deletes an existing pending order.
        /// </summary>
        /// <param name="orderId">An existing pending order ID.</param>
        /// <param name="side">Order side: buy or sell.</param>
        /// <param name="timeoutInMilliseconds">Timeout of the synchronous operation.</param>
        public void DeletePendingOrderEx(string orderId, TradeRecordSide side, int timeoutInMilliseconds)
        {
            this.DeletePendingOrderEx(null, orderId, "<OrderID>", side, timeoutInMilliseconds);
        }

        /// <summary>
        /// The method deletes an existing pending order.
        /// </summary>
        /// <param name="operationId">
        /// Can be null, in this case FDK generates a new unique operation ID automatically.
        /// Otherwise, please use GenerateOperationId method of DataClient object.
        /// </param>
        /// <param name="orderId">An existing pending order ID.</param>
        /// <param name="side">Order side: buy or sell.</param>
        public void DeletePendingOrderEx(string operationId, string orderId, TradeRecordSide side)
        {
            this.DeletePendingOrderEx(null, orderId, "<OrderID>", side, this.Client.SynchOperationTimeout);
        }

        void DeletePendingOrderEx(string operationId, string orderId, string clientId, TradeRecordSide side, int timeoutInMilliseconds)
        {
            this.Client.DataTradeHandle.DeleteOrder(operationId, orderId, clientId, side, timeoutInMilliseconds);
        }

        #endregion

        #region Modify

        /// <summary>
        /// The method modifies an existing trade record.
        /// </summary>
        /// <param name="orderId">An existing pending order ID.</param>
        /// <param name="symbol">Currency pair.</param>
        /// <param name="type">Order type: Limit or Stop.</param>
        /// <param name="side">Order side: buy or sell.</param>
        /// <param name="newActivationPrice">A new activation price.</param>
        /// <param name="volume">A new volume of pending order.</param>
        /// <param name="newStopLoss">A new stop loss price of pending order.</param>
        /// <param name="newTakeProfit">A new take profit price of pending order.</param>
        /// <param name="newExpiration">A new expiration time.</param>
        /// <param name="newComment">A new comment.</param>
        /// <returns>A modified trade record.</returns>
        public TradeRecord ModifyTradeRecord(string orderId, string symbol, TradeRecordType type, TradeRecordSide side,
                                            double volume, double? newActivationPrice = null, double? newStopLoss = null, double? newTakeProfit = null, DateTime? newExpiration = null, string newComment = null)
        {
            return this.ModifyTradeRecordEx(orderId, symbol, type, side, volume, newActivationPrice, newStopLoss, newTakeProfit, newExpiration, newComment, this.Client.SynchOperationTimeout);
        }

        /// <summary>
        /// The method modifies an existing trade record.
        /// </summary>
        /// <param name="orderId">An existing pending order ID.</param>
        /// <param name="symbol">Currency pair.</param>
        /// <param name="type">Order type: Limit or Stop.</param>
        /// <param name="side">Order side: buy or sell.</param>
        /// <param name="newActivationPrice">A new activation price.</param>
        /// <param name="volume">A new volume of pending order.</param>
        /// <param name="newStopLoss">A new stop loss price of pending order.</param>
        /// <param name="newTakeProfit">A new take profit price of pending order.</param>
        /// <param name="newExpiration">A new expiration time.</param>
        /// <param name="newComment">A new comment.</param>
        /// <param name="timeoutInMilliseconds">Timeout of the synchronous operation.</param>
        /// <returns>A modified trade record.</returns>
        public TradeRecord ModifyTradeRecordEx(string orderId, string symbol, TradeRecordType type, TradeRecordSide side,
                                        double volume, double? newActivationPrice, double? newStopLoss, double? newTakeProfit, DateTime? newExpiration, string newComment, int timeoutInMilliseconds)
        {
            return this.ModifyTradeRecordEx(null, orderId, "<OrderID>", symbol, type, side, volume, newActivationPrice, newStopLoss, newTakeProfit, newExpiration, newComment, timeoutInMilliseconds);
        }

        /// <summary>
        /// The method modifies an existing trade record.
        /// </summary>
        /// <param name="operationId">
        /// Can be null, in this case FDK generates a new unique operation ID automatically.
        /// Otherwise, please use GenerateOperationId method of DataClient object.
        /// </param>
        /// <param name="orderId">An existing pending order ID.</param>
        /// <param name="symbol">Currency pair.</param>
        /// <param name="type">Order type: Limit or Stop.</param>
        /// <param name="side">Order side: buy or sell.</param>
        /// <param name="newActivationPrice">A new activation price.</param>
        /// <param name="volume">A new volume of pending order.</param>
        /// <param name="newStopLoss">A new stop loss price of pending order.</param>
        /// <param name="newTakeProfit">A new take profit price of pending order.</param>
        /// <param name="newExpiration">A new expiration time.</param>
        /// <param name="newComment">A new comment.</param>
        /// <returns>A modified trade record.</returns>
        public TradeRecord ModifyTradeRecordEx(string operationId, string orderId, string symbol, TradeRecordType type, TradeRecordSide side,
                                        double volume, double? newActivationPrice = null, double? newStopLoss = null, double? newTakeProfit = null, DateTime? newExpiration = null, string newComment = null)
        {
            return this.ModifyTradeRecordEx(operationId, orderId, "<OrderID>", symbol, type, side, volume, newActivationPrice, newStopLoss, newTakeProfit, newExpiration, newComment, this.Client.SynchOperationTimeout);
        }

        TradeRecord ModifyTradeRecordEx(string operationId, string orderId, string clientId, string symbol, TradeRecordType type, TradeRecordSide side,
                                        double volume, double? newActivationPrice, double? newStopLoss, double? newTakeProfit, DateTime? newExpiration, string newComment, int timeoutInMilliseconds)
        {
            var order = this.Client.DataTradeHandle.ModifyOrder(operationId, orderId, clientId, symbol, type, side, volume, newActivationPrice, newStopLoss, newTakeProfit, newExpiration, newComment, timeoutInMilliseconds);
            return new TradeRecord(this.Client, order);
        }

        #endregion

        #region ClosePosition methods

        /// <summary>
        /// The method closes an existing position.
        /// The method is supported by Gross account only.
        /// </summary>
        /// <param name="orderId">Order ID; can not be null.</param>
        /// <returns>Can not be null.</returns>
        public ClosePositionResult ClosePosition(string orderId)
        {
            return this.ClosePositionEx(orderId, null, this.Client.SynchOperationTimeout);
        }

        /// <summary>
        /// The method closes an existing position.
        /// The method is supported by Gross account only.
        /// </summary>
        /// <param name="orderId">Order ID; can not be null.</param>
        /// <param name="operationId">
        /// Can be null, in this case FDK generates a new unique operation ID automatically.
        /// Otherwise, please use GenerateOperationId method of DataClient object.
        /// </param>
        /// <returns>Can not be null.</returns>
        public ClosePositionResult ClosePositionEx(string orderId, string operationId)
        {
            return this.ClosePositionEx(orderId, operationId, this.Client.SynchOperationTimeout);
        }

        /// <summary>
        /// The method closes an existing position.
        /// The method is supported by Gross account only.
        /// </summary>
        /// <param name="orderId">Order ID; can not be null.</param>
        /// <param name="timeoutInMilliseconds">Timeout of the operation in milliseconds.</param>
        /// <returns>Can not be null.</returns>
        public ClosePositionResult ClosePositionEx(string orderId, int timeoutInMilliseconds)
        {
            return this.Client.DataTradeHandle.CloseOrder(null, orderId, null, timeoutInMilliseconds);
        }

        /// <summary>
        /// The method closes an existing position.
        /// The method is supported by Gross account only.
        /// </summary>
        /// <param name="orderId">Order ID; can not be null.</param>
        /// <param name="operationId">
        /// Can be null, in this case FDK generates a new unique operation ID automatically.
        /// Otherwise, please use GenerateOperationId method of DataClient object.
        /// </param>
        /// <param name="timeoutInMilliseconds">Timeout of the operation in milliseconds.</param>
        /// <returns>Can not be null.</returns>
        public ClosePositionResult ClosePositionEx(string orderId, string operationId, int timeoutInMilliseconds)
        {
            return this.Client.DataTradeHandle.CloseOrder(operationId, orderId, null, timeoutInMilliseconds);
        }

        #endregion

        #region ClosePositionPartially methods

        /// <summary>
        /// The method closes an existing market order.
        /// The method is supported by Gross account only.
        /// </summary>
        /// <param name="orderId">Order ID; can not be null.</param>
        /// <param name="volume">closing volume</param>
        /// <returns></returns>
        public ClosePositionResult ClosePositionPartially(string orderId, double volume)
        {
            return this.ClosePositionPartiallyEx(orderId, volume, null, this.Client.SynchOperationTimeout);
        }

        /// <summary>
        /// The method closes an existing market order.
        /// The method is supported by Gross account only.
        /// </summary>
        /// <param name="orderId">Order ID; can not be null.</param>
        /// <param name="volume">closing volume</param>
        /// <param name="timeoutInMilliseconds">Timeout of the operation in milliseconds.</param>
        /// <returns></returns>
        public ClosePositionResult ClosePositionPartiallyEx(string orderId, double volume, int timeoutInMilliseconds)
        {
            return this.ClosePositionPartiallyEx(orderId, volume, null, timeoutInMilliseconds);
        }

        /// <summary>
        /// The method closes an existing market order.
        /// The method is supported by Gross account only.
        /// </summary>
        /// <param name="orderId">Order ID; can not be null.</param>
        /// <param name="volume">closing volume</param>
        /// <param name="operationId">
        /// Can be null, in this case FDK generates a new unique operation ID automatically.
        /// Otherwise, please use GenerateOperationId method of DataClient object.
        /// </param>
        /// <returns></returns>
        public ClosePositionResult ClosePositionPartiallyEx(string orderId, double volume, string operationId)
        {
            return this.ClosePositionPartiallyEx(orderId, volume, operationId, this.Client.SynchOperationTimeout);
        }

        /// <summary>
        /// The method closes an existing market order.
        /// The method is supported by Gross account only.
        /// </summary>
        /// <param name="orderId">Order ID; can not be null.</param>
        /// <param name="volume">closing volume</param>
        /// <param name="operationId">
        /// Can be null, in this case FDK generates a new unique operation ID automatically.
        /// Otherwise, please use GenerateOperationId method of DataClient object.
        /// </param>
        /// <param name="timeoutInMilliseconds">Timeout of the operation in milliseconds.</param>
        /// <returns></returns>
        public ClosePositionResult ClosePositionPartiallyEx(string orderId, double volume, string operationId, int timeoutInMilliseconds)
        {
            if (volume <= 0)
                throw new ArgumentOutOfRangeException(nameof(volume), "Closing volume should be positive");

            return this.Client.DataTradeHandle.CloseOrder(operationId, orderId, volume, timeoutInMilliseconds);
        }

        #endregion

        /// <summary>
        /// The method closes by two orders.
        /// The method is supported by Gross account only.
        /// </summary>
        /// <param name="firstOrderId">The first order ID; can not be null.</param>
        /// <param name="secondOrderId">The second order ID; can not be null.</param>
        /// <returns>True, if the operation has been succeeded; otherwise false.</returns>
        public bool CloseByPositions(string firstOrderId, string secondOrderId)
        {
            return this.CloseByPositionsEx(firstOrderId, secondOrderId, this.Client.SynchOperationTimeout);
        }

        /// <summary>
        /// The method closes by two orders.
        /// The method is supported by Gross account only.
        /// </summary>
        /// <param name="firstOrderId">The first order ID; can not be null.</param>
        /// <param name="secondOrderId">The second order ID; can not be null.</param>
        /// <param name="timeoutInMilliseconds">Timeout of the operation in milliseconds.</param>
        /// <returns>True, if the operation has been succeeded; otherwise false.</returns>
        public bool CloseByPositionsEx(string firstOrderId, string secondOrderId, int timeoutInMilliseconds)
        {
            return this.Client.DataTradeHandle.CloseByOrders(firstOrderId, secondOrderId, timeoutInMilliseconds);
        }

        /// <summary>
        /// The method closes all opened market orders.
        /// The method is supported by Gross account only.
        /// </summary>
        /// <returns>Number of affected orders.</returns>
        public int CloseAllPositions()
        {
            return this.CloseAllPositionsEx(this.Client.SynchOperationTimeout);
        }

        /// <summary>
        /// The method closes all opened market orders.
        /// The method is supported by Gross account only.
        /// </summary>
        /// <param name="timeoutInMilliseconds">Timeout of the operation in milliseconds.</param>
        /// <returns>Number of affected orders.</returns>
        public int CloseAllPositionsEx(int timeoutInMilliseconds)
        {
            return this.Client.DataTradeHandle.CloseAllOrders(timeoutInMilliseconds);
        }

        /// <summary>
        /// The method returns all trade records for the account.
        /// </summary>
        /// <returns>can not be null</returns>
        public TradeRecord[] GetTradeRecords()
        {
            return this.GetTradeRecordsEx(this.Client.SynchOperationTimeout);
        }

        /// <summary>
        /// The method returns all trade records for the account.
        /// </summary>
        /// <param name="timeoutInMilliseconds">Timeout of the operation in milliseconds</param>
        /// <returns>can not be null</returns>
        public TradeRecord[] GetTradeRecordsEx(int timeoutInMilliseconds)
        {
            var orders = this.Client.DataTradeHandle.GetOrders(this.Client, timeoutInMilliseconds);

            foreach (var order in orders)
            {
                order.DataTrade = this.Client;
            }

            return orders;
        }

        /// <summary>
        /// The method gets snapshot of trade transaction reports and subscribe to notifications.
        /// All reports will be received as events.
        /// </summary>
        /// <param name="direction">Time direction of reports snapshot</param>>
        /// <param name="subscribeToNotifications">Specify false to receive only history snapshot; true to receive history snapshot and updates.</param>
        /// <param name="from">
        /// Optional parameter, which specifies the start date and time for trade transaction reports.
        /// You should specify the parameter, if you specified "to" parameter.
        /// The parameter is supported since 1.6 FIX version.
        /// </param>
        /// <param name="to">
        /// Optional parameter, which specifies the finish date and time for trade transaction reports.
        /// You should specify the parameter, if you specified "from" parameter.
        /// The parameter is supported since 1.6 FIX version.
        /// </param>
        /// <returns>Can not be null.</returns>
        public StreamIterator<TradeTransactionReport> GetTradeTransactionReports(TimeDirection direction, bool subscribeToNotifications, DateTime? from, DateTime? to)
        {
            return this.GetTradeTransactionReports(direction, subscribeToNotifications, from, to, 16);
        }

        /// <summary>
        /// The method gets snapshot of trade transaction reports and subscribe to notifications.
        /// All reports will be received as events.
        /// </summary>
        /// <param name="direction">Time direction of reports snapshot</param>>
        /// <param name="subscribeToNotifications">Specifye false to receive only history snapshot; true to receive history snapshot and updates.</param>
        /// <param name="from">
        /// Optional parameter, which specifies the start date and time for trade transaction reports.
        /// You should specify the parameter, if you specified "to" parameter.
        /// The parameter is supported since 1.6 FIX version.
        /// </param>
        /// <param name="to">
        /// Optional parameter, which specifies the finish date and time for trade transaction reports.
        /// You should specify the parameter, if you specified "from" parameter.
        /// The parameter is supported since 1.6 FIX version.
        /// </param>
        /// <param name="preferedBufferSize"> Specifies number of reports requested at once. Server has itself limitation and if you specify out of range value it will be ignored.</param>
        /// <returns>Can not be null.</returns>
        public StreamIterator<TradeTransactionReport> GetTradeTransactionReports(TimeDirection direction, bool subscribeToNotifications, DateTime? from, DateTime? to, int preferedBufferSize)
        {
            return this.GetTradeTransactionReportsEx(direction, subscribeToNotifications, from, to, preferedBufferSize, this.Client.SynchOperationTimeout);
        }

        /// <summary>
        /// The method gets snapshot of trade transaction reports and subscribe to notifications.
        /// All reports will be received as events.
        /// </summary>
        /// <param name="direction">Time direction of reports snapshot</param>
        /// <param name="subscribeToNotifications">Specifye false to receive only history snapshot; true to receive history snapshot and updates.</param>
        /// <param name="from">
        /// Optional parameter, which specifies the start date and time for trade transaction reports.
        /// You should specify the parameter, if you specified "to" parameter.
        /// The parameter is supported since 1.6 FIX version.
        /// </param>
        /// <param name="to">
        /// Optional parameter, which specifies the finish date and time for trade transaction reports.
        /// You should specify the parameter, if you specified "from" parameter.
        /// The parameter is supported since 1.6 FIX version.
        /// </param>
        /// <param name="preferedBufferSize"> Specifies number of reports requested at once. Server has itself limitation and if you specify out of range value it will be ignored.</param>
        /// <param name="timeoutInMilliseconds">Timeout of the operation in milliseconds</param>
        /// <returns>Can not be null.</returns>
        public unsafe StreamIterator<TradeTransactionReport> GetTradeTransactionReportsEx(TimeDirection direction, bool subscribeToNotifications, DateTime? from, DateTime? to, int preferedBufferSize, int timeoutInMilliseconds)
        {
            var data = this.Client.DataTradeHandle.GetTradeTransactionReportsAndSubscribeToNotifications(direction, subscribeToNotifications, from, to, preferedBufferSize, timeoutInMilliseconds);
            return new TradeTransactionReportsIterator(this.Client, data);
        }

        /// <summary>
        /// The method stops trade transaction reports receiving.
        /// </summary>
        public void UnsubscribeTradeTransactionReports()
        {
            this.UnsubscribeTradeTransactionReportsEx(this.Client.SynchOperationTimeout);
        }

        /// <summary>
        /// The method stops trade transaction reports receiving.
        /// </summary>
        /// <param name="timeoutInMilliseconds">Timeout of the operation in milliseconds</param>
        public void UnsubscribeTradeTransactionReportsEx(int timeoutInMilliseconds)
        {
            this.Client.DataTradeHandle.UnsubscribeTradeTransactionReports(timeoutInMilliseconds);
        }
    }
}
