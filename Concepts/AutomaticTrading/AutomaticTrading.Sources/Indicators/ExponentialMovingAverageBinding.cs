namespace AutomaticTrading.Sources.Indicators
{
    using System;
    using System.Collections.Generic;
    using SoftFX.AutomaticTrading.Core.Indicators;
    using SoftFX.AutomaticTrading.Hosting;
    using SoftFX.AutomaticTrading.Hosting.Indicators;
    using SoftFX.AutomaticTrading.Indicators;

    public class ExponentialMovingAverageBinding : IndicatorBinding
    {
        static readonly IEnumerable<IndicatorParameter> Params = new[]
            {
                IndicatorParameter.Create("Period", IndicatorParameterType.Integer),
                IndicatorParameter.Create("Factor", IndicatorParameterType.Float)
            };

        public ExponentialMovingAverageBinding()
            : base("Exponential Moving Average", "Exponential Moving Average", "SoftFX", "Trend")
        {

        }

        public override IIndicator CreateIndicator(IHostContext context, IDictionary<string, object> parameters)
        {
            var period = parameters.Get("Period", 10);
            var factor = parameters.Get("Factor", 2D);
            return new ExponentialMovingAverageIndicator(period, factor);
        }

        public override IEnumerable<IndicatorParameter> Parameters
        {
            get { return Params; }
        }

        protected override Type ValueType
        {
            get { return typeof(double); }
        }
    }
}
