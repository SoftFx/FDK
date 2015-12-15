namespace SoftFX.AutomaticTrading.Hosting.Infrastructure
{
    using System.Collections.Generic;
    using System.Linq;
    using SoftFX.AutomaticTrading.Core.Indicators;
    using SoftFX.AutomaticTrading.Hosting.Indicators;

    public class IndicatorManager
    {
        public IndicatorManager(IIndicatorBindingDiscovery discovery)
        {
            this.Indicators = discovery.Indicators;
        }

        public IEnumerable<IIndicatorBinding> Indicators { get; private set; }

        public IIndicator CreateIndicator(string name)
        {
            return this.CreateIndicator(name, new Dictionary<string, object>());
        }

        public IIndicator CreateIndicator(string name, IDictionary<string, object> parameters)
        {
            var binding = this.Indicators.FirstOrDefault(o => o.Name == name);
            if (binding == null)
                return null;

            return binding.CreateIndicator(new HostContext(), parameters);
        }
    }
}
