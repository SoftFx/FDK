namespace Mql2Fdk.Launcher.Data
{
    using System;

    sealed class PriceType
    {
        public PriceType(StrategyLauncher.PriceType priceType, string description)
        {
            if (description == null)
                throw new ArgumentNullException("description");

            this.Type = priceType;
            this.Description = description ?? string.Empty;
        }

        public StrategyLauncher.PriceType Type { get; private set; }

        public string Description { get; private set; }
    }
}
