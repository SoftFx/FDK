namespace SoftFX.AutomaticTrading.Hosting.Infrastructure
{
    using System.Collections.Generic;
    using SoftFX.AutomaticTrading.Hosting.Indicators;

    public interface IIndicatorBindingDiscovery
    {
        IEnumerable<IIndicatorBinding> Indicators { get; }
    }
}
