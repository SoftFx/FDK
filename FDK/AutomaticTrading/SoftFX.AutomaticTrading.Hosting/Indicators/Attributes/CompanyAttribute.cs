namespace SoftFX.AutomaticTrading.Hosting.Indicators
{
    using System;

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class CompanyAttribute : Attribute
    {
        public CompanyAttribute()
            : this(string.Empty)
        {
        }

        public CompanyAttribute(string company)
        {
            this.Company = company ?? string.Empty;
        }

        public string Company { get; private set; }
    }
}
