namespace SoftFX.AutomaticTrading.Hosting.Indicators
{
    using System.Collections.Generic;

    public interface IDefineParameters
    {
        IEnumerable<IndicatorParameter> Parameters { get; }
    }
}
