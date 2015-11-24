namespace Lrp.Engine
{
    using System;

	class XmlSerializableAttribute : Attribute
	{
		public XmlSerializableAttribute()
            : this(true)
		{
		}

		public XmlSerializableAttribute(bool required)
		{
			this.Required = required;
		}

		public bool Required { get; private set; }
	}
}
