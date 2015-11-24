namespace Mql2Fdk
{
    using System;
    using System.Globalization;
    using System.Linq;
    using SoftFX.Extended;

    /// <summary>
    /// 
    /// </summary>
    public partial class MqlAdapter
    {
        #region Trade Functions

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected int HistoryTotal()
        {
            return 0;
        }

        /// <summary>
        /// Returns market and pending orders count.
        /// </summary>
        /// <returns></returns>
        protected int OrdersTotal()
        {
            return this.currentSnapshot.TradeRecords.Count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="select"></param>
        /// <param name="pool"></param>
        /// <returns></returns>
        protected bool OrderSelect(int index, int select, int pool = MODE_TRADES)
        {
            if (pool == MODE_HISTORY)
                throw new NotImplementedException("Currently OrderSelect function is not implemented for MODE_HISTORY pool");

            if (pool != MODE_TRADES)
                throw new ArgumentOutOfRangeException("pool", pool, "Expected values: MODE_TRADES and MODE_HISTORY");


            if (select != SELECT_BY_POS && select != SELECT_BY_TICKET)
                throw new ArgumentOutOfRangeException("select", select, "Expected values: SELECT_BY_POS and SELECT_BY_TICKET");

            var snapshot = this.currentSnapshot;
            if (snapshot == null)
            {
                this.selectedTradeRecord = null;
                return false;
            }

            var records = snapshot.TradeRecords;
            if (records == null)
            {
                this.selectedTradeRecord = null;
                return false;
            }

            if (select == SELECT_BY_POS)
            {
                if (index < 0 || index >= records.Count)
                {
                    this.selectedTradeRecord = null;
                    return false;
                }

                this.selectedTradeRecord = records[index];
                return true;
            }

            var orderId = index.ToString(CultureInfo.InvariantCulture);

            foreach (var element in records)
            {
                if (element.OrderId == orderId)
                {
                    this.selectedTradeRecord = element;
                    return true;
                }
            }

            this.selectedTradeRecord = null;

            return false;
        }

        /// <summary>
        /// Returns type of the selected order by OrderSelect method.
        /// </summary>
        /// <returns>type of the selected order</returns>
        /// <exception cref="System.ArgumentNullException">if order has not been selected</exception>
        protected int OrderType()
        {
            var record = this.GetSelectedTradeRecord();

            if (record.Side == TradeRecordSide.Buy)
            {
                if (record.Type == TradeRecordType.Position)
                    return OP_BUY;
                else if (record.Type == TradeRecordType.Limit)
                    return OP_BUYLIMIT;
                else
                    return OP_BUYSTOP;
            }
            else
            {
                if (record.Type == TradeRecordType.Position)
                    return OP_SELL;
                else if (record.Type == TradeRecordType.Limit)
                    return OP_SELLLIMIT;
                else
                    return OP_SELLSTOP;
            }
        }

        /// <summary>
        /// Returns the net profit value (without swaps or commissions) for the selected order.
        /// </summary>
        /// <returns>profit value</returns>
        protected double OrderProfit()
        {
            var record = this.GetSelectedTradeRecord();
            return record.Profit.Value;
        }

        /// <summary>
        /// Returns symbol of the selected order by OrderSelect method.
        /// </summary>
        /// <returns>symbol of the selected order</returns>
        /// <exception cref="System.ArgumentNullException">if order has not been selected</exception>
        protected string OrderSymbol()
        {
            var record = this.GetSelectedTradeRecord();
            return record.Symbol;
        }

        /// <summary>
        /// Returns ticket number for the selected order.
        /// </summary>
        /// <returns>ticket number of the selected order</returns>
        /// <exception cref="System.ArgumentNullException">if order has not been selected</exception>
        protected int OrderTicket()
        {
            var record = this.GetSelectedTradeRecord();
            return int.Parse(record.OrderId, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns amount of lots for the selected order.
        /// </summary>
        /// <returns>volume of the selected order in lots</returns>
        protected double OrderLots()
        {
            var record = this.GetSelectedTradeRecord();
            var info = this.currentSnapshot.Symbols[record.Symbol];
            return record.Volume / info.RoundLot;
        }

        /// <summary>
        /// Returns the number of closed orders in the account history loaded into the terminal. The history list size depends on the current settings of the "Account history" tab of the terminal. 
        /// </summary>
        /// <returns></returns>
        protected int OrdersHistoryTotal()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns close price for the currently selected order.
        /// Note: The order must be previously selected by the OrderSelect() function. 
        /// </summary>
        /// <returns></returns>
        protected double OrderClosePrice()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns stop loss value for the currently selected order.
        /// </summary>
        /// <returns>stop loss value or zero</returns>
        protected double OrderStopLoss()
        {
            var record = this.GetSelectedTradeRecord();
            var info = this.currentSnapshot.Symbols[record.Symbol];
            return record.StopLoss ?? 0;
        }

        /// <summary>
        /// Returns take profit value for the currently selected order.
        /// </summary>
        /// <returns>take profit value or zero</returns>
        protected double OrderTakeProfit()
        {
            var record = this.GetSelectedTradeRecord();
            var info = this.currentSnapshot.Symbols[record.Symbol];
            return record.TakeProfit ?? 0;
        }

        /// <summary>
        /// Returns open price for the currently selected order.
        /// </summary>
        /// <returns>open price value</returns>
        protected double OrderOpenPrice()
        {
            var record = this.GetSelectedTradeRecord();
            return record.Price;
        }

        /// <summary>
        /// Returns open time for the currently selected order.
        /// </summary>
        /// <returns>open time value</returns>
        protected datetime OrderOpenTime()
        {
            var record = this.GetSelectedTradeRecord();
            var value = record.Created ?? datetime.ReferenceTime;
            return new datetime(value);
        }

        /// <summary>
        /// Currently magic numbers are not supported by server.
        /// </summary>
        /// <returns>always zero</returns>
        protected int OrderMagicNumber()
        {
            return 0;
        }

        /// <summary>
        /// Returns swap value for the currently selected order.
        /// </summary>
        /// <returns></returns>
        protected double OrderSwap()
        {
            var record = this.GetSelectedTradeRecord();
            return record.Swap;
        }

        /// <summary>
        /// Returns calculated commission for the currently selected order.
        /// </summary>
        /// <returns></returns>
        protected double OrderCommission()
        {
            var record = this.GetSelectedTradeRecord();
            return record.Commission;
        }

        /// <summary>
        /// Returns comment for the selected order.
        /// </summary>
        /// <returns></returns>
        protected string OrderComment()
        {
            var record = this.GetSelectedTradeRecord();
            return record.Comment;
        }

        /// <summary>
        /// Returns expiration date for the selected pending order.
        /// </summary>
        /// <returns></returns>
        protected datetime OrderExpiration()
        {
            var record = this.GetSelectedTradeRecord();
            return record.Expiration ?? new datetime();
        }

        TradeRecord GetSelectedTradeRecord()
        {
            var record = this.selectedTradeRecord;
            if (record == null)
                throw new InvalidOperationException("Use OrderSelect method to select order");

            return record;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol">symbol for trading</param>
        /// <param name="cmd">operation type</param>
        /// <param name="volume">number of lots</param>
        /// <param name="price">preferred price of the trade; ignored for market requests</param>
        /// <param name="slippage">ignored</param>
        /// <param name="stoploss">stop loss level.</param>
        /// <param name="takeprofit">take profit level</param>
        /// <param name="comment">ignored</param>
        /// <param name="magic">ignored</param>
        /// <param name="expiration">order expiration time (for pending orders only)</param>
        /// <param name="color">ignored</param>
        /// <returns></returns>
        protected int OrderSend(string symbol, int cmd, double volume, double price, int slippage, double stoploss,
                                double takeprofit, string comment = null, int magic = 0, int expiration = 0,
                                int color = CLR_NONE)
        {
            try
            {
                TradeCommand command;
                TradeRecordSide side;
                ConvertCmdToCommandAndSide(cmd, out command, out side);
                var info = this.currentSnapshot.Symbols[symbol];
                var volumeInLots = volume * info.RoundLot;

                var stopLossPrice = stoploss > 0 ? (double?)stoploss : null;
                var takeProfitPrice = takeprofit > 0 ? (double?)takeprofit : null;
                var record = this.manager.Trader.Server.SendOrder(symbol, command, side, price, volumeInLots, stopLossPrice, takeProfitPrice, null, null);

                this.SafeRefreshSnapshot();

                return int.Parse(record.OrderId, CultureInfo.InvariantCulture);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Deletes previously opened pending order.
        /// </summary>
        /// <param name="ticket">unique number of the order ticket</param>
        /// <param name="Color"></param>
        /// <returns>true, if the corresponded order has been delete, otherwise false</returns>
        protected bool OrderDelete(int ticket, int Color = CLR_NONE)
        {
            try
            {
                var record = this.TradeRecordFromTicket(ticket);
                if (record == null)
                    return false;

                record.Delete();

                this.SafeRefreshSnapshot();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Closes opened order.
        /// </summary>
        /// <param name="ticket">unique number of the order ticket</param>
        /// <param name="lots">a closing volume in lots</param>
        /// <param name="price">ignored</param>
        /// <param name="slippage">ignored</param>
        /// <param name="Color"></param>
        /// <returns></returns>
        protected bool OrderClose(int ticket, double lots, double price, int slippage, int Color = CLR_NONE)
        {
            var record = this.TradeRecordFromTicket(ticket);
            if (record == null)
                return false;

            try
            {
                var info = this.currentSnapshot.Symbols[record.Symbol];
                var volume = info.RoundLot * lots;

                if (this.currentSnapshot.AccountInfo.Type == AccountType.Gross)
                {
                    var result = record.ClosePartially(volume);
                    this.SafeRefreshSnapshot();
                    return result.Sucess;
                }
                else
                {
                    if (!record.IsPosition)
                        return false;

                    var side = TradeRecordSide.Buy == record.Side ? TradeRecordSide.Sell : TradeRecordSide.Buy;

                    this.manager.Trader.Server.SendOrder(record.Symbol, TradeCommand.Market, side, 0, volume, null, null, null, null);
                    this.SafeRefreshSnapshot();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Closes an opened order by another opposite opened order. If the function succeeds, the return value is true.
        /// </summary>
        /// <param name="ticket">unique number of the order ticket</param>
        /// <param name="opposite">unique number of the opposite order ticket.</param>
        /// <param name="Color"></param>
        /// <returns></returns>
        protected bool OrderCloseBy(int ticket, int opposite, int Color = CLR_NONE)
        {
            try
            {
                var firstOrderId = ticket.ToString(CultureInfo.InvariantCulture);
                var secondOrderId = opposite.ToString(CultureInfo.InvariantCulture);
                var result = this.manager.Trader.Server.CloseByPositions(firstOrderId, secondOrderId);

                this.SafeRefreshSnapshot();

                return result;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Modification of characteristics for the previously opened position or pending orders.
        /// </summary>
        /// <param name="ticket">unique number of the order ticket</param>
        /// <param name="price">new open price of the pending order</param>
        /// <param name="stoploss">new StopLoss level</param>
        /// <param name="takeprofit">new TakeProfit level</param>
        /// <param name="expiration"></param>
        /// <param name="arrow_color"></param>
        /// <returns></returns>
        protected bool OrderModify(int ticket, double price, double stoploss, double takeprofit, datetime expiration, int arrow_color = CLR_NONE)
        {
            try
            {
                var record = this.TradeRecordFromTicket(ticket);

                var newActivationPrice = price > 0 ? (double?)price : null;
                var newStopLoss = stoploss > 0 ? (double?)stoploss : null;
                var newTakeProfit = takeprofit > 0 ? (double?)takeprofit : null;
                var newExpiration = expiration.Value > 0 ? (DateTime?)expiration.DateTime : null;
                record.Modify(newActivationPrice, newStopLoss, newTakeProfit, newExpiration);

                this.SafeRefreshSnapshot();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Returns close time for the currently selected order. If order close time is not 0 then the order selected and has been closed and retrieved from the account history. Open and pending orders close time is equal to 0.
        /// Note: The order must be previously selected by the OrderSelect() function. 
        /// </summary>
        /// <returns></returns>
        protected datetime OrderCloseTime()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Prints information about the selected order in the log in the following format:
        /// ticket number; open time; trade operation; amount of lots; open price; Stop Loss; Take Profit; close time; close price; commission; swap; profit; comment; magic number; pending order expiration date.
        /// Order must be selected by the OrderSelect() function. 
        /// </summary>
        protected void OrderPrint()
        {
            throw new NotImplementedException();
        }


        void SafeRefreshSnapshot()
        {
            try
            {
                this.manager.RefreshSnapshot();
                this.currentSnapshot = this.manager.TakeSnapshot(this.symbol, this.priceType, this.periodicity);
            }
            catch
            {
            }
        }

        TradeRecord TradeRecordFromTicket(int ticket)
        {
            if (this.currentSnapshot == null || this.currentSnapshot.TradeRecords == null)
                return null;

            var orderId = ticket.ToString(CultureInfo.InvariantCulture);

            return this.currentSnapshot.TradeRecords.FirstOrDefault(o => o.OrderId == orderId);
        }

        static void ConvertCmdToCommandAndSide(int cmd, out TradeCommand command, out TradeRecordSide side)
        {
            if (cmd == OP_BUY)
            {
                command = TradeCommand.Market;
                side = TradeRecordSide.Buy;
            }
            else if (cmd == OP_SELL)
            {
                command = TradeCommand.Market;
                side = TradeRecordSide.Sell;
            }
            else if (cmd == OP_BUYLIMIT)
            {
                command = TradeCommand.Limit;
                side = TradeRecordSide.Buy;
            }
            else if (cmd == OP_SELLLIMIT)
            {
                command = TradeCommand.Limit;
                side = TradeRecordSide.Sell;
            }
            else if (cmd == OP_BUYSTOP)
            {
                command = TradeCommand.Stop;
                side = TradeRecordSide.Buy;
            }
            else if (cmd == OP_SELLSTOP)
            {
                command = TradeCommand.Stop;
                side = TradeRecordSide.Sell;
            }
            else
                throw new ArgumentOutOfRangeException("cmd", cmd, "Expected: OP_BUY, OP_SELL, OP_BUYLIMIT, OP_SELLLIMIT, OP_BUYSTOP or OP_SELLSTOP");
        }

        #endregion

        #region Positions Methods

        /// <summary>
        /// Gets total buy amount for a symbol.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        protected double PositionBuyAmount(string symbol)
        {
            Position position;
            this.currentSnapshot.Positions.TryGetValue(symbol, out position);
            if (position == null)
                return 0;

            return position.BuyAmount;
        }

        /// <summary>
        /// Gets total sell amount for a symbol.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        protected double PositionSellAmount(string symbol)
        {
            Position position;
            this.currentSnapshot.Positions.TryGetValue(symbol, out position);
            if (position == null)
                return 0;

            return position.SellAmount;
        }

        #endregion
    }
}
