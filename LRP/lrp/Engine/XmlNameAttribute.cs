namespace Lrp.Engine
{
    using System;

    class XmlNameAttribute : Attribute
    {
        public XmlNameAttribute(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name", "Xml name can not be null");

            this.Name = name;
        }

        public string Name { get; private set; }
    }
}
