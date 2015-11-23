namespace AutomaticTrading.Sources.Indicators
{
    using System;
    using System.Collections.Generic;
    using SoftFX.AutomaticTrading.Core.Indicators;
    using SoftFX.AutomaticTrading.Hosting;
    using SoftFX.AutomaticTrading.Hosting.Indicators;
    using SoftFX.AutomaticTrading.Indicators;

    public class SimpleMovingAverageBinding : IndicatorBinding
    {
        static readonly IEnumerable<IndicatorParameter> Params = new[]
            {
                IndicatorParameter.Create("Period", IndicatorParameterType.Integer)
            };

        public SimpleMovingAverageBinding()
            : base("Simple Moving Average", "Simple Moving Average", "SoftFX", "Trend")
        {

        }

        public override IIndicator CreateIndicator(IHostContext context, IDictionary<string, object> parameters)
        {
            var period = parameters.Get("Period", 10);
            return new SimpleMovingAverageIndicator(period);
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
