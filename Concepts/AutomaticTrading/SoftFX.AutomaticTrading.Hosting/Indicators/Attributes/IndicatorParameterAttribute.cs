namespace SoftFX.AutomaticTrading.Hosting.Indicators
{
    using System;

    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class IndicatorParameterAttribute : Attribute
    {
        readonly IndicatorParameter parameter;

        public IndicatorParameterAttribute(string name, IndicatorParameterType type)
        {
            this.parameter = IndicatorParameter.Create(name, type);
        }

        public string Name
        {
            get { return this.parameter.Name; }
        }

        public IndicatorParameterType Type
        {
            get { return this.parameter.Type; }
        }
    }
}
