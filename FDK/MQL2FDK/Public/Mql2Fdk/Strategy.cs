namespace Mql2Fdk
{
    using System.ComponentModel.Composition;
    using SoftFX.Basic;
    using SoftFX.Extended;

    [InheritedExport]
    public abstract class Strategy
    {
        internal abstract void Start();

        internal abstract void Stop();

        internal abstract void Initialize(Manager manager, IStrategyLog log, string symbol, PriceType priceType, BarPeriod periodicity);
    }
}
