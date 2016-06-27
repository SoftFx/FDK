namespace SoftFX.Extended.Financial.Adapter
{
    using System;
    using TickTrader.BusinessLogic;

    sealed class CalculatorAsset : IAssetModel
    {
        readonly Asset asset;

        public CalculatorAsset(Asset asset)
        {
            this.asset = asset;
        }

        public decimal Amount
        {
            get { return (decimal)this.asset.Volume; }
        }

        public decimal LockedAmount
        {
            get { return (decimal)this.asset.LockedVolume; }
        }

        public decimal FreeAmount
        {
            get { return Amount - LockedAmount; }
        }

        public short CurrencyId
        {
            get { return (short)this.asset.Currency.GetHashCode(); }
        }

        public string Currency
        {
            get { return this.asset.Currency; }
        }

        public decimal Margin
        {
            get
            {
                return (decimal)this.asset.LockedVolume;
            }
            set
            {
                this.asset.LockedVolume = (double)this.asset.Owner.RoundingService.RoundMargin(this.asset.Currency, value);
            }
        }
    }
}
