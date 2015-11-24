namespace SoftFX.Extended.Financial
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// Represents financial state of account.
    /// </summary>
    [DebuggerDisplay("#{Generation}: Balance = {Balance}; Profit = {Profit}; Margin = {Margin}")]
    public sealed class StateInfo
    {
        #region Construction

        internal StateInfo(AccountEntry account, IDictionary<string, Asset> assets, IDictionary<string, PriceEntry> prices, IDictionary<string, Quote> quotes, SymbolEntries symbols, long generation)
        {
            this.Status = account.MarginLevelStatus;
            this.Generation = generation;

            this.Balance = account.Balance;
            this.Profit = account.Profit.GetValueOrDefault();
            this.Margin = account.Margin.GetValueOrDefault();
            this.Equity = account.Equity.GetValueOrDefault();
            this.MarginLevel = account.MarginLevel.GetValueOrDefault();
            this.Commission = account.Commission;
            this.Swap = account.Swap;
            this.AgentCommission = account.AgentCommission;

            this.Prices = new Dictionary<string, PriceEntry>(prices);
            this.UnknownSymbols = new SortedSet<string>(account.Trades.Where(o => o.SymbolEntry == null).Select(o => o.Symbol), StringComparer.InvariantCultureIgnoreCase).ToArray();

            var records = new List<TradeRecord>(account.Trades.Count);
            var positions = new List<Position>(account.Trades.Count);

            ResetPositionsProfit(account.Trades);

            foreach (var element in account.Trades)
            {
                if (!TryProcessAsTradeRecord(element, records))
                    TryProcessAsPosition(element, positions);
            }

            this.Quotes = new Dictionary<string, Quote>(quotes);
            this.TradeRecords = records.ToArray();
            this.Positions = positions.ToArray();
            this.Assets = new Dictionary<string, Asset>(assets);
        }

        static void ResetPositionsProfit(IEnumerable<TradeEntry> entries)
        {
            foreach (var position in entries.Select(o => o.Tag).OfType<Position>())
                position.Profit = null;
        }

        static bool TryProcessAsTradeRecord(TradeEntry entry, ICollection<TradeRecord> records)
        {
            var record = entry.Tag as TradeRecord;
            if (record == null)
                return false;

            record.Profit = entry.Profit;

            records.Add(record);

            return true;
        }

        static bool TryProcessAsPosition(TradeEntry entry, ICollection<Position> positions)
        {
            var position = entry.Tag as Position;
            if (position == null)
                return false;

            if (entry.Profit.HasValue)
                position.Profit = position.Profit.GetValueOrDefault() + entry.Profit;

            // some magic; see Calculate method of state calcualtor
            if (entry.Side == TradeRecordSide.Buy)
                positions.Add(position);

            return true;
        }

        #endregion

        /// <summary>
        /// Gets status of Profit property.
        /// </summary>
        public AccountEntryStatus Status { get; private set; }

        /// <summary>
        /// Gets the number, which indicates how many times financial information has been updated.
        /// </summary>
        public long Generation { get; private set; }

        /// <summary>
        /// Gets balance of account, which has been specified for data trade object.
        /// </summary>
        public double Balance { get; private set; }

        /// <summary>
        /// Gets equity of account.
        /// </summary>
        public double Equity { get; private set; }

        /// <summary>
        /// Gets profit of all opened positions for data trade account by data feed quotes.
        /// </summary>
        public double Profit { get; private set; }

        /// <summary>
        /// Gets margin of data trade account by data feed quotes.
        /// </summary>
        public double Margin { get; private set; }

        /// <summary>
        /// Gets total commission.
        /// </summary>
        public double Commission { get; private set; }

        /// <summary>
        /// Gets total agent commission.
        /// </summary>
        public double AgentCommission { get; private set; }

        /// <summary>
        /// Gets total swap.
        /// </summary>
        public double Swap { get; private set; }

        /// <summary>
        /// Gets free margin.
        /// </summary>
        public double FreeMargin
        {
            get
            {
                return this.Equity - this.Margin;
            }
        }

        /// <summary>
        /// Gets margin level.
        /// </summary>
        public double MarginLevel { get; private set; }

        /// <summary>
        /// Quotes snapshot, which has been used for calculation the financial information.
        /// </summary>
        public IDictionary<string, PriceEntry> Prices { get; private set; }

        /// <summary>
        /// Quotes snapshot, which has been used for calculation the financial information.
        /// </summary>
        public IDictionary<string, Quote> Quotes { get; private set; }

        /// <summary>
        /// Gets list of available assets.
        /// </summary>
        public IDictionary<string, Asset> Assets { get; private set; }

        /// <summary>
        /// Gets list of available trade records.
        /// </summary>
        public TradeRecord[] TradeRecords { get; private set; }

        /// <summary>
        /// Gets list of opened positions. Available for .NET account only.
        /// </summary>
        public Position[] Positions { get; private set; }

        /// <summary>
        /// Gets list of symbols, which are not supported by server.
        /// Example: user has opened position by BTC/USD, but the corresponding symbol information is not available.
        /// </summary>
        public string[] UnknownSymbols { get; private set; }

        #region Overrides

        /// <summary>
        /// Returns formatted string for the class instance.
        /// </summary>
        /// <returns>Can not be null.</returns>
        public override string ToString()
        {
            return string.Format("#{0}: Balance = {1}; Profit = {2}; Margin = {3}", this.Generation, this.Balance, this.Profit, this.Margin);
        }

        #endregion
    }
}
