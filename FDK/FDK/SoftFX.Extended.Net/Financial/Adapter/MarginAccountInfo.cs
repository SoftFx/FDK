namespace SoftFX.Extended.Financial.Adapter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TickTrader.BusinessLogic;
    using TickTrader.BusinessObjects;

    sealed class MarginAccountInfo : IMarginAccountInfo
    {
        readonly AccountEntry entry;

        public MarginAccountInfo(AccountEntry entry)
        {
            this.entry = entry;
        }

        public decimal Balance
        {
            get { return (decimal)this.entry.Balance; }
        }

        public string BalanceCurrency
        {
            get { return this.entry.Currency; }
        }

        public int Leverage
        {
            get { return (int)this.entry.Leverage; }
        }

        public AccountingTypes AccountingType
        {
            get { return CalculatorConvert.ToAccountingTypes(this.entry.Type); }
        }

        public long Id
        {
            get { return this.entry.GetHashCode(); }
        }

        public IEnumerable<ICalculatorOrder> Orders
        {
            get { return this.entry.Trades.Select(CalculatorConvert.ToCalculatorOrder); }
        }

        #region Events

        public event Action<ICalculatorOrder> OrderAdded
        {
            add { }
            remove { }
        }

        public event Action<ICalculatorOrder> OrderRemoved
        {
            add { }
            remove { }
        }

        public event Action<IEnumerable<ICalculatorOrder>> OrdersAdded
        {
            add { }
            remove { }
        }

        #endregion

        public event Action<IPositionModel, PositionChageTypes> PositionChanged
        {
            add { }
            remove { }
        }

        public IEnumerable<IPositionModel> Positions
        {
            get { return Enumerable.Empty<IPositionModel>(); }
        }
    }
}
