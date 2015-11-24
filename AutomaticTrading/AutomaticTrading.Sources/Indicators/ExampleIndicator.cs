namespace AutomaticTrading.Sources.Indicators
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using SoftFX.AutomaticTrading.Core.Indicators;
    using SoftFX.AutomaticTrading.Hosting.Indicators;
    using SoftFX.AutomaticTrading.Hosting;

    [SelfBindableAttribute]
    [DisplayName("Factor")]
    [Description("Self binding test indicator")]
    [Company("SoftFX")]
    [Category("Test Indicators")]
    [IndicatorParameter("Factor", IndicatorParameterType.Float)]
    public class ExampleIndicator : IndicatorBase<double>
    {
        public double Factor { get; private set; }

        public ExampleIndicator()
        {
            this.Factor = 1D;
        }

        public ExampleIndicator(IHostContext context, IDictionary<string, object> parameters)
        {
            this.Factor = parameters.Get("Factor", 1D);
        }
        
        protected override double OnCalculate(double value, bool current)
        {
            return value * this.Factor;
        }
    }
}
