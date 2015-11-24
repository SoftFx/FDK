namespace SoftFX.Extended.Financial.Adapter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TickTrader.BusinessLogic;
    using TickTrader.BusinessObjects;

    sealed class CashAccountInfo : ICashAccountInfo
    {
        readonly AccountEntry entry;
        readonly IEnumerable<Asset> assets;

        public CashAccountInfo(AccountEntry entry, IEnumerable<Asset> assets)
        {
            this.entry = entry;
            this.assets = assets;
        }

        public IEnumerable<IAssetModel> Assets
        {
            get { return this.assets.Select(CalculatorConvert.ToAssetModel); }
        }

        public AccountingTypes AccountingType
        {
            get { return AccountingTypes.Cash; }
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

        public event Action<IAssetModel, AssetChangeTypes> AssetsChanged
        {
            add { }
            remove { }
        }

        public event Action<ICalculatorOrder> OrderAdded
        {
            add { }
            remove { }
        }

        public event Action<IEnumerable<ICalculatorOrder>> OrdersAdded
        {
            add { }
            remove { }
        }

        public event Action<ICalculatorOrder> OrderRemoved
        {
            add { }
            remove { }
        }

        #endregion
    }
}
