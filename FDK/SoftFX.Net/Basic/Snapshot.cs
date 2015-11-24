namespace SoftFX.Basic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using SoftFX.Extended;
    using SoftFX.Extended.Events;
    using SoftFX.Extended.Financial;
    using SoftFX.Extended.Storage;

    /// <summary>
    /// Provides atomic access to manager data.
    /// </summary>
    public class Snapshot
    {
        #region Construction

        internal Snapshot(DataTrade trade, DataFeed feed, StateCalculator calculator, object synchronizer, AutoResetEvent syncEvent)
        {
            this.synchronizer = synchronizer;
            this.syncEvent = syncEvent;
            this.calculator = calculator;

            this.Quotes = new Dictionary<string, Quote>();

            trade.Logon += this.OnTradeLogon;
            trade.SessionInfo += this.OnTradeSession;
            trade.AccountInfo += this.OnAccountInfo;
            trade.Logout += this.OnTradeLogout;

            feed.Logon += this.OnFeedLogon;
            feed.Logout += this.OnFeedLogout;
            feed.SymbolInfo += this.OnFeedSymbolInfo;
            feed.SessionInfo += this.OnFeedSessionInfo;

            calculator.StateInfoChanged += this.OnFinancialInfoChanged;
        }

        /// <summary>
        /// The constructor takes snapshot from manager.
        /// </summary>
        /// <param name="snapshot">a snapshot instance</param>
        /// <param name="storage"></param>
        /// <param name="symbol"></param>
        /// <param name="periodicity"></param>
        /// <param name="priceType"></param>
        /// <exception cref="System.ArgumentNullException">if snapshot is null</exception>
        public Snapshot(Snapshot snapshot, DataFeedStorage storage, string symbol, BarPeriod periodicity, PriceType priceType)
        {
            if (snapshot == null)
                throw new ArgumentNullException("snapshot");

            if (storage == null)
                throw new ArgumentNullException("storage");

            this.IsFeedLoggedOn = snapshot.IsFeedLoggedOn;
            this.IsTradeLoggedOn = snapshot.IsTradeLoggedOn;
            this.AccountInfo = snapshot.AccountInfo;
            this.FeedSessionInfo = snapshot.FeedSessionInfo;
            this.TradeSessionInfo = snapshot.TradeSessionInfo;
            this.TradeRecords = snapshot.TradeRecords;
            this.Positions = snapshot.Positions;
            this.Quotes = snapshot.Quotes;
            this.Symbols = snapshot.Symbols;

            this.storage = storage;
            this.symbol = symbol;
            this.periodicity = periodicity;
            this.priceType = priceType;

            this.synchronizer = snapshot.synchronizer;
        }

        #endregion

        #region Feed Events

        void OnFeedLogon(object sender, LogonEventArgs e)
        {
            lock (this.synchronizer)
            {
                this.IsFeedLoggedOn = true;
            }
            this.syncEvent.Set();
        }

        void OnFeedSessionInfo(object sender, SessionInfoEventArgs e)
        {
            lock (this.synchronizer)
            {
                this.FeedSessionInfo = e.Information;
            }
            this.syncEvent.Set();
        }

        void OnFeedSymbolInfo(object sender, SymbolInfoEventArgs e)
        {
            var symbols = new Dictionary<string, SymbolInfo>(e.Information.Length);
            foreach (var element in e.Information)
            {
                symbols[element.Name] = element;
            }

            lock (this.synchronizer)
            {
                this.Symbols = symbols;
            }
            this.syncEvent.Set();
        }

        void OnFeedLogout(object sender, LogoutEventArgs e)
        {
            lock (this.synchronizer)
            {
                this.IsFeedLoggedOn = false;
            }
            this.syncEvent.Set();
        }

        #endregion

        #region Trade Events

        void OnTradeLogon(object sender, LogonEventArgs e)
        {
            lock (this.synchronizer)
            {
                this.IsTradeLoggedOn = true;
            }
            this.syncEvent.Set();
        }

        void OnTradeSession(object sender, SessionInfoEventArgs e)
        {
            lock (this.synchronizer)
            {
                this.TradeSessionInfo = e.Information;
            }
            this.syncEvent.Set();
        }

        void OnAccountInfo(object sender, AccountInfoEventArgs e)
        {
            lock (this.synchronizer)
            {
                this.AccountInfo = e.Information;
            }
            this.syncEvent.Set();
        }

        void OnTradeLogout(object sender, LogoutEventArgs e)
        {
            lock (this.synchronizer)
            {
                this.IsTradeLoggedOn = false;
            }
            this.syncEvent.Set();
        }

        #endregion

        #region Calculator Events

        void OnFinancialInfoChanged(object sender, StateInfoEventArgs e)
        {
            this.Update(e.Information);
        }

        void Update(StateInfo info)
        {
            var accountInfo = this.AccountInfo;
            var positions = new Dictionary<string, Position>();
            var tradeRecords = new List<TradeRecord>();

            if (accountInfo != null)
            {
                var newAccountInfo = new AccountInfo
                {
                    Leverage = accountInfo.Leverage,
                    Currency = accountInfo.Currency,
                    AccountId = accountInfo.AccountId,
                    Type = accountInfo.Type,
                    MarginCallLevel = accountInfo.MarginCallLevel,
                    StopOutLevel = accountInfo.StopOutLevel,

                    Balance = info.Equity,
                    Margin = info.Margin,
                    Equity = info.Equity
                };
                accountInfo = newAccountInfo;


                foreach (var element in info.Positions)
                {
                    positions.Add(element.Symbol, element);

                    var volume = element.BuyAmount - element.SellAmount;
                    if (volume != 0)
                    {
                        var record = new TradeRecord
                        {
                            Symbol = element.Symbol,
                            Volume = Math.Abs(volume),
                            OrderId = this.VirtualOrderIdFromSymbol(element.Symbol),
                            Type = TradeRecordType.Position
                        };
                        if (volume >= 0)
                        {
                            record.Side = TradeRecordSide.Buy;
                            record.Price = element.BuyPrice ?? Math.Abs(element.SettlementPrice);
                        }
                        else
                        {
                            record.Side = TradeRecordSide.Sell;
                            record.Price = element.SellPrice ?? Math.Abs(element.SettlementPrice);
                        }
                        
                        record.Price = Math.Abs(element.SettlementPrice);
                        tradeRecords.Add(record);
                    }
                }
                tradeRecords.AddRange(info.TradeRecords);
            }

            lock (this.synchronizer)
            {
                this.AccountInfo = accountInfo;
                this.TradeRecords = tradeRecords;
                this.Positions = positions;

                foreach (var element in info.Prices)
                {
                    Quote quote;
                    if (!info.Quotes.TryGetValue(element.Key, out quote))
                        continue;

                    this.Quotes[element.Key] = new Quote(element.Key, quote.CreatingTime, element.Value.Bid, element.Value.Ask);
                }
            }

            this.syncEvent.Set();
        }

        string VirtualOrderIdFromSymbol(string symbol)
        {
            string result;

            if (this.symbolToVirtualOrderId.TryGetValue(symbol, out result))
                return result;

            var index = 1 + this.symbolToVirtualOrderId.Count;
            result = index.ToString();
            this.symbolToVirtualOrderId[symbol] = result;

            return result;
        }

        #endregion

        #region Internal Methods

        internal void Refresh()
        {
            this.calculator.Calculate();
            var info = calculator.GetState();
            this.Update(info);
        }

        internal void Stop()
        {
            this.calculator.StateInfoChanged -= this.OnFinancialInfoChanged;
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Retruns true, if feed connection is logged on.
        /// </summary>
        public bool IsFeedLoggedOn { get; private set; }

        /// <summary>
        /// Returns true, if trade connection is logged on.
        /// </summary>
        public bool IsTradeLoggedOn { get; private set; }

        /// <summary>
        /// Returns account information.
        /// </summary>
        public AccountInfo AccountInfo { get; private set; }

        /// <summary>
        /// Returns information about feed session.
        /// </summary>
        public SessionInfo FeedSessionInfo { get; private set; }

        /// <summary>
        /// Returns trade information about trade session.
        /// </summary>
        public SessionInfo TradeSessionInfo { get; private set; }

        /// <summary>
        /// Returns list of trade records.
        /// </summary>
        public List<TradeRecord> TradeRecords { get; private set; }

        /// <summary>
        /// Returns list of opened positions (for NET accounts only)
        /// </summary>
        public Dictionary<string, Position> Positions { get; private set; }

        /// <summary>
        /// Returns list of the latest know quotes.
        /// </summary>
        public Dictionary<string, Quote> Quotes { get; private set; }

        /// <summary>
        /// Returns information about symbols.
        /// </summary>
        public Dictionary<string, SymbolInfo> Symbols { get; private set; }

        /// <summary>
        /// Returns history bars for a specified symbol
        /// </summary>
        /// <param name="symbol">a requested symbol</param>
        /// <param name="period">a requested period</param>
        /// <returns></returns>
        public Bar[] GetBars(string symbol, BarPeriod period)
        {
            var request = string.Format("{0}:{1}", symbol, period);

            Bar[] result;
            this.requestToBars.TryGetValue(request, out result);

            if (result != null)
                return result;

            var priceType = PriceType.Bid;

            if (symbol.EndsWith(AskSuffix))
            {
                symbol = symbol.Substring(0, symbol.Length - AskSuffix.Length);
                priceType = PriceType.Ask;
            }

            if (this.ServerDateTime > DateTime.MinValue)
                result = storage.Online.GetBars(symbol, priceType, period, this.ServerDateTime, -RequestedBarsNumber);
            else
                result = new Bar[0];

            this.requestToBars[request] = result;

            return result;
        }

        /// <summary>
        /// Returns known bars for the current symbol
        /// </summary>
        public Bar[] Bars
        {
            get
            {
                if (this.bars != null)
                    return this.bars;

                var time = this.ServerDateTime;

                if (time > DateTime.MinValue)
                    this.bars = this.storage.Online.GetBars(symbol, priceType, periodicity, time, -RequestedBarsNumber);
                else
                    this.bars = new Bar[0];

                return this.bars;
            }
        }

        /// <summary>
        /// Returns the last known server time.
        /// </summary>
        public DateTime ServerDateTime
        {
            get
            {
                if (!this.serverDateTime.HasValue)
                    this.serverDateTime = this.GetDateTimeFromQuotes();

                return this.serverDateTime.Value;
            }
        }

        DateTime GetDateTimeFromQuotes()
        {
            lock (this.synchronizer)
            {
                var value = this.Quotes
                                .Values
                                .Select(o => o.CreatingTime)
                                .Concat(new[] { DateTime.MinValue })
                                .Max();

                return value;
            }
        }

        #endregion

        #region Manager Members

        readonly object synchronizer;
        readonly AutoResetEvent syncEvent;
        readonly StateCalculator calculator;
        readonly DataFeedStorage storage;

        #endregion

        #region Adviser Members

        string symbol;
        BarPeriod periodicity;
        PriceType priceType;

        #endregion

        #region Members

        DateTime? serverDateTime;
        Bar[] bars;
        readonly Dictionary<string, Bar[]> requestToBars = new Dictionary<string, Bar[]>();
        readonly Dictionary<string, string> symbolToVirtualOrderId = new Dictionary<string, string>();

        #endregion

        #region Constants

        const int RequestedBarsNumber = 1024;
        const string AskSuffix = "_Ask";

        #endregion
    }
}