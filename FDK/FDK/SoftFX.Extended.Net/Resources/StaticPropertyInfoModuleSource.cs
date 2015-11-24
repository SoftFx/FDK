namespace SoftFX.Extended.Resources
{
    using System;
    using System.Reflection;

    sealed class StaticPropertyInfoModuleSource : IModuleSource
    {
        readonly PropertyInfo property;

        public StaticPropertyInfoModuleSource(PropertyInfo property)
        {
            if (property == null)
                throw new ArgumentNullException("property");

            if (property.PropertyType != typeof(byte[]))
                throw new ArgumentException("Property type should be byte[].");

            if (!property.GetGetMethod(true).IsStatic)
                throw new ArgumentException("Property should be static.");

            this.property = property;
        }

        public string Name
        {
            get { return this.property.Name; }
        }

        public byte[] Data
        {
            get { return (byte[])this.property.GetValue(null, null); }
        }
    }
}
