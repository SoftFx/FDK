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

        public void LogInfo(string message)
        {
        }

        public void LogWarn(string message)
        {
        }

        public void LogError(string message)
        {
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

        public IEnumerable<IOrderModel> Orders
        {
            get { return this.entry.Trades.Select(CalculatorConvert.ToCalculatorOrder); }
        }

        #region Events

        public event Action<IAssetModel, AssetChangeTypes> AssetsChanged
        {
            add { }
            remove { }
        }

        public event Action<IOrderModel> OrderAdded
        {
            add { }
            remove { }
        }

        public event Action<IOrderModel> OrderRemoved
        {
            add { }
            remove { }
        }

        public event Action<IOrderModel> OrderReplaced
        {
            add { }
            remove { }
        }

        public event Action<IEnumerable<IOrderModel>> OrdersAdded
        {
            add { }
            remove { }
        }

        #endregion
    }
}
