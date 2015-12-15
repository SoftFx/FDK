namespace Lrp.Engine
{
    using System.Xml;

    class Return
    {
        public DataType Type { get; private set; }

        [XmlName("Type"), XmlSerializable()]
        private string TypeName { get; set; }

        public Return(DataTypes types, XmlNode node)
        {
            XmlSerializer.Initialize(this, node);
            this.Type = types[this.TypeName];
        }
    }
}
