namespace SoftFX.Extended.Financial
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using SoftFX.Extended.Extensions;
    using SoftFX.Extended.Financial.Adapter;
    using TickTrader.BusinessLogic;

    /// <summary>
    /// Represents account entry.
    /// </summary>
    public class AccountEntry : FinancialEntry<FinancialCalculator>
    {
        /// <summary>
        /// Creates a new account entry
        /// </summary>
        /// <param name="owner">instance of financial calculator</param>
        public AccountEntry(FinancialCalculator owner)
            : base(owner)
        {
            this.leverage = 1D;
            this.Currency = string.Empty;
            this.Trades = new FinancialEntries<TradeEntry>(this);
            this.Assets = new Dictionary<string, Asset>();
        }

        #region Methods

        /// <summary>
        /// Resets all calculated properties to null
        /// </summary>
        public void Clear()
        {
            this.Profit = null;
            this.Margin = null;

            this.Assets.Clear();

            this.ProfitStatus = AccountEntryStatus.NotCalculated;
            this.MarginStatus = AccountEntryStatus.NotCalculated;
            this.AssetsStatus = AccountEntryStatus.NotCalculated;

            foreach (var element in this.Trades)
            {
                element.Clear();
            }
        }
    
        #endregion

        #region Internal Methods

        internal void Calculate()
        {
            foreach (var element in this.Trades)
            {
                if (this.Owner.Symbols.HasBeenChanged || element.SymbolEntry == null)
                    element.SymbolEntry = this.Owner.Symbols.TryGetSymbolEntry(element.Symbol);

                element.PrepareForCalculation();
            }

            var zzz = this.Owner.Symbols.TryGetCurrencyIndex(this.Currency);

            if (zzz >= 0)
            {
                var account = CalculatorConvert.ToMarginAccountInfo(this);
                using (var calculator = new AccountCalculator(account, this.Owner.MarketState))
                {
                    this.Profit = (double)calculator.Profit;
                    this.Margin = (double)calculator.Margin;

                    this.ProfitStatus = !this.Trades.Any(o => o.ProfitStatus != TradeEntryStatus.Calculated) ? AccountEntryStatus.Calculated : AccountEntryStatus.CalculatedWithErrors;
                    this.MarginStatus = !this.Trades.Any(o => !o.Margin.HasValue) ? AccountEntryStatus.Calculated : AccountEntryStatus.CalculatedWithErrors;
                }

                this.CalculateAssets(zzz);
            }
            else
            {
                this.ProfitStatus = AccountEntryStatus.UnknownAccountCurrency;
                this.MarginStatus = AccountEntryStatus.UnknownAccountCurrency;
                this.AssetsStatus = AccountEntryStatus.UnknownAccountCurrency;
            }
        }

        void CalculateAssets(int zzz)
        {
            var converter = this.Owner.MarketState.ConversionMap;

            var count = this.Owner.Symbols.CurrenciesCount;
            var assets = Enumerable.Range(0, count).Select(o => new Asset(this)).ToArray();
                
            var status = AccountEntryStatus.Calculated;

            foreach (var element in this.Trades)
            {
                if (element.Type != TradeRecordType.Position)
                    continue;
                
                var symbol = element.SymbolEntry;
                if (symbol == null)
                {
                    status = AccountEntryStatus.CalculatedWithErrors;
                    continue;
                }

                if (element.Side == TradeRecordSide.Buy)
                {
                    assets[symbol.ToIndex].Volume += element.NativeVolume;
                    assets[symbol.FromIndex].Volume -= element.NativeVolume * element.Price;
                }
                else if (element.Side == TradeRecordSide.Sell)
                {
                    assets[symbol.ToIndex].Volume -= element.NativeVolume;
                    assets[symbol.FromIndex].Volume += element.NativeVolume * element.Price;
                }
            }

            var freeMagin = this.FreeMargin.GetValueOrDefault();
            assets[zzz].Volume += freeMagin * this.Leverage;

            var accountCurrency = this.Owner.Symbols.GetCurrencyFromIndex(zzz);

            for (var index = 0; index < assets.Length; ++index)
            {
                var asset = assets[index];
                if (asset.Volume == 0)
                    continue;

                var currency = this.Owner.Symbols.GetCurrencyFromIndex(index);

                var rate = this.Owner.CalculateAssetRate(asset.Volume, currency, accountCurrency);
                if (rate.HasValue)
                {
                    asset.Rate = rate.Value;
                    asset.DepositCurrency = this.RoundingService.RoundProfit(asset.Volume * asset.Rate / this.Leverage);
                }

                asset.Currency = currency;
                this.Assets[currency] = asset;
            }

            if (this.ProfitStatus == AccountEntryStatus.CalculatedWithErrors || this.MarginStatus == AccountEntryStatus.CalculatedWithErrors)
                status = AccountEntryStatus.CalculatedWithErrors;

            this.AssetsStatus = status;
        }

        #endregion

        #region Read / Write Properties

        /// <summary>
        /// Gets or sets account type.
        /// </summary>
        [Category("Parameters")]
        public AccountType Type
        {
            get
            {
                return this.type;
            }
            set
            {
                if (value != AccountType.Net && value != AccountType.Gross && value != AccountType.Cash)
                {
                    var message = string.Format("Unsupported account type = {0}", value);
                    throw new ArgumentException(message, "value");
                }

                this.type = value;
            }
        }

        /// <summary>
        /// Gets or sets the account instance leverage.
        /// </summary>
        [Category("Parameters")]
        public double Leverage
        {
            get
            {
                return this.leverage;
            }
            set
            {
                if (double.IsNaN(value) || double.IsInfinity(value) || value <= 0)
                {
                    var message = string.Format("Leverage should be positive finite value");
                    throw new ArgumentOutOfRangeException("value", value, message);
                }
                this.leverage = value;
            }
        }

        /// <summary>
        /// Gets or sets the account instance balance.
        /// </summary>
        [Category("Parameters")]
        public double Balance { get; set; }

        /// <summary>
        /// Gets or sets the account instance currency.
        /// </summary>
        [Category("Parameters")]
        public string Currency { get; set; }

        #endregion

        #region Calculated Properties

        /// <summary>
        /// Gets calculated profit if it is available, otherwise returns null.
        /// </summary>
        [Category("Calculated")]
        public double? Profit { get; internal set; }

        /// <summary>
        /// Gets status of Profit property.
        /// </summary>
        [Category("Calculated")]
        [DisplayName("Profit Status")]
        public AccountEntryStatus ProfitStatus { get; internal set; }

        /// <summary>
        /// Gets calculated margin if it is available, otherwise returns null.
        /// </summary>
        [Category("Calculated")]
        public double? Margin { get; internal set; }

        /// <summary>
        /// Gets status of Margin property.
        /// </summary>
        [Category("Calculated")]
        [DisplayName("Margin Status")]
        public AccountEntryStatus MarginStatus { get; internal set; }

        /// <summary>
        /// Gets calculated equity if it is available, otherwise returns null.
        /// </summary>
        [Category("Calculated")]
        public double? Equity
        {
            get
            {
                if (!this.Profit.HasValue)
                    return null;

                return this.Balance + this.Profit.Value + this.Commission + this.AgentCommission + this.Swap;
            }
        }

        /// <summary>
        /// Gets status of Equity property.
        /// </summary>
        [Category("Calculated")]
        [DisplayName("Equity Status")]
        public AccountEntryStatus EquityStatus
        {
            get
            {
                return this.ProfitStatus;
            }
        }

        /// <summary>
        /// Gets calculated margin level if it is available, otherwise returns null.
        /// </summary>
        [Category("Calculated")]
        [DisplayName("Margin Level")]
        public double? MarginLevel
        {
            get
            {
                var equity = this.Equity;
                if (!equity.HasValue)
                    return null;

                var margin = this.Margin;
                if (!margin.HasValue)
                    return null;

                return equity.Value / margin.Value;
            }
        }

        /// <summary>
        /// Gets status of Margin Level property.
        /// </summary>
        [Category("Calculated")]
        [DisplayName("Margin Level Status")]
        public AccountEntryStatus MarginLevelStatus
        {
            get
            {
                if (this.ProfitStatus == AccountEntryStatus.NotCalculated || this.MarginStatus == AccountEntryStatus.NotCalculated)
                    return AccountEntryStatus.NotCalculated;
                if (this.ProfitStatus == AccountEntryStatus.UnknownAccountCurrency || this.MarginStatus == AccountEntryStatus.UnknownAccountCurrency)
                    return AccountEntryStatus.UnknownAccountCurrency;
                if (this.ProfitStatus == AccountEntryStatus.CalculatedWithErrors || this.MarginStatus == AccountEntryStatus.CalculatedWithErrors)
                    return AccountEntryStatus.CalculatedWithErrors;

                return AccountEntryStatus.Calculated;
            }
        }

        /// <summary>
        /// Gets calculated margin level if it is available, otherwise returns null.
        /// </summary>
        [Category("Calculated")]
        [DisplayName("Free Margin")]
        public double? FreeMargin
        {
            get
            {
                var equity = this.Equity;
                if (!equity.HasValue)
                    return null;

                var margin = this.Margin;
                if (!margin.HasValue)
                    return null;

                return equity.Value - margin.Value;
            }
        }

        /// <summary>
        /// Gets status of Margin Level property.
        /// </summary>
        [Category("Calculated")]
        [DisplayName("Free Margin Status")]
        public AccountEntryStatus FreeMarginStatus
        {
            get
            {
                return this.MarginLevelStatus;
            }
        }

        /// <summary>
        /// Gets total commission.
        /// </summary>
        [Category("Calculated")]        
        public double Commission
        {
            get
            {
                return this.Trades.Sum(o => o.Commission);
            }
        }

        /// <summary>
        /// Gets total agent commission.
        /// </summary>
        [Category("Calculated")]
        [DisplayName("Agent Commission")]
        public double AgentCommission
        {
            get
            {
                return this.Trades.Sum(o => o.AgentCommission);
            }
        }

        /// <summary>
        /// Gets total swap.
        /// </summary>
        [Category("Calculated")]
        public double Swap
        {
            get
            {
                return this.Trades.Sum(o => o.Swap);
            }
        }


        /// <summary>
        /// Gets list of available assets.
        /// </summary>
        [Category("Calculated")]
        public IDictionary<string, Asset> Assets { get; private set; }

        /// <summary>
        /// Gets status of Assets property.
        /// </summary>
        [Category("Calculated")]
        [DisplayName("Assets Status")]
        public AccountEntryStatus AssetsStatus { get; private set; }

        /// <summary>
        /// Provides access to trade entries, which belong to the account.
        /// </summary>
        [Browsable(false)]
        public FinancialEntries<TradeEntry> Trades { get; private set; }

        internal IAccountRoundingService RoundingService
        {
            get
            {
                return this.roundingService ?? (this.roundingService = this.CreateDefaultRoundingService()); 
            }
            set
            {
                this.roundingService = value;
            }
        }

        IAccountRoundingService CreateDefaultRoundingService()
        {
            return new AccountRoundingService(FinancialRounding.Instance, DefaultPrecision.Instance, this.Currency);
        }

        #endregion

        #region Fields

        AccountType type;
        double leverage;

        IAccountRoundingService roundingService;

        #endregion
    }
}
