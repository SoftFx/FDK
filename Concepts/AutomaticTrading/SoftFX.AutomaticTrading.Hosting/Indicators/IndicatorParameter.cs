namespace SoftFX.AutomaticTrading.Hosting.Indicators
{
    public sealed class IndicatorParameter
    {
        public IndicatorParameter(string name, IndicatorParameterType type)
        {
            this.Name = name ?? string.Empty;
            this.Type = type;
        }

        public static IndicatorParameter Create(string name, IndicatorParameterType type)
        {
            return new IndicatorParameter(name, type);
        }

        public string Name { get; private set; }

        public IndicatorParameterType Type { get; private set; }
    }
}
