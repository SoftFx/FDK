namespace SoftFX.AutomaticTrading.Hosting.Indicators
{
    using System;
    using System.Collections.Generic;
    using SoftFX.AutomaticTrading.Core.Indicators;

    public interface IIndicatorBinding : IDefineParameters
    {
        string Name { get; }

        string Descrption { get; }

        string Company { get; }

        string Category { get; }

        IIndicator CreateIndicator(IHostContext context, IDictionary<string, object> parameters);

        bool IsTypeSupported(Type type);
    }
}
